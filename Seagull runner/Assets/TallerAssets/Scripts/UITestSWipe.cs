using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Taller;
using TMPro;

public class UITestSWipe : MonoBehaviour
{
    public TextMeshProUGUI direction;
    public TextMeshProUGUI screenPos;

    private void Start()
    {
        EventTouch.OnSwipeFinished += OnSwipeFinished;
    }

    private void OnSwipeFinished(TouchResult touchResult)
    {
        direction.SetText(touchResult.direction4Way.ToString());
        screenPos.SetText(touchResult.startScreenPoint + "  endp " + touchResult.endScreenPoint);
    }
}