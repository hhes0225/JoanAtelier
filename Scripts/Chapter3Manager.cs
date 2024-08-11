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

    //그냥 스토리 진행상 활성화 비활성화 해야 하는 오브젝트들
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

    //엑셀 대사 파싱할 때 필요한 변수들
    int i = 0;
    int branchNum = 0;
    bool isFirst=false;

    //선택지 선택 시 필요한 변수들
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

        Debug.Log("Chapter3Manager awake 세팅 끝");
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
                Debug.Log("슬롯 안차있음");
                usedObjects[8].transform.GetChild(1).GetComponent<Text>().text = "빈 슬롯이 존재합니다.";
                usedObjects[8].SetActive(true);
            }
            //score1 = 0;//에러난다면 지울 것
        });

        //버튼1 onClickListener 등록
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
                Debug.Log("선택지 에러남");
            }
        });

        //버튼2 onClickListener 등록
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
                Debug.Log("선택지 에러남");
            }
        });

        //선택지 2 버튼1 onClickListener 등록
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
                Debug.Log("선택지 에러남");
            }
        });

        //선택지 2 버튼2 onClickListener 등록
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
                Debug.Log("선택지 에러남");
            }
        });

        Debug.Log("Chapter1Manager start 세팅 끝");

        StartCoroutine("StoryFlow");
    }

    #region 엑셀 파일에서 대사 가져오기
    void excelParsing()
    {
        while (dialogDB3.Entities[i].branch >= 0)
        {
            //Debug.Log("branch: " + dialogDB3.Entities[i].branch);

            //맨 처음에 리스트 생성
            if (!isFirst)
            {
                isFirst = true;

                tmpDialog = new NewDialog(0, new DialogForm(dialogDB3.Entities[i].name, dialogDB3.Entities[i].dialog));

                dialogList.Add(tmpDialog);
                //Debug.Log("첫 배열 추가");
            }
            //branch가 i보다 크면 i를 다음 branch로 넘기고 다음 dialogForm 배열 세팅 
            else if (dialogDB3.Entities[i].branch > branchNum)
            {
                branchNum++;

                tmpDialog = new NewDialog(0, new DialogForm(dialogDB3.Entities[i].name, dialogDB3.Entities[i].dialog));

                dialogList.Add(tmpDialog);
                //Debug.Log("중간 정산: " + dialogList[branchNum-1].dialogForm.Count);
                //Debug.Log("다음 branch element");
            }
            else
            {
                dialogList[branchNum].dialogForm.Add(new DialogForm(dialogDB3.Entities[i].name, dialogDB3.Entities[i].dialog));
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
            Debug.Log("퍼즐 제출");

            foreach (GameObject i in submitObject)
            {
                Debug.Log(i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                //Debug.Log("왜 true가 안될까요: " + i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("세 개의 불기둥"));
                if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("세 개의 불기둥"))
                {
                    Debug.Log("근거 슬롯 맞춤1");
                    score1 += 25;
                    continue;
                }
                    

                //Debug.Log(i.transform.GetChild(0).GetComponent<Text>().text);
                if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("말리니타스"))
                {
                    Debug.Log("결론 슬롯 맞춤");
                    conclusionCorrect = true;
                    continue;
                }
                else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("세 개의 불기둥"))
                {
                    //Debug.Log(i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                    Debug.Log("근거 슬롯 맞춤1");
                    score1 += 25;
                    continue;
                }
                else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("인간 탄생"))
                {
                    //Debug.Log(i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                    Debug.Log("근거 슬롯 맞춤2");
                    score1 += 25;
                    continue;
                }
                else
                {
                    Debug.Log("정답 해당 안됨");
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

            //만약에 같은 정답인 슬롯 2번 넣는 꼼수를 부릴 경우 하나 틀림 처리
            if (score1==100 && submitObject[1].transform.GetChild(0).GetChild(0).GetComponent<Text>().text== submitObject[2].transform.GetChild(0).GetChild(0).GetComponent<Text>().text)
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

        //디아나 등장
        usedObjects[1].GetComponent<Image>().DOFade(1, 1);
        dialogManager.StartDialog(dialogList[1]);
        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        //디아나 왼쪽으로 이동, 파르하 등장
        Sequence characterFlow = DOTween.Sequence();

        characterFlow.Append(usedObjects[1].transform.DOMoveX(GameObject.Find("Image_diana").transform.position.x - 370, 1));
        characterFlow.Join(usedObjects[2].GetComponent<Image>().DOFade(1, 1));

        dialogManager.StartDialog(dialogList[2]);
        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        //그림 활성화 전 정리 세팅
        characterFlow.Join(usedObjects[2].GetComponent<Image>().DOFade(0, 1));
        yield return new WaitForSeconds(1f);
        characterFlow.Append(usedObjects[2].transform.DOMoveX(GameObject.Find("Image_farha").transform.position.x - 610, 1));

        usedObjects[2].SetActive(false);

        //게임 영역 블록하고 있던 판넬 비활성화
        usedObjects[3].SetActive(false);
        usedObjects[4].SetActive(false);

        //그림 퀴즈 활성화
        usedObjects[5].SetActive(true);
        usedObjects[5].GetComponent<Image>().DOFade(1, 1);

        yield return new WaitForSeconds(1f);

        //인물 체인지 버튼 활성화
        usedObjects[6].SetActive(true);
        usedObjects[6].GetComponent<Image>().DOFade(1, 1);

        //제출 버튼 눌렀을 때 까지 대기
        yield return new WaitUntil(() => quizNum > 0);

        //퀴즈 제출 이후
        //그림 퀴즈 비활성화
        usedObjects[5].GetComponent<Image>().DOFade(0, 1);
        usedObjects[5].SetActive(false);


        //인물 체인지 버튼 비활성화
        usedObjects[6].GetComponent<Image>().DOFade(0, 1);
        usedObjects[6].SetActive(false);

        //캐릭터들 대화 모드로 변경(이미지)
        //일단 파르하 원래 위치로
        if (usedObjects[1].activeSelf)//디아나 활성화되어 있으면
        {
            usedObjects[2].transform.DOMoveX(GameObject.Find("Image_diana").transform.position.x + 610, 1);
            yield return new WaitForSeconds(1f);
            usedObjects[2].SetActive(true);
            usedObjects[2].GetComponent<Image>().DOFade(1, 1);
        }
        else if (usedObjects[2].activeSelf)//파르하 활성화되어 있으면
        {
            usedObjects[1].GetComponent<Image>().DOFade(0, 1);
            yield return new WaitForSeconds(1f);
            usedObjects[1].SetActive(true);
            usedObjects[1].GetComponent<Image>().DOFade(1, 1);

            usedObjects[2].transform.DOMoveX(GameObject.Find("Image_diana").transform.position.x + 610, 1);
            //usedObjects[2].GetComponent<Image>().DOFade(1, 1);

        }
       

        //게임 존 판넬로 막아버리기
        usedObjects[3].SetActive(true);

        yield return null;

        //제출 후 ~ 선택지 전 대사 출력
        dialogManager.StartDialog(dialogList[9]);
        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);


        //선택지 버튼 존 활성화
        usedObjects[7].SetActive(true);
        //usedObjects[7].transform.GetChild(0).GetComponent<Image>().DOF

        //버튼 누를 때까지 기다리기
        yield return new WaitUntil(() => (isMagicClicked || isReligionClicked));

        usedObjects[7].SetActive(false);

        //버튼 누르면 나오는 대사
        dialogManager.StartDialog(dialogList[10]);
        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        //디아나 퇴장, 파르하 중간으로
        usedObjects[1].GetComponent<Image>().DOFade(0, 1);
        yield return new WaitForSeconds(1f);
        usedObjects[1].SetActive(false);

        usedObjects[2].transform.DOMoveX(GameObject.Find("Image_farha").transform.position.x - 250, 1);
        yield return new WaitForSeconds(1f);

        //디아나 퇴장 이후 대사(선택지 전까지)
        dialogManager.StartDialog(dialogList[11]);
        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        //선택지 존 다시 활성화
        usedObjects[9].SetActive(true);


        //버튼 누를때까지 기다리기 2
        yield return new WaitUntil(() => (isMagicClicked2 || isReligionClicked2));

        //버튼 누르면 나오는 대사
        dialogManager.StartDialog(dialogList[12]);
        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);


        /*세이브파일에 저장하기*/
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
            Debug.Log("선택지 1 마법 종교 데이터 저장에 문제 발생");
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
            Debug.Log("선택지 2 마법 종교 데이터 저장에 문제 발생");
        }

        //Debug.Log("Chapter3Scene의 점수는: " + score1);
        //Debug.Log("Chapter3Scene의 점수 저장은 이렇게 됨: "+DataController.Instance.gameData.stage2score);
        DataController.Instance.SaveGameData();

        //페이드 전용 판넬 활성화
        fadeout.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        //엔딩 씬으로 이동
        //******** 스테이지 2 엔딩 씬 추가되면 여기 숫자 꼭 바꿀 것!!!!!!!!!
        fadeout.GetComponent<TransitionEffect>().FadeOut(7);

    }

    public void increase()
    {
        GameManager.wrongDragCount++;
        return;
    }
    

}
