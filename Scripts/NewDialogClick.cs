using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewDialogClick : MonoBehaviour, IPointerDownHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        if (NewDialogManager.Instance.chatState == NewDialogManager.ChatState.Chat)
        {
            NewDialogManager.Instance.DisplayNextSentence();
            //만약 대화상태라면 다음 메세지로 이동.
        }
    }
}
