using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.seele
{
    public class UIController : MonoBehaviour
    {
        public RectTransform lifeIconContainer;
        public GameObject lifesPrefab;
        public List<GameObject> lifeIcons;
        public Texture2D cursorTexture;
        public CursorMode cursorMode = CursorMode.Auto;
        public bool autoCenterHotSpot = false;
        public Vector2 hotSpotCustom = Vector2.zero;
        private Vector2 hotSpotAuto;

        void Start()
        {
            Vector2 hotSpot;
            if (autoCenterHotSpot)
            {
                hotSpotAuto = new Vector2(cursorTexture.width * 0.5f, cursorTexture.height * 0.5f);
                hotSpot = hotSpotAuto;
            }
            else
            {
                hotSpot = hotSpotCustom;
            }
            Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.ForceSoftware);

            lifeIcons = new List<GameObject>();
            UpdateLifes();
        }

        void Update()
        {
            UpdateLifes();
        }

        public void UpdateLifes()
        {
            GameObject o;
            Vector3 location = lifeIconContainer.position;
            Vector3 scale = lifeIconContainer.localScale;
            RectTransform t = GetComponent<RectTransform>();
            foreach (GameObject g in lifeIcons)
            {
                Destroy(g);
            }
            lifeIcons.Clear();
            for (int i = 0; i < LevelController.lifes; i++)
            {
                scale -= new Vector3(0.3F, 0.3F, 0);
                o = Instantiate(lifesPrefab, location, Quaternion.identity);
                t = o.GetComponent<RectTransform>();
                location -= new Vector3(0, (t.sizeDelta.y * (0.5F + (scale.y / 2F))) + t.sizeDelta.y * (scale.y / 2F) + 20F, 0);
                o.transform.SetParent(lifeIconContainer.transform);
                o.transform.position = location;
                o.transform.localScale = scale;
                lifeIcons.Add(o);
            }
        }

        public void UpdateEnergy(float energy)
        {

        }

    }

}