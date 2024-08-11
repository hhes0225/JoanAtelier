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
            //���� ��ȭ���¶�� ���� �޼����� �̵�.
        }
    }
}
