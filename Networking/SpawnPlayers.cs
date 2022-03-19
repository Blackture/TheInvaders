using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace CardBattle.Networking
{
    public class SpawnPlayers : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(CreatePlayers());
        }

        private IEnumerator CreatePlayers()
        {
            Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["InGame"]);
            yield return new WaitUntil(() => CreateAndJoinRooms.CheckPlayersIngame());
            GameController.Instance.PlayerObjects.Add(PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity));
            GameController.Instance.Players.Add(PhotonNetwork.LocalPlayer);
            Destroy(GameObject.Find("Create And Join Rooms"));
        }
    }
}
