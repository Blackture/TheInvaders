using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using CardBattle.RemoteConfig;
using System;
using CardBattle.Core;
using CardBattle.Display;
using CardBattle.Display.Panels;

namespace CardBattle.Networking
{
    public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
    {
        public GAMEMODE gamemode;
        public DIFFICULTY difficulty;

        private string nickname;
        private PanelManager panelManager;

        private Dictionary<object, bool> waitForProperties = new Dictionary<object, bool>();

        private void Start()
        {
            panelManager = MenuManager.Instance.panelManager;
            StartCoroutine(ReadySetup());
            DontDestroyOnLoad(this);
        }

        private IEnumerator ReadySetup()
        {
            yield return new WaitUntil(() => PhotonNetwork.IsConnectedAndReady && PhotonNetwork.InLobby);
            SetLobbyMenu(true);
        }

        private void SetLobbyMenu(bool active)
        {
            panelManager.GetPanel<JoinRoomPanel>("JoinRoomPanel").Set(active);
            panelManager.GetPanel<CreateRoomPanel>("CreateRoomPanel").Set(active);
        }

        public void CreateRoom()
        {
            SetLobbyMenu(false);
            string _inputNickname = panelManager.GetPanel<CreateRoomPanel>("CreateRoomPanel").nameInput.text;
            string _inputRoomName = panelManager.GetPanel<CreateRoomPanel>("CreateRoomPanel").roomNameInput.text;
            nickname = (_inputNickname == "") ? "Player 0" : _inputNickname;
            PhotonNetwork.CreateRoom(_inputRoomName);
        }

        public void JoinRoom()
        {
            SetLobbyMenu(false);
            string _inputNickname = panelManager.GetPanel<JoinRoomPanel>("JoinRoomPanel").nameInput.text;
            string _inputRoomName = panelManager.GetPanel<JoinRoomPanel>("JoinRoomPanel").roomNameInput.text;
            nickname = (_inputNickname == "") ? "Player 1" : _inputNickname;
            PhotonNetwork.JoinRoom(_inputRoomName);
            Debug.Log("Joining");

            //32758
            //Game does not exist
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);
            Debug.Log(returnCode + "\n" + message);
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            ExitGames.Client.Photon.Hashtable ht = PhotonNetwork.LocalPlayer.CustomProperties;
            ht.Add("Ready", false);
            PhotonNetwork.LocalPlayer.SetCustomProperties(ht);
            waitForProperties.Add("Ready", true);

            if (PhotonNetwork.IsMasterClient)
            {
                gamemode = panelManager.GetPanel<CreateRoomPanel>("CreateRoomPanel").gamemodeS.GetCurrent<GAMEMODE>();
                difficulty = panelManager.GetPanel<CreateRoomPanel>("CreateRoomPanel").difficultyS.GetCurrent<DIFFICULTY>();
                Debug.Log(gamemode + "\n" + difficulty);
            }

            StartCoroutine(WaitForPlayers());
        }

        private IEnumerator WaitForPlayers()
        {
            yield return new WaitUntil(() => !waitForProperties["Ready"]);

            GameObject.Find("Level Changer").GetComponent<LevelChanger>().FadeToLevel("WaitForPlayer");
            PhotonNetwork.LocalPlayer.NickName = nickname;
            PhotonNetwork.CurrentRoom.IsOpen = true;
            Debug.Log("Joined");

            if (!PhotonNetwork.InRoom) StopCoroutine(WaitForPlayers());
            yield return new WaitUntil(() => PhotonNetwork.CurrentRoom?.PlayerCount == 2);
            PhotonNetwork.CurrentRoom.IsOpen = false;

            ExitGames.Client.Photon.Hashtable ht = PhotonNetwork.LocalPlayer.CustomProperties;
            ht.Add("configVersion", Configs.Instance.ConfigVersion);
            PhotonNetwork.LocalPlayer.SetCustomProperties(ht);
            waitForProperties.Add("configVersion", true);
            yield return new WaitUntil(() => !waitForProperties["configVersion"]);

            yield return new WaitUntil(() => CheckForVersionProperty());

            if (!CheckConfigVersion()) FindObjectOfType<Quit>().ExitRoom();

            yield return new WaitUntil(() => CheckPlayersReady());
            ChangeLevel();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true if all players got something in "configVersion"</returns>
        private bool CheckForVersionProperty()
        {
            bool b = true;
            foreach (Player p in PhotonNetwork.CurrentRoom.Players.Values)
            {
                if (!p.CustomProperties.ContainsKey("configVersion") || p.CustomProperties["configVersion"] == null) b = false;
            }
            return b;
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
        {
            base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);

            List<object> keys = new List<object>();
            foreach (object s in targetPlayer.CustomProperties.Keys)
            {
                foreach (object key in waitForProperties.Keys)
                {
                    if (s == key) keys.Add(key);
                }
            }

            foreach (object key in keys)
            {
                waitForProperties[key] = false;
                Debug.Log(key.ToString());
            }
        }

        private void ChangeLevel()
        {
            ExitGames.Client.Photon.Hashtable ht = PhotonNetwork.LocalPlayer.CustomProperties;
            ht.Add("InGame", false);
            PhotonNetwork.LocalPlayer.SetCustomProperties(ht);

            PhotonNetwork.AutomaticallySyncScene = true;
            GameObject.Find("Level Changer Network").GetComponent<LevelChanger>().FadeToLevel("CardFight");
        }

        public static bool CheckPlayersReady()
        {
            bool res = true;
            foreach (Player p in PhotonNetwork.CurrentRoom?.Players?.Values)
            {
                if (p.CustomProperties["Ready"] != null)
                {
                    if (!(bool)p.CustomProperties?["Ready"]) res = false;
                }
            }
            return res;
        }

        /// <summary>
        /// Checks for the players' config version and returns a value based on the equality of the versions
        /// But somehow one int get nulled out though it shouldn't even be possible
        /// </summary>
        /// <returns>true if both players have the same config</returns>
        public static bool CheckConfigVersion()
        {
            bool res = true;
            List<int> versions = new List<int>();
            foreach (Player p in PhotonNetwork.CurrentRoom?.Players?.Values)
            {
                Debug.Log((p == null).ToString() + "\n" + (p.CustomProperties?["configVersion"] == null).ToString());
                versions.Add((int)p.CustomProperties?["configVersion"]);
            }

            if (versions.Count > 2 || versions.Count < 2 || versions[0] != versions[1]) res = false;

            return res;
        }

        public static bool CheckPlayersIngame()
        {
            bool res = true;
            foreach (Player p in PhotonNetwork.CurrentRoom?.Players?.Values)
            {
                if (p.CustomProperties["InGame"] != null)
                {
                    if (!(bool)p.CustomProperties?["InGame"]) res = false;
                }
            }
            return res;
        }
    }
}
