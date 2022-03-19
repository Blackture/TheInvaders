using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardBattle.Core.Players;
using CardBattle.Core.Actions;
using CardBattle.Core.Actions.Derived;

namespace CardBattle.Core.Turns
{
    public class Turn
    {
        private Player player;
        public static readonly List<Action> turnActions = new List<Action>();

        public Turn(Player player)
        {
            this.player = player;
        }


        public void Reset()
        {
            turnActions.Clear();
            turnActions.Add(new ColorDice());
            turnActions.Add(GameManager.turnManager.financeCards);
            //turnActions.Add()
        }
    }
}