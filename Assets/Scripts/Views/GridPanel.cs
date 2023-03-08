using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class GridPanel : MonoBehaviourPun
{
    //private PlayerType playerType;
    //private NetworkManager _networkManager;

    public Player player;
    public Cell cell;
    public static List<List<Cell>> grids = new();
    public static int row = 3;
    public static int col = 3;

    //public Dictionary<RingType, Ring> RingPrefabs = new Dictionary<RingType, Ring> { };
    //public Ring LRingPrefab = (Ring)Resources.Load("Assets/Photon/PhotonUnityNetworking/Resources/Large.prefab");
    //public Ring MRingPrefab = (Ring)Resources.Load("Assets/Photon/PhotonUnityNetworking/Resources/Middle.prefab");
    //public Ring SRingPrefab = (Ring)Resources.Load("Assets/Photon/PhotonUnityNetworking/Resources/Small.prefab");

    public Ring LRingPrefab;
    public Ring MRingPrefab;
    public Ring SRingPrefab;

    private void Awake()
    {
        //_networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    private void Start()
    {
        var gridPanelSize = GetComponent<RectTransform>().rect.width;
        var gridSpacing = GetComponent<GridLayoutGroup>().spacing.x;
        float gridPanelPadding = GetComponent<GridLayoutGroup>().padding.left;
        Cell.CellSize = (gridPanelSize - (row - 1) * gridSpacing - 2 * gridPanelPadding) / row;
        GetComponent<GridLayoutGroup>().cellSize = new Vector2(Cell.CellSize, Cell.CellSize);
        Init();
        //playerType = player.PlayerType;
    }

    public void Init()
    {
        for (var i = 0; i < row; i++)
        {
            grids.Add(new List<Cell>());
            for (var j = 0; j < col; j++)
                grids[i].Add(Instantiate(cell, transform));
        }
    }

    [PunRPC]
    public void SetPosition(RingType ringType, int row, int col)
    {
        Debug.LogError("SetPosition: " + ringType + " " + row + " " + col + "  玩家： " + player);
        Ring clone; // 默认克隆large
        switch (ringType)
        {
            case RingType.Large:
                clone = Instantiate<Ring>(LRingPrefab, grids[col][row].transform, false);
                //Debug.Log(NetworkManager.playerTurn);
                clone.SetColor(NetworkManager.playerTurn); // 放置在网格中的预制体ring的颜色需要根据玩家确定
                clone.GetComponent<Ring>().SetTransparency(Constants.Vars.transparency);
                break;
            case RingType.Middle:
                clone = Instantiate<Ring>(MRingPrefab, grids[col][row].transform, false);
                //Debug.Log(NetworkManager.playerTurn);
                clone.SetColor(NetworkManager.playerTurn); // 放置在网格中的预制体ring的颜色需要根据玩家确定
                clone.GetComponent<Ring>().SetTransparency(Constants.Vars.transparency);
                break;
            case RingType.Small:
                clone = Instantiate<Ring>(SRingPrefab, grids[col][row].transform, false);
                //Debug.Log(NetworkManager.playerTurn);
                clone.SetColor(NetworkManager.playerTurn); // 放置在网格中的预制体ring的颜色需要根据玩家确定
                clone.GetComponent<Ring>().SetTransparency(Constants.Vars.transparency);
                break;
        }

        //_networkManager.photonView.RPC("ChangeTurn", RpcTarget.AllBuffered);
        //_networkManager.photonView.RPC("ChangeTurn", RpcTarget.All);
        NetworkManager.ChangeTurn();
    }
}