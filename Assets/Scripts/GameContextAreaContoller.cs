using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.seele
{
    public class GameContextAreaContoller : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == PlayerController.PLAYER_TAG)
            {
                PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
                playerController.Die();
            }
        }

    }
}