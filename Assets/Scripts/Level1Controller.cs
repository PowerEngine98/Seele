using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.seele
{
    public class Level1Controller : Executable
    {

        public int numberOfOrbs;

        public TranslatorController controller;

        public override void Execute()
        {
            numberOfOrbs++;
            if (numberOfOrbs == 2)
            {
                controller.GoToTarget();
            }
        }
    }
}