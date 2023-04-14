using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace CardBattle.Display.Panels
{
    internal class JoinRoomPanel : Panel
    {
        public TMP_InputField roomNameInput;
        public TMP_InputField nameInput;
        public Button BnJoin;
        public Button BnJoinRandom;

        private List<GameObject> menuItems;
        private bool isActive = true;

        private List<GameObject> GetMenuItems()
        {
            return new List<GameObject>()
            {
                BnJoin.transform.Find("Text").gameObject,
                BnJoin.transform.Find("Loading").gameObject,

                BnJoinRandom.transform.Find("Text").gameObject,
                BnJoinRandom.transform.Find("Loading").gameObject,

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

            roomNameInput.interactable = interactable;
            nameInput.interactable = interactable;
            BnJoin.interactable = interactable;

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
