using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Photon.Pun;
using CardBattle.Core;

namespace CardBattle.Networking
{
    [RequireComponent(typeof(PhotonView))]
    [RequireComponent(typeof(GameManager))]
    [RequireComponent(typeof(NetworkManager))]
    public class GameController : MonoBehaviourPunCallbacks, IPunObservable
    {
        public static GameController Instance;
        public GameManager manager;

        public List<GameObject> PlayerObjects => playerObjects;
        public List<Photon.Realtime.Player> Players => players;

        private List<GameObject> playerObjects = new List<GameObject>();
        private List<Photon.Realtime.Player> players = new List<Photon.Realtime.Player>();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        public void Start()
        {
            manager = GetComponent<GameManager>();
            DontDestroyOnLoad(this);
            if (PhotonNetwork.IsMasterClient)
            {
                photonView.RPC("InitiateGame", RpcTarget.All);
            }
            gameObject.AddComponent<SpawnPlayers>();
            StartCoroutine(WaitForPlayersSpawned());
        }

        [PunRPC]
        public void InitiateGame()
        {
            manager.Initiate(GameObject.Find("Create And Join Rooms").GetComponent<CreateAndJoinRooms>().gamemode);
            manager.Initiate(GameObject.Find("Create And Join Rooms").GetComponent<CreateAndJoinRooms>().difficulty);
        }

        [PunRPC]
        public void InitiatePlayers()
        {
            manager.Initiate(playerObjects, players);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            
        }

        private IEnumerator WaitForPlayersSpawned()
        {
            yield return new WaitUntil(() => GameObject.Find("Create And Join Rooms") == null);
            photonView.RPC("InitiatePlayers", RpcTarget.All);
        }
    }
}
