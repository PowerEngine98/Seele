using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.seele
{
    public class ObjectRotator : MonoBehaviour
    {

        public float rotationSpeed = 1;

        void Update()
        {
            transform.Rotate(0, rotationSpeed, 0, Space.Self);
        }
    }
}