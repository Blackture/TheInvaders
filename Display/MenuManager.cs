using UnityEngine;

namespace CardBattle.Display
{
    [AddComponentMenu("Menu/Menu Manager")]
    [RequireComponent(typeof(PanelManager))]
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;
        public PanelManager panelManager;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(Instance);
        }
    }
}
