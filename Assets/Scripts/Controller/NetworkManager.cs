using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Utils;

namespace Controller
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public GameObject playerPrefab;
        public static PlayerType playerTurn = PlayerType.MasterPlayer;
        private static NetworkManager _instance;
        
        public static NetworkManager Instance => _instance;
        
        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {   
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();

            var roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 2;

            if (PhotonNetwork.JoinOrCreateRoom("room01", roomOptions, TypedLobby.Default))
                Debug.Log("JoinOrCreateRoom");
        }
        
        // 有其他玩家进入
        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            Debug.Log("OnPlayerEnteredRoom");
        }

        [PunRPC]
        public void ChangeTurn()
        {
            playerTurn = (playerTurn == PlayerType.MasterPlayer) ? PlayerType.ClientPlayer : PlayerType.MasterPlayer;
            Debug.LogError("ChangeTurn: now is " + playerTurn);
        }

        public static bool isMyTurn()
        {
            if (PhotonNetwork.IsMasterClient) return playerTurn == PlayerType.MasterPlayer;
            else return playerTurn == PlayerType.ClientPlayer;
        }
    }
}