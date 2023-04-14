using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace CardBattle.Display.Panels
{
    internal class CreateRoomPanel : Panel
    {
        public Core.Slider gamemodeS;
        public Core.Slider difficultyS;
        public TMP_InputField roomNameInput;
        public TMP_InputField nameInput;
        public Button BnCreate;

        private List<GameObject> menuItems;
        private bool isActive = true;

        private List<GameObject> GetMenuItems()
        {
            return new List<GameObject>()
            {
                BnCreate.transform.Find("Text").gameObject,
                BnCreate.transform.Find("Loading").gameObject,

                roomNameInput.transform.Find("Text Area").gameObject,
                roomNameInput.transform.Find("Loading").gameObject,

                nameInput.transform.Find("Text Area").gameObject,
                nameInput.transform.Find("Loading").gameObject,
            };
        }

        protected override void Load()
        {
            Set(isActive);
        }

        protected override void Awake()
        {
            base.Awake();
            menuItems = GetMenuItems();
        }

        public override void Set(bool interactable)
        {
            base.Set(interactable);
            isActive = interactable;

            nameInput.interactable = interactable;
            roomNameInput.interactable = interactable;  
            BnCreate.interactable = interactable;

            foreach (GameObject g in menuItems)
            {
                if (g.name == "Loading")
                {
                    g.SetActive(!interactable);
                }
                else
                {
                    g.SetActive(interactable);
                }
            }
        }
    }
}
