using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PointBubble : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField]
    private GameObject popupBubble;



    public void OnPointerDown(PointerEventData eventData)
    {
        popupBubble.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        popupBubble.SetActive(false);
    }
}
