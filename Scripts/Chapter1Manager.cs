using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using UnityEditor;


public class Chapter1Manager : MonoBehaviour
{
    GameObject pauseUI;
    GameObject picture1;
    GameObject character;
    int lastchild;
    bool conclusionCorrect;
    int score1;
    //public int wrongDragCount = 0;

    public static int quizNum=0;

    [SerializeField]
    private DialogSystem[] dialogSystem;

    [SerializeField]
    private GameObject[] tutorialPanel;

    [SerializeField]
    private GameObject[] toolSlotObject;

    [SerializeField]
    private GameObject[] submitObject;

    [SerializeField]
    private DialogDB dialogDB;

    //[SerializeField]
    public List<NewDialog> dialogList;

    private NewDialog tmpDialog;

    public NewDialogManager dialogManager;

    GameObject fadeout;
    GameObject dataController;

    int i = 0;
    int branchNum = 0;
    bool isFirst=false;
    bool isSameSlot = false;

    private void Awake()
    {
        fadeout = GameObject.Find("FadeObject");
        dataController = GameObject.Find("DataController");
        GameManager.SetResolution();
        GameManager.wrongDragCount = 0;
        quizNum = 0;
        lastchild = GameObject.FindGameObjectWithTag("StageManager").GetComponent<Transform>().childCount - 1;
        pauseUI = GameObject.FindGameObjectWithTag("StageManager").GetComponent<Transform>().GetChild(lastchild - 2).gameObject;
        picture1= GameObject.FindGameObjectWithTag("StageManager").GetComponent<Transform>().GetChild(lastchild - 3).gameObject;
        character = GameObject.Find("Image_Character").gameObject;
        dialogManager = NewDialogManager.Instance;

        excelParsing();

        Debug.Log("Chapter1Manager awake ���� ��");
    }

    void Start()
    {
        Debug.Log("stage1 ����");

        //���ư��� Ŭ�� ��
        GameObject.FindGameObjectWithTag("PauseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("�Ͻ����� ��ư Ŭ����");
            pauseUI.SetActive(true);
            if (pauseUI.activeSelf)
                Debug.Log("pauseUI Ȱ��ȭ true");
            else
            {
                Debug.Log("pauseUI Ȱ��ȭ false");
            }

        });

