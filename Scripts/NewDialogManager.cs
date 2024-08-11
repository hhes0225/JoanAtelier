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

    #region 싱글톤
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
                    Debug.Log("활성화된 매니저 오브젝트 없음");
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

    //대화문 실행 함수
    public void StartDialog(NewDialog dialog)
    {
        chatState = ChatState.Chat;
        //대화 모드로 변경
        

        dialogID = dialog.id;

        sentencesQueue.Clear();
        //문장 전부 넣기 전에 있는 문장 있으면 다 제거해서 큐 비우기


        foreach (DialogForm sentence in dialog.dialogForm)
        {
            sentencesQueue.Enqueue(sentence);

            //받아온 문장 모두 큐에 넣기
        }

        DisplayNextSentence();
    }
    
    //대화문 진행
    public void DisplayNextSentence()
    {
        //StartCoroutine(initText());
        //Count는 큐의 size(마치 배열의 length같은 개념
        if (sentencesQueue.Count == 0)
        {
            //마지막 문장이면 종료(큐 길이 0이면 종료)
            EndDialog();
            return;
        }

        tempDialog = sentencesQueue.Dequeue();
        charName.text = tempDialog.charName;

        #region 스킵
        /*
        //:기준으로 스플릿, 큐에서 문장 꺼내서 스트링 배열에 입력
        string[] sentence = sentences.Dequeue().Split(':');

        for(int i = 0; i < sentence.Length; i++)
        {
            //초기 이미지 None 설정
            if (i == 0)
                charImage.sprite = null;

            switch (i)
            {
                case 1:
                    //스프라이트 출력, -1:None, 그 외: 스프라이트 따라감
                    int idx = int.Parse(sentence[i]);
                    //charImage.sprite = (idx != -1 ? CharSprites[idx] : null);
                    break;
                //case 2:
                    //함수 실행
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
        //대화상태 변경(대화중 X로)
        chatState = ChatState.Normal;
    }

}
