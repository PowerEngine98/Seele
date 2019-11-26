using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;


namespace com.seele
{
    public class NameCollider : MonoBehaviour
    {

        public string mensaje;
        public bool entro = false;


        void Update()
        {



        }
        void OnGUI()
        {

            if (entro)
            {

                GUIStyle style = new GUIStyle();
                style.fontSize = 60;
                
                style.alignment = TextAnchor.MiddleCenter;
                GUI.Label(new Rect(Screen.width / 2 - 150, 50, 300, 100), mensaje, style);

            }


        }
        void OnTriggerEnter()
        {

            entro = true;

        }

        void OnTriggerExit()
        {

            entro = false;

        }

    }
}