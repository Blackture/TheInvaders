using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Photon.Pun;

namespace Assets.Scripts.Networking
{
    public  class GeneralMatchController : MonoBehaviourPunCallbacks
    {
        // The time limit for each player's turn, in seconds
        public float turnTimeLimit = 60.0f;

        // The current turn number
        private int turn = 0;

        // The current player's ID
        private int currentPlayerId = 0;

        // The ID of the next player
        private int nextPlayerId = 1;

        // The remaining time for the current player's turn
        private float turnTimer = 0.0f;

        public int Turn => turn;
        public int CurrentPlayerId => currentPlayerId;

        void Update()
        {
            // Check if it's the current player's turn
            if (PhotonNetwork.LocalPlayer.ActorNumber == currentPlayerId)
            {
                // Decrement the turn timer
                turnTimer -= Time.deltaTime;

                // Check if the turn timer has expired
                if (turnTimer <= 0.0f)
                {
                    // End the current player's turn
                    EndTurn();
                }
            }
        }

        // Start the next player's turn
        public void StartTurn()
        {
            // Increment the turn counter
            turn++;

            // Set the current player to the next player
            currentPlayerId = nextPlayerId;

            // Set the next player to the next player in the room
            nextPlayerId = (nextPlayerId + 1) % PhotonNetwork.CurrentRoom.PlayerCount;

            // Reset the turn timer
            turnTimer = turnTimeLimit;
        }

        // End the current player's turn
        public void EndTurn()
        {
            // Send an RPC to the other players to end the current player's turn
            photonView.RPC("OnTurnEnded", RpcTarget.Others, currentPlayerId);

            // Start the next player's turn
            StartTurn();
        }
    }
}
