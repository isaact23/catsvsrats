using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rat
{
    public class Rat : MonoBehaviour
    {
        public RatTypeScriptableObject ratType;
        public PathScriptableObject path;

        // HP bar
        public GameObject hpRed;
        public GameObject hpGreen;

        private float hp;
        private float pathProgress;

        public void TakeDamage(float damage)
        {
            hp -= damage;
            if (hp <= 0f) {
                Die();
            }

            UpdateHpBar();
        }

        // Start is called before the first frame update
        private void Start()
        {
            GetComponent<SpriteRenderer>().sprite = ratType.sprite;
            hp = ratType.startHp;
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
            Vector2 ratPos = Vector2.Lerp(lowerCoord, upperCoord, percentBetween);
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
            Debug.Log("This rat reached the end!");
            Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
