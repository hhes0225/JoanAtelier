using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;


//�� ������ �����մϴ�
//�̹� ����� �׸��Դϴ�.
//��� ���԰� �ٸ� �׸��� �ٰ� �����Դϴ�.

public class Chapter2Manager : MonoBehaviour
{
    GameObject pauseUI;
    GameObject picture1;
    GameObject character;
    int lastchild;
    bool conclusionCorrect;
    int score1;
    int picture1Score;
    int picture2Score;

    public static int quizNum=0;
    bool submitFirstQuiz=false;
    bool submitSecondQuiz = false;

    [SerializeField]
    private DialogSystem[] dialogSystem;

    //�׳� ���丮 ����� Ȱ��ȭ ��Ȱ��ȭ �ؾ� �ϴ� ������Ʈ��
    [SerializeField]
    private GameObject[] usedObjects;

    [SerializeField]
    private GameObject[] toolSlotObject;

    [SerializeField]
    private GameObject[] pictureChangeObject;

    [SerializeField]
    private GameObject[] submitObject;

    [SerializeField]
    private DialogDB2 dialogDB2;

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

    private void Awake()
    {
        fadeout = GameObject.Find("FadeObject");
        dataController = GameObject.Find("DataController");
        GameManager.SetResolution();
        GameManager.wrongDragCount = 0;
        quizNum = 0;
        submitFirstQuiz = false;
        submitSecondQuiz = false;
        lastchild = GameObject.FindGameObjectWithTag("StageManager").GetComponent<Transform>().childCount - 1;
        pauseUI = GameObject.FindGameObjectWithTag("StageManager").GetComponent<Transform>().GetChild(lastchild - 2).gameObject;
        picture1= GameObject.FindGameObjectWithTag("StageManager").GetComponent<Transform>().GetChild(lastchild - 3).gameObject;
        //character = GameObject.Find("Image_Character").gameObject;
        dialogManager = NewDialogManager.Instance;

        isMagicClicked = false;
        isReligionClicked = false;

        excelParsing();

        Debug.Log("Chapter2Manager awake ���� ��");
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
            
            if (submitObject[0].transform.childCount>0 && submitObject[1].transform.childCount > 0 && submitObject[2].transform.childCount > 0)
            {
                if (submitObject[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("ù��° �׸�") && !submitFirstQuiz)
                {
                    GradePicture1Score();
                }
                else if (submitObject[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("�ι�° �׸�") && !submitSecondQuiz)
                {
                    GradePicture2Score();
                }
                else
                {
                    Debug.Log("���� ���� ���� error Ȥ�� �̹� ������ ���� �ٽ� ����");


                    if (submitObject[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("ù��° �׸�") && submitFirstQuiz)
                    {
                        //ù��° �׸� �����ߴµ� �� �����Ϸ��� ��
                        usedObjects[7].transform.GetChild(1).GetComponent<Text>().text = "�̹� ������ �׸��Դϴ�.";
                        usedObjects[7].SetActive(true);
                    }
                    else if (submitObject[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("�ι�° �׸�") && submitSecondQuiz)
                    {
                        //�ι�° �׸� �����ߴµ� �� �����Ϸ��� ��
                        usedObjects[7].transform.GetChild(1).GetComponent<Text>().text = "�̹� ������ �׸��Դϴ�.";
                        usedObjects[7].SetActive(true);
                    }
                    else
                    {
                        Debug.Log("���� ���� ���� error or ���� ��ä����");
                    }
                }
            }

            else
            {
                Debug.Log("���� ��������");
                usedObjects[7].transform.GetChild(1).GetComponent<Text>().text = "�� ������ �����մϴ�.";
                usedObjects[7].SetActive(true);
            }
            
            //score1 = 0;//�������ٸ� ���� ��

            //�ϳ��� ����� ���¶��(xor�� �Ǻ�) �ڽ� ���� �����
            if (submitFirstQuiz ^ submitSecondQuiz)
            {
                submitObject[0].GetComponent<SlotDropHandler>().DeleteSubmitSlot();
                submitObject[1].GetComponent<SlotDropHandler>().DeleteSubmitSlot();
                submitObject[2].GetComponent<SlotDropHandler>().DeleteSubmitSlot();
            }

            Debug.Log("submitFirst: " + submitFirstQuiz + ", submitSecond: " + submitSecondQuiz);
            
        });

        
        //�������� ������ ��ư1 onClickListener ���
        usedObjects[6].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            if (usedObjects[6].transform.GetChild(0).tag == "MagicChoice")
            {
                isMagicClicked = true;
            }
            else if(usedObjects[6].transform.GetChild(0).tag == "ReligionChoice")
            {
                isReligionClicked = true;
            }
            else
            {
                Debug.Log("������ ������");
            }
        });

        //��ư2 onClickListener ���
        usedObjects[6].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            if (usedObjects[6].transform.GetChild(1).tag == "MagicChoice")
            {
                isMagicClicked = true;
            }
            else if (usedObjects[6].transform.GetChild(1).tag == "ReligionChoice")
            {
                isReligionClicked = true;
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
        while (dialogDB2.Entities[i].branch >= 0)
        {
            //Debug.Log("branch: " + dialogDB2.Entities[i].branch);

            //�� ó���� ����Ʈ ����
            if (!isFirst)
            {
                isFirst = true;

                tmpDialog = new NewDialog(0, new DialogForm(dialogDB2.Entities[i].name, dialogDB2.Entities[i].dialog));

                dialogList.Add(tmpDialog);
                //Debug.Log("ù �迭 �߰�");
            }
            //branch�� i���� ũ�� i�� ���� branch�� �ѱ�� ���� dialogForm �迭 ���� 
            else if (dialogDB2.Entities[i].branch > branchNum)
            {
                branchNum++;

                tmpDialog = new NewDialog(0, new DialogForm(dialogDB2.Entities[i].name, dialogDB2.Entities[i].dialog));

                dialogList.Add(tmpDialog);
                //Debug.Log("�߰� ����: " + dialogList[branchNum-1].dialogForm.Count);
                //Debug.Log("���� branch element");
            }
            else
            {
                dialogList[branchNum].dialogForm.Add(new DialogForm(dialogDB2.Entities[i].name, dialogDB2.Entities[i].dialog));
                //Debug.Log("branch ���� X, ��� �߰� "+branchNum);
            }

            i++;
        }
    }
    #endregion

    #region �׸�1 ��� ���� ä�� �Լ�
    void GradePicture1Score()
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
            Debug.Log("�׸� 1 ���� ä��");
            Debug.Log("submitFirstQuiz: " + submitFirstQuiz);

            foreach (GameObject i in submitObject)
            {
                Debug.Log("���� ��ȸ ����: " + i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);

                if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("��¦"))
                {
                    //Debug.Log(i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                    Debug.Log("�ٰ� ���� ����2");
                    score1 += 25;
                    continue;
                }


                #region �׸� 1 ä������
                if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("��ǰ"))
                {
                    Debug.Log("��� ���� ����");
                    conclusionCorrect = true;
                    continue;
                }
                else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("������"))
                {
                    //Debug.Log(i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                    Debug.Log("�ٰ� ���� ����1");
                    score1 += 25;
                    continue;
                }
                else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("��¦"))
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
                #endregion
            }
            submitFirstQuiz = true;
        }

        if (conclusionCorrect)
        {
            score1 += 50;
        }
        else
            score1 = 0;

        //quizNum++;
        //conclusionCorrect = false;
        //Debug.Log("quiz num: " + quizNum);

        //���࿡ ���� ������ ���� 2�� �ִ� �ļ��� �θ� ��� �ϳ� Ʋ�� ó��
        if (score1 == 100 && submitObject[1].transform.GetChild(0).GetChild(0).GetComponent<Text>().text == submitObject[2].transform.GetChild(0).GetChild(0).GetComponent<Text>().text)
        {
            score1 -= 25;
        }

        //���� ���
        Debug.Log("Ʋ�� drop Ƚ��: " + GameManager.wrongDragCount);
        //Debug.Log("�� Ʋ�� dropȽ�� / 3 * 10 : " + ((wrongDragCount/3)*10));

        score1 = score1 - (GameManager.wrongDragCount / 3) * 10;

        picture1Score = score1;
        Debug.Log("picture1 score: " + picture1Score);
        score1 = 0;

        //Debug.Log("���� score: " + score1);
    }
    #endregion


    #region �׸� 2 ��� ���� ä�� �Լ�
    void GradePicture2Score()
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

            Debug.Log("�׸� 2 ���� ä��");

            foreach (GameObject i in submitObject)
            {
                Debug.Log("���� ��ȸ ����: " + i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                Debug.Log("submitFirstQuiz: " + submitFirstQuiz);

                #region �׸� 2 ä������
                if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("����"))
                {
                    Debug.Log("��� ���� ����");
                    conclusionCorrect = true;
                    continue;
                }
                else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("230"))
                {
                    //Debug.Log(i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                    Debug.Log("�ٰ� ���� ����1");
                    score1 += 25;
                    continue;
                }
                else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("�Ŀ�Ƽ��"))
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

                #endregion
            }
            submitSecondQuiz = true;

        }

        if (conclusionCorrect)
        {
            score1 += 50;
        }
        else
            score1 = 0;

        //quizNum++;
        //conclusionCorrect = false;
        //Debug.Log("quiz num: " + quizNum);

        //���࿡ ���� ������ ���� 2�� �ִ� �ļ��� �θ� ��� �ϳ� Ʋ�� ó��
        if (score1 == 100 && submitObject[1].transform.GetChild(0).GetChild(0).GetComponent<Text>().text == submitObject[2].transform.GetChild(0).GetChild(0).GetComponent<Text>().text)
        {
            score1 -= 25;
        }

        //���� ���
        Debug.Log("Ʋ�� drop Ƚ��: " + GameManager.wrongDragCount);
        //Debug.Log("�� Ʋ�� dropȽ�� / 3 * 10 : " + ((wrongDragCount/3)*10));

        score1 = score1 - (GameManager.wrongDragCount / 3) * 10;

        picture2Score = score1;
        Debug.Log("picture2 score: " + picture2Score);
        score1 = 0;

        //Debug.Log("���� score: " + score1);
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

            //�׸� 1 ä��
            if (submitObject[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("ù��° �׸�") && !submitFirstQuiz)
            {

                Debug.Log("�׸� 1 ���� ä��");
                Debug.Log("submitFirstQuiz: " + submitFirstQuiz);


                foreach (GameObject i in submitObject)
                {
                    Debug.Log("���� ��ȸ ����: " + i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                  
                    #region �׸� 1 ä������
                    if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("��ǰ"))
                    {
                        Debug.Log("��� ���� ����");
                        conclusionCorrect = true;
                        continue;
                    }
                    else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("������"))
                    {
                        //Debug.Log(i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                        Debug.Log("�ٰ� ���� ����1");
                        score1 += 25;
                        continue;
                    }
                    else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("��¦�̴� ����"))
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
                    #endregion
                }
                submitFirstQuiz = true;

            }


            //�׸� 2 ä��
            else if (submitObject[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("�ι�° �׸�") && !submitSecondQuiz)
            {
                Debug.Log("�׸� 2 ���� ä��");

                foreach (GameObject i in submitObject)
                {
                    Debug.Log("���� ��ȸ ����: " + i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                    Debug.Log("submitFirstQuiz: " + submitFirstQuiz);

                    #region �׸� 2 ä������
                    if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("����"))
                    {
                        Debug.Log("��� ���� ����");
                        conclusionCorrect = true;
                        continue;
                    }
                    else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("230"))
                    {
                        //Debug.Log(i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                        Debug.Log("�ٰ� ���� ����1");
                        score1 += 25;
                        continue;
                    }
                    else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("�Ŀ�Ƽ��"))
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
                    
                    #endregion
                }
                submitSecondQuiz = true;
            }
            //����ó��
            else
            {
                Debug.Log("���� ���� ���� error Ȥ�� �̹� ������ ���� �ٽ� ����");
            } 
        }

        if (conclusionCorrect)
        {
            score1 += 50;
        }
        else
            score1 = 0;

        //quizNum++;
        //conclusionCorrect = false;
        //Debug.Log("quiz num: " + quizNum);

        //���࿡ ���� ������ ���� 2�� �ִ� �ļ��� �θ� ��� �ϳ� Ʋ�� ó��
        if (score1 == 100 && submitObject[1].transform.GetChild(0).GetChild(0).GetComponent<Text>().text == submitObject[2].transform.GetChild(0).GetChild(0).GetComponent<Text>().text)
        {
            score1 -= 25;
        }

        //���� ���
        Debug.Log("Ʋ�� drop Ƚ��: " + GameManager.wrongDragCount);
        //Debug.Log("�� Ʋ�� dropȽ�� / 3 * 10 : " + ((wrongDragCount/3)*10));

        score1 = score1 - (GameManager.wrongDragCount / 3) * 10;

        foreach (GameObject i in submitObject) { 
            if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("ù��° �׸�"))
            {
                picture1Score = score1;
                Debug.Log("picture1 score: " + picture1Score);
                score1 = 0;
                break;
            }
            else if(i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("�ι�° �׸�")){
                picture2Score = score1;
                Debug.Log("picture2 score: " + picture2Score);
                score1 = 0;
                break;
            }
        }

            //Debug.Log("���� score: " + score1);
        }
    #endregion



    IEnumerator StoryFlow()
    {
        //�� ó���� ���̵�� �ǳ�
        usedObjects[0].SetActive(true);
        yield return new WaitForSeconds(1f);

        usedObjects[0].GetComponent<Image>().DOFade(0, 1);
        usedObjects[0].transform.GetChild(0).gameObject.GetComponent<Text>().DOFade(0, 1);

        //��� ����
        dialogManager.StartDialog(dialogList[0]);
        yield return new WaitForSeconds(1f);

        usedObjects[0].SetActive(false);

        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        //���� ����
        usedObjects[1].GetComponent<Image>().DOFade(1, 1);
        dialogManager.StartDialog(dialogList[1]);
        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        //�׸� ����, ���� �������� �̵�
        usedObjects[1].transform.DOMoveX(GameObject.Find("Image_muller").transform.position.x - 370, 1);

        //���� ���� ����ϰ� �ִ� �ǳ� ��Ȱ��ȭ
        usedObjects[2].SetActive(false);
        usedObjects[3].SetActive(false);

        //�׸� ��, �׸� Ȱ��ȭ
        pictureChangeObject[2].transform.GetChild(0).GetComponent<Image>().DOFade(0, 1);
        pictureChangeObject[2].transform.GetChild(0).GetChild(0).GetComponent<Image>().DOFade(0, 1);
        pictureChangeObject[3].transform.GetChild(0).GetComponent<Image>().DOFade(0, 1);
        pictureChangeObject[3].transform.GetChild(0).GetChild(0).GetComponent<Image>().DOFade(0, 1);

        pictureChangeObject[4].GetComponent<Image>().DOFade(0, 1);
        pictureChangeObject[5].GetComponent<Image>().DOFade(0, 1);

        yield return new WaitForSeconds(1f);

        pictureChangeObject[0].SetActive(true);
        pictureChangeObject[1].SetActive(true);


        pictureChangeObject[2].transform.GetChild(0).GetComponent<Image>().DOFade(1, 1);
        pictureChangeObject[2].transform.GetChild(0).GetChild(0).GetComponent<Image>().DOFade(1, 1);
        pictureChangeObject[3].transform.GetChild(0).GetComponent<Image>().DOFade(1, 1);
        pictureChangeObject[3].transform.GetChild(0).GetChild(0).GetComponent<Image>().DOFade(1, 1);

        pictureChangeObject[4].GetComponent<Image>().DOFade(1, 1);

        //���� �ϳ� �������� �� -> XOR
        yield return new WaitUntil(() => submitFirstQuiz ^ submitSecondQuiz);

        dialogManager.StartDialog(dialogList[9]);
        NewDialogManager.Instance.DisplayNextSentence();
        //Ŭ������ �ʾƵ� ��� �Ѱܼ� ������� ������ ����
        //yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        Debug.Log("�Ѿ�°�?: "+(submitFirstQuiz && submitSecondQuiz));

        yield return new WaitUntil(() => submitFirstQuiz&&submitSecondQuiz);
        Debug.Log("��ü ���� �Ϸ�!");

        //���� ���� ����
        //�׸� ���� ��Ȱ��ȭ
        pictureChangeObject[0].GetComponent<Image>().DOFade(0, 1);
        pictureChangeObject[0].SetActive(false);
        pictureChangeObject[1].GetComponent<Image>().DOFade(0, 1);
        pictureChangeObject[1].SetActive(false);

        //�׸� ����, ���� ���������� �̵�
        usedObjects[1].transform.DOMoveX(GameObject.Find("Image_muller").transform.position.x + 370, 1);

        //���� �� �ǳڷ� ���ƹ�����
        usedObjects[2].SetActive(true);

        yield return null;

        //���� �� ~ ������ �� ��� ���
        dialogManager.StartDialog(dialogList[4]);
        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        //������ ��ư �� Ȱ��ȭ
        usedObjects[6].SetActive(true);
        
        //��ư ���� ������ ��ٸ���
        yield return new WaitUntil(() => (isMagicClicked || isReligionClicked));

        //������ ��ư ����
        usedObjects[6].SetActive(false);

        //��ư ������ ������ ���
        dialogManager.StartDialog(dialogList[5]);
        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        //���� ����
        usedObjects[1].GetComponent<Image>().DOFade(0, 1);
        yield return new WaitForSeconds(1f);
        usedObjects[1].SetActive(false);

        /*���̺����Ͽ� �����ϱ�*/
        DataController.Instance.gameData.isClear2 = true;
        DataController.Instance.gameData.stage2score = (picture1Score+picture2Score)/2; //�� ������ ��հ�

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
            Debug.Log("���� ���� ������ ���忡 ���� �߻�");
        }

        //Debug.Log("Chapter2Scene�� ������: " + score1);
        Debug.Log("Chapter2Scene�� ���� ������ �̷��� ��: "+DataController.Instance.gameData.stage2score);
        DataController.Instance.SaveGameData();

        //���̵� ���� �ǳ� Ȱ��ȭ
        fadeout.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        //���� ������ �̵�
        //******** �������� 2 ���� �� �߰��Ǹ� ���� ���� �� �ٲ� ��!!!!!!!!!
        fadeout.GetComponent<TransitionEffect>().FadeOut(5);
    }

    public void increase()
    {
        GameManager.wrongDragCount++;
        return;
    }

    public void showWrongSlotAreaNotice()
    {
        usedObjects[7].transform.GetChild(1).GetComponent<Text>().text = "��� ���԰� �ٸ� �׸��� �ٰ� �����Դϴ�.";
        usedObjects[7].SetActive(true);
    }

}
