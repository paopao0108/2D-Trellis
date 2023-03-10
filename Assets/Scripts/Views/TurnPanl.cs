using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnPanl : MonoBehaviour
{
    private static GameObject _yourTurnText;
    private static GameObject _otherTurnText;
    private void Awake()
    {
        _yourTurnText = GameObject.Find("YourTurnText");
        _otherTurnText = GameObject.Find("OtherTurnText");
        //Debug.Log("turntext£º" + _yourTurnText + _otherTurnText);
    }

    public static void ShowTurn()
    {
        _yourTurnText.SetActive(true);
        _otherTurnText.SetActive(false);
    }
    public static void HideTurn()
    {
        _yourTurnText.SetActive(false);
        _otherTurnText.SetActive(true);
    }

}
