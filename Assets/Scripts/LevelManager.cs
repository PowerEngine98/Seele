using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace com.seele
{
    public class LevelManager : MonoBehaviour
    {

        private static string[] levels = { "MainMenu", "Demo", "Level_1" };

        public enum Level
        {
            MAIN_MENU,
            TUTORIAL,
            LEVEL_1
        }

        public void LoadLevel(string name)
        {
            SceneManager.LoadScene(name);
        }

        public static void LoadScene(string name)
        {
            SceneManager.LoadScene(name);
        }

        public static void LoadScene(Level level)
        {
            SceneManager.LoadScene(levels[(int)level]);
        }

        public static void GoToMainMenu()
        {
            LoadScene(Level.MAIN_MENU);
        }

        public static void Restart()
        {
            Application.LoadLevel(Application.loadedLevel);
        }

    }

}

