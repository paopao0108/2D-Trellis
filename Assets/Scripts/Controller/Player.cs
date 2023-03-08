using System;
using Photon.Pun;
using UnityEngine;
using Utils;

namespace Controller
{
    public class Player : MonoBehaviourPunCallbacks
    {
        public PlayerType PlayerType =>
            PhotonNetwork.IsMasterClient ? PlayerType.MasterPlayer : PlayerType.ClientPlayer;

        private bool turn; // 控制玩家轮流操作

        private void Update()
        {
            if (!photonView.IsMine) return;
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            var player = PhotonNetwork.Instantiate(gameObject.name, Vector3.zero, gameObject.transform.rotation);
            Debug.Log("Enter Room");
            InitInfoPanel();
            InitRingPanel();
        }

        public void InitInfoPanel()
        {
            if (PlayerType == PlayerType.MasterPlayer)
            {
                InfoPanel.PlayerPanel1.photonView.RPC("SetStatus", RpcTarget.AllBuffered, true);
                InfoPanel.PlayerPanel1.photonView.RPC("SetRingColor", RpcTarget.AllBuffered, PlayerType);
            }
            else
            {
                InfoPanel.PlayerPanel2.photonView.RPC("SetStatus", RpcTarget.AllBuffered, true);
                InfoPanel.PlayerPanel2.photonView.RPC("SetRingColor", RpcTarget.AllBuffered, PlayerType);
            }
        }


        public void InitRingPanel()
        {
            RingPanel.Large.photonView.RPC("SetColor", RpcTarget.AllBuffered, PlayerType);
            RingPanel.Middle.photonView.RPC("SetColor", RpcTarget.AllBuffered, PlayerType);
            RingPanel.Small.photonView.RPC("SetColor", RpcTarget.AllBuffered, PlayerType);
        }
    }
}