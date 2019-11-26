using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace com.seele
{
    public class RampBoostArea : ExecutableArea
    {
        public NavMeshAgent agent;

        public override void ExecuteEnter()
        {
            if (agent)
            {
                agent.speed = PlayerController.RAMP_SPEED;
            }
        }

        public override void ExecuteExit()
        {
            if (agent)
            {
                agent.speed = PlayerController.NOMINAL_SPEED;
            }
        }

    }
}

