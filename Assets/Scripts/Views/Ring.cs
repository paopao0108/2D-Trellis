using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

public class Ring : MonoBehaviourPun, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private bool _isOnUI;
    private bool _disabled = false;
    private Vector3 _startMousePos, _newMousePos;
    private int _num = 3;
    private Ring _clone;
    private RectTransform _rt, _rtClone;
    private Transform _zeroPoint;
    private GridPanel _gridPanel;
    private GameController _gameController;

    public Ring RingPrefab;
    public RingType RingType;
    [NonSerialized] public int curRow, curCol;
    public Player player;

    private void Awake()
    {
        _rt = transform.GetComponent<RectTransform>();
        _zeroPoint = GameObject.Find("zeroPoint").transform;
        _gridPanel = GameObject.Find("GamePanel/GridPanel").GetComponent<GridPanel>();
        _gameController = GameObject.Find("Canvas").GetComponent< GameController> ();
        Debug.Log("canvas:" + _gameController);
    }

    public void Clone()
    {
        _clone = Instantiate(this, _rt.parent);
        _rtClone = _clone.GetComponent<RectTransform>();
        // if (_clone.photonView == null) _clone.gameObject.AddComponent<PhotonView>();
        // PhotonNetwork.AllocateViewID(_clone.photonView);
    }

    public void DeClone()
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
        curRow = Mathf.RoundToInt(posInPanel.x / Cell.CellSize);
        curCol = Mathf.RoundToInt(posInPanel.y / Cell.CellSize);
    }

    private void HandleDragResult()
    {
        if (curRow <= GridPanel.row - 1 && curRow >= 0 && curCol <= GridPanel.row - 1 && curCol >= 0)
        {
            if (GridPanel.grids[curCol][curRow].Pos[_rt.tag] == "")
            {
                _gridPanel.photonView.RPC("SetPosition", RpcTarget.AllBuffered, RingType, curRow, curCol);
                _disabled = --_num <= 0; // 放置一个ring之后，数目减少
                if (_disabled) SetTransparency(Constants.Vars.transparency);
                NetworkManager.Instance.photonView.RPC("ChangeTurn", RpcTarget.AllBuffered);

                GridPanel.grids[curCol][curRow].Pos[_rt.tag] = NetworkManager.playerTurn.ToString(); // 将ring的类型存下

                Debug.Log("sizeType: " + _rt.tag);
                Debug.Log("playerTurn: " + NetworkManager.playerTurn.ToString());
                // 判断输赢
                if (Utils.Utils.IsSuccession(GridPanel.row, curRow, curCol, _rt.tag, GridPanel.grids)) 
                { 
                    Debug.Log("游戏结束！！");
                }

                // Test：无法传递给远程
                //SendMessageUpwards("SendGameOver");
                _gameController.photonView.RPC("SendGameOver", RpcTarget.AllBuffered);
            }
        }
        DeClone();
    }



    public void SetColor(PlayerType playerType)
    {
        GetComponent<Image>().color = playerType == PlayerType.MasterPlayer ? Constants.Colors.MasterColor : Constants.Colors.ClientColor;
    }

    public void SetTransparency(float transparency)
    {
        var color = GetComponent<Image>().color;
        //Debug.Log("color" + color);
        color.a = transparency;
        GetComponent<Image>().color = color;
    }
}