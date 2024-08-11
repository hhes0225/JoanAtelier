using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDialogTrigger : MonoBehaviour
{
    public NewDialog dialog;

    public bool isInteract = true;
    public bool isDisable = false;

    void Interact()
    {
        if (isInteract)
            ActiveDialog();

        //ĳ���� Ŭ�� �� ��ȭ�� ����
    }

    void ActiveDialog()
    {
        NewDialogManager.Instance.StartDialog(dialog);
        if (isDisable)
            gameObject.SetActive(false);
    }
}
