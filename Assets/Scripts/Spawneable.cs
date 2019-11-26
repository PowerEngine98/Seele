using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.seele
{
    public abstract class Spawneable : MonoBehaviour
    {
        public ObjectSpawnerRegion objectSpawner;

        private void OnDestroy()
        {
            if (objectSpawner)
            {
                objectSpawner.OnObjectDeleted(this);
            }
        }

    }
}