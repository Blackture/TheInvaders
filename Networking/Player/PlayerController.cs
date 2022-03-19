using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.AI;
using Photon.Pun;
using CardBattle.Core.Players;

namespace TheInvaders.Battlefield
{
    #region ...
    /// <summary>
    /// The cam and unit selection controller of the battlefields
    /// </summary>
    #endregion
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(CameraController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Path AI")]
        [SerializeField] private Material valid;
        [SerializeField] private Material invalid;

        private bool isOnTurn;
        private Photon.Realtime.Player p;
        private float timeStamp = 0;
        private Transform target;
        private GameObject unitController;
        private NavMeshPath path;
        private NavMeshPath path2;
        private List<GameObject> lines = new List<GameObject>();

        #region ...
        /// <summary>
        /// Is this player's turn?
        /// </summary>
        #endregion
        public bool IsOnTurn { get { return isOnTurn; } }
        #region ...
        /// <summary>
        /// The player who is using this camera
        /// </summary>
        #endregion
        public Photon.Realtime.Player P { get { return p; } }

        private void Start()
        {
            isOnTurn = true;
            target = transform;
            unitController = null;
            path = new NavMeshPath();
            path2 = new NavMeshPath();
        }

        void Update()
        {
        }
    }
}
