using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardBattle.Core.Actions
{
    public abstract class Action
    {
        public readonly bool isOptional;

        /// <summary>
        /// Automatically sets the action's optionality to false
        /// </summary>
        public Action()
        {
            this.isOptional = false;
        }
        public Action(bool isOptional)
        {
            this.isOptional = isOptional;
        }

        public abstract void Execute();
    }
}
