using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.seele
{
    public class SliderController : PressurePlateObserver
    {

        public PlataformController controller;

        public override void onPressurePlateTriggerEnter(PressurePlateController pressurePlate)
        {
            controller.GoToTarget();
        }

        public override void onPressurePlateTriggerExit(PressurePlateController pressurePlate)
        {
            controller.GoToOrigin();
        }

        public override void onPressurePlateTriggerStay(PressurePlateController pressurePlate)
        {
            if (!controller.goingToTarget)
            {
                controller.GoToTarget();
            }
        }
    }
}