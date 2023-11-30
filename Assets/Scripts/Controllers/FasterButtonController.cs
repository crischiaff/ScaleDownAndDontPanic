using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FasterButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.Instance.IsJamStarted())
        {
            MusicManager.Instance.PlayClick();
            TimeManager.Instance.FasterSpeed();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (GameManager.Instance.IsJamStarted())
        {
            MusicManager.Instance.PlayClick();
            TimeManager.Instance.NormalSpeed();
        }
    }
}

