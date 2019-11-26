using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.seele
{
    public class TranslatorController : MonoBehaviour
    {
        protected Vector3 origin;
        public float velocity = 1;
        public Vector3 target = Vector3.zero;
        public bool goingToTarget = false;
        public bool goingToOrigin = false;

        void Start()
        {
            origin = transform.position;
        }

        private void FixedUpdate()
        {
            if (goingToTarget)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, velocity * Time.deltaTime);
                if (Vector3.Distance(transform.position, target) < 0.01)
                {
                    transform.position = target;
                    goingToTarget = false;
                }
            }
            if (goingToOrigin)
            {
                transform.position = Vector3.MoveTowards(transform.position, origin, velocity * Time.deltaTime);
                if (Vector3.Distance(transform.position, origin) < 0.01)
                {
                    transform.position = origin;
                    goingToOrigin = false;
                }
            }
        }

        public void GoToOrigin()
        {
            goingToOrigin = true;
            goingToTarget = false;
        }

        public void GoToTarget()
        {
            goingToOrigin = false;
            goingToTarget = true;
        }

        public void Stop()
        {
            goingToOrigin = false;
            goingToTarget = false;
        }

    }
}