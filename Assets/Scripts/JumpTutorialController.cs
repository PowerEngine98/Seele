using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.seele
{
    public class JumpTutorialController : ExecutableArea
    {
        public InstructionPanelController panelController;

        private bool shown;
        public override void ExecuteEnter()
        {
            if (!shown)
            {
                panelController.Show(1);
                shown = true;
            }
        }

        public override void ExecuteExit()
        {

        }
    }
}