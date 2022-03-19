using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

namespace CardBattle.Networking
{
    public class NetworkSceneManager : MonoBehaviour
    {
        public int currentIndex => SceneManager.GetActiveScene().buildIndex;
        public string currentName => SceneManager.GetActiveScene().name;
        public bool IsSceneLoaded => isSceneLoaded;

        private bool isSceneLoaded = false;

        private void Start()
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0 == SceneManager.GetSceneByName("WaitForPlayer"))
            {
                ExitGames.Client.Photon.Hashtable ht = PhotonNetwork.LocalPlayer.CustomProperties;
                ht["Ready"] = true;
                PhotonNetwork.LocalPlayer.SetCustomProperties(ht);
            }
            else if (arg0 == SceneManager.GetSceneByName("CardFight"))
            {
                ExitGames.Client.Photon.Hashtable ht = PhotonNetwork.LocalPlayer.CustomProperties;
                ht["InGame"] = true;
                PhotonNetwork.LocalPlayer.SetCustomProperties(ht);
            }
        }
    }
}
