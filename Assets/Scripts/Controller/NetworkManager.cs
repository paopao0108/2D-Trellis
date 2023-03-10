using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Utils;
using Views;

namespace Controller
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        private static NetworkManager _instance;


        public static RoomOptions roomOptions;
        public static NetworkManager Instance => _instance;
        public GameObject playerPrefab;
        public static PlayerType playerTurn = PlayerType.MasterPlayer;

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {   
            PhotonNetwork.ConnectUsingSettings(); // 连接到大厅
        }

        private void Update()
        {
            if (isMyTurn()) TurnPanl.ShowTurn();
            else TurnPanl.HideTurn();

            //if (isMyTurn()) PlayerPanel.BlinkingRing();
            //else PlayerPanel.UnBlinkingRing();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();

            roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 2;

            if (PhotonNetwork.JoinOrCreateRoom("room01", roomOptions, TypedLobby.Default))
                Debug.Log("JoinOrCreateRoom");
        }
        
        // 有玩家进入
        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            Debug.Log("OnPlayerEnteredRoom");
        }


        [PunRPC]
        public void ChangeTurn()
        {
            playerTurn = (playerTurn == PlayerType.MasterPlayer) ? PlayerType.ClientPlayer : PlayerType.MasterPlayer;
            //Debug.LogError("ChangeTurn: now is " + playerTurn);
        }

        public static bool isMyTurn()
        {
            if (PhotonNetwork.IsMasterClient) return playerTurn == PlayerType.MasterPlayer;
            else return playerTurn == PlayerType.ClientPlayer;
        }

        public static bool IsReady()
        {
            return PhotonNetwork.PlayerList.Length == roomOptions.MaxPlayers;
        }


    }
}