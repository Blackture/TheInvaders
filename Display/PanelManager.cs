using UnityEngine;
using System.Collections.Generic;

namespace CardBattle.Display
{
    [System.Serializable]
    [AddComponentMenu("Menu/Panel Manager")]
    public class PanelManager : MonoBehaviour
    {
        [SerializeField] private List<Panel> panels = new List<Panel>();
        [SerializeField] [Tooltip("Which panels should be active on start")] Core.Collections.Dictionary<string, bool> activeOnStart = new Core.Collections.Dictionary<string, bool>();

        private Core.Collections.Dictionary<string, Panel> internalPanels = new Core.Collections.Dictionary<string, Panel>();

        private void Awake()
        {
            foreach (Panel panel in panels)
            {
                internalPanels.Add(panel.panelName, panel);
            }
        }

        private void Start()
        {
            foreach (Core.Collections.DictionaryPair<string,bool> pair in activeOnStart.pairs) 
            {
                if (pair.item1)
                {
                    OpenPanel(pair.item0);
                }
                else
                {
                    ClosePanel(pair.item0);
                }
            }
        }

        public Panel GetPanel(string panelName) => internalPanels[panelName];
        public T GetPanel<T>(string panelName) where T : Panel => (T)internalPanels[panelName];

        public void OpenPanel(string panelName) => internalPanels[panelName].Open();
        public void ClosePanel(string panelName) => internalPanels[panelName].Close();
    }
}
