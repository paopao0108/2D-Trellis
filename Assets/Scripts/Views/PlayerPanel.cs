using Controller;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;


public class PlayerPanel : MonoBehaviourPun
{
    private TextMeshProUGUI _uiName;

    private RawImage _uiStatus;

    //private SpriteRenderer _uiRingColor;
    private Image _uiRingColor;

    private void Awake()
    {
        _uiName = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        _uiStatus = transform.Find("Status").GetComponent<RawImage>();
        //_uiRingColor = transform.Find("Image").GetComponent<SpriteRenderer>();
        _uiRingColor = transform.Find("Image").GetComponent<Image>();
    }

    [PunRPC]
    public void SetStatus(bool status)
    {
        _uiStatus.color = status ? Color.green : Color.red;
    }

    [PunRPC]
    public void SetName(string name)
    {
        _uiName.text = name;
    }

    [PunRPC]
    public void SetRingColor(EPlayerType type)
    {
        _uiRingColor.color = type == EPlayerType.MasterPlayer
            ? Constants.Colors.MasterColor
            : Constants.Colors.ClientColor;
    }
}