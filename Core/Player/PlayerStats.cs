using UnityEngine;
using System.Collections.Generic;

namespace CardBattle.Core.Players
{
    public class PlayerStats : MonoBehaviour, IPlayerStats
    {
        private int money;
        protected List<Debt> debts;
        private List<int> villages = new List<int>();
        private List<int> towns = new List<int>();

        public Revenue revenue;

        public int Money
        {
            get => money;
            set => money = value >= 0 ? value : 0;
        }
        public List<Debt> Debts
        {
            get => debts;
        }

        public int VillageCount { get { return villages.Count; } }
        public int TownCount { get { return towns.Count; } }
    }
}
