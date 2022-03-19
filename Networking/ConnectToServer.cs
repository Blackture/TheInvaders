using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using CardBattle.RemoteConfig;

namespace CardBattle.Networking
{
    public class ConnectToServer : MonoBehaviourPunCallbacks
    {
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Connect());
        }

        private IEnumerator Connect()
        {
            yield return new WaitUntil(() => Configs.Instance.Fetched == true);
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnected();
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
            GameObject.Find("Level Changer").GetComponent<LevelChanger>().FadeToLevel("Lobby");
        }
    }
}
