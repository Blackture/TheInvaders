using UnityEngine;
using CardBattle.RemoteConfig;

namespace CardBattle.Core.Players
{
    public class Debt
    {
        private int amount;
        private int time;
        private PlayerStats creditor;
        private PlayerStats debtor;

        public int Amount { get => amount; }
        public int Time { get => time; }
        public PlayerStats Creditor { get => creditor; }
        public PlayerStats Debtor { get => debtor; }

        public Debt(int amount, int time, PlayerStats creditor, PlayerStats debtor)
        {
            this.amount = amount;
            this.time = time;
            this.creditor = creditor;
            this.debtor = debtor;
        }

        public Debt(PlayerStats creditor, PlayerStats debtor)
        {
            Reset();
            this.creditor = creditor;
            this.debtor = debtor;
        }

        public void UpdateTime()
        {
            time++;
            amount = Mathf.CeilToInt(amount * Configs.Debt_Interest);
        }
        public void AddDebt(int amount) => this.amount += amount;
        /// <summary>
        /// Pays the debts
        /// </summary>
        /// <returns>True if the payment was successful.</returns>
        public bool PayDebt()
        {
            bool res = false;
            if (debtor.Money >= amount)
            {
                debtor.Money -= amount;
                creditor.Money += amount;
                Reset();
            }
            return res;
        }

        private void Reset()
        {
            amount = 0;
            time = 0;
        }
    }
}
