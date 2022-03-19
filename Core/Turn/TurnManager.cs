using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardBattle.Core.Players;
using CardBattle.Core.AI;
using CardBattle.Core.Actions;
using CardBattle.Core.Actions.Derived;

namespace CardBattle.Core.Turns
{
    public class TurnManager : MonoBehaviour
    {
        public CardStack<Card> financeCards;

        private int playerTurnIndex = 0;
        private Player currentPlayer;
        public Player CurrentPlayer { get => currentPlayer; }
        private Player opponentPlayer;
        public Player OpponentPlayer { get => opponentPlayer; }
        private Bot bot;
        public Bot Bot { get => bot; }

        public void Init(Bot bot)
        {
            this.bot = bot;


            InitCards();
            FirstTurn();
        }

        private void InitCards()
        {
            InitFinanceCards();
        }

        private void InitFinanceCards()
        {
            List<Card> cards = new List<Card>();

            for (int i = 0; i < 4; i++) //0 = green; 1 = yellow; 2 = blue; 3 = red
            {
                string color = "";
                switch (i)
                {
                    case 0:
                        color = "Green";
                        break;
                    case 1:
                        color = "Yellow";
                        break;
                    case 2:
                        color = "Blue";
                        break;
                    case 3:
                        color = "Red";
                        break;
                }

                for (int j = 1; j < 10; j++)
                {
                    Action t;
                    if (i == 0) t = new FinanceAction(MULTIPLIER.NONE, j, 0, MULTIPLIER.NONE, 0);
                    else if (i == 1) t = new FinanceAction(GameManager.instance.bot, MULTIPLIER.NONE, 0, 0, MULTIPLIER.NONE, j);
                    else if (i == 2) t = new FinanceAction(MULTIPLIER.NONE, 0, 0, MULTIPLIER.NONE, j);
                    else t = new FinanceAction(MULTIPLIER.NONE, 0, 0, MULTIPLIER.NONE, -j);
                    cards.Add(new Card($"{color} {j}", t, null));
                }
            }

            financeCards = new CardStack<Card>(cards);
        }

        public Player FirstTurn()
        {
            opponentPlayer = GameManager.instance.players[1];
            opponentPlayer.isItHisTurn = false;
            currentPlayer = GameManager.instance.players[0];
            currentPlayer.isItHisTurn = true;

            return currentPlayer;
        }

        public Player NextTurn()
        { 
            opponentPlayer = currentPlayer;
            opponentPlayer.isItHisTurn = false;
            currentPlayer = NextPlayer();
            currentPlayer.isItHisTurn = true;

            return currentPlayer;
        }

        private Player NextPlayer()
        {
            playerTurnIndex++;
            if (playerTurnIndex > 1)
            {
                GameManager.instance.bot.StartTurn();
                playerTurnIndex = 0;
            }
            return GameManager.instance.players[playerTurnIndex];
        }
    }
}
