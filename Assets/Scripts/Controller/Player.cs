using System;
using Photon.Pun;
using UnityEngine;
using Utils;

namespace Controller
{
    public class Player : MonoBehaviourPunCallbacks
    {
        public EPlayerType EPlayerType =>
            PhotonNetwork.IsMasterClient ? EPlayerType.MasterPlayer : EPlayerType.ClientPlayer;

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
            InitRingPanel(EPlayerType);
        }

        public void InitInfoPanel()
        {
            if (EPlayerType == EPlayerType.MasterPlayer)
            {
                InfoPanel.PlayerPanel1.photonView.RPC("SetStatus", RpcTarget.AllBuffered, true);
                InfoPanel.PlayerPanel1.photonView.RPC("SetRingColor", RpcTarget.AllBuffered, EPlayerType);
            }
            else
            {
                InfoPanel.PlayerPanel2.photonView.RPC("SetStatus", RpcTarget.AllBuffered, true);
                InfoPanel.PlayerPanel2.photonView.RPC("SetRingColor", RpcTarget.AllBuffered, EPlayerType);
            }
        }

        public void InitRingPanel(EPlayerType ePlayerType)
        {
            RingPanel.InitRingColor(ePlayerType);
        }
    }
}