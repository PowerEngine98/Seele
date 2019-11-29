using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace com.seele
{
    public class CollectableItem : Spawneable
    {
        public static string COLLECTABLE_TAG = "Collectable";
        public static float distance = 0.2F;
        private Vector3 origin;

        private float speedY = 0.2F;

        private void Start()
        {
            this.origin = transform.position;
            Random random = new Random();
            this.transform.position = new Vector3(origin.x, origin.y + (distance * (-1 + (2 * (float)random.NextDouble()))), origin.z);
        }

        private void Update()
        {
            transform.Translate(0, speedY * Time.deltaTime, 0);
            if (Vector3.Distance(origin, transform.position) >= distance)
            {
                speedY *= -1;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == PlayerController.PLAYER_TAG)
            {
                OnPlayerCollision();
            }
        }

        public virtual void OnPlayerCollision()
        {
            Destroy(gameObject);
        }
    }
}

