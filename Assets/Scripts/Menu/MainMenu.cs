﻿using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

    public class MainMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject findOpponentPanel = null;
        [SerializeField] private GameObject waitingStatusPanel = null;
        [SerializeField] private TextMeshProUGUI waitingStatusText = null;
        [SerializeField] private TextMeshProUGUI statusPlayersText = null;
        int playerCount ;

        bool isCreateRoom = false;
        private bool isConnecting = false;

        private const string GameVersion = "0.1";
        private const int MaxPlayersPerRoom = 2;

        private void Awake() => PhotonNetwork.AutomaticallySyncScene = true;
        private void Start() {
            
        }
        private void Update() {
            if(isCreateRoom){
                statusPlayersText.text = playerCount.ToString() + "/" + MaxPlayersPerRoom.ToString();
            }else{
                statusPlayersText.text = " ";
            }
        }

        public void FindOpponent()
        {
            isConnecting = true;

            findOpponentPanel.SetActive(false);
            waitingStatusPanel.SetActive(true);

            waitingStatusText.text = "Searching...";
            
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.GameVersion = GameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected To Master");

            if (isConnecting)
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            waitingStatusPanel.SetActive(false);
            findOpponentPanel.SetActive(true);

            Debug.Log($"Disconnected due to: {cause}");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("No clients are waiting for an opponent, creating a new room");

            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MaxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Client successfully joined a room");

            playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

            if(playerCount != MaxPlayersPerRoom)
            {
                string csqsq = playerCount.ToString();
                waitingStatusText.text = "Waiting For Opponent";
                isCreateRoom = true;

                Debug.Log("Client is waiting for an opponent");
            }
            else
            {
                waitingStatusText.text = "Opponent Found";
                Debug.Log("Match is ready to begin OnJoinenRoom " + playerCount);

            }
            
        }
        
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            if(PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayersPerRoom)
            {
                PhotonNetwork.CurrentRoom.IsOpen  = false;

                waitingStatusText.text = "Opponent Found";
                Debug.Log("Match is ready to begin OnPlayerEnteredRoom");

                PhotonNetwork.LoadLevel("Scene_Main");
            }
        }
    }