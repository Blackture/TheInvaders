using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardBattle.Core.Actions;

namespace CardBattle.Core.Actions.Derived
{
    public class CardStack<T> : Action where T : Card
    {
        private List<T> cards = new List<T>();

        public CardStack(List<T> cards, bool isOptional = false) : base(isOptional)
        {
            this.cards = Shuffle(cards);
        }

        /// <summary>
        /// Shuffles this stack of cards
        /// </summary>
        /// <returns>Returns this stack shuffled</returns>
        public List<T> Shuffle()
        {
            List<T> _cards = new List<T>();
            while (cards.Count > 0)
            {
                T t = cards[Random.Range(0, cards.Count)];
                _cards.Add(t);
                cards.Remove(t);
            }
            return _cards;
        }

        /// <summary>
        /// Shuffles the stack of cards (<paramref name="cards"/>) inputted.
        /// </summary>
        /// <param name="cards"></param>
        /// <returns>Return the shuffled stack of cards</returns>
        public List<T> Shuffle(List<T> cards)
        {
            List<T> paraCards = cards;
            List<T> _cards = new List<T>();
            while (paraCards.Count > 0)
            {
                T t = paraCards[Random.Range(0, paraCards.Count)];
                _cards.Add(t);
                paraCards.Remove(t);
            }
            return _cards;
        }

        /// <summary>
        /// Draws a card and puts it back under the stack of cards
        /// </summary>
        /// <returns>Returns the card that was drawn.</returns>
        public T DrawCard()
        {
            T t = cards[0];
            cards.RemoveAt(0);
            cards.Add(t);
            return t;
        }

        /// <summary>
        /// Draws a card and executes the card
        /// </summary>
        public override void Execute()
        {
            //T card = DrawCard();
            //card.Execute();
            DrawCard().Execute();
        }
    }
}

