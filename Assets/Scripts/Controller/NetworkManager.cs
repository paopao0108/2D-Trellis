using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Utils;

namespace Controller
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public GameObject playerPrefab;
        public static PlayerType playerTurn = PlayerType.MasterPlayer; // ¿ØÖÆÍæ¼Ò
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

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            Debug.Log("OnPlayerEnteredRoom");
        }

        [PunRPC]
        public static void ChangeTurn()
        {
            playerTurn = (playerTurn == PlayerType.MasterPlayer) ? PlayerType.ClientPlayer : PlayerType.MasterPlayer;
        }
    }
}