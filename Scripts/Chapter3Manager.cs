using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;


public class Chapter3Manager : MonoBehaviour
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

    //�׳� ���丮 ����� Ȱ��ȭ ��Ȱ��ȭ �ؾ� �ϴ� ������Ʈ��
    [SerializeField]
    private GameObject[] usedObjects;

    [SerializeField]
    private GameObject[] toolSlotObject;

    [SerializeField]
    private GameObject[] submitObject;

    [SerializeField]
    private DialogDB3 dialogDB3;

    //[SerializeField]
    public List<NewDialog> dialogList;

    private NewDialog tmpDialog;

    public NewDialogManager dialogManager;

    GameObject fadeout;
    GameObject dataController;

    //���� ��� �Ľ��� �� �ʿ��� ������
    int i = 0;
    int branchNum = 0;
    bool isFirst=false;

    //������ ���� �� �ʿ��� ������
    bool isMagicClicked;
    bool isReligionClicked;
    bool isMagicClicked2;
    bool isReligionClicked2;

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
        //character = GameObject.Find("Image_Character").gameObject;
        dialogManager = NewDialogManager.Instance;

        isMagicClicked = false;
        isReligionClicked = false;

        excelParsing();

        Debug.Log("Chapter3Manager awake ���� ��");
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

        GameObject.Find("Characters").transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
        {
            if (usedObjects[1].activeSelf)
            {
                usedObjects[1].GetComponent<Image>().DOFade(0, 1);
                usedObjects[1].SetActive(false);
                usedObjects[2].SetActive(true);
                usedObjects[2].GetComponent<Image>().DOFade(1, 1);

            }
            else if (usedObjects[2].activeSelf)
            {
                usedObjects[2].GetComponent<Image>().DOFade(0, 1);
                usedObjects[2].SetActive(false);
                usedObjects[1].SetActive(true);
                usedObjects[1].GetComponent<Image>().DOFade(1, 1);
            }
        });

        GameObject.Find("SubmitButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (submitObject[0].transform.childCount > 0 && submitObject[1].transform.childCount > 0 && submitObject[2].transform.childCount > 0)
                GradeScore();
            else
            {
                Debug.Log("���� ��������");
                usedObjects[8].transform.GetChild(1).GetComponent<Text>().text = "�� ������ �����մϴ�.";
                usedObjects[8].SetActive(true);
            }
            //score1 = 0;//�������ٸ� ���� ��
        });

        //��ư1 onClickListener ���
        usedObjects[7].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            if (usedObjects[7].transform.GetChild(0).tag == "MagicChoice")
            {
                isMagicClicked = true;
            }
            else if(usedObjects[7].transform.GetChild(0).tag == "ReligionChoice")
            {
                isReligionClicked = true;
            }
            else
            {
                Debug.Log("������ ������");
            }
        });

        //��ư2 onClickListener ���
        usedObjects[7].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            if (usedObjects[7].transform.GetChild(1).tag == "MagicChoice")
            {
                isMagicClicked = true;
            }
            else if (usedObjects[7].transform.GetChild(1).tag == "ReligionChoice")
            {
                isReligionClicked = true;
            }
            else
            {
                Debug.Log("������ ������");
            }
        });

        //������ 2 ��ư1 onClickListener ���
        usedObjects[9].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            if (usedObjects[7].transform.GetChild(0).tag == "MagicChoice")
            {
                isMagicClicked2 = true;
            }
            else if (usedObjects[7].transform.GetChild(0).tag == "ReligionChoice")
            {
                isReligionClicked2 = true;
            }
            else
            {
                Debug.Log("������ ������");
            }
        });

        //������ 2 ��ư2 onClickListener ���
        usedObjects[9].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            if (usedObjects[7].transform.GetChild(1).tag == "MagicChoice")
            {
                isMagicClicked2 = true;
            }
            else if (usedObjects[7].transform.GetChild(1).tag == "ReligionChoice")
            {
                isReligionClicked2 = true;
            }
            else
            {
                Debug.Log("������ ������");
            }
        });

        Debug.Log("Chapter1Manager start ���� ��");

        StartCoroutine("StoryFlow");
    }

    #region ���� ���Ͽ��� ��� ��������
    void excelParsing()
    {
        while (dialogDB3.Entities[i].branch >= 0)
        {
            //Debug.Log("branch: " + dialogDB3.Entities[i].branch);

            //�� ó���� ����Ʈ ����
            if (!isFirst)
            {
                isFirst = true;

                tmpDialog = new NewDialog(0, new DialogForm(dialogDB3.Entities[i].name, dialogDB3.Entities[i].dialog));

                dialogList.Add(tmpDialog);
                //Debug.Log("ù �迭 �߰�");
            }
            //branch�� i���� ũ�� i�� ���� branch�� �ѱ�� ���� dialogForm �迭 ���� 
            else if (dialogDB3.Entities[i].branch > branchNum)
            {
                branchNum++;

                tmpDialog = new NewDialog(0, new DialogForm(dialogDB3.Entities[i].name, dialogDB3.Entities[i].dialog));

                dialogList.Add(tmpDialog);
                //Debug.Log("�߰� ����: " + dialogList[branchNum-1].dialogForm.Count);
                //Debug.Log("���� branch element");
            }
            else
            {
                dialogList[branchNum].dialogForm.Add(new DialogForm(dialogDB3.Entities[i].name, dialogDB3.Entities[i].dialog));
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
            Debug.Log("���� ����");

            foreach (GameObject i in submitObject)
            {
                Debug.Log(i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                //Debug.Log("�� true�� �ȵɱ��: " + i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("�� ���� �ұ��"));
                if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("�� ���� �ұ��"))
                {
                    Debug.Log("�ٰ� ���� ����1");
                    score1 += 25;
                    continue;
                }
                    

                //Debug.Log(i.transform.GetChild(0).GetComponent<Text>().text);
                if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("������Ÿ��"))
                {
                    Debug.Log("��� ���� ����");
                    conclusionCorrect = true;
                    continue;
                }
                else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("�� ���� �ұ��"))
                {
                    //Debug.Log(i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                    Debug.Log("�ٰ� ���� ����1");
                    score1 += 25;
                    continue;
                }
                else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("�ΰ� ź��"))
                {
                    //Debug.Log(i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                    Debug.Log("�ٰ� ���� ����2");
                    score1 += 25;
                    continue;
                }
                else
                {
                    Debug.Log("���� �ش� �ȵ�");
                    continue;
                }
            }

            if (conclusionCorrect)
            {
                score1 += 50;
            }
            else
                score1 = 0;

            quizNum++;
            conclusionCorrect = false;
            Debug.Log("quiz num: " + quizNum);

            //���࿡ ���� ������ ���� 2�� �ִ� �ļ��� �θ� ��� �ϳ� Ʋ�� ó��
            if (score1==100 && submitObject[1].transform.GetChild(0).GetChild(0).GetComponent<Text>().text== submitObject[2].transform.GetChild(0).GetChild(0).GetComponent<Text>().text)
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



    IEnumerator StoryFlow()
    {
        usedObjects[0].SetActive(true);
        yield return new WaitForSeconds(1f);

        usedObjects[0].GetComponent<Image>().DOFade(0, 1);
        usedObjects[0].transform.GetChild(0).gameObject.GetComponent<Text>().DOFade(0, 1);

        dialogManager.StartDialog(dialogList[0]);
        yield return new WaitForSeconds(1f);

        usedObjects[0].SetActive(false);

        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        //��Ƴ� ����
        usedObjects[1].GetComponent<Image>().DOFade(1, 1);
        dialogManager.StartDialog(dialogList[1]);
        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        //��Ƴ� �������� �̵�, �ĸ��� ����
        Sequence characterFlow = DOTween.Sequence();

        characterFlow.Append(usedObjects[1].transform.DOMoveX(GameObject.Find("Image_diana").transform.position.x - 370, 1));
        characterFlow.Join(usedObjects[2].GetComponent<Image>().DOFade(1, 1));

        dialogManager.StartDialog(dialogList[2]);
        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        //�׸� Ȱ��ȭ �� ���� ����
        characterFlow.Join(usedObjects[2].GetComponent<Image>().DOFade(0, 1));
        yield return new WaitForSeconds(1f);
        characterFlow.Append(usedObjects[2].transform.DOMoveX(GameObject.Find("Image_farha").transform.position.x - 610, 1));

        usedObjects[2].SetActive(false);

        //���� ���� ����ϰ� �ִ� �ǳ� ��Ȱ��ȭ
        usedObjects[3].SetActive(false);
        usedObjects[4].SetActive(false);

        //�׸� ���� Ȱ��ȭ
        usedObjects[5].SetActive(true);
        usedObjects[5].GetComponent<Image>().DOFade(1, 1);

        yield return new WaitForSeconds(1f);

        //�ι� ü���� ��ư Ȱ��ȭ
        usedObjects[6].SetActive(true);
        usedObjects[6].GetComponent<Image>().DOFade(1, 1);

        //���� ��ư ������ �� ���� ���
        yield return new WaitUntil(() => quizNum > 0);

        //���� ���� ����
        //�׸� ���� ��Ȱ��ȭ
        usedObjects[5].GetComponent<Image>().DOFade(0, 1);
        usedObjects[5].SetActive(false);


        //�ι� ü���� ��ư ��Ȱ��ȭ
        usedObjects[6].GetComponent<Image>().DOFade(0, 1);
        usedObjects[6].SetActive(false);

        //ĳ���͵� ��ȭ ���� ����(�̹���)
        //�ϴ� �ĸ��� ���� ��ġ��
        if (usedObjects[1].activeSelf)//��Ƴ� Ȱ��ȭ�Ǿ� ������
        {
            usedObjects[2].transform.DOMoveX(GameObject.Find("Image_diana").transform.position.x + 610, 1);
            yield return new WaitForSeconds(1f);
            usedObjects[2].SetActive(true);
            usedObjects[2].GetComponent<Image>().DOFade(1, 1);
        }
        else if (usedObjects[2].activeSelf)//�ĸ��� Ȱ��ȭ�Ǿ� ������
        {
            usedObjects[1].GetComponent<Image>().DOFade(0, 1);
            yield return new WaitForSeconds(1f);
            usedObjects[1].SetActive(true);
            usedObjects[1].GetComponent<Image>().DOFade(1, 1);

            usedObjects[2].transform.DOMoveX(GameObject.Find("Image_diana").transform.position.x + 610, 1);
            //usedObjects[2].GetComponent<Image>().DOFade(1, 1);

        }
       

        //���� �� �ǳڷ� ���ƹ�����
        usedObjects[3].SetActive(true);

        yield return null;

        //���� �� ~ ������ �� ��� ���
        dialogManager.StartDialog(dialogList[9]);
        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);


        //������ ��ư �� Ȱ��ȭ
        usedObjects[7].SetActive(true);
        //usedObjects[7].transform.GetChild(0).GetComponent<Image>().DOF

        //��ư ���� ������ ��ٸ���
        yield return new WaitUntil(() => (isMagicClicked || isReligionClicked));

        usedObjects[7].SetActive(false);

        //��ư ������ ������ ���
        dialogManager.StartDialog(dialogList[10]);
        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        //��Ƴ� ����, �ĸ��� �߰�����
        usedObjects[1].GetComponent<Image>().DOFade(0, 1);
        yield return new WaitForSeconds(1f);
        usedObjects[1].SetActive(false);

        usedObjects[2].transform.DOMoveX(GameObject.Find("Image_farha").transform.position.x - 250, 1);
        yield return new WaitForSeconds(1f);

        //��Ƴ� ���� ���� ���(������ ������)
        dialogManager.StartDialog(dialogList[11]);
        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        //������ �� �ٽ� Ȱ��ȭ
        usedObjects[9].SetActive(true);


        //��ư ���������� ��ٸ��� 2
        yield return new WaitUntil(() => (isMagicClicked2 || isReligionClicked2));

        //��ư ������ ������ ���
        dialogManager.StartDialog(dialogList[12]);
        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);


        /*���̺����Ͽ� �����ϱ�*/
        DataController.Instance.gameData.isClear3 = true;
        DataController.Instance.gameData.stage3score=score1;

        if (isMagicClicked)
        {
            DataController.Instance.gameData.magic++;
        }
        else if (isReligionClicked)
        {
            DataController.Instance.gameData.religion++;
        }
        else
        {
            Debug.Log("������ 1 ���� ���� ������ ���忡 ���� �߻�");
        }

        if (isMagicClicked2)
        {
            DataController.Instance.gameData.magic++;
        }
        else if (isReligionClicked2)
        {
            DataController.Instance.gameData.religion++;
        }
        else
        {
            Debug.Log("������ 2 ���� ���� ������ ���忡 ���� �߻�");
        }

        //Debug.Log("Chapter3Scene�� ������: " + score1);
        //Debug.Log("Chapter3Scene�� ���� ������ �̷��� ��: "+DataController.Instance.gameData.stage2score);
        DataController.Instance.SaveGameData();

        //���̵� ���� �ǳ� Ȱ��ȭ
        fadeout.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        //���� ������ �̵�
        //******** �������� 2 ���� �� �߰��Ǹ� ���� ���� �� �ٲ� ��!!!!!!!!!
        fadeout.GetComponent<TransitionEffect>().FadeOut(7);

    }

    public void increase()
    {
        GameManager.wrongDragCount++;
        return;
    }
    

}
