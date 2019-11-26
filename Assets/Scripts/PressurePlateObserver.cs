using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.seele
{
    public abstract class PressurePlateObserver : MonoBehaviour
    {
        public abstract void onPressurePlateTriggerEnter(PressurePlateController pressurePlate);

        public abstract void onPressurePlateTriggerExit(PressurePlateController pressurePlate);

        public abstract void onPressurePlateTriggerStay(PressurePlateController pressurePlate);

    }
}