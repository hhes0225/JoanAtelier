using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

//ToolDropHandler.cs 스크립트 개선 후 버전
public class PictureInteraction : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    private Image image;
    string objectName;

    string slotAddContents;
    [SerializeField]
    private GameObject scrollViewContent;
    private List<GameObject> evidenceSlots;
    Transform[] allChildren; //ScrollView의 Content에서 모든 자식 순회하기 위함

    public static bool isCoroutineCalled = false;

    GameObject pictureChange;


    void Awake()
    {
        isCoroutineCalled = false;
        image = transform.GetComponent<Image>();
        objectName = gameObject.name;
        allChildren = scrollViewContent.transform.GetComponentsInChildren<Transform>();
        evidenceSlots = new List<GameObject>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("objectName: " + gameObject.name+" / objectParentName"+gameObject.transform.parent.name);
        if(objectName !="Picture1"&& objectName != "Picture2" && objectName!="Picture")
            image.color = new Color(0f, 0f, 0f, 100f/255f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (objectName != "Picture1"&& objectName != "Picture2" && objectName != "Picture")
            image.color = new Color(0f, 0f, 0f, 0f);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("isCoroutineCalled 값은? " + isCoroutineCalled);

        //증거 슬롯 리스트 만들기
        //(instantiate로 복제된 애들이 Content에 대체되고 진짜들은 SubmitSlot에 가서 Destroy될 수도 있으므로 동적으로 변경)
        evidenceSlots.Clear();
        allChildren = scrollViewContent.transform.GetComponentsInChildren<Transform>();

        Debug.Log("contents 크기: "+allChildren.Length);

        foreach (Transform child in allChildren)
        {

            // Debug.Log("transform name: "+transform.name);
            // 자기 자신의 경우엔 무시 
            //if (child.name == transform.name)
            //    break;

            if (child.tag == "EvidenceSlot")
            {
                evidenceSlots.Add(child.gameObject);
                Debug.Log(child.name + " EvidenceSlot List에 추가됨");
            }
        }

        //스테이지에 따라서 다른 슬롯 drop 상호작용
        if (SceneManager.GetActiveScene().name == "Chapter1Scene")
            FirstStageDragSetting(eventData);
        else if (SceneManager.GetActiveScene().name == "Chapter2Scene") {
            pictureChange = SecondStagePicture();

            //Debug.Log("현재 활성화 그림 탭 : " + pictureChange.transform.GetChild(0).name);
            //Debug.Log("두번째 조건 bool 결과: " + (pictureChange.transform.GetChild(1).name == "Picture2" && pictureChange.transform.GetChild(1).gameObject.activeSelf));

            if (pictureChange.transform.GetChild(0).name == "Picture1" && pictureChange.transform.GetChild(0).gameObject.activeSelf)
                SecondStageDragSettingPicture1(eventData);
            else if (pictureChange.transform.GetChild(1).name == "Picture2" && pictureChange.transform.GetChild(1).gameObject.activeSelf) {
                SecondStageDragSettingPicture2(eventData);
            }
        }
        else if (SceneManager.GetActiveScene().name == "Chapter3Scene")
            ThridStageDragSetting(eventData);


        Debug.Log("isCoroutineCalled: " + PictureInteraction.isCoroutineCalled);

        #region 꼽주기 코루틴 백업
        /*
        //한번 감점 경고 받고 또 틀렸을 때에는 다시 isCoroutineTrue=false로 초기화
        if (GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter1Manager>().wrongDragCount > 3 && PictureInteraction.isCoroutineCalled)
        {
            PictureInteraction.isCoroutineCalled = false;
        }

        //wrongDragCount가 0 초과면서 모듈러 3이 0일 때(나머지가 0일 때->3의 배수) 삼진아웃
        Debug.Log("PictureInteraction에서.. wrongcount = " + GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().wrongDragCount);
        Debug.Log("PictureInteraction에서.. wrongcount%3 = " + GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().wrongDragCount % 3);
        if (GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().wrongDragCount > 0 && GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().wrongDragCount % 3 == 0 && !isCoroutineCalled) { 
            StartCoroutine(ThreeOut());
            Debug.Log("꼽주기 코루틴 호출됨..");
            Debug.Log("코루틴 후 isCoroutineCalled 값은? " + PictureInteraction.isCoroutineCalled);
        }
        */
        #endregion

        //한번 감점 경고 받고 또 틀렸을 때에는 다시 isCoroutineTrue=false로 초기화
        if (GameManager.wrongDragCount > 3 && PictureInteraction.isCoroutineCalled)
        {
            PictureInteraction.isCoroutineCalled = false;
        }

        //wrongDragCount가 0 초과면서 모듈러 3이 0일 때(나머지가 0일 때->3의 배수) 삼진아웃
        Debug.Log("PictureInteraction에서.. wrongcount = " + GameManager.wrongDragCount);
        Debug.Log("PictureInteraction에서.. wrongcount%3 = " + GameManager.wrongDragCount % 3);
        if (GameManager.wrongDragCount > 0 && GameManager.wrongDragCount % 3 == 0 && !isCoroutineCalled)
        {
            StartCoroutine(ThreeOut());
            Debug.Log("꼽주기 코루틴 호출됨..");
            Debug.Log("코루틴 후 isCoroutineCalled 값은? " + PictureInteraction.isCoroutineCalled);
        }

        //빈 슬롯에 등록
        foreach (GameObject obj in evidenceSlots)
        {
            //string tmpText = obj.transform.GetChild(0).GetComponent<Text>().text;
            if (obj.transform.GetChild(0).GetComponent<Text>().text.Contains("EMPTY") && slotAddContents!="wrong")
            {
                obj.transform.GetChild(0).GetComponent<Text>().text = slotAddContents;

                Debug.Log("슬롯 어디에 등록?: " + obj.transform.parent.parent.parent);
                Debug.Log("슬롯 내용물: "+slotAddContents);
                Debug.Log("슬롯에 등록됨");
                break;
            }
            else
            {
                continue;
            }
        }


    }

    //잘못된 영역에 3번 놓았을 때 대사 출력
    IEnumerator ThreeOut()
    {
        if (SceneManager.GetActiveScene().name == "Chapter1Scene")
        {
            GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().dialogManager.StartDialog(GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().dialogList[14]);
            PictureInteraction.isCoroutineCalled = true;
        }
        else if (SceneManager.GetActiveScene().name == "Chapter2Scene")
        {
            GameObject.Find("2StageScene").GetComponent<Chapter2Manager>().dialogManager.StartDialog(GameObject.Find("2StageScene").GetComponent<Chapter2Manager>().dialogList[10]);
            PictureInteraction.isCoroutineCalled = true;
        }
        else if (SceneManager.GetActiveScene().name == "Chapter3Scene")
        {
            //디아나가 활성화되어있다면 디아나가 꼽줌
            if (GameObject.Find("Characters").transform.GetChild(0).gameObject.activeSelf)
            {
                GameObject.Find("3StageScene").GetComponent<Chapter3Manager>().dialogManager.StartDialog(GameObject.Find("3StageScene").GetComponent<Chapter3Manager>().dialogList[5]);
                PictureInteraction.isCoroutineCalled = true;
            }
            //파르하가 활성화되어있다면 디아나가 꼽줌
            else if (GameObject.Find("Characters").transform.GetChild(1).gameObject.activeSelf)
            {
                GameObject.Find("3StageScene").GetComponent<Chapter3Manager>().dialogManager.StartDialog(GameObject.Find("3StageScene").GetComponent<Chapter3Manager>().dialogList[8]);
                PictureInteraction.isCoroutineCalled = true;
            }
            
        }

        yield return null;
    }

    //다른 스크립트에서 접근해서 슬롯에 등록할 수 있는 함수(CharacterClick 시 단서 등록할 때 사용)
    public void registerSlot(string communicationInfo)
    {
        if (evidenceSlots == null)
        {
            evidenceSlots = new List<GameObject>();
        }
        Debug.Log(evidenceSlots);

        //증거 슬롯 리스트 만들기
        //(instantiate로 복제된 애들이 Content에 대체되고 진짜들은 SubmitSlot에 가서 Destroy될 수도 있으므로 동적으로 변경)
        evidenceSlots.Clear();
        allChildren = scrollViewContent.transform.GetComponentsInChildren<Transform>();

        //Debug.Log("contents 크기: " + allChildren.Length);

        foreach (Transform child in allChildren)
        {
            /*
            Debug.Log("transform name: "+transform.name);
            // 자기 자신의 경우엔 무시 
            if (child.name == transform.name)
                break;
            */
            if (child.tag == "EvidenceSlot")
            {
                evidenceSlots.Add(child.gameObject);
                Debug.Log(child.name + " EvidenceSlot List에 추가됨");
            }
        }


        //빈 슬롯에 등록
        foreach (GameObject obj in evidenceSlots)
        {
            if (obj.transform.GetChild(0).GetComponent<Text>().text.Contains("EMPTY") && communicationInfo != "wrong")
            {
                obj.transform.GetChild(0).GetComponent<Text>().text = communicationInfo;
                Debug.Log("슬롯에 등록됨");
                break;
            }
            else
            {
                continue;
            }
        }

    }


    void FirstStageDragSetting(PointerEventData eventData)
    {
        #region 첫 번째 퀴즈
        //첫 번째 퀴즈
        if (Chapter1Manager.quizNum == 0)
        {
            if (eventData.pointerDrag.name == "Image_machine1")
            {
                Debug.Log("안료측정기기");
                slotAddContents = "비리디타늄으로 만든 녹빛 물감.\n암석에서 추출 가능하다.";
            }
            else if (eventData.pointerDrag.gameObject.name == "Image_machine2")
            {
                Debug.Log("연대측정기기");
                slotAddContents = "발란트로스 력 작품으로 추정됨";
            }

            //슬롯, 도구 drop한 영역 보기
            switch (objectName)
            {
                case "Point1Zone"://사과나무
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("안료 or 연대 측정 기기");
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("붉은 색 봄꽃과 거대한 나무"))
                    {
                        Debug.Log("도상학 슬롯 2");
                        slotAddContents = "여신의 만든 최초의 나무의 상징은 거대한 사과 나무이다.";
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("불타오르매"))
                    {
                        Debug.Log("신문 슬롯 2");
                        slotAddContents = "정화되지 못한 공허는 대지가 되고, 태양의 불꽃은 사과나무와 붉은 꽃이 되었다.";
                    }
                    else
                    {
                        Debug.Log("틀린 위치에 Drop!");
                        GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().increase();
                        slotAddContents = "wrong";
                    }

                    Debug.Log("Point1Zone에 drop"); break;

                case "Point2Zone"://봄꽃
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("안료 or 연대 측정 기기");
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("종교화에 있어"))
                    {
                        Debug.Log("도상학 슬롯 1");
                        slotAddContents = "세계의 탄생을 상징하는 계절은 '봄'이다.";
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("불타오르매"))
                    {
                        Debug.Log("신문 슬롯 2");
                        slotAddContents = "정화되지 못한 공허는 대지가 되고, 태양의 불꽃은 사과나무와 붉은 꽃이 되었다.";
                    }
                    else
                    {
                        Debug.Log("틀린 위치에 Drop!");
                        GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().increase();
                        slotAddContents = "wrong";
                    }

                    Debug.Log("Point2Zone에 drop"); break;

                case "Point3Zone"://구름바람
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("안료 or 연대 측정 기기");
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("공허의 기운"))
                    {
                        Debug.Log("신문 슬롯 1");
                        slotAddContents = "여신의 숨결은 구름과 바람이 되었다.";
                    }
                    else
                    {
                        slotAddContents = "wrong";
                        Debug.Log("틀린 위치에 Drop!");
                        GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().increase();
                    }

                    Debug.Log("Point3Zone에 drop"); break;

                default://picture 영역이면
                    if (eventData.pointerDrag.name != "Image_machine1" && eventData.pointerDrag.name != "Image_machine2")
                    {
                        GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().increase();
                        slotAddContents = "wrong";
                        Debug.Log("슬롯은 특정 영역에만 Drop 가능");
                    }
                    Debug.Log("그림 캔버스에 drop"); break;
            }
        }
        #endregion

        #region 두 번째 퀴즈
        //첫 번째 퀴즈
        else if (Chapter1Manager.quizNum == 1)
        {

            if (eventData.pointerDrag.name == "Image_machine1")
            {
                Debug.Log("안료측정기기");
                slotAddContents = "크리세우스 광석으로 만든 금빛 안료가 주로 사용됨.";
            }
            else if (eventData.pointerDrag.gameObject.name == "Image_machine2")
            {
                Debug.Log("연대측정기기");
                slotAddContents = "600년대 초 작품으로 추정됨.";
            }

            //슬롯, 도구 drop한 영역 보기
            switch (objectName)
            {
                case "Point1Zone"://책과 새
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("안료 or 연대 측정 기기");
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("지혜를 가장 큰"))
                    {
                        Debug.Log("신문 슬롯 1");
                        slotAddContents = "책은 지혜를 중시하는 마도학자의 상징이다.";
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("무생물"))
                    {
                        Debug.Log("신문 슬롯 2");
                        slotAddContents = "마도학자는 식물과 무생물을 동물보다 더 중요시한다.";
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("오판디오"))
                    {
                        Debug.Log("도상학 슬롯 1");
                        slotAddContents = "오판디오 력 시대에서 센텐티아를 숭배하는 계파에 의해 센텐티아의 동물적 상징이 사용되었다.";
                    }
                    else
                    {
                        Debug.Log("틀린 위치에 Drop!");
                        GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().increase();
                        slotAddContents = "wrong";
                    }

                    Debug.Log("Point1Zone에 drop"); break;

                case "Point2Zone"://의복
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("안료 or 연대 측정 기기");
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("현대의 마도학자"))
                    {
                        Debug.Log("도상학 슬롯 2");
                        slotAddContents = "현대의 마도학자들은 센텐티아의 상징을 사용하였다.";
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("황금빛 의복"))
                    {
                        Debug.Log("신문 슬롯 1");
                        slotAddContents = "황금빛 의복은 마도학자의 상징이다.";
                    }
                    else
                    {
                        Debug.Log("틀린 위치에 Drop!");
                        GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().increase();
                        slotAddContents = "wrong";
                    }

                    Debug.Log("Point2Zone에 drop"); break;

                case "Point3Zone"://머리부근 
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("안료 or 연대 측정 기기");
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("현대의 마도학자"))
                    {
                        Debug.Log("도상학 슬롯 2");
                        slotAddContents = "현대의 마도학자들은 센텐티아의 상징을 사용하였다.";
                    }
                    else
                    {
                        slotAddContents = "wrong";
                        Debug.Log("틀린 위치에 Drop!");
                        GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().increase();
                    }

                    Debug.Log("Point3Zone에 drop"); break;

                default://picture 영역이면
                    if (eventData.pointerDrag.name != "Image_machine1" && eventData.pointerDrag.name != "Image_machine2")
                    {
                        GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().increase();
                        slotAddContents = "wrong";
                        Debug.Log("슬롯은 특정 영역에만 Drop 가능");
                    }
                    Debug.Log("그림 캔버스에 drop"); break;
            }
        }
        #endregion
    }

    //두 개의 그림 중 활성화되어있는 그림 getter
    GameObject SecondStagePicture()
    {
        GameObject pictureChange = GameObject.Find("PictureChangeArea");

        return pictureChange;
    }

    #region 스테이지 2 그림 1
    void SecondStageDragSettingPicture1(PointerEventData eventData)
    {

        
        //GameObject pictureChange = GameObject.Find("PictureChangeArea");
        //Debug.Log("pictureChange.transform.GetChild(0).name: " + pictureChange.transform.GetChild(0).name);
        //Debug.Log("pictureChange.transform.GetChild(1).name: " + pictureChange.transform.GetChild(1).name);

        #region 스테이지2 드래그 상호작용
        if (eventData.pointerDrag.name == "Image_machine1")
        {
            Debug.Log("안료측정기기");
            slotAddContents = "갈색의 퀼루스 토양 안료가 사용됨";
        }
        else if (eventData.pointerDrag.gameObject.name == "Image_machine2")
        {
            Debug.Log("연대측정기기");
            slotAddContents = "400년대 중반 그림으로 추정됨";
        }

        //Debug.Log("내 이름은... "+objectName);

        Debug.Log("***objectName: " + gameObject.name + " / objectParentName: " + gameObject.transform.parent.name);

        //슬롯, 도구 drop한 영역 보기
        switch (objectName)
        {
            case "Point1Zone"://황금 말동상
                Debug.Log("내 이름은... " + objectName);
                if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                {
                    Debug.Log("안료 or 연대 측정 기기");
                }

                else
                {
                    Debug.Log("틀린 위치에 Drop!");
                    GameManager.increase();
                    slotAddContents = "wrong";
                }

                Debug.Log("Point1Zone에 drop"); break;

            case "Point2Zone"://어린아이
                Debug.Log("내 이름은... " + objectName);
                Debug.Log("내 이름은... " + eventData.pointerDrag.name);
                if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                {
                    Debug.Log("안료 or 연대 측정 기기");
                }
                else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("분별력"))
                {
                    Debug.Log("신문 슬롯 2");
                    slotAddContents = "사도 알렌티아는 남성형으로\n묘사되기도 한다.";
                }
                else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("용맹"))
                {
                    Debug.Log("도상학 슬롯 1");
                    slotAddContents = "사도 알렌티아는 주로\n여성형으로 묘사된다.";
                }
                else
                {
                    Debug.Log("틀린 위치에 Drop!");
                    GameManager.increase();
                    slotAddContents = "wrong";
                }

                Debug.Log("Point2Zone에 drop"); break;

            case "Point3Zone"://시야 가리는 나뭇가지
                Debug.Log("내 이름은... " + objectName);
                if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                {
                    Debug.Log("안료 or 연대 측정 기기");
                }
                else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("대격변"))
                {
                    Debug.Log("신문 슬롯 1");
                    slotAddContents = "루피늄은 당시 화가들이 나무 질감을\n표현하기 위해 사용했다.";
                }
                else
                {
                    slotAddContents = "wrong";
                    Debug.Log("틀린 위치에 Drop!");
                    GameManager.increase();
                }

                Debug.Log("Point3Zone에 drop"); break;

            default://picture 영역이면
                if (eventData.pointerDrag.name != "Image_machine1" && eventData.pointerDrag.name != "Image_machine2")
                {
                    GameManager.increase();
                    slotAddContents = "wrong";
                    Debug.Log("슬롯은 특정 영역에만 Drop 가능");
                }
                Debug.Log("그림 캔버스에 drop"); break;
        }
        #endregion

    }
    #endregion

    #region 스테이지 2 그림 2
    void SecondStageDragSettingPicture2(PointerEventData eventData)
    {
        //GameObject pictureChange = GameObject.Find("PictureChangeArea");
        //Debug.Log("pictureChange.transform.GetChild(0).name: " + pictureChange.transform.GetChild(0).name);
        //Debug.Log("pictureChange.transform.GetChild(1).name: " + pictureChange.transform.GetChild(1).name);

        Debug.Log("***objectName: " + gameObject.name + " / objectParentName: " + gameObject.transform.parent.name);

        #region 스테이지2 드래그 상호작용
        if (eventData.pointerDrag.name == "Image_machine1")
        {
            Debug.Log("안료측정기기");
            slotAddContents = "갈색의 디오나이트 광물 안료가 사용됨";
        }
        else if (eventData.pointerDrag.gameObject.name == "Image_machine2")
        {
            Debug.Log("연대측정기기");
            slotAddContents = "약 230년 전 그림으로 추정됨";
        }

        //Debug.Log("내 이름은... "+objectName);

        //슬롯, 도구 drop한 영역 보기
        switch (objectName)
        {
            case "Point1Zone"://황금 말동상
                Debug.Log("내 이름은... " + objectName);
                if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                {
                    Debug.Log("안료 or 연대 측정 기기");
                }

                else
                {
                    Debug.Log("틀린 위치에 Drop!");
                    GameManager.increase();
                    slotAddContents = "wrong";
                }

                Debug.Log("Point1Zone에 drop"); break;

            case "Point2Zone"://어린아이
                Debug.Log("내 이름은... " + objectName);
                Debug.Log("내 이름은... " + eventData.pointerDrag.name);
                if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                {
                    Debug.Log("안료 or 연대 측정 기기");
                }
                else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("전쟁이 잦은"))
                {
                    Debug.Log("도상학 슬롯 1");
                    slotAddContents = "알렌티아는 전쟁이 잦은 시기에\n여성형으로 묘사되는 사도이다.";
                }
                else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("인내"))
                {
                    Debug.Log("도상학 슬롯 2");
                    slotAddContents = "파엔티아는 이네르스 력에 여성형으로 묘사되는 사도이다.";
                }
                else
                {
                    Debug.Log("틀린 위치에 Drop!");
                    GameManager.increase();
                    slotAddContents = "wrong";
                }

                Debug.Log("Point2Zone에 drop"); break;

            case "Point3Zone"://시야 가리는 나뭇가지
                Debug.Log("내 이름은... " + objectName);
                if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                {
                    Debug.Log("안료 or 연대 측정 기기");
                }
                else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("대격변"))
                {
                    Debug.Log("신문 슬롯 1");
                    slotAddContents = "루피늄은 당시 화가들이 나무 질감을 표현하기 위해 사용했다.";
                }
                else
                {
                    slotAddContents = "wrong";
                    Debug.Log("틀린 위치에 Drop!");
                    GameManager.increase();
                }

                Debug.Log("Point3Zone에 drop"); break;

            default://picture 영역이면
                if (eventData.pointerDrag.name != "Image_machine1" && eventData.pointerDrag.name != "Image_machine2")
                {
                    GameManager.increase();
                    slotAddContents = "wrong";
                    Debug.Log("슬롯은 특정 영역에만 Drop 가능");
                }
                Debug.Log("그림 캔버스에 drop"); break;
        }
        #endregion


    }
    #endregion

    #region 스테이지 2 백업
    void SecondStageDragSetting(PointerEventData eventData)
    {
        GameObject pictureChange = GameObject.Find("PictureChangeArea");
        Debug.Log("pictureChange.transform.GetChild(0).name: " + pictureChange.transform.GetChild(0).name);
        Debug.Log("pictureChange.transform.GetChild(1).name: " + pictureChange.transform.GetChild(1).name);

        //그림 1이 활성화되었을 때
        if (pictureChange.transform.GetChild(0).name=="Picture1" && pictureChange.transform.GetChild(0).gameObject.activeSelf)
        {
            #region 스테이지2 드래그 상호작용
            if (eventData.pointerDrag.name == "Image_machine1")
            {
                Debug.Log("안료측정기기");
                slotAddContents = "갈색의 퀼루스 토양 안료가 사용됨";
            }
            else if (eventData.pointerDrag.gameObject.name == "Image_machine2")
            {
                Debug.Log("연대측정기기");
                slotAddContents = "400년대 중반 그림으로 추정됨";
            }

            //Debug.Log("내 이름은... "+objectName);

            //슬롯, 도구 drop한 영역 보기
            switch (objectName)
            {
                case "Point1Zone"://황금 말동상
                    Debug.Log("내 이름은... " + objectName);
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("안료 or 연대 측정 기기");
                    }
 
                    else
                    {
                        Debug.Log("틀린 위치에 Drop!");
                        GameManager.increase();
                        slotAddContents = "wrong";
                    }

                    Debug.Log("Point1Zone에 drop"); break;

                case "Point2Zone"://어린아이
                    Debug.Log("내 이름은... " + objectName);
                    Debug.Log("내 이름은... " + eventData.pointerDrag.name);
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("안료 or 연대 측정 기기");
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("분별력"))
                    {
                        Debug.Log("신문 슬롯 2");
                        slotAddContents = "<size=27>사도 알렌티아는 남성형으로 묘사되기도 한다.</size>";
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("용맹"))
                    {
                        Debug.Log("도상학 슬롯 1");
                        slotAddContents = "그림 속 인물은 사도 알렌티아이다.";
                    }
                    else
                    {
                        Debug.Log("틀린 위치에 Drop!");
                        GameManager.increase();
                        slotAddContents = "wrong";
                    }

                    Debug.Log("Point2Zone에 drop"); break;

                case "Point3Zone"://시야 가리는 나뭇가지
                    Debug.Log("내 이름은... " + objectName);
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("안료 or 연대 측정 기기");
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("대격변"))
                    {
                        Debug.Log("신문 슬롯 1");
                        slotAddContents = "루피늄은 당시 화가들이 나무 질감을 표현하기 위해 사용했다.";
                    }
                    else
                    {
                        slotAddContents = "wrong";
                        Debug.Log("틀린 위치에 Drop!");
                        GameManager.increase();
                    }

                    Debug.Log("Point3Zone에 drop"); break;

                default://picture 영역이면
                    if (eventData.pointerDrag.name != "Image_machine1" && eventData.pointerDrag.name != "Image_machine2")
                    {
                        GameManager.increase();
                        slotAddContents = "wrong";
                        Debug.Log("슬롯은 특정 영역에만 Drop 가능");
                    }
                    Debug.Log("그림 캔버스에 drop"); break;
            }
            #endregion
        }

        //그림 2가 활성화되었을 때
        else if (pictureChange.transform.GetChild(1).name == "Picture2" && pictureChange.transform.GetChild(1).gameObject.activeSelf)
        {
            #region 스테이지2 드래그 상호작용
            if (eventData.pointerDrag.name == "Image_machine1")
            {
                Debug.Log("안료측정기기");
                slotAddContents = "갈색의 디오나이트 광물 안료가 사용됨";
            }
            else if (eventData.pointerDrag.gameObject.name == "Image_machine2")
            {
                Debug.Log("연대측정기기");
                slotAddContents = "약 230년 전 그림으로 추정됨";
            }

            //Debug.Log("내 이름은... "+objectName);

            //슬롯, 도구 drop한 영역 보기
            switch (objectName)
            {
                case "Point1Zone"://황금 말동상
                    Debug.Log("내 이름은... " + objectName);
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("안료 or 연대 측정 기기");
                    }

                    else
                    {
                        Debug.Log("틀린 위치에 Drop!");
                        GameManager.increase();
                        slotAddContents = "wrong";
                    }

                    Debug.Log("Point1Zone에 drop"); break;

                case "Point2Zone"://어린아이
                    Debug.Log("내 이름은... " + objectName);
                    Debug.Log("내 이름은... " + eventData.pointerDrag.name);
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("안료 or 연대 측정 기기");
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("분별력"))
                    {
                        Debug.Log("신문 슬롯 2");
                        slotAddContents = "<size=27>사도 알렌티아는 여성형으로 묘사되기도 한다.</size>";
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("용맹"))
                    {
                        Debug.Log("도상학 슬롯 1");
                        slotAddContents = "그림 속 인물은 사도 파엔티아일 수도 있다.";
                    }
                    else
                    {
                        Debug.Log("틀린 위치에 Drop!");
                        GameManager.increase();
                        slotAddContents = "wrong";
                    }

                    Debug.Log("Point2Zone에 drop"); break;

                case "Point3Zone"://시야 가리는 나뭇가지
                    Debug.Log("내 이름은... " + objectName);
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("안료 or 연대 측정 기기");
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("대격변"))
                    {
                        Debug.Log("신문 슬롯 1");
                        slotAddContents = "루피늄은 당시 화가들이 나무 질감을 표현하기 위해 사용했다.";
                    }
                    else
                    {
                        slotAddContents = "wrong";
                        Debug.Log("틀린 위치에 Drop!");
                        GameManager.increase();
                    }

                    Debug.Log("Point3Zone에 drop"); break;

                default://picture 영역이면
                    if (eventData.pointerDrag.name != "Image_machine1" && eventData.pointerDrag.name != "Image_machine2")
                    {
                        GameManager.increase();
                        slotAddContents = "wrong";
                        Debug.Log("슬롯은 특정 영역에만 Drop 가능");
                    }
                    Debug.Log("그림 캔버스에 drop"); break;
            }
            #endregion
        }


    }
    #endregion

    void ThridStageDragSetting(PointerEventData eventData)
    {
        #region 스테이지3 드래그 상호작용
        if (eventData.pointerDrag.name == "Image_machine1")
        {
            Debug.Log("안료측정기기");
            slotAddContents = "어두운 갈색의 디오나이트 광물 안료가 사용됨";
        }
        else if (eventData.pointerDrag.gameObject.name == "Image_machine2")
        {
            Debug.Log("연대측정기기");
            slotAddContents = "이네르스 력에 제작된 그림";
        }

        //Debug.Log("내 이름은... "+objectName);

        //슬롯, 도구 drop한 영역 보기
        switch (objectName)
        {
            case "Point1Zone"://세 개의 빛무리
                Debug.Log("내 이름은... " + objectName);
                if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                {
                    Debug.Log("안료 or 연대 측정 기기");
                }
                else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("최초의 이교도"))
                {
                    Debug.Log("신문 슬롯 2");
                    slotAddContents = "<size=27>말리니타스가 세 개의 불기둥을 쏘아올리고, 이를 경외한 사람들이 이교도가 되었다.</size>";
                }
                else
                {
                    Debug.Log("틀린 위치에 Drop!");
                    GameManager.increase();
                    slotAddContents = "wrong";
                }

                Debug.Log("Point1Zone에 drop"); break;

            case "Point2Zone"://큰 별조각
                Debug.Log("내 이름은... " + objectName);
                Debug.Log("내 이름은... " + eventData.pointerDrag.name);
                if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                {
                    Debug.Log("안료 or 연대 측정 기기");
                }
                else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("따스한 성품"))
                {
                    Debug.Log("도상학 슬롯 2");
                    slotAddContents = "별은 '인간 탄생'의 의미를 가지며, 여신의 따스한 성품을 별의 색감으로 표현한다.";
                }
                else
                {
                    Debug.Log("틀린 위치에 Drop!");
                    GameManager.increase();
                    slotAddContents = "wrong";
                }

                Debug.Log("Point2Zone에 drop"); break;

            case "Point3Zone"://금환일식
                Debug.Log("내 이름은... " + objectName);
                if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                {
                    Debug.Log("안료 or 연대 측정 기기");
                }
                else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("디아나 테네시스"))
                {
                    Debug.Log("신문 슬롯 1");
                    slotAddContents = "말리니타스는 악신으로, 그림자가 태양을 가릴 때 공허에서 태어났다.";
                }
                else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("태초의 혼란"))
                {
                    Debug.Log("도상학 슬롯 1");
                    slotAddContents = "금환일식은 여신 아스트라이아의 상징 중 하나이며, 여신은 일식의 혼란을 잠재웠다.";
                }
                else
                {
                    slotAddContents = "wrong";
                    Debug.Log("틀린 위치에 Drop!");
                    GameManager.increase();
                }

                Debug.Log("Point3Zone에 drop"); break;

            default://picture 영역이면
                if (eventData.pointerDrag.name != "Image_machine1" && eventData.pointerDrag.name != "Image_machine2")
                {
                    GameManager.increase();
                    slotAddContents = "wrong";
                    Debug.Log("슬롯은 특정 영역에만 Drop 가능");
                }
                Debug.Log("그림 캔버스에 drop"); break;
        }
        #endregion
    }


}
