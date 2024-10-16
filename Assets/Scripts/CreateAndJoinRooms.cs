using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    private int minPlayersToStart = 3;  // Minimum number of players to start the game
    private int maxPlayersInRoom = 5;   // Maximum number of players in the room/lobby

    // Start is called before the first frame update
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)maxPlayersInRoom; // Set max players to 5
        PhotonNetwork.CreateRoom(createInput.text, roomOptions);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room: " + PhotonNetwork.CurrentRoom.Name);
        CheckPlayersInRoom();  // Check if the required number of players is reached
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("New player joined: " + newPlayer.NickName);
        CheckPlayersInRoom();  // Check again when a new player joins the room
    }

    private void CheckPlayersInRoom()
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        // Check if the minimum player count is reached
        if (playerCount >= minPlayersToStart)
        {
            Debug.Log("Enough players in the room. Loading game...");
            PhotonNetwork.LoadLevel("Game");  // Load the game scene when enough players have joined
        }
        else
        {
            Debug.Log("Waiting for more players. Current player count: " + playerCount);
        }
    }
}