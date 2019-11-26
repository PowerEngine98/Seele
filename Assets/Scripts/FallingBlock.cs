using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.seele
{
    public class FallingBlock : ExecutableArea
    {
        public Rigidbody rigidBody;
        public float fallTime = 1;
        public float deathTime = 5;
        private float elapsed;

        private bool entered;

        void Start()
        {
            rigidBody.useGravity = false;
        }

        void Update()
        {
            if (entered)
            {
                elapsed += Time.deltaTime;
                if (elapsed >= fallTime)
                {
                    rigidBody.useGravity = true;
                }
                if (elapsed >= deathTime)
                {
                    Destroy(gameObject);
                }
            }
        }

        void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == PlayerController.PLAYER_TAG)
            {
                entered = true;
            }
        }

        public override void ExecuteEnter()
        {
            entered = true;
        }

        public override void ExecuteExit()
        {

        }
    }
}