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

        Debug.Log("Chapter1Manager awake 세팅 끝");
    }

    void Start()
    {
        Debug.Log("stage1 시작");

        //돌아가기 클릭 시
        GameObject.FindGameObjectWithTag("PauseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("일시정지 버튼 클릭됨");
            pauseUI.SetActive(true);
            if (pauseUI.activeSelf)
                Debug.Log("pauseUI 활성화 true");
            else
            {
                Debug.Log("pauseUI 활성화 false");
            }

        });

        GameObject.Find("SubmitButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (submitObject[0].transform.childCount > 0 && submitObject[1].transform.childCount > 0 && submitObject[2].transform.childCount > 0)
                GradeScore();
            else
            {
                Debug.Log("슬롯 안차있음");
                tutorialPanel[11].transform.GetChild(1).GetComponent<Text>().text = "빈 슬롯이 존재합니다.";
                tutorialPanel[11].SetActive(true);
            }
        });

        Debug.Log("Chapter1Manager start 세팅 끝");

        StartCoroutine("Tutorial");
    }

    #region 엑셀 파일에서 대사 가져오기
    void excelParsing()
    {
        while (dialogDB.Entities[i].branch >= 0)
        {
            //Debug.Log("branch: " + dialogDB.Entities[i].branch);
            //Debug.Log("script: " + dialogDB.Entities[i].dialog);

            //맨 처음에 리스트 생성
            if (!isFirst)
            {
                isFirst = true;

                tmpDialog = new NewDialog(0, new DialogForm(dialogDB.Entities[i].name, dialogDB.Entities[i].dialog));

                dialogList.Add(tmpDialog);
                //Debug.Log("첫 배열 추가");
            }
            //branch가 i보다 크면 i를 다음 branch로 넘기고 다음 dialogForm 배열 세팅 
            else if (dialogDB.Entities[i].branch > branchNum)
            {
                //Debug.Log("dialogDB: " + dialogDB.Entities[i].branch + ",branchNum: " + branchNum);
                branchNum++;

                tmpDialog = new NewDialog(0, new DialogForm(dialogDB.Entities[i].name, dialogDB.Entities[i].dialog));

                dialogList.Add(tmpDialog);
                //Debug.Log("else if new branch 추가됨: " + (branchNum-1) +", 대사: "+tmpDialog.dialogForm[0].sentences);

                //Debug.Log("중간 정산: " + dialogList[branchNum-1].dialogForm.Count);
                //Debug.Log("다음 branch element");
            }
            else
            {
                dialogList[branchNum].dialogForm.Add(new DialogForm(dialogDB.Entities[i].name, dialogDB.Entities[i].dialog));
                //Debug.Log("else new branch 추가됨: " + (branchNum) + ", 대사: " + dialogList[branchNum].dialogForm[dialogList[branchNum].dialogForm.Count-1].sentences);
                //Debug.Log("branch 변동 X, 대사 추가 "+branchNum);
            }

            i++;
        }
    }
    #endregion

    #region 결론 슬롯 채점 함수
    void GradeScore()
    {
        int submit1childCnt = GameObject.Find("SubmitSlot").transform.childCount;
        int submit2childCnt = GameObject.Find("SubmitSlot1").transform.childCount;
        int submit3childCnt = GameObject.Find("SubmitSlot2").transform.childCount;

        if (submit1childCnt == 0 && submit2childCnt == 0 && submit3childCnt == 0)
        {
            Debug.Log("다시 제출하세요");
        }
        else
        {
            if (quizNum == 0)
            {
                Debug.Log("첫번째 퍼즐 제출");

                foreach (GameObject i in submitObject)
                {
                    //Debug.Log(i.transform.GetChild(0).GetComponent<Text>().text);
                    if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("가품"))
                    {
                        #if UNITY_EDITOR
                        Debug.Log("결론 슬롯 맞춤");
                        #endif
                        conclusionCorrect = true;
                        continue;
                    }
                    else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("비리디타늄"))
                    {
#if UNITY_EDITOR
                        Debug.Log("근거 슬롯 맞춤");
#endif
                        score1 += 25;
                        continue;
                    }
                    else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("발란트로스"))
                    {
#if UNITY_EDITOR
                        Debug.Log("근거 슬롯 맞춤");
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
                Debug.Log("두번째 퍼즐 제출");

                foreach (GameObject i in submitObject)
                {
                    //Debug.Log(i.transform.GetChild(0).GetComponent<Text>().text);
                    if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("성직자"))
                    {
#if UNITY_EDITOR
                        Debug.Log("결론 슬롯 맞춤");
#endif
                        conclusionCorrect = true;
                        continue;
                    }
                    else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("무생물"))
                    {
#if UNITY_EDITOR
                        Debug.Log("근거 슬롯 맞춤");
#endif
                        score1 += 25;
                        continue;
                    }
                    else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("계파"))
                    {
#if UNITY_EDITOR
                        Debug.Log("근거 슬롯 맞춤");
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

            //초기화
            quizNum++;
            conclusionCorrect = false;
            Debug.Log("quiz num: " + quizNum);

            //만약에 같은 정답인 슬롯 2번 넣는 꼼수를 부릴 경우 하나 틀림 처리
            if (score1 == 100 && submitObject[1].transform.GetChild(0).GetChild(0).GetComponent<Text>().text == submitObject[2].transform.GetChild(0).GetChild(0).GetComponent<Text>().text)
            {
                score1 -= 25;
            }

            //감점 요소
            Debug.Log("틀린 drop 횟수: " + GameManager.wrongDragCount);
            //Debug.Log("총 틀린 drop횟수 / 3 * 10 : " + ((wrongDragCount/3)*10));

            score1 = score1 - (GameManager.wrongDragCount / 3) * 10;

            Debug.Log("최종 score: " + score1);
        }
    }
    #endregion

    void initSlots()
    {
        //quiz 2전 초기화 작업
        //제출 슬롯 초기화
        Destroy(GameObject.Find("SubmitSlot").transform.GetChild(0).gameObject);
        Destroy(GameObject.Find("SubmitSlot1").transform.GetChild(0).gameObject);
        Destroy(GameObject.Find("SubmitSlot2").transform.GetChild(0).gameObject);

        //잘못된 곳에 드래그한 횟수 초기화
        GameManager.wrongDragCount = 0;
        score1 = 0;
        conclusionCorrect = false;

        //슬롯 탭을 클릭하고 있다면 도구 탭 활성화 상태로 초기화
        toolSlotObject[0].SetActive(false);
        toolSlotObject[1].SetActive(true);

        toolSlotObject[3].GetComponent<Toggle>().isOn = true;

        //슬롯 탭(제출 전 슬롯) 초기화
        toolSlotObject[4].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "성직자를 그린 그림이다.";
        toolSlotObject[4].transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "마도학자를 그린 그림이다.";

        for(int i = 1; i <= 5; i++)
        {
            toolSlotObject[4].transform.GetChild(i + 1).GetChild(0).GetComponent<Text>().text = "EMPTY" + i;
        }
    }

    void initBooks()
    {
        //책 이름 변경사항 있으면 변경

    }

    void initPicture()
    {
        //그림 변경
        //tutorialPanel[2].

        //그림 크기와 위치 변경

        //포인트 위치 변경

        //포인트 클릭 시 설명 버블 변경
        Image changeBubble = GameObject.Find("Image_book2").transform.GetChild(0).GetComponent<Image>();
        changeBubble.sprite = Resources.Load<Sprite>("tools/아스트라이아의사도들_말풍선") as Sprite;
    }

    IEnumerator Tutorial()
    {
        //yield return StartCoroutine 페이드 아웃
        tutorialPanel[3].SetActive(true);
        yield return new WaitForSeconds(1f);

        tutorialPanel[3].GetComponent<Image>().DOFade(0, 1);
        tutorialPanel[3].transform.GetChild(0).gameObject.GetComponent<Text>().DOFade(0, 1);

        dialogManager.StartDialog(dialogList[0]);
        yield return new WaitForSeconds(1f);

        tutorialPanel[3].SetActive(false);

        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        GameObject.Find("Image_Character").GetComponent<Image>().DOFade(1, 1);
        Debug.Log("스승님 등장");
        //스승님 등장

        dialogManager.StartDialog(dialogList[1]);

        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        dialogManager.StartDialog(dialogList[2]);

        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        //슬롯 튜토리얼 판넬 활성화, 슬롯 파트 block시켰던 투명 판넬 비활성화

        GameObject.Find("BlockGameTab").gameObject.SetActive(false);
        tutorialPanel[0].SetActive(true);
        Debug.Log("슬롯 버튼 클릭하세요 등장");

        yield return new WaitUntil(() => toolSlotObject[0].activeSelf == true);

        tutorialPanel[0].SetActive(false);
        dialogManager.StartDialog(dialogList[3]);

        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        //도구 튜토리얼 판넬 활성화

        tutorialPanel[1].SetActive(true);
        Debug.Log("도구 버튼 클릭하세요 등장");

        yield return new WaitUntil(() => toolSlotObject[1].activeSelf == true);

        tutorialPanel[1].SetActive(false);

        //도구 탭 클릭 이후
        dialogManager.StartDialog(dialogList[4]);

        yield return new WaitWhile(() => GameObject.Find("Image_Character").GetComponent<CharacterClickHandler>().teacherSaid);

        // 스승님 클릭 이후
        tutorialPanel[2].SetActive(true);
        Sequence gameStartFlow = DOTween.Sequence();

        gameStartFlow.Append(GameObject.Find("Image_Character").GetComponent<Image>().transform.DOMoveX(GameObject.Find("Image_Character").transform.position.x-370, 1));
        gameStartFlow.Join(tutorialPanel[2].GetComponent<Image>().DOFade(1, 1));

        GameObject.Find("BlockSubmitTab").SetActive(false);
        Debug.Log("첫 번째 퀴즈 활성화");

        //제출 버튼 눌렀을 때 까지 대기
        yield return new WaitUntil(() => quizNum>0);

        

        if (GameManager.wrongDragCount != 0)
        {
            Debug.Log("Chapter1Manager: test로 값 늘려봄 : " + GameManager.wrongDragCount);
        }

        //75점 초과면 -> 결론, 근거 전부 다 맞
        if (score1 == 100)
        {
            dialogManager.StartDialog(dialogList[6]);

            yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);
        }
        //50점 초과면 -> 결론 맞, 근거 최소 1 맞
        else if (score1 > 50)
        {
            dialogManager.StartDialog(dialogList[7]);

            yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);
        }
        //50점 이하면->틀려
        else
        {
            dialogManager.StartDialog(dialogList[8]);

            yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);
        }

        initSlots();

        Debug.Log("첫 번째 퀴즈 제출 이후");

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

        Debug.Log("두 번째 퀴즈 시작");
        initPicture();

        yield return new WaitUntil(() => quizNum > 1);

        //75점 초과면 -> 결론, 근거 전부 다 맞
        if (score1 == 100)
        {
            dialogManager.StartDialog(dialogList[10]);

            yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);
        }
        //50점 초과면 -> 결론 맞, 근거 최소 1 맞
        else if (score1 > 50)
        {
            dialogManager.StartDialog(dialogList[11]);

            yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);
        }
        //50점 이하면->틀려
        else
        {
            dialogManager.StartDialog(dialogList[12]);

            yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);
        }

        Debug.Log("두 번째 퀴즈 제출 이후");

        dialogManager.StartDialog(dialogList[13]);

        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        fadeout.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        DataController.Instance.gameData.isClear1 = true;
        DataController.Instance.SaveGameData();
        //dataController.GetComponent<DataController>().SaveGameData();
        fadeout.GetComponent<TransitionEffect>().FadeOut(1);
        //스테이지 화면으로 복귀
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



    #region 백업(실패한)
    /*
    IEnumerator FirstTutorialDialog()
    {
        //첫 번째 대화문(branch1)
        yield return new WaitUntil(() => dialogSystem[0].UpdateDialog());

        //대사 타이밍 겹치지 않기 위해 시간 둠
        yield return new WaitForSeconds(1.7f);

        //슬롯 탭 활성화
        GameObject.Find("BlockGameTab").gameObject.SetActive(false);

        //그림 세팅
        character.transform.position = new Vector3(230, 724, 0);
        picture1.SetActive(true);

        yield return new WaitForSeconds(1.7f);

        Debug.Log("두 번째 활성화");

        //두 번째 대화문(branch2)활성화
        yield return new WaitUntil(() => dialogSystem[1].UpdateDialog());

        yield return new WaitForSeconds(1.7f);

        Debug.Log("두 번째 활성화 끝");

        tutorialPanel[0].SetActive(true);

        yield return new WaitForSeconds(1.7f);

        yield return new WaitWhile(() => questObject[0].activeSelf == true);

        tutorialPanel[0].SetActive(false);

        yield return new WaitForSeconds(1.7f);

        //세 번째 대화문(branch3)활성화
        yield return new WaitUntil(() => dialogSystem[2].UpdateDialog());

        yield return new WaitForSeconds(1.7f);

    }
    */
    #endregion
}
