using UnityEngine;
using System.Collections.Generic;
using CardBattle.Core.Actions;

namespace CardBattle.Core.Actions.Derived
{
    public partial class Card
    {
        private string cardTitle;
        private string description;
        private List<Material> materials;
        private Action action;

        public string CardTitle { get => cardTitle; }
        public string Description { get => description; }

        public List<Material> Materials
        {
            get
            {
                Material[] _mats = new Material[materials.Count];
                materials.CopyTo(_mats);
                List<Material> __mats = new List<Material>();
                __mats.AddRange(_mats);
                return __mats;
            }
        }

        public Card(string cardTitle, Action action, List<Material> materials)
        {
            this.cardTitle = cardTitle;
            this.action = action;
            this.materials = materials;
        }

        public void Execute() => action.Execute();
    }
}
