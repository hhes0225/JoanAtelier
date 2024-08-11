using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;


//빈 슬롯이 존재합니다
//이미 제출된 그림입니다.
//결론 슬롯과 다른 그림의 근거 슬롯입니다.

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

    //그냥 스토리 진행상 활성화 비활성화 해야 하는 오브젝트들
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

    //엑셀 대사 파싱할 때 필요한 변수들
    int i = 0;
    int branchNum = 0;
    bool isFirst=false;

    //선택지 선택 시 필요한 변수들
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

        Debug.Log("Chapter2Manager awake 세팅 끝");
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
            
            if (submitObject[0].transform.childCount>0 && submitObject[1].transform.childCount > 0 && submitObject[2].transform.childCount > 0)
            {
                if (submitObject[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("첫번째 그림") && !submitFirstQuiz)
                {
                    GradePicture1Score();
                }
                else if (submitObject[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("두번째 그림") && !submitSecondQuiz)
                {
                    GradePicture2Score();
                }
                else
                {
                    Debug.Log("정답 제출 슬롯 error 혹은 이미 제출한 퀴즈 다시 제출");


                    if (submitObject[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("첫번째 그림") && submitFirstQuiz)
                    {
                        //첫번째 그림 제출했는데 또 제출하려고 함
                        usedObjects[7].transform.GetChild(1).GetComponent<Text>().text = "이미 제출한 그림입니다.";
                        usedObjects[7].SetActive(true);
                    }
                    else if (submitObject[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("두번째 그림") && submitSecondQuiz)
                    {
                        //두번째 그림 제출했는데 또 제출하려고 함
                        usedObjects[7].transform.GetChild(1).GetComponent<Text>().text = "이미 제출한 그림입니다.";
                        usedObjects[7].SetActive(true);
                    }
                    else
                    {
                        Debug.Log("정답 제출 슬롯 error or 슬롯 덜채워짐");
                    }
                }
            }

            else
            {
                Debug.Log("슬롯 안차있음");
                usedObjects[7].transform.GetChild(1).GetComponent<Text>().text = "빈 슬롯이 존재합니다.";
                usedObjects[7].SetActive(true);
            }
            
            //score1 = 0;//에러난다면 지울 것

            //하나만 제출된 상태라면(xor로 판별) 자식 슬롯 지우기
            if (submitFirstQuiz ^ submitSecondQuiz)
            {
                submitObject[0].GetComponent<SlotDropHandler>().DeleteSubmitSlot();
                submitObject[1].GetComponent<SlotDropHandler>().DeleteSubmitSlot();
                submitObject[2].GetComponent<SlotDropHandler>().DeleteSubmitSlot();
            }

            Debug.Log("submitFirst: " + submitFirstQuiz + ", submitSecond: " + submitSecondQuiz);
            
        });

        
        //종교마법 선택지 버튼1 onClickListener 등록
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
                Debug.Log("선택지 에러남");
            }
        });

        //버튼2 onClickListener 등록
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
                Debug.Log("선택지 에러남");
            }
        });
        

        Debug.Log("Chapter1Manager start 세팅 끝");

        StartCoroutine("StoryFlow");
    }

    #region 엑셀 파일에서 대사 가져오기
    void excelParsing()
    {
        while (dialogDB2.Entities[i].branch >= 0)
        {
            //Debug.Log("branch: " + dialogDB2.Entities[i].branch);

            //맨 처음에 리스트 생성
            if (!isFirst)
            {
                isFirst = true;

                tmpDialog = new NewDialog(0, new DialogForm(dialogDB2.Entities[i].name, dialogDB2.Entities[i].dialog));

                dialogList.Add(tmpDialog);
                //Debug.Log("첫 배열 추가");
            }
            //branch가 i보다 크면 i를 다음 branch로 넘기고 다음 dialogForm 배열 세팅 
            else if (dialogDB2.Entities[i].branch > branchNum)
            {
                branchNum++;

                tmpDialog = new NewDialog(0, new DialogForm(dialogDB2.Entities[i].name, dialogDB2.Entities[i].dialog));

                dialogList.Add(tmpDialog);
                //Debug.Log("중간 정산: " + dialogList[branchNum-1].dialogForm.Count);
                //Debug.Log("다음 branch element");
            }
            else
            {
                dialogList[branchNum].dialogForm.Add(new DialogForm(dialogDB2.Entities[i].name, dialogDB2.Entities[i].dialog));
                //Debug.Log("branch 변동 X, 대사 추가 "+branchNum);
            }

            i++;
        }
    }
    #endregion

    #region 그림1 결론 슬롯 채점 함수
    void GradePicture1Score()
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
            Debug.Log("그림 1 정답 채점");
            Debug.Log("submitFirstQuiz: " + submitFirstQuiz);

            foreach (GameObject i in submitObject)
            {
                Debug.Log("슬롯 순회 내용: " + i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);

                if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("반짝"))
                {
                    //Debug.Log(i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                    Debug.Log("근거 슬롯 맞춤2");
                    score1 += 25;
                    continue;
                }


                #region 그림 1 채점영역
                if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("진품"))
                {
                    Debug.Log("결론 슬롯 맞춤");
                    conclusionCorrect = true;
                    continue;
                }
                else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("남성형"))
                {
                    //Debug.Log(i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                    Debug.Log("근거 슬롯 맞춤1");
                    score1 += 25;
                    continue;
                }
                else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("반짝"))
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

        //만약에 같은 정답인 슬롯 2번 넣는 꼼수를 부릴 경우 하나 틀림 처리
        if (score1 == 100 && submitObject[1].transform.GetChild(0).GetChild(0).GetComponent<Text>().text == submitObject[2].transform.GetChild(0).GetChild(0).GetComponent<Text>().text)
        {
            score1 -= 25;
        }

        //감점 요소
        Debug.Log("틀린 drop 횟수: " + GameManager.wrongDragCount);
        //Debug.Log("총 틀린 drop횟수 / 3 * 10 : " + ((wrongDragCount/3)*10));

        score1 = score1 - (GameManager.wrongDragCount / 3) * 10;

        picture1Score = score1;
        Debug.Log("picture1 score: " + picture1Score);
        score1 = 0;

        //Debug.Log("최종 score: " + score1);
    }
    #endregion


    #region 그림 2 결론 슬롯 채점 함수
    void GradePicture2Score()
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

            Debug.Log("그림 2 정답 채점");

            foreach (GameObject i in submitObject)
            {
                Debug.Log("슬롯 순회 내용: " + i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                Debug.Log("submitFirstQuiz: " + submitFirstQuiz);

                #region 그림 2 채점영역
                if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("위작"))
                {
                    Debug.Log("결론 슬롯 맞춤");
                    conclusionCorrect = true;
                    continue;
                }
                else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("230"))
                {
                    //Debug.Log(i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                    Debug.Log("근거 슬롯 맞춤1");
                    score1 += 25;
                    continue;
                }
                else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("파엔티아"))
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

        //만약에 같은 정답인 슬롯 2번 넣는 꼼수를 부릴 경우 하나 틀림 처리
        if (score1 == 100 && submitObject[1].transform.GetChild(0).GetChild(0).GetComponent<Text>().text == submitObject[2].transform.GetChild(0).GetChild(0).GetComponent<Text>().text)
        {
            score1 -= 25;
        }

        //감점 요소
        Debug.Log("틀린 drop 횟수: " + GameManager.wrongDragCount);
        //Debug.Log("총 틀린 drop횟수 / 3 * 10 : " + ((wrongDragCount/3)*10));

        score1 = score1 - (GameManager.wrongDragCount / 3) * 10;

        picture2Score = score1;
        Debug.Log("picture2 score: " + picture2Score);
        score1 = 0;

        //Debug.Log("최종 score: " + score1);
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

            //그림 1 채점
            if (submitObject[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("첫번째 그림") && !submitFirstQuiz)
            {

                Debug.Log("그림 1 정답 채점");
                Debug.Log("submitFirstQuiz: " + submitFirstQuiz);


                foreach (GameObject i in submitObject)
                {
                    Debug.Log("슬롯 순회 내용: " + i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                  
                    #region 그림 1 채점영역
                    if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("진품"))
                    {
                        Debug.Log("결론 슬롯 맞춤");
                        conclusionCorrect = true;
                        continue;
                    }
                    else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("남성형"))
                    {
                        //Debug.Log(i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                        Debug.Log("근거 슬롯 맞춤1");
                        score1 += 25;
                        continue;
                    }
                    else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("반짝이는 질감"))
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
                    #endregion
                }
                submitFirstQuiz = true;

            }


            //그림 2 채점
            else if (submitObject[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("두번째 그림") && !submitSecondQuiz)
            {
                Debug.Log("그림 2 정답 채점");

                foreach (GameObject i in submitObject)
                {
                    Debug.Log("슬롯 순회 내용: " + i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                    Debug.Log("submitFirstQuiz: " + submitFirstQuiz);

                    #region 그림 2 채점영역
                    if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("위작"))
                    {
                        Debug.Log("결론 슬롯 맞춤");
                        conclusionCorrect = true;
                        continue;
                    }
                    else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("230"))
                    {
                        //Debug.Log(i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                        Debug.Log("근거 슬롯 맞춤1");
                        score1 += 25;
                        continue;
                    }
                    else if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("파엔티아"))
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
                    
                    #endregion
                }
                submitSecondQuiz = true;
            }
            //예외처리
            else
            {
                Debug.Log("정답 제출 슬롯 error 혹은 이미 제출한 퀴즈 다시 제출");
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

        //만약에 같은 정답인 슬롯 2번 넣는 꼼수를 부릴 경우 하나 틀림 처리
        if (score1 == 100 && submitObject[1].transform.GetChild(0).GetChild(0).GetComponent<Text>().text == submitObject[2].transform.GetChild(0).GetChild(0).GetComponent<Text>().text)
        {
            score1 -= 25;
        }

        //감점 요소
        Debug.Log("틀린 drop 횟수: " + GameManager.wrongDragCount);
        //Debug.Log("총 틀린 drop횟수 / 3 * 10 : " + ((wrongDragCount/3)*10));

        score1 = score1 - (GameManager.wrongDragCount / 3) * 10;

        foreach (GameObject i in submitObject) { 
            if (i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("첫번째 그림"))
            {
                picture1Score = score1;
                Debug.Log("picture1 score: " + picture1Score);
                score1 = 0;
                break;
            }
            else if(i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("두번째 그림")){
                picture2Score = score1;
                Debug.Log("picture2 score: " + picture2Score);
                score1 = 0;
                break;
            }
        }

            //Debug.Log("최종 score: " + score1);
        }
    #endregion



    IEnumerator StoryFlow()
    {
        //맨 처음에 페이드용 판넬
        usedObjects[0].SetActive(true);
        yield return new WaitForSeconds(1f);

        usedObjects[0].GetComponent<Image>().DOFade(0, 1);
        usedObjects[0].transform.GetChild(0).gameObject.GetComponent<Text>().DOFade(0, 1);

        //대사 시작
        dialogManager.StartDialog(dialogList[0]);
        yield return new WaitForSeconds(1f);

        usedObjects[0].SetActive(false);

        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        //뮐러 등장
        usedObjects[1].GetComponent<Image>().DOFade(1, 1);
        dialogManager.StartDialog(dialogList[1]);
        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        //그림 등장, 뮐러 왼쪽으로 이동
        usedObjects[1].transform.DOMoveX(GameObject.Find("Image_muller").transform.position.x - 370, 1);

        //게임 영역 블록하고 있던 판넬 비활성화
        usedObjects[2].SetActive(false);
        usedObjects[3].SetActive(false);

        //그림 탭, 그림 활성화
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

        //둘중 하나 제출했을 때 -> XOR
        yield return new WaitUntil(() => submitFirstQuiz ^ submitSecondQuiz);

        dialogManager.StartDialog(dialogList[9]);
        NewDialogManager.Instance.DisplayNextSentence();
        //클릭하지 않아도 대사 넘겨서 사용자의 불편함 감소
        //yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        Debug.Log("넘어가는가?: "+(submitFirstQuiz && submitSecondQuiz));

        yield return new WaitUntil(() => submitFirstQuiz&&submitSecondQuiz);
        Debug.Log("전체 제출 완료!");

        //퀴즈 제출 이후
        //그림 퀴즈 비활성화
        pictureChangeObject[0].GetComponent<Image>().DOFade(0, 1);
        pictureChangeObject[0].SetActive(false);
        pictureChangeObject[1].GetComponent<Image>().DOFade(0, 1);
        pictureChangeObject[1].SetActive(false);

        //그림 퇴장, 뮐러 오른쪽으로 이동
        usedObjects[1].transform.DOMoveX(GameObject.Find("Image_muller").transform.position.x + 370, 1);

        //게임 존 판넬로 막아버리기
        usedObjects[2].SetActive(true);

        yield return null;

        //제출 후 ~ 선택지 전 대사 출력
        dialogManager.StartDialog(dialogList[4]);
        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        //선택지 버튼 존 활성화
        usedObjects[6].SetActive(true);
        
        //버튼 누를 때까지 기다리기
        yield return new WaitUntil(() => (isMagicClicked || isReligionClicked));

        //선택지 버튼 숨김
        usedObjects[6].SetActive(false);

        //버튼 누르면 나오는 대사
        dialogManager.StartDialog(dialogList[5]);
        yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);

        //뮐러 퇴장
        usedObjects[1].GetComponent<Image>().DOFade(0, 1);
        yield return new WaitForSeconds(1f);
        usedObjects[1].SetActive(false);

        /*세이브파일에 저장하기*/
        DataController.Instance.gameData.isClear2 = true;
        DataController.Instance.gameData.stage2score = (picture1Score+picture2Score)/2; //두 퀴즈의 평균값

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
            Debug.Log("마법 종교 데이터 저장에 문제 발생");
        }

        //Debug.Log("Chapter2Scene의 점수는: " + score1);
        Debug.Log("Chapter2Scene의 점수 저장은 이렇게 됨: "+DataController.Instance.gameData.stage2score);
        DataController.Instance.SaveGameData();

        //페이드 전용 판넬 활성화
        fadeout.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        //엔딩 씬으로 이동
        //******** 스테이지 2 엔딩 씬 추가되면 여기 숫자 꼭 바꿀 것!!!!!!!!!
        fadeout.GetComponent<TransitionEffect>().FadeOut(5);
    }

    public void increase()
    {
        GameManager.wrongDragCount++;
        return;
    }

    public void showWrongSlotAreaNotice()
    {
        usedObjects[7].transform.GetChild(1).GetComponent<Text>().text = "결론 슬롯과 다른 그림의 근거 슬롯입니다.";
        usedObjects[7].SetActive(true);
    }

}
