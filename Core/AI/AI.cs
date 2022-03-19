using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardBattle.Core.Players;

namespace CardBattle.Core.AI
{
    [RequireComponent(typeof(PlayerStats))]
    public class Bot : PlayerStats
    {
        public static Bot AddComponentTo(GameObject attachTo)
        {
            Bot b = attachTo.AddComponent<Bot>();
            b.revenue = new Revenue(b);
            b.debts = new List<Debt>();
            return b;
        }

        public void StartTurn() { }
    }
}