        GameObject.Find("SubmitButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (submitObject[0].transform.childCount > 0 && submitObject[1].transform.childCount > 0 && submitObject[2].transform.childCount > 0)
                GradeScore();
            else
            {
                Debug.Log("���� ��������");
                tutorialPanel[11].transform.GetChild(1).GetComponent<Text>().text = "�� ������ �����մϴ�.";
                tutorialPanel[11].SetActive(true);
            }
        });

        Debug.Log("Chapter1Manager start ���� ��");

        StartCoroutine("Tutorial");
    }

    #region ���� ���Ͽ��� ��� ��������
    void excelParsing()
    {
        while (dialogDB.Entities[i].branch >= 0)
        {
            //Debug.Log("branch: " + dialogDB.Entities[i].branch);
            //Debug.Log("script: " + dialogDB.Entities[i].dialog);

            //�� ó���� ����Ʈ ����
            if (!isFirst)
            {
                isFirst = true;

                tmpDialog = new NewDialog(0, new DialogForm(dialogDB.Entities[i].name, dialogDB.Entities[i].dialog));

                dialogList.Add(tmpDialog);
                //Debug.Log("ù �迭 �߰�");
            }
            //branch�� i���� ũ�� i�� ���� branch�� �ѱ�� ���� dialogForm �迭 ���� 
            else if (dialogDB.Entities[i].branch > branchNum)
            {
                //Debug.Log("dialogDB: " + dialogDB.Entities[i].branch + ",branchNum: " + branchNum);
                branchNum++;

                tmpDialog = new NewDialog(0, new DialogForm(dialogDB.Entities[i].name, dialogDB.Entities[i].dialog));

                dialogList.Add(tmpDialog);
                //Debug.Log("else if new branch �߰���: " + (branchNum-1) +", ���: "+tmpDialog.dialogForm[0].sentences);

                //Debug.Log("�߰� ����: " + dialogList[branchNum-1].dialogForm.Count);
                //Debug.Log("���� branch element");
            }
            else
            {
                dialogList[branchNum].dialogForm.Add(new DialogForm(dialogDB.Entities[i].name, dialogDB.Entities[i].dialog));
                //Debug.Log("else new branch �߰���: " + (branchNum) + ", ���: " + dialogList[branchNum].dialogForm[dialogList[branchNum].dialogForm.Count-1].sentences);
                //Debug.Log("branch ���� X, ��� �߰� "+branchNum);
            }

            i++;
        }
    }
    #endregion

    #region ��� ���� ä�� �Լ�
    void GradeScore()
    {
        int submit1childCnt = GameObject.Find("SubmitSlot").transform.childCount;
        int submit2childCnt = GameObject.Find("SubmitSlot1").transform.childCount;
        int submit3childCnt = GameObject.Find("SubmitSlot2").transform.childCount;

        if (submit1childCnt == 0 && submit2childCnt == 0 && submit3childCnt == 0)
        {
            Debug.Log("�ٽ� �����ϼ���");
        }
        else
        {
            if (quizNum == 0)
            {
                Debug.Log("ù��° ���� ����");

                foreach (GameObject i in submitObject)
                {
                    //Debug.Log(i.transform.GetChild(0).GetComponent<Text>().text);
                    if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("��ǰ"))
                    {
                        #if UNITY_EDITOR
                        Debug.Log("��� ���� ����");
                        #endif
                        conclusionCorrect = true;
                        continue;
                    }
                    else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("�񸮵�Ÿ��"))
                    {
#if UNITY_EDITOR
                        Debug.Log("�ٰ� ���� ����");
#endif
                        score1 += 25;
                        continue;
                    }
                    else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("�߶�Ʈ�ν�"))
                    {
#if UNITY_EDITOR
                        Debug.Log("�ٰ� ���� ����");
#endif
                        score1 += 25;
                    }
                    else
                    {
                        continue;
                    }
                }

            }
            else
            {
                Debug.Log("�ι�° ���� ����");

                foreach (GameObject i in submitObject)
                {
                    //Debug.Log(i.transform.GetChild(0).GetComponent<Text>().text);
                    if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("������"))
                    {
#if UNITY_EDITOR
                        Debug.Log("��� ���� ����");
#endif
                        conclusionCorrect = true;
                        continue;
                    }
                    else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("������"))
                    {
#if UNITY_EDITOR
                        Debug.Log("�ٰ� ���� ����");
#endif
                        score1 += 25;
                        continue;
                    }
                    else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("����"))
                    {
#if UNITY_EDITOR
                        Debug.Log("�ٰ� ���� ����");
#endif
                        score1 += 25;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            if (conclusionCorrect)
            {
                score1 += 50;
            }
            else
                score1 = 0;

            //�ʱ�ȭ
            quizNum++;
            conclusionCorrect = false;
            Debug.Log("quiz num: " + quizNum);

            //���࿡ ���� ������ ���� 2�� �ִ� �ļ��� �θ� ��� �ϳ� Ʋ�� ó��
            if (score1 == 100 && submitObject[1].transform.GetChild(0).GetChild(0).GetComponent<Text>().text == submitObject[2].transform.GetChild(0).GetChild(0).GetComponent<Text>().text)
            {
                score1 -= 25;
            }

            //���� ���
            Debug.Log("Ʋ�� drop Ƚ��: " + GameManager.wrongDragCount);
            //Debug.Log("�� Ʋ�� dropȽ�� / 3 * 10 : " + ((wrongDragCount/3)*10));

            score1 = score1 - (GameManager.wrongDragCount / 3) * 10;

            Debug.Log("���� score: " + score1);
        }
    }
    #endregion

    void initSlots()
    {
        //quiz 2�� �ʱ�ȭ �۾�
        //���� ���� �ʱ�ȭ
        Destroy(GameObject.Find("SubmitSlot").transform.GetChild(0).gameObject);
        Destroy(GameObject.Find("SubmitSlot1").transform.GetChild(0).gameObject);
        Destroy(GameObject.Find("SubmitSlot2").transform.GetChild(0).gameObject);

        //�߸��� ���� �巡���� Ƚ�� �ʱ�ȭ
        GameManager.wrongDragCount = 0;
        score1 = 0;
        conclusionCorrect = false;

        //���� ���� Ŭ���ϰ� �ִٸ� ���� �� Ȱ��ȭ ���·� �ʱ�ȭ
        toolSlotObject[0].SetActive(false);
        toolSlotObject[1].SetActive(true);

        toolSlotObject[3].GetComponent<Toggle>().isOn = true;

        //���� ��(���� �� ����) �ʱ�ȭ
        toolSlotObject[4].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "�����ڸ� �׸� �׸��̴�.";
        toolSlotObject[4].transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "�������ڸ� �׸� �׸��̴�.";

        for(int i = 1; i <= 5; i++)
        {
            toolSlotObject[4].transform.GetChild(i + 1).GetChild(0).GetComponent<Text>().text = "EMPTY" + i;
        }
    }

    void initBooks()
    {
        //å �̸� ������� ������ ����

    }

    void initPicture()
    {
        //�׸� ����
        //tutorialPanel[2].

        //�׸� ũ��� ��ġ ����

        //����Ʈ ��ġ ����

        //����Ʈ Ŭ�� �� ���� ���� ����
        Image changeBubble = GameObject.Find("Image_book2").transform.GetChild(0).GetComponent<Image>();
        changeBubble.sprite = Resources.Load<Sprite>("tools/�ƽ�Ʈ���̾��ǻ絵��_��ǳ��") as Sprite;
    }

    IEnumerator Tutorial()
    {
        //yield return StartCoroutine ���̵� �ƿ�
        tutorialPanel[3].SetActive(true);
        yield return new WaitForSeconds(1f);

        tutorialPanel[3].GetComponent<Image>().DOFade(0, 1);
        tutorialPanel[3].transform.GetChild(0).gameObject.GetComponent<Text>().DOFade(0, 1);

        dialogManager.StartDialog(dialogList[0]);
        yield return new WaitForSeconds(1f);

        tutorialPanel[3].SetActive(false);

        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        GameObject.Find("Image_Character").GetComponent<Image>().DOFade(1, 1);
        Debug.Log("���´� ����");
        //���´� ����

        dialogManager.StartDialog(dialogList[1]);

        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        dialogManager.StartDialog(dialogList[2]);

        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        //���� Ʃ�丮�� �ǳ� Ȱ��ȭ, ���� ��Ʈ block���״� ���� �ǳ� ��Ȱ��ȭ

        GameObject.Find("BlockGameTab").gameObject.SetActive(false);
        tutorialPanel[0].SetActive(true);
        Debug.Log("���� ��ư Ŭ���ϼ��� ����");

        yield return new WaitUntil(() => toolSlotObject[0].activeSelf == true);

        tutorialPanel[0].SetActive(false);
        dialogManager.StartDialog(dialogList[3]);

        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        //���� Ʃ�丮�� �ǳ� Ȱ��ȭ

        tutorialPanel[1].SetActive(true);
        Debug.Log("���� ��ư Ŭ���ϼ��� ����");

        yield return new WaitUntil(() => toolSlotObject[1].activeSelf == true);

        tutorialPanel[1].SetActive(false);

        //���� �� Ŭ�� ����
        dialogManager.StartDialog(dialogList[4]);

        yield return new WaitWhile(() => GameObject.Find("Image_Character").GetComponent<CharacterClickHandler>().teacherSaid);

        // ���´� Ŭ�� ����
        tutorialPanel[2].SetActive(true);
        Sequence gameStartFlow = DOTween.Sequence();

        gameStartFlow.Append(GameObject.Find("Image_Character").GetComponent<Image>().transform.DOMoveX(GameObject.Find("Image_Character").transform.position.x-370, 1));
        gameStartFlow.Join(tutorialPanel[2].GetComponent<Image>().DOFade(1, 1));

        GameObject.Find("BlockSubmitTab").SetActive(false);
        Debug.Log("ù ��° ���� Ȱ��ȭ");

        //���� ��ư ������ �� ���� ���
        yield return new WaitUntil(() => quizNum>0);

        

        if (GameManager.wrongDragCount != 0)
        {
            Debug.Log("Chapter1Manager: test�� �� �÷��� : " + GameManager.wrongDragCount);
        }

        //75�� �ʰ��� -> ���, �ٰ� ���� �� ��
        if (score1 == 100)
        {
            dialogManager.StartDialog(dialogList[6]);

            yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);
        }
        //50�� �ʰ��� -> ��� ��, �ٰ� �ּ� 1 ��
        else if (score1 > 50)
        {
            dialogManager.StartDialog(dialogList[7]);

            yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);
        }
        //50�� ���ϸ�->Ʋ��
        else
        {
            dialogManager.StartDialog(dialogList[8]);

            yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);
        }

        initSlots();

        Debug.Log("ù ��° ���� ���� ����");

        dialogManager.StartDialog(dialogList[9]);

        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        tutorialPanel[2].GetComponent<Image>().DOFade(0, 1);
        tutorialPanel[4].GetComponent<Image>().DOFade(0, 1);
        tutorialPanel[5].GetComponent<Image>().DOFade(0, 1);
        tutorialPanel[6].GetComponent<Image>().DOFade(0, 1);
        

        yield return new WaitForSeconds(1.5f);

        tutorialPanel[7].SetActive(true);
        tutorialPanel[2].SetActive(false);
        tutorialPanel[7].GetComponent<Image>().DOFade(1, 1);
        tutorialPanel[8].GetComponent<Image>().DOFade(1, 1.5f);
        tutorialPanel[9].GetComponent<Image>().DOFade(1, 1.5f);
        tutorialPanel[10].GetComponent<Image>().DOFade(1, 1.5f);

        Debug.Log("�� ��° ���� ����");
        initPicture();

        yield return new WaitUntil(() => quizNum > 1);

        //75�� �ʰ��� -> ���, �ٰ� ���� �� ��
        if (score1 == 100)
        {
            dialogManager.StartDialog(dialogList[10]);

            yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);
        }
        //50�� �ʰ��� -> ��� ��, �ٰ� �ּ� 1 ��
        else if (score1 > 50)
        {
            dialogManager.StartDialog(dialogList[11]);

            yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);
        }
        //50�� ���ϸ�->Ʋ��
        else
        {
            dialogManager.StartDialog(dialogList[12]);

            yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);
        }

        Debug.Log("�� ��° ���� ���� ����");

        dialogManager.StartDialog(dialogList[13]);

        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        fadeout.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        DataController.Instance.gameData.isClear1 = true;
        DataController.Instance.SaveGameData();
        //dataController.GetComponent<DataController>().SaveGameData();
        fadeout.GetComponent<TransitionEffect>().FadeOut(1);
        //�������� ȭ������ ����
    }

    public void increase()
    {
        GameManager.wrongDragCount++;
        return;
    }

    public void StartTutorialCoroutine()
    {
        StartCoroutine(Tutorial());
    }



    #region ���(������)
    /*
    IEnumerator FirstTutorialDialog()
    {
        //ù ��° ��ȭ��(branch1)
        yield return new WaitUntil(() => dialogSystem[0].UpdateDialog());

        //��� Ÿ�̹� ��ġ�� �ʱ� ���� �ð� ��
        yield return new WaitForSeconds(1.7f);

        //���� �� Ȱ��ȭ
        GameObject.Find("BlockGameTab").gameObject.SetActive(false);

        //�׸� ����
        character.transform.position = new Vector3(230, 724, 0);
        picture1.SetActive(true);

        yield return new WaitForSeconds(1.7f);

        Debug.Log("�� ��° Ȱ��ȭ");

        //�� ��° ��ȭ��(branch2)Ȱ��ȭ
        yield return new WaitUntil(() => dialogSystem[1].UpdateDialog());

        yield return new WaitForSeconds(1.7f);

        Debug.Log("�� ��° Ȱ��ȭ ��");

        tutorialPanel[0].SetActive(true);

        yield return new WaitForSeconds(1.7f);

        yield return new WaitWhile(() => questObject[0].activeSelf == true);

        tutorialPanel[0].SetActive(false);

        yield return new WaitForSeconds(1.7f);

        //�� ��° ��ȭ��(branch3)Ȱ��ȭ
        yield return new WaitUntil(() => dialogSystem[2].UpdateDialog());

        yield return new WaitForSeconds(1.7f);

    }
    */
    #endregion
}
