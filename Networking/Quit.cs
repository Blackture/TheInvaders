using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;

namespace CardBattle.Networking
{
    public class Quit : MonoBehaviourPunCallbacks
    {
        private bool quit = false;
        private bool backToLobby = false;

        public void ExitRoom()
        {
            StopAllCoroutines();
            if (PhotonNetwork.InRoom)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    Destroy(GameObject.Find("Create And Join Rooms"));
                }

                if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();

                if (PhotonNetwork.InLobby) PhotonNetwork.LeaveLobby();

                if (PhotonNetwork.IsConnected)
                {
                    backToLobby = true;
                    quit = false;
                    PhotonNetwork.Disconnect();
                }
            }
        }

        public void Exit()
        {
            StopAllCoroutines();
            if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();

            if (PhotonNetwork.InLobby) PhotonNetwork.LeaveLobby();

            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.Disconnect();
                quit = true;
            }
            else Application.Quit();
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            PhotonNetwork.LeaveLobby();
        }

        public override void OnLeftLobby()
        {
            base.OnLeftLobby();
            if (quit) Exit();
            if (backToLobby) ExitRoom();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            try
            {
                base.OnDisconnected(cause);
                if (quit)
                {
                    Application.Quit();
                }
                if (backToLobby)
                {
                    backToLobby = false;
                    quit = false;
                    PhotonNetwork.JoinLobby();
                    GameObject g = GameObject.Find("Level Changer") ?? GameObject.Find("Level Changer Network") ?? throw new System.Exception("Failed");
                    g.GetComponent<LevelChanger>().FadeToLevel("ConnectScene");
                }
            }
            catch { }
        }
    }
}
