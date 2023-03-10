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
        _gameController = GameObject.Find("Canvas").GetComponent<GameController>();
    }

    public void ResetRing()
    {
        this._disabled = false;
    }

    public void Clone()
    {
        _clone = Instantiate(this, _rt.parent);
        _rtClone = _clone.GetComponent<RectTransform>();
    }

    public void DeClone()
    {
        Destroy(_rtClone.gameObject);
        _clone = null;
        _rtClone = null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.LogError("准备就绪: " + NetworkManager.IsReady());
        _isOnUI = RectTransformUtility.RectangleContainsScreenPoint(_rt, Input.mousePosition);
        
        // 测试
        if (!_isOnUI || _disabled || !NetworkManager.isMyTurn() || !NetworkManager.IsReady()) return;
        //if (!_isOnUI || _disabled) return;

        Clone();
        RectTransformUtility.ScreenPointToWorldPointInRectangle(_clone.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera, out _startMousePos);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 测试
        if (!_isOnUI || _disabled || !NetworkManager.isMyTurn() || !NetworkManager.IsReady()) return;
        //if (!_isOnUI || _disabled) return;

        _rtClone = _clone.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToWorldPointInRectangle(_rtClone, eventData.position,
            eventData.pressEventCamera, out _newMousePos);
        _rtClone.position =
            _rt.position + new Vector3(_newMousePos.x - _startMousePos.x, _newMousePos.y - _startMousePos.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 测试
        if (!_isOnUI || _disabled || !NetworkManager.isMyTurn() || !NetworkManager.IsReady()) return;
        //if (!_isOnUI || _disabled) return;

        Debug.Log("OnEndDrag");
        if (_clone != null) _clone._disabled = true; // 克隆体默认不能再拖拽

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

        //curRow = Mathf.RoundToInt(posInPanel.x / Cell.CellSize);
        //curCol = Mathf.RoundToInt(posInPanel.y / Cell.CellSize);
        // 改用鼠标在面板中的位置
        var mouseInPanel = new Vector3(_newMousePos.x - _zeroPoint.position.x,
            _newMousePos.y - _zeroPoint.position.y, 0);
        curRow = Mathf.FloorToInt(mouseInPanel.x / Cell.CellSize);
        curCol = Mathf.FloorToInt(mouseInPanel.y / Cell.CellSize);

        // 位置测试
        //Debug.LogError("原点位置：" + _zeroPoint.position);

        //Debug.LogError("圆环在面板中位置：" + posInPanel);
        //Debug.LogError("圆环在面板中坐标：" + curRow + " " + curCol);

        //var mouseInPanel = new Vector3(_newMousePos.x - _zeroPoint.position.x,
            //_newMousePos.y - _zeroPoint.position.y, 0);
        Debug.LogError("鼠标位置：" + _newMousePos);
        Debug.LogError("鼠标在面板中位置：" + mouseInPanel);
        Debug.LogError("鼠标在面板中坐标：" + Mathf.Floor(mouseInPanel.x / Cell.CellSize) + " " +  Mathf.Floor(mouseInPanel.y / Cell.CellSize));
        Debug.LogError("鼠标在面板中坐标：" + Mathf.RoundToInt(mouseInPanel.x / Cell.CellSize) + " " + Mathf.RoundToInt(mouseInPanel.y / Cell.CellSize));
        //var posInPanel = new Vector3(_newMousePos.x - _zeroPoint.position.x,
        //    _newMousePos.y - _zeroPoint.position.y, 0);
    }

    private void HandleDragResult()
    {
        if (curRow <= GridPanel.row - 1 && curRow >= 0 && curCol <= GridPanel.row - 1 && curCol >= 0)
        {
            if (GridPanel.grids[curCol][curRow].Pos[_rt.tag] == "")
            {
                RingPanel.Instance.ringNums[_rt.tag].DecreaseNum();

                _gridPanel.photonView.RPC("SetPosition", RpcTarget.AllBuffered, RingType, curRow, curCol); // 注意：setposition会销毁克隆体

                // 音效播放
                _gameController.PlaySound(_gameController.dropSound);

                _disabled = RingPanel.Instance.ringNums[_rt.tag].num <= 0;
                if (_disabled) SetTransparency(Constants.Vars.transparency); // 若没有数量，则禁用（通过设置透明度来达到视觉效果）
                
                GridPanel.grids[curCol][curRow].Pos[_rt.tag] = NetworkManager.playerTurn.ToString(); // 将ring的类型存下

                //Debug.Log("sizeType: " + _rt.tag);
                //Debug.Log("playerTurn: " + NetworkManager.playerTurn.ToString());

                // 判断输赢
                if (Utils.Utils.IsSuccession(GridPanel.row, curRow, curCol, _rt.tag, GridPanel.grids)) 
                { 
                    Debug.Log("游戏结束！！");
                    GameController.winner = NetworkManager.playerTurn; // 根据此时的玩家来显示不同的游戏结束页面，结束时当前玩家为赢家
                    _gameController.photonView.RPC("SendGameOver", RpcTarget.AllBuffered, NetworkManager.playerTurn);
                }

                // 测试
                //_gameController.photonView.RPC("SendGameOver", RpcTarget.AllBuffered, PlayerType.MasterPlayer);

                NetworkManager.Instance.photonView.RPC("ChangeTurn", RpcTarget.AllBuffered);
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