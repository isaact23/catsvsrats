using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rat
{
    public class RatObject : MonoBehaviour
    {
        public RatManager ratManager;
        public RatTypeScriptableObject ratType;
        public PathScriptableObject path;
        public PathScriptableObject mutantPath;

        // HP bar
        public GameObject hpRed;
        public GameObject hpGreen;

        private SpriteRenderer spriteRenderer;
        private Vector3 cheesePosition;
        private float hp;
        private float timeElapsed;
        private float pathProgress;
        private int currentSprite;
        private bool eatingCheese = false;

        public void TakeDamage(DamageType damageType, float damage)
        {
            float netDamage = damage;
            if (damageType == DamageType.Standard) {
                netDamage -= ratType.standardDefense;
            } else if (damageType == DamageType.Bomb) {
                netDamage -= ratType.bombDefense;
            } else if (damageType == DamageType.Magic) {
                netDamage -= ratType.magicDefense;
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

        // Start is called before the first frame update
        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = ratType.color;
            hp = ratType.startHp;
            timeElapsed = 0f;
            pathProgress = 0f;
            currentSprite = -1;
            UpdateHpBar();
            
            // Flip
            if (ratType.flip) {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        // Update is called once per frame
        private void Update()
        {
            timeElapsed += Time.deltaTime;
            pathProgress = timeElapsed * ratType.speed;

            UpdateSprite();

            if (!eatingCheese) {
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
                Vector3 hpPos = hpGreen.transform.position;
                Vector3 hpScale = hpGreen.transform.localScale;
                hpPos.x = -hpRatio / 2;
                hpScale.x = hpRatio;
                hpGreen.transform.position = hpPos;
                hpGreen.transform.localScale = hpScale;
            }
        }

        private void ReachEnd()
        {
            if (path.pathType == PathType.Cheese) {
                eatingCheese = true;
                cheesePosition = ratManager.GetCheesePosition();
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
            // Reset to beginning of the mutant track
            path = mutantPath;
            timeElapsed = 0f;
            pathProgress = 0f;

            // Choose a new rat type for the next generation
            int ratTypeCount = ratType.nextGeneration.Length;
            if (ratTypeCount > 0) {
                ratType = ratType.nextGeneration[Random.Range(0, ratTypeCount)];
            }
            
            hp = ratType.startHp;
            spriteRenderer.color = ratType.color;
            
            // Force update sprite
            currentSprite = -1;
            UpdateSprite();
        }

        private void Die()
        {
            ratManager.RemoveRat(this);
            Destroy(gameObject);
        }
    }
}
