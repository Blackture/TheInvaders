using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace CardBattle.Display
{
    public abstract class Panel : MonoBehaviour
    {
        public string panelName;
        public List<Selectable> interactables;

        public void Open()
        {
            gameObject.SetActive(true);
            Load();
        }

        public void Close() => gameObject.SetActive(false);

        protected abstract void Load();
        protected virtual void Update() { }
        protected virtual void Start() { }
        protected virtual void Awake() { }
        public virtual void Set(bool interactable)
        {
            foreach (Selectable selectable in interactables)
            {
                selectable.interactable = interactable;
            }
        }
    }
}
