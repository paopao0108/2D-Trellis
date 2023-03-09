using System;
using System.Collections.Generic;
using Controller;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;


public enum ERingType
{
    LRing,
    MRing,
    SRing
}

public class Ring : MonoBehaviourPun, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private bool _isOnUI;
    private bool _disabled = false;
    private Vector3 _startMousePos, _newMousePos;
    private Ring _clone;
    private RectTransform _rt, _rtClone;
    private Transform _zeroPoint;
    private GridPanel _gridPanel;
    public Ring RingPrefab;
    public Player player;

    [NonSerialized] public int CurRow, CurCol;

    public ERingType RingType =>
        _rt.tag switch
        {
            "L" => ERingType.LRing,
            "M" => ERingType.MRing,
            "S" => ERingType.SRing,
            _ => throw new ArgumentOutOfRangeException(_rt.tag)
        };

    private void Awake()
    {
        _rt = transform.GetComponent<RectTransform>();
        _zeroPoint = GameObject.Find("zeroPoint").transform;
        _gridPanel = GameObject.Find("GamePanel/GridPanel").GetComponent<GridPanel>();
    }

    private void Clone()
    {
        _clone = Instantiate(this, _rt.parent);
        _rtClone = _clone.GetComponent<RectTransform>();
        // if (_clone.photonView == null) _clone.gameObject.AddComponent<PhotonView>();
        // PhotonNetwork.AllocateViewID(_clone.photonView);
    }

    private void DeClone()
    {
        Destroy(_rtClone.gameObject);
        _clone = null;
        _rtClone = null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isOnUI = RectTransformUtility.RectangleContainsScreenPoint(_rt, Input.mousePosition);
        if (!_isOnUI || _disabled || !NetworkManager.isMyTurn()) return;

        Debug.Log("OnBeginDrag " + NetworkManager.isMyTurn());
        Clone();
        RectTransformUtility.ScreenPointToWorldPointInRectangle(_clone.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera, out _startMousePos);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isOnUI || _disabled || !NetworkManager.isMyTurn()) return;

        _rtClone = _clone.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToWorldPointInRectangle(_rtClone, eventData.position,
            eventData.pressEventCamera, out _newMousePos);
        _rtClone.position =
            _rt.position + new Vector3(_newMousePos.x - _startMousePos.x, _newMousePos.y - _startMousePos.y, 0);
        //Locate();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_isOnUI || _disabled || !NetworkManager.isMyTurn()) return;

        Debug.Log("OnEndDrag");
        if (_clone != null) _clone._disabled = true;

        Locate();
        HandleDragResult();
    }

    /// <summary>
    /// 计算ring的坐标位置
    /// </summary>
    private void Locate()
    {
        var posInPanel = new Vector3(_rtClone.position.x - _zeroPoint.position.x,
            _rtClone.position.y - _zeroPoint.position.y, 0);
        CurCol = Mathf.RoundToInt(posInPanel.x / Cell.CellSize);
        CurRow = Mathf.RoundToInt(posInPanel.y / Cell.CellSize);
    }

    private void HandleDragResult()
    {
        Debug.Log($"Handle: {CurRow},{CurCol} {GridPanel.Grids[CurCol][CurRow].Pos[RingType]}");
        if (CurRow <= GridPanel.row - 1 && CurRow >= 0 && CurCol <= GridPanel.row - 1 && CurCol >= 0)
        {
            if (GridPanel.Grids[CurRow][CurCol].Pos[RingType] == EPlayerType.None)
            {
                _gridPanel.photonView.RPC("SetPosition", RpcTarget.AllBuffered, RingType, CurRow, CurCol);

                Debug.LogError($"Set {CurRow},{CurCol} to ${NetworkManager.EPlayerTurn}");
                GridPanel.Grids[CurRow][CurCol].Pos[RingType] = NetworkManager.EPlayerTurn;

                RingPanel.Decrease(RingType);
                _disabled = !RingPanel.HasEnough(RingType);
                Debug.Log(RingType + " left " + RingPanel.Nums[RingType]);

                if (_disabled) SetTransparency(Constants.Vars.Transparency);

                Debug.Log("ringType: " + RingType);
                Debug.Log("playerTurn: " + NetworkManager.EPlayerTurn);

                // 判断输赢
                if (_gridPanel.Check(CurRow, CurCol, RingType))
                {
                    Debug.LogError("游戏结束! 胜利者为: " + NetworkManager.EPlayerTurn);
                    Debug.LogError("胜利位置为: " + CurRow + " " + CurCol);
                    _disabled = true;
                }

                // 轮到对方
                NetworkManager.Instance.photonView.RPC("ChangeTurn", RpcTarget.AllBuffered);
            }
        }

        DeClone();
    }


    public void SetColor(EPlayerType ePlayerType)
    {
        GetComponent<Image>().color = ePlayerType == EPlayerType.MasterPlayer
            ? Constants.Colors.MasterColor
            : Constants.Colors.ClientColor;
    }

    public void SetTransparency(float transparency)
    {
        var color = GetComponent<Image>().color;
        //Debug.Log("color" + color);
        color.a = transparency;
        GetComponent<Image>().color = color;
    }
}