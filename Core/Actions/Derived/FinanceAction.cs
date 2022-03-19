using UnityEngine;
using System.Collections.Generic;
using CardBattle.Core.Turns;
using CardBattle.Core.Players;
using CardBattle.Core.AI;

namespace CardBattle.Core.Actions.Derived
{
    public class FinanceAction : Action
    {
        public MULTIPLIER multiplier;
        public int additionalMoney;
        public int enemyAdditionalMoney = 0;
        public int stealMoney;
        public MULTIPLIER enemyMultiplier;
        private PlayerStats opponent;
        private PlayerStats current;

        /// <summary>
        /// Opponent player as enemy
        /// </summary>
        /// <param name="multiplier"></param>
        /// <param name="additionalMoney"></param>
        /// <param name="stealMoney"></param>
        /// <param name="enemyMultiplier"></param>
        public FinanceAction(MULTIPLIER multiplier = MULTIPLIER.NONE, int additionalMoney = 0, int stealMoney = 0, MULTIPLIER enemyMultiplier = MULTIPLIER.NONE, int enemyAdditionalMoney = 0, bool isOptional = false) : base(isOptional)
        {
            this.multiplier = multiplier;
            this.additionalMoney = additionalMoney;
            this.stealMoney = stealMoney;
            this.enemyMultiplier = enemyMultiplier;
            this.enemyAdditionalMoney = enemyAdditionalMoney;
            opponent = GameManager.instance.players[GameManager.turnManager.CurrentPlayer.PlayerIndex];
            current = GameManager.turnManager.CurrentPlayer;
        }

        /// <summary>
        /// Bot as enemy (Bot of the neutral villages)
        /// </summary>
        /// <param name="bot"></param>
        /// <param name="multiplier"></param>
        /// <param name="additionalMoney"></param>
        /// <param name="stealMoney"></param>
        /// <param name="enemyMultiplier"></param>
        public FinanceAction(Bot bot, MULTIPLIER multiplier = MULTIPLIER.NONE, int additionalMoney = 0, int stealMoney = 0, MULTIPLIER enemyMultiplier = MULTIPLIER.NONE, int enemyAdditionalMoney = 0, bool isOptional = false) : base(isOptional)
        {
            this.multiplier = multiplier;
            this.additionalMoney = additionalMoney;
            this.stealMoney = stealMoney;
            this.enemyMultiplier = enemyMultiplier;
            this.enemyAdditionalMoney = enemyAdditionalMoney;
            opponent = bot;
            current = GameManager.turnManager.CurrentPlayer;
        }

        public override void Execute()
        {
            current.revenue.additionalIncome += additionalMoney;
            opponent.revenue.additionalIncome += enemyAdditionalMoney;

            if (opponent.Money - stealMoney < 0)
            {
                current.Money += opponent.Money;
                opponent.Money = 0;
            }
            else
            {
                current.Money += stealMoney;
                opponent.Money -= stealMoney;
            }

            SetMultiplier();
            SetOppenentMultiplier();
        }

        private void SetMultiplier()
        {
            multiplier = (current.revenue.Multiplier == 2 && multiplier == MULTIPLIER.DOUBLE) ? MULTIPLIER.QUADRUPLE : multiplier;
            switch (multiplier)
            {
                case MULTIPLIER.DOUBLE:
                    current.revenue.DoubleMultiplier();
                    break;
                case MULTIPLIER.QUADRUPLE:
                    current.revenue.QuadrupleMultiplier();
                    break;
            }
        }

        private void SetOppenentMultiplier()
        {
            enemyMultiplier = (opponent.revenue.Multiplier == 2 && enemyMultiplier == MULTIPLIER.DOUBLE) ? MULTIPLIER.QUADRUPLE : enemyMultiplier;

            switch (enemyMultiplier)
            {
                case MULTIPLIER.DOUBLE:
                    opponent.revenue.DoubleMultiplier();
                    break;
                case MULTIPLIER.QUADRUPLE:
                    opponent.revenue.QuadrupleMultiplier();
                    break;
            }
        }
    }
}
