using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using CardBattle.Core.Players;
using CardBattle.RemoteConfig;

namespace CardBattle.Core.Actions.Derived
{
    public class FinanceBox : Action
    { 
        private DiceBox diceBox;

        /// <summary>
        /// <list type="table">
        /// <item>If this' additionalMoney is greater than 0, it gets replaced by the thrown amount.</item>
        /// <item>If this' enemyAdditionalMoney is greater than 0, it gets replaced by the thrown amount.</item>
        /// <item>If this' stealMoney is greater than 0, it gets replaced by the thrown amount.</item>
        /// </list>
        /// </summary>
        public FinanceAction financeAction;

        /// <summary>
        /// <list type="table">
        /// <item>If <paramref name="financeAction"/>'s additionalMoney is greater than 0, it gets replaced by the thrown amount.</item>
        /// <item>If <paramref name="financeAction"/>'s enemyAdditionalMoney is greater than 0, it gets replaced by the thrown amount.</item>
        /// <item>If <paramref name="financeAction"/>'s stealMoney is greater than 0, it gets replaced by the thrown amount.</item>
        /// </list>
        /// </summary>
        /// <param name="financeAction"></param>
        /// <param name="isOptional"></param>
        public FinanceBox(FinanceAction financeAction, bool isOptional = false) : base(isOptional)
        {
            diceBox = new DiceBox(Configs.FinanceBox_DiceAmount);
            diceBox.OnThrow.AddListener(Execute);
            this.financeAction = financeAction;
        }

        public override void Execute()
        {
            diceBox.Execute();
        }

        private void Execute(int amount)
        {
            if (financeAction.additionalMoney > 0) financeAction.additionalMoney = amount;
            if (financeAction.enemyAdditionalMoney > 0) financeAction.enemyAdditionalMoney = amount;
            if (financeAction.stealMoney > 0) financeAction.stealMoney = amount;
            financeAction.Execute();
        }
    }
}