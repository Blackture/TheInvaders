using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardBattle.Core.Actions.Derived
{
    public class Dice : Action
    {
        protected Action[] actions;

        public Dice(bool isOptional = false) : base(isOptional)
        {
            actions = null;
        }

        public Dice(Action[] actions, bool isOptional = false) : base(isOptional)
        {
            if (actions.Length > 6) throw new System.Exception("Too many actions");
            this.actions = actions;
        }

        public int Throw()
        {
            int rnd = Random.Range(1, 7);
            if (actions != null) actions[rnd]?.Execute();
            return rnd;
        }
        public override void Execute()
        {
            if (actions != null) actions[Throw()]?.Execute();
        }
    }
}
