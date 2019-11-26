using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.seele
{

    public class BridgeController : PressurePlateObserver
    {

        public PlataformController controller;

        public override void onPressurePlateTriggerEnter(PressurePlateController pressurePlate)
        {
            controller.GoToTarget();
        }

        public override void onPressurePlateTriggerExit(PressurePlateController pressurePlate)
        {

        }

        public override void onPressurePlateTriggerStay(PressurePlateController pressurePlate)
        {
            controller.GoToTarget();
        }

    }

}