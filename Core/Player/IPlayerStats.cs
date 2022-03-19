using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardBattle.Core.Players
{
    public interface IPlayerStats
    {
        public int Money { get; set; }
        public List<Debt> Debts { get; }
        public int VillageCount { get; }
        public int TownCount { get; }
    }
}
