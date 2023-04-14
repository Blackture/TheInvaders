using Assets.Scripts.Networking;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace CardBattle.Networking.PlayerSync
{
    public class EndTurnButton : MonoBehaviour
    {
        // Reference to the MyGame script
        public GeneralMatchController game;

        void Start()
        {
            // Get the Button component of the game object
            Button button = GetComponent<Button>();

            // Add an OnClick event to the button
            button.onClick.AddListener(EndTurn);
        }

        // End the current player's turn
        void EndTurn()
        {
            // Send an RPC to the other players to end the current player's turn
            game.photonView.RPC("OnTurnEnded", RpcTarget.Others, game.CurrentPlayerId);

            // Start the next player's turn
            game.StartTurn();
        }
    }
}