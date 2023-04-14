using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

namespace CardBattle.Core.Turns
{
    [RequireComponent(typeof(GameManager))]
    [RequireComponent(typeof(Networking.NetworkManager))]
    public class QuickMatch : MonoBehaviour
    {
        public void FinishTurn()
        {
            //Networking.NetworkManager.instance.
        }
    }
}
