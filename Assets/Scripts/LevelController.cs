using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.seele
{
    public class LevelController : MonoBehaviour
    {

        public static int MAX_LIFES = 3;
        public static int lifes = MAX_LIFES;

        public static void OnDie()
        {
            lifes--;
            if (lifes > 0)
            {
                LevelManager.Restart();
            }
            else
            {
                lifes = MAX_LIFES;
                LevelManager.GoToMainMenu();
            }
        }

    }
}