using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NewDialogManager : MonoBehaviour
{
    public enum ChatState
    {
        Normal,
        Chat
    }

    public ChatState chatState = ChatState.Normal;
    public Text charName;
    public Text ChatText;


    public SpriteRenderer charImage;


    Queue<DialogForm> sentencesQueue;
    DialogForm tempDialog;

    Sprite[] CharSprites;

    int dialogID;

    #region �̱���
    private static NewDialogManager _instance = null;

    public static NewDialogManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (NewDialogManager)FindObjectOfType(typeof(NewDialogManager));
                if (_instance == null)
                {
                    Debug.Log("Ȱ��ȭ�� �Ŵ��� ������Ʈ ����");
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null & _instance != this)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);
            AwakeAfter();
        }
    }
    #endregion

    private void AwakeAfter()
    {
        //sentences = new Queue<string>();
        sentencesQueue = new Queue<DialogForm>();
        CharSprites = Resources.LoadAll<Sprite>("characters");
    }

    //��ȭ�� ���� �Լ�
    public void StartDialog(NewDialog dialog)
    {
        chatState = ChatState.Chat;
        //��ȭ ���� ����
        

        dialogID = dialog.id;

        sentencesQueue.Clear();
        //���� ���� �ֱ� ���� �ִ� ���� ������ �� �����ؼ� ť ����


        foreach (DialogForm sentence in dialog.dialogForm)
        {
            sentencesQueue.Enqueue(sentence);

            //�޾ƿ� ���� ��� ť�� �ֱ�
        }

        DisplayNextSentence();
    }
    
    //��ȭ�� ����
    public void DisplayNextSentence()
    {
        //StartCoroutine(initText());
        //Count�� ť�� size(��ġ �迭�� length���� ����
        if (sentencesQueue.Count == 0)
        {
            //������ �����̸� ����(ť ���� 0�̸� ����)
            EndDialog();
            return;
        }

        tempDialog = sentencesQueue.Dequeue();
        charName.text = tempDialog.charName;

        #region ��ŵ
        /*
        //:�������� ���ø�, ť���� ���� ������ ��Ʈ�� �迭�� �Է�
        string[] sentence = sentences.Dequeue().Split(':');

        for(int i = 0; i < sentence.Length; i++)
        {
            //�ʱ� �̹��� None ����
            if (i == 0)
                charImage.sprite = null;

            switch (i)
            {
                case 1:
                    //��������Ʈ ���, -1:None, �� ��: ��������Ʈ ����
                    int idx = int.Parse(sentence[i]);
                    //charImage.sprite = (idx != -1 ? CharSprites[idx] : null);
                    break;
                //case 2:
                    //�Լ� ����
                    //if(chat)
            }
        }
        */
        #endregion

        StopAllCoroutines();
        StartCoroutine(TypeSentence(tempDialog.sentences));
    }

    IEnumerator initText()
    {
        ChatText.text = "";
        yield return null;
    }

    IEnumerator TypeSentence(string sentence)
    {
        //sentence.Dofade
        //ChatText.text = sentence;
        //ChatText.DOText(sentence, 1f);
        //yield return null;
        
        ChatText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            ChatText.text += letter;
            yield return null;
        }
        
    }

    void EndDialog()
    {
        //��ȭ���� ����(��ȭ�� X��)
        chatState = ChatState.Normal;
    }

}
