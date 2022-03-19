using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using CardBattle.RemoteConfig;

namespace CardBattle.Core.Players
{
    public class Revenue
    {
        private PlayerStats player;
        public Dictionary<PlayerStats, int> influencers = new Dictionary<PlayerStats, int>();
        private int income;
        private int multiplier;
        public int additionalIncome;

        public int Multiplier { get => multiplier; }

        public Revenue(PlayerStats player)
        {
            this.player = player;
        } 

        public void DoubleMultiplier() => multiplier = 2;
        public void QuadrupleMultiplier() => multiplier = 4;
        public void ZeroMultiplier() => multiplier = 0;
        public void ResetMultiplier() => multiplier = 1;

        private void RecalculateIncome()
        {
            income = Mathf.RoundToInt(Configs.Revenue_VillageFactor * player.VillageCount + Configs.Revenue_TownFactor * player.TownCount);
        }

        private void ResetRevenue()
        {
            ResetMultiplier();
            additionalIncome = 0;
            RecalculateIncome();
        }

        public int GetRevenue()
        {
            RecalculateIncome();
            int revenue = (income + additionalIncome) * multiplier;
            if (CheckForDebt(revenue))
            {
                revenue = 0;
            }
            ResetRevenue();
            return revenue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revenue"></param>
        /// <returns>True if he's debting additional money a creditor (He = the creditor)</returns>
        private bool CheckForDebt(int revenue)
        {
            bool res = false;
            if (revenue < 0)
            {
                AddDepts(Mathf.Abs(revenue));
                res = true;
            }
            return res;
        }

        private void AddDepts(int revenue)
        {
            List<PlayerStats> playerStats = influencers.Keys.ToList();
            Dictionary<int, Debt> debts = GetDebts(playerStats);
            int[] divisors = GetDebtDivisors();
            for (int i = 0; i < debts.Count; i++)
            {
                debts[i].AddDebt(Mathf.RoundToInt(revenue / divisors[i]));
            }
        }

        private Dictionary<int, Debt> GetDebts(List<PlayerStats> playerStats)
        {
            Dictionary<int, Debt> debts = new Dictionary<int, Debt>();
            foreach (Debt debt in player.Debts)
            {
                if (playerStats.Contains(debt.Creditor))
                {
                    debts.Add(playerStats.IndexOf(debt.Creditor), debt);
                }
            }

            if (debts.Count < playerStats.Count)
            {
                for (int i = 0; i < playerStats.Count; i++)
                {
                    if (!debts.ContainsKey(i))
                    {
                        Debt d = new Debt(playerStats[i], player);
                        playerStats[i].Debts.Add(d);
                        debts.Add(i, d);
                    }
                }
            }

            return debts;
        }

        private int[] GetDebtDivisors()
        {
            int[] divisors = new int[influencers.Count];
            int[] subs = influencers.Values.ToArray();
            int summedSubs = subs.Sum();

            for (int i = 0; i < influencers.Count; i++)
            {
                divisors[i] = summedSubs / subs[i];
            }

            return divisors;
        }
    }
}

