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

        public Ring[] rings;

        private void Start()
        {
            //rings = GameObject.Find("RingPanel").gameObject.GetComponentsInChildren<Ring>();
            
        }

        private void Update()
        {
            if (!photonView.IsMine) return;

            //Debug.Log("设置的当前玩家： " + NetworkManager.playerTurn);
            //Debug.Log("获取当前玩家： " + this.PlayerType);
            //if (NetworkManager.playerTurn != this.PlayerType)
            //{
            //    RingPanel.DisableRing(RingPanel.Large);
            //}
            //else
            //{
            //    RingPanel.EnableRing(RingPanel.Large);
            //}
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