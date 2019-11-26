using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.seele
{
    public class DialogueAgentArea : ExecutableArea
    {
        public DialogueController dialogueController;
        public TextAsset dialogueSource;

        public Executable executable;

        public override void ExecuteEnter()
        {
            if (dialogueController != null && dialogueSource != null)
            {
                dialogueController.ChangeSource(dialogueSource);
                dialogueController.ChangeExecutable(executable);
                dialogueController.Display(true);
            }
        }

        public override void ExecuteExit()
        {
            if (dialogueController != null)
            {
                dialogueController.CancelDialogue();
            }
        }
    }

}