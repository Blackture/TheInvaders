using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardBattle.Core.Players
{
    public class Player : PlayerStats
    {
        private int playerIndex;
        private Photon.Realtime.Player networkPlayer;

        public bool isItHisTurn; //he in relation to the player

        public int PlayerIndex => playerIndex;
        public Photon.Realtime.Player NetworkPlayer => networkPlayer;

        public static Player AddComponentTo(GameObject attachTo, Photon.Realtime.Player networkPlayer)
        {
            Player p = attachTo.AddComponent<Player>();
            GameManager.instance.players.Add(p);
            p.playerIndex = GameManager.instance.players.IndexOf(p);
            p.revenue = new Revenue(p);
            p.debts = new List<Debt>();
            return p;
        }
    }
}
