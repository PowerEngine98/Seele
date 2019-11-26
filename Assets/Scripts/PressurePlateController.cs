using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.seele
{

    public class PressurePlateController : MonoBehaviour
    {

        public PressurePlateObserver observer;

        private void OnTriggerEnter(Collider other)
        {
            observer.onPressurePlateTriggerEnter(this);
        }

        private void OnTriggerExit(Collider other)
        {
            observer.onPressurePlateTriggerExit(this);
        }

        private void OnTriggerStay(Collider other)
        {
            observer.onPressurePlateTriggerStay(this);
        }

    }

}