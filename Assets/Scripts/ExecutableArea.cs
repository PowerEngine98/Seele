using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.seele
{
    public abstract class ExecutableArea : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == PlayerController.PLAYER_TAG)
            {
                ExecuteEnter();
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == PlayerController.PLAYER_TAG)
            {
                ExecuteExit();
            }
        }

        public abstract void ExecuteEnter();

        public abstract void ExecuteExit();

    }
}
