using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using CardBattle.Core.Players;
using CardBattle.Core.Turns;
using CardBattle.RemoteConfig;

namespace CardBattle.Core
{
    [RequireComponent(typeof(TurnManager))]
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public static TurnManager turnManager;

        public List<Player> players = new List<Player>();
        public AI.Bot bot;

        public MatchType gamemode;
        public DIFFICULTY difficulty;

        public void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
            }
        }

        public void Start()
        {
            turnManager = GetComponent<TurnManager>();
        }

        public void Initiate()
        {
            players.Clear();

        }

        public void Initiate(MatchType gamemode)
        {
            this.gamemode = gamemode;
        }

        public void Initiate(DIFFICULTY difficulty)
        {
            this.difficulty = difficulty;
        }

        public void Initiate(List<GameObject> attachTos, List<Photon.Realtime.Player> networkPlayers)
        {
            for (int i = 0; i < 2; i++)
            {
                IPlayerStats stats = Player.AddComponentTo(attachTos[i], networkPlayers[i]);
                stats.Money = Configs.GameManager_InitialMoney;
            }
            bot = new AI.Bot();
            turnManager.Init(bot);
        }
    }
}
