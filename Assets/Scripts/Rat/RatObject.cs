using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rat
{
    public class RatObject : MonoBehaviour
    {
        public RatManager ratManager;
        public HealthManager healthManager;
        public PlacingManager placingManager;
        public RatTypeScriptableObject ratType;
        public PathScriptableObject path;
        public PathScriptableObject mutantPath;
        public AudioClip mutateSound;

        // HP bar
        public GameObject hpRed;
        public GameObject hpGreen;

        private AudioSource audioSource;
        private SpriteRenderer spriteRenderer;
        private Vector3 cheesePosition;
        private float hp;
        private float timeElapsed;
        private float pathProgress;
        private int currentSprite;
        private bool eatingCheese = false;
        private float secsSinceLastBite = 0f;
        private float secsSinceLastSound = 0f;
        private float standardDamage = 0f;
        private float bombDamage = 0f;
        private float magicDamage = 0f;

        public void TakeDamage(DamageType damageType, float damage)
        {
            float netDamage = damage;
            if (damageType == DamageType.Standard) {
                netDamage -= ratType.standardDefense;
                standardDamage += netDamage;
            } else if (damageType == DamageType.Bomb) {
                audioSource.clip = ratType.hitByBombSound;
                audioSource.Play();
                netDamage -= ratType.bombDefense;
                bombDamage += netDamage;
            } else if (damageType == DamageType.Magic) {
                netDamage -= ratType.magicDefense;
                magicDamage += netDamage;
            }

            if (netDamage > 0) {
                hp -= damage;
            }
            if (hp <= 0f) {
                Die();
            }

            UpdateHpBar();
        }

        public float DistFromExit()
        {
            return pathProgress - path.coordinates.Length;
        }

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            spriteRenderer.color = ratType.color;
            hp = ratType.startHp;
            timeElapsed = 0f;
            pathProgress = 0f;
            currentSprite = -1;
            UpdateHpBar();
            Orient();
            UpdatePosition();
        }

        // Update is called once per frame
        private void Update()
        {
            timeElapsed += Time.deltaTime;
            pathProgress = timeElapsed * ratType.speed;

            UpdateSprite();

            // Determine if it's time to make sound
            secsSinceLastSound += Time.deltaTime;
            bool doSound = false;
            if (secsSinceLastSound > 1 / ratType.soundsPerSecond) {
                secsSinceLastSound = 0f;
                doSound = true;
            }
            
            if (eatingCheese) {
                if (doSound && ratType.eatSound != null) {
                    audioSource.clip = ratType.eatSound;
                    audioSource.Play();
                }
                secsSinceLastBite += Time.deltaTime;
                if (secsSinceLastBite > 1 / ratType.bitesPerSecond) {
                    secsSinceLastBite = 0f;
                    healthManager.SubtractHealth(ratType.biteDamage);
                }
            } else {
                UpdatePosition();
                if (doSound && ratType.walkSound != null) {
                    audioSource.clip = ratType.walkSound;
                    audioSource.Play();
                }
            }
        }

        // Flip sprite in direction according to RatTypeScriptableObject
        private void Orient()
        {
            if (ratType.flip) {
                spriteRenderer.flipX = true;
            }
            else {
                spriteRenderer.flipX = false;
            }
        }

        private void UpdatePosition() {
            // Find bounding coordinates
            float lowerCoordFloat = Mathf.Floor(pathProgress);
            int lowerCoordId = (int) lowerCoordFloat;
            float upperCoordFloat = Mathf.Ceil(pathProgress);
            int upperCoordId = (int) upperCoordFloat;

                if (upperCoordId >= path.coordinates.Length) {
                ReachEnd();
                return;
            }

            Vector2 lowerCoord = path.coordinates[lowerCoordId];
            Vector2 upperCoord = path.coordinates[upperCoordId];

            // Find actual coordinate to place Rat
            float percentBetween = pathProgress - lowerCoordId;
            Vector3 ratPos = Vector2.Lerp(lowerCoord, upperCoord, percentBetween);
            ratPos.z = -1;
            transform.position = ratPos;
        }
        
        private void UpdateSprite()
        {
            if (eatingCheese) {
                if (ratType.eatingSprites.Length != 0) {
                    // Determine sprite to show
                    int oldSprite = currentSprite;
                    currentSprite = ((int)(timeElapsed * ratType.spritesPerSecond)) % ratType.eatingSprites.Length;
                    if (oldSprite != currentSprite) {
                        spriteRenderer.sprite = ratType.eatingSprites[currentSprite];
                    }
                }
            }
            else {
                if (ratType.sprites.Length != 0) {
                    // Determine sprite to show
                    int oldSprite = currentSprite;
                    currentSprite = ((int)(timeElapsed * ratType.spritesPerSecond)) % ratType.sprites.Length;
                    if (oldSprite != currentSprite) {
                        spriteRenderer.sprite = ratType.sprites[currentSprite];
                    }
                }
            }
        }

        private void UpdateHpBar()
        {
            float hpRatio = hp / ratType.startHp;
            if (hpRatio > 0.99f) {
                hpRed.SetActive(false);
                hpGreen.SetActive(false);
            }
            else {
                hpRed.SetActive(true);
                hpGreen.SetActive(true);

                // Update HP bar scaling
                Vector3 hpPos = Vector3.zero;
                Vector3 hpScale = hpGreen.transform.localScale;
                hpPos.x = (hpRatio - 1) / 2;
                hpScale.x = hpRatio;
                hpGreen.transform.localPosition = hpPos;
                hpGreen.transform.localScale = hpScale;
            }
        }

        private void ReachEnd()
        {
            if (path.pathType == PathType.Cheese) {
                eatingCheese = true;
                cheesePosition = ratManager.GetCheesePosition(ratType.cheesePosition);
                pathProgress = 0f;
                transform.position = cheesePosition;
            } else if (path.pathType == PathType.Mutate) {
                if (mutantPath == null) {
                    Debug.LogError("Rat is on mutant path but has no mutant path assigned.");
                    Die();
                }
                else {
                    Mutate();
                }
            } else if (path.pathType == PathType.Exit) {
                Die();
            }
        }

        private void Mutate()
        {
            audioSource.clip = mutateSound;
            audioSource.Play();
            
            // Reset to beginning of the mutant track
            path = mutantPath;
            timeElapsed = 0f;
            pathProgress = 0f;

            // Choose a new rat type for the next generation
            ratType = Instantiate(ratType);
            Debug.Log(standardDamage + " " + bombDamage + " " + magicDamage);
            if (standardDamage > bombDamage && standardDamage > magicDamage) {
                ratType.standardDefense += 1;
                ratType.color = new Color(0.8f, 0.5f, 0.1f);
            } else if (bombDamage > standardDamage && bombDamage > magicDamage) {
                ratType.bombDefense += 1;
                ratType.color = Color.gray;
            } else if (magicDamage > standardDamage && magicDamage > bombDamage) {
                ratType.magicDefense += 1;
                ratType.color = Color.magenta;
            } else if (hp <= 1f) {
                ratType.startHp *= 2;
                ratType.color = Color.red;
            } else if (standardDamage >= 1f && bombDamage >= 1f && magicDamage >= 1f) {
                ratType.bitesPerSecond *= 3;
                ratType.color = Color.yellow;
            } else {
                ratType.speed *= 2;
                ratType.color = Color.green;
            }
            
            hp = ratType.startHp;
            spriteRenderer.color = ratType.color;

            // Force update sprite
            currentSprite = -1;
            UpdateSprite();
            Orient();
            secsSinceLastSound = 100f;
        }

        private void Die()
        {
            ratManager.RemoveRat(this);
            placingManager.money += ratType.cashDrop;
            Destroy(gameObject);
        }
    }
}
