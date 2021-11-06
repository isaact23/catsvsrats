using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rat
{
    public class RatObject : MonoBehaviour
    {
        public RatTypeScriptableObject ratType;
        public PathScriptableObject path;

        // HP bar
        public GameObject hpRed;
        public GameObject hpGreen;

        private float hp;
        private float timeElapsed;
        private float pathProgress;

        public void TakeDamage(float damage)
        {
            hp -= damage;
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
            GetComponent<SpriteRenderer>().sprite = ratType.sprite;
            hp = ratType.startHp;
            timeElapsed = 0f;
            pathProgress = 0f;
            UpdateHpBar();
        }

        // Update is called once per frame
        private void Update()
        {
            pathProgress += Time.deltaTime * ratType.speed;

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

        private void UpdateHpBar()
        {
            float hpRatio = hp / ratType.startHp;
            Debug.Log(hpRatio);
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
            Die();
        }

        private void Die()
        {
            Debug.Log("This rat has died.");
            Destroy(gameObject);
        }
    }
}
