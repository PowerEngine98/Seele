using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.seele
{

    public delegate void VoidCallback();

    public class CameraController : MonoBehaviour
    {
        public Camera camera;

        public Transform parent;

        public Vector3 outset;

        public float defaultSize = 10;

        public float focusDistance = 5;

        public float zoomSpeed = 1;

        public bool focused;

        public bool zoomIn;

        public bool zoomOut;

        private VoidCallback focusCallback;

        private VoidCallback enlargeCallback;

        void Update()
        {
            if (!focused)
            {
                SetInDefaultPosition();
            }
            if (zoomIn)
            {
                camera.orthographicSize -= zoomSpeed * Time.deltaTime;
                if (camera.orthographicSize <= focusDistance)
                {
                    camera.orthographicSize = focusDistance;
                    zoomIn = false;
                    focused = true;
                    if (focusCallback != null)
                    {
                        focusCallback();
                        focusCallback = null;
                    }
                }
            }
            if (zoomOut)
            {
                camera.orthographicSize += zoomSpeed * Time.deltaTime;
                if (camera.orthographicSize >= defaultSize)
                {
                    camera.orthographicSize = defaultSize;
                    zoomOut = false;
                    if (enlargeCallback != null)
                    {
                        enlargeCallback();
                        enlargeCallback = null;
                    }
                }
            }
        }

        public void Focus()
        {
            zoomIn = true;
            zoomOut = false;
        }

        public void Focus(VoidCallback callback)
        {
            focusCallback = callback;
            Focus();
        }

        public void Enlarge()
        {
            focused = false;
            zoomIn = false;
            zoomOut = true;
        }

        public void Enlarge(VoidCallback callback)
        {
            enlargeCallback = callback;
            Enlarge();
        }

        public void ResetSize()
        {
            focused = false;
            zoomIn = false;
            zoomOut = false;
            camera.orthographicSize = defaultSize;
        }

        public void SetInFocusSize()
        {
            focused = true;
            zoomIn = false;
            zoomOut = false;
            camera.orthographicSize = focusDistance;
        }

        public void SetInDefaultPosition()
        {
            transform.position = parent.position + outset;
        }
    }

}