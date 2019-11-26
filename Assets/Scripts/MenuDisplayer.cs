using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.seele
{
    public class MenuDisplayer : ExecutableArea
    {
        public CameraController cameraController;
        public InstructionPanelController panelController;

        void Start()
        {
            cameraController.SetInFocusSize();
            VoidCallback callback = () =>
            {
                panelController.Show(0);
            };
            cameraController.Enlarge(callback);
        }

        public override void ExecuteEnter()
        {
            VoidCallback callback = () =>
            {
                LevelManager.LoadScene(LevelManager.Level.LEVEL_1);
            };
            cameraController.Focus(callback);
        }

        public override void ExecuteExit()
        {

        }
    }
}