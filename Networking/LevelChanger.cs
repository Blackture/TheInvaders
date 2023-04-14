using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.Events;

namespace CardBattle.Networking
{
    public class LevelChanger : MonoBehaviour
    {
        public Animator anim;

        private string levelToLoad;

        public void FadeToLevel(string levelName)
        {
            levelToLoad = levelName;
            anim.SetTrigger("FadeOut");
        }

        public void OnFadeComplete()
        {
            SceneManager.LoadScene(levelToLoad);
        }

        public void OnFadeCompleteNetwork()
        {
            PhotonNetwork.LoadLevel(levelToLoad);
        }
    }
}
