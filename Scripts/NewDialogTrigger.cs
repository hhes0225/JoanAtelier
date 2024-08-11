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

        //캐릭터 클릭 시 대화문 실행
    }

    void ActiveDialog()
    {
        NewDialogManager.Instance.StartDialog(dialog);
        if (isDisable)
            gameObject.SetActive(false);
    }
}
