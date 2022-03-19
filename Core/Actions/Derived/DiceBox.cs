using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace CardBattle.Core.Actions.Derived
{
    public class DiceBox : Action
    {
        public UnityEvent<int> OnThrow = new UnityEvent<int>();
        private List<Dice> dice = new List<Dice>();

        public DiceBox(int diceAmount, bool isOptional = false) : base(isOptional)
        {
            for (int i = 0; i < diceAmount; i++)
            {
                dice.Add(new Dice());
            }
        }

        public override void Execute()
        {
            int sum = 0;
            for (int i = 0; i < dice.Count; i++)
            {
                sum += dice[i].Throw();
            }
            OnThrow.Invoke(sum);
        }
    }
}