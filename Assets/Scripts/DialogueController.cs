using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.seele
{

    public class DialogueController : MonoBehaviour
    {
        public Text title;
        public Text text;
        public GameObject panel;
        public PlayerController playerController;
        public Executable executable;
        private Dialog[] dialogues;
        private int index;

        public TextAsset dialoguesSource;

        void Start()
        {
            panel.gameObject.SetActive(false);
            LoadSource();
        }

        private void LoadSource()
        {
            if (dialoguesSource != null)
            {
                dialogues = JsonArrayParser.FromJson<Dialog>(dialoguesSource.text);
                if (dialogues != null)
                {
                    index = 0;
                    RenderDialogue();
                }
            }
        }
        public void ChangeSource(TextAsset source)
        {
            dialoguesSource = source;
            LoadSource();
        }

        public void ChangeExecutable(Executable executable)
        {
            this.executable = executable;
        }

        public void Display(bool freeze)
        {
            panel.gameObject.SetActive(true);
            RenderDialogue();
            if (playerController != null && freeze)
            {
                playerController.freezed = true;
            }
        }

        public void NextDialogue()
        {
            if (dialogues != null)
            {
                if (index == dialogues.Length - 1)
                {
                    OnFinish(false);
                }
                index = index == dialogues.Length - 1 ? 0 : index + 1;
                RenderDialogue();
            }
        }

        private void RenderDialogue()
        {
            Dialog dialogue = dialogues[index];
            title.text = dialogue.title != null ? dialogue.title : string.Empty;
            text.text = dialogue.text != null ? dialogue.text : string.Empty;
        }

        public void SkipDialogues()
        {
            index = 0;
            OnFinish(false);
        }

        public void CancelDialogue()
        {
            index = 0;
            OnFinish(true);
        }

        private void OnFinish(bool cancelled)
        {
            panel.gameObject.SetActive(false);
            if (!cancelled && executable)
            {
                executable.Execute();
            }
            if (playerController != null)
            {
                playerController.freezed = false;
            }
        }

    }
}