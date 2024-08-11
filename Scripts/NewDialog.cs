using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class NewDialog
{
    public int id = 0;
    //public DialogForm[] dialogForm;
    public List<DialogForm> dialogForm;
    public NewDialog(int id, DialogForm dialogForm)
    {
        this.id = id;
        this.dialogForm = new List<DialogForm>();
        this.dialogForm.Add(dialogForm);
    }
}

[Serializable]
public class DialogForm
{
    public string charName;//�ι� �̸�
    [TextArea(3, 10)]
    public string sentences;//���

    public DialogForm(string charName, string sentence)
    {
        this.charName = charName;
        this.sentences = sentence;
    }
}
