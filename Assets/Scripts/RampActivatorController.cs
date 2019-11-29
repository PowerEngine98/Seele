using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.seele
{
    public class RampActivatorController : Executable
    {
        public int numberOfOrbs;
        public int orbsToTake;
        public PlataformController plataform;

        public override void Execute()
        {
            numberOfOrbs++;
            if (numberOfOrbs == orbsToTake)
            {
                OrbsTaken();
            }
        }

        public void OrbsTaken()
        {
            plataform.GoToTarget();
        }

    }
}

