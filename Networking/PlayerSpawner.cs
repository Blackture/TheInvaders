using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviour
{
    // Dictionary to store references to the player objects that have been spawned
    Dictionary<int, GameObject> playerObjects = new Dictionary<int, GameObject>();

    // Reference to the prefab that should be instantiated for the player object
    public GameObject playerPrefab;

    void Start()
    {
        // Spawn player objects for all players in the room
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            SpawnPlayerObject(player);
        }

        // Destroy this game object
        Destroy(gameObject);
    }

    // Spawn a player object for the specified player
    void SpawnPlayerObject(Player player)
    {
        // Check if a player object has already been spawned for this player
        if (playerObjects.ContainsKey(player.ActorNumber))
        {
            // If a player object has already been spawned, retrieve the existing player object
            GameObject playerObject = playerObjects[player.ActorNumber];
        }
        else
        {
            // If a player object has not yet been spawned, create a new player object
            GameObject playerObject = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);

            // Set the player object as a child of the "Players" game object
            playerObject.transform.SetParent(GameObject.Find("Players").transform);

            // Store a reference to the player object in the dictionary
            playerObjects.Add(player.ActorNumber, playerObject);
        }
    }
}