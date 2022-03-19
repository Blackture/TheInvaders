using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardBattle.Core.Actions.Derived
{
    public class ColorDice : Action
    {
        private COLOR[] colors = { COLOR.WHITE, COLOR.RED, COLOR.BLUE, COLOR.GREEN, COLOR.WHITE, COLOR.GREEN };
        private FinanceBox financeBox;

        public ColorDice() : base(true)
        {
            financeBox = new FinanceBox(new FinanceAction());
        }

        private COLOR GetRandomColor() => colors[Random.Range(1, 7)];

        public override void Execute()
        {
            COLOR color = GetRandomColor();
            switch (color)
            {
                case 0:
                    WhiteColor();
                    break;
                case (COLOR)1:
                    RedColor();
                    break;
                case (COLOR)2:
                    BlueColor();
                    break;
                case (COLOR)3:
                    GreenColor();
                    break;
            }
        }

        private void RedColor()
        {
            //if on player's field is a neutral village: => it's now owned by the enemy
            //if on player's field is a village by the player: => Enemy can decide to fight for that village
            GameManager.turnManager.NextTurn();
        }

        private void WhiteColor()
        {
            Turns.Turn.turnActions[1].Execute(); //draw finance card and execute the drawn card
        }

        private void GreenColor()
        {
            financeBox.financeAction.additionalMoney = 1;
            financeBox.financeAction.enemyAdditionalMoney = 0;
            financeBox.financeAction.stealMoney = 0;
            financeBox.Execute();
            //show
        }

        private void BlueColor()
        {
            financeBox.financeAction.additionalMoney = 0;
            financeBox.financeAction.enemyAdditionalMoney = 0;
            financeBox.financeAction.stealMoney = 1;
            financeBox.Execute();
            //show
        }
    }
}
