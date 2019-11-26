using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace com.seele
{
    public class ObjectSpawnerRegion : MonoBehaviour
    {
        public float minSpawnTime = 1F;
        public float maxSpawnTime = 2F;

        public bool enableDespawn;

        public float minDespawnTime = 1F;

        public float maxDespawnTime = 2F;

        public Spawneable prefab;

        private int numberOfInstances;

        public int count;

        public int maxInstances = 1;

        private float nextSpawnTime;

        private float timeElapsed;

        void Update()
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= nextSpawnTime)
            {
                Random random = new Random();
                if (numberOfInstances < maxInstances)
                {
                    float x = transform.position.x + (transform.localScale.x * ((float)random.NextDouble() - (1 / 2F)));
                    float y = transform.position.y + (transform.localScale.y * ((float)random.NextDouble() - (1 / 2F)));
                    float z = transform.position.z + (transform.localScale.z * ((float)random.NextDouble() - (1 / 2F)));
                    Vector3 location = new Vector3(x, y, z);
                    Spawneable newObject = Instantiate(prefab, location, Quaternion.identity);
                    newObject.transform.parent = transform;
                    newObject.objectSpawner = this;
                    numberOfInstances++;
                    count++;
                    if (enableDespawn)
                    {
                        Destroy(newObject.gameObject, (float)(random.NextDouble() * (maxDespawnTime - minDespawnTime)) + minDespawnTime);
                    }
                }
                nextSpawnTime = ((float)random.NextDouble() * (maxSpawnTime - minSpawnTime)) + minSpawnTime;
                timeElapsed = 0;
            }
        }

        public void OnObjectDeleted(Spawneable spawneable)
        {
            numberOfInstances--;
            count--;
        }

    }
}