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
    public string charName;//인물 이름
    [TextArea(3, 10)]
    public string sentences;//대사

    public DialogForm(string charName, string sentence)
    {
        this.charName = charName;
        this.sentences = sentence;
    }
}
