using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CardBattle.Core;
using System;
using System.Collections;
using System.IO;
using WebSocketSharp;
using UnityEngine.SceneManagement;
using Photon.Pun;
using CardBattle.Display;
using UnityEditor.XR;
using TMPro;

namespace CardBattle.Networking
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public Core.MatchType selectedMatchType;
        public TMP_Text countdownText;
        public LevelChanger levelChanger;
        public string lobbyName = "MyLobby";
        public GameObject canvas;

        private int minPlayers = 0;
        private int maxPlayers = 0;
        private string gameScene = "";
        private string matchType = "";
        private Coroutine waitForLobby;
        private Coroutine waitForPlayers;
        private RoomOptions roomOptions;
        private static NetworkManager networkManager;

        public void SetSelectedMatchType(string s)
        {
            switch(s)
            {
                case "Quick Match":
                    selectedMatchType = Core.MatchType.Quickmatch;
                    break;
                case "Normal Match":
                    selectedMatchType = Core.MatchType.Normal;
                    break;
            }
        }

        public void SetPlayersNickName(string s)
        {
            PhotonNetwork.LocalPlayer.NickName = s;
        }

        #region ConnectToMaster & Start
        void Start()
        {
            // Connect to the Photon Server
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = RemoteConfig.Configs.Instance.ConfigVersion.ToString();
            }

            if (SceneManager.GetActiveScene().name == "Lobby" && PhotonNetwork.IsConnectedAndReady)
            {
                if (networkManager != null)
                {
                    Destroy(networkManager.gameObject);
                }
                else
                {
                    networkManager = this;
                    DontDestroyOnLoad(this);
                }
                canvas.SetActive(true);

            }
        }

        public override void OnConnectedToMaster()
        {
            // Join the specified lobby when connected to the master server
            PhotonNetwork.JoinLobby(new TypedLobby(lobbyName, LobbyType.Default));
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            if (waitForLobby != null) StopCoroutine(waitForLobby);
            // Disable the join match button
            if (canvas != null) canvas.SetActive(false);
        }
        #endregion
        #region Join the lobby
        public override void OnJoinedLobby()
        {
            // Load the lobby scene when successfully joined the lobby
            levelChanger.FadeToLevel("Lobby");
        }

        #endregion
        #region Join & leave a match
        public void OnJoinMatchButtonPressed(TMP_InputField matchName)
        {
            // Start the matchmaking process
            JoinSpecificMatch(matchName.textComponent.text); 
        }

        public void OnJoinMatchButtonPressed()
        {
            // Start the matchmaking process
            StartMatchmaking();
        }

        void CreateRoom()
        {
            roomOptions = new RoomOptions();
        }

        public void JoinSpecificMatch(string matchName)
        {
            // Attempt to join the specified match
            PhotonNetwork.JoinRoom(matchName);
        }

        void StartMatchmaking()
        {
            // Set the number of players based on the selected match type
            switch (selectedMatchType)
            {
                case Core.MatchType.Quickmatch:
                    minPlayers = 2;
                    maxPlayers = 2;
                    gameScene = "QuickMatch";
                    matchType = "QuickMatch";
                    break;
                case Core.MatchType.Normal:
                    minPlayers = 2;
                    maxPlayers = 2;
                    gameScene = "NormalMatch";
                    matchType = "NormalMatch";
                    break;
            }

            roomOptions = new RoomOptions();
            roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable { { "minPlayers", minPlayers }, { "maxPlayers", maxPlayers }, { "matchType", matchType } };
            roomOptions.MaxPlayers = 0;

            // Join a room that meets the criteria, or create a new one if none are available
            PhotonNetwork.JoinRandomRoom(new ExitGames.Client.Photon.Hashtable { { "minPlayers", minPlayers }, { "maxPlayers", maxPlayers }, { "matchType", matchType } }, 0); 
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            // If no suitable rooms are available, create a new room
            PhotonNetwork.CreateRoom(null, roomOptions, null);
        }

        public override void OnJoinedRoom()
        {
            levelChanger.FadeToLevel("WaitForPlayers");
            // Start the matchmaking
            waitForPlayers = StartCoroutine(WaitForPlayers());
            StartCoroutine(WaitForScene());
        }

        IEnumerator WaitForScene()
        {
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "WaitForPlayers");
            levelChanger = GameObject.Find("Level Changer Network").GetComponent<LevelChanger>();
            GameObject.Find("Canvas/ExitToLobby").GetComponent<Button>().interactable = true;
        }

        IEnumerator WaitForPlayers()
        {
            while (PhotonNetwork.CurrentRoom.PlayerCount < minPlayers || PhotonNetwork.CurrentRoom.PlayerCount > maxPlayers || ((string)PhotonNetwork.CurrentRoom.CustomProperties["matchType"] == "NormalMatch" && PhotonNetwork.CurrentRoom.PlayerCount % 2 != 0))
            {
                // Wait for one second
                yield return new WaitForSeconds(1f);
            }

            // If the minimum player count is exceeded, start the countdown
            if (PhotonNetwork.CurrentRoom.PlayerCount >= minPlayers)
            {
                for (int i = 10; i > 0; i--)
                {
                    // Update the countdown UI
                    UpdateCountdownUI(i);

                    // Wait for one second
                    yield return new WaitForSeconds(1f);
                }
            }

            // Lock the room to prevent more players from joining
            PhotonNetwork.CurrentRoom.IsOpen = false;

            // Load the game scene
            levelChanger.FadeToLevel(gameScene);
        }

        void UpdateCountdownUI(int seconds)
        {
            // Update the countdown UI element
            countdownText.text = $"{seconds} seconds remaining";
        }

        // Call this function to allow the player to leave the room
        public void LeaveMatch()
        {
            // Leave the room
            if (waitForPlayers != null) StopCoroutine(waitForPlayers);
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.JoinLobby(new TypedLobby(lobbyName, LobbyType.Default));        
        }
        #endregion
    }
}
