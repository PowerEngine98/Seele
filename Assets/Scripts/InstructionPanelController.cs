using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;

namespace com.seele
{
    public class InstructionPanelController : MonoBehaviour
    {
        public Sprite[] images;

        public Image image;

        private int currentIndex;

        public float fadeInSpeed = 1;
        public float fadeOutSpeed = 1;

        public float minFadeValue = 0;

        public float maxFadeValue = 1;

        private bool hiding;
        private bool showing;

        void Start()
        {
            gameObject.SetActive(false);
        }

        void Update()
        {
            if (showing)
            {
                var color = image.color;
                color.a += fadeInSpeed * Time.deltaTime;
                image.color = color;
                if (color.a >= maxFadeValue)
                {
                    color.a = maxFadeValue;
                    image.color = color;
                }
            }
            if (hiding)
            {
                var color = image.color;
                color.a -= fadeInSpeed * Time.deltaTime;
                image.color = color;
                if (color.a <= minFadeValue)
                {
                    color.a = minFadeValue;
                    image.color = color;
                    gameObject.SetActive(false);
                    currentIndex = currentIndex == images.Length - 1 ? 0 : currentIndex + 1;
                    hiding = false;
                }
            }
        }

        public void Show()
        {
            image.sprite = images[currentIndex];
            var color = image.color;
            color.a = minFadeValue;
            image.color = color;
            gameObject.SetActive(true);
            showing = true;
            hiding = false;
        }

        public void Show(int index)
        {
            currentIndex = index;
            Show();
        }

        public void Hide()
        {
            showing = false;
            hiding = true;
        }
    }
}

