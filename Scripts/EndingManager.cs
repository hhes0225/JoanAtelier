using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*스테이지 추가할 경우 엔딩 조건 수정하기*/

public class EndingManager : MonoBehaviour
{
    public Text message;
    public Image cutSceneImage;
    GameObject fadeout, fadeout1;

    int i = 0;
    int j = 0;
    private string[] TrueMagicText = {"\"이 그림도 감정해주게!\"",
        "\"내가 의뢰한 그림은 어떤 학자를 그린 작품이었는가!\"",
        "\"내 초상화에 어떤 상징을 넣으면 나의 지성미를 돋보일 수 있겠는가?\"",
        "그동안 사건을 성공적으로 해결한 덕인지, 소문을 타고 아틀리에가 이전보다 더욱 유명해졌다.",
        "영지민들은 물론이고, 다른 영지에서도 의뢰하기 위해 찾아왔다.",
        "스승님은 아틀리에를 잘 이끌어서 고맙다는, 자랑스러운 제자를 칭찬하는 편지를 보내왔다.",
        "편지에는 조안이 운영하는 아틀리에는 어떨지 궁금하다며, 조만간 찾아가겠다는 내용도 있었다.",
        "뮐러, 디아나, 파르하, 그리고 다른 의뢰인들도 그림 감정을 의뢰하러 오거나, 근황을 전하고 담소를 나누기 위해 찾아왔다.",
        "",
        "아틀리에가 유명해지고 난 후 찾아오는 의뢰인들은 주로 마도학계의 사람들이었다.",
        "마도학자와 아카데미의 마도학부 학생들이 의뢰를 하기 위해 찾아왔으며,",
        "아틀리에 주변에서는 세상의 이치와 마력의 원리에 대한 견해를 토론하는 사람이 북적인다.",
        "사람들은 실체가 없는 여신보다 당장 세상을 이해하는 데 타당한 마도학에 흥미를 가지기 시작한다.",
        " ",
        "조안은 그들을 보며 디아나가 보내준 마도학 서적을 읽는다.",
        "그동안 여러 그림을 감정하며 마도학에 큰 영향을 받았고, 신보다는 인간 중심의 시각으로 그림을 바라보게 되었다.",
        "조안은 오늘도 그림을 분석하며, 마도학과 연금술을 공부하고 앞으로도 그럴 것이다.",
        "스승님의 아틀리에, 그리고 이제는 조안의 아틀리에를 위해.",
        "True Ending 1. 마력과 마도학의 세계, 그리고 조안의 아틀리에"};
    private string[] TrueReligionText = {"\"이 그림도 감정해주게!\"",
        "\"내가 의뢰한 그림은 어떤 성자를 그린 작품인가!\"",
        "\"내 초상화에 신의 어떤 상징을 넣으면 더 위엄있어 보이겠는가!\"",
        "그동안 사건을 성공적으로 해결한 덕인지, 소문을 타고 아틀리에가 이전보다 더욱 유명해졌다.",
        "영지민들은 물론이고, 다른 영지에서도 의뢰하기 위해 찾아왔다.",
        "스승님은 아틀리에를 잘 이끌어서 고맙다는, 자랑스러운 제자를 칭찬하는 편지를 보내왔다.",
        "편지에는 조안이 운영하는 아틀리에는 어떨지 궁금하다며, 조만간 찾아가겠다는 내용도 있었다.",
        "뮐러, 디아나, 파르하, 그리고 다른 의뢰인들도 그림 감정을 의뢰하러 오거나,  담소를 나누기 위해 찾아왔다.",
        "",
        "아틀리에가 유명해지고 난 후 찾아오는 의뢰인들은 주로 종교계의 사람들이었다.",
        "성직자와 신실한 종교인들이 의뢰를 하기 위해 찾아왔으며,",
        "아틀리에 주변에서는 교리를 해석하고 아스테르 교의 교리를 전파하는 사람이 북적인다.",
        "사람들은 이 세상 곳곳에 스며있는 여신의 축복을 찬양한다.",
        " ",
        "조안은 그들을 보며 파르하가 보내준 아스테르 교 미술 서적을 읽는다.",
        "그동안 여러 그림을 감정하며 종교에 큰 영향을 받았고, 인간보다는 여신님과 그의 사도들 중심의 시각으로 그림을 바라보게 되었다.",
        "조안은 오늘도 그림을 분석하며, 아스테르 교를 공부하고 앞으로도 그럴 것이다.",
        "스승님의 아틀리에, 그리고 이제는 조안의 아틀리에를 위해.",
        "True Ending 2. 여신과 아스트리아의 세계, 그리고 조안의 아틀리에"};
    private string[] GoodText = { "\"저 건물은 무얼 하는 건물일까?\"",
        "\"아마 그림 분석해주는 곳일걸?\"",
        "\"조안의 아틀리에라고 알고 있어!\"",
        "조안의 아틀리에는 아는 사람만 아는, 오는 사람만 오는 그런 곳이 되었다.",
        "그림을 정확히 분석받은 손님들은 다시 그림을 의뢰하러 찾아오지만, 잘못 분석한 그림의 손님들은 보통은 다시 찾아오지 않았다.",
        "그렇게 지내다 보니, 새로운 손님이 유입되지 않고 단골 손님 위주의 영업이 이루어졌다.",
        "그래도 이전의 사건들이 인상깊었는지, 사람들은 조안의 아틀리에를 기억하고 있었다.",
        "아마 그림 감정이나 분석이 필요하게 된다면 사람들은 한번쯤 조안의 아틀리에를 떠올리고 찾아갈 것이다.",
        "조안의 아틀리에는 잊히지 않았다.",
        "조안의 아틀리에 앞으로 점점 더 나아질 것이다.",
        "Good Ending. 조안의 아틀리에"};
    private string[] NormalText = {"\"저 건물은 무얼 하는 건물일까?\"",
        "\"그러게, 잘 모르겠네.\"",
        "조안의 아틀리에는 아는 사람만 아는, 오는 사람만 오는 그런 곳이 되었다.",
        "그림을 정확히 분석받은 손님들은 다시 그림을 의뢰하러 찾아오지만, 잘못 분석한 그림의 손님들은 다시는 찾아오지 않았다.",
        "그렇게 지내다 보니, 새로운 손님이 유입되지 않고 단골 손님 위주의 영업이 이루어졌다.",
        "사람들은 아틀리에 건물이 무엇을 하는 곳인지 잘 모르고 지나쳤다.",
        "그렇게 아틀리에는 그저 그 자리에 흐릿하게 존재할 뿐이다.",
        "있는 듯 없는 듯, 투명한 셀로판처럼.",
        "Normal Ending. 셀로판 아틀리에"};
    private string[] BadText = { "\"엉터리!\"", "\"저 아틀리에 주인은 사기꾼이야!\"",
        "\"저기에 의뢰했던 그림이 가품이라고 해서 처리했는데 진품이었다고!\"",
        "\"난 여신님의 그림인 줄 알고 식당 벽에 건 그림이 이교도 그림이었어!\"",
        "\"이 영지에서 썩 꺼져라!\"",
        "잘못된 감정 결과에 조안과 아틀리에의 악명은 점점 쌓여갔다.",
        "일부 영지민들은 아틀리에를 고발했다.",
        "그 결과로, 아틀리에는 영주에게 몰수당하고, 조안은 아틀리에에서 쫓겨났다.",
        "스승님의 아틀리에에는 새로운 사람이 임명되었다.",
        "영지에서 퇴거 명령이 들어왔지만, 조안은 답하지 않았다.",
        "그러던 중, 밤에 건물의 불이 켜진 것을 발견한 사람이 영지 행정 공무집행원에게 신고했고, 곧 아틀리에로 사람들이 몰려오기 시작했다.",
        "쫓겨난 조안은 절망하지 않았다.",
        "조안은 처음부터 다시 배우기 위해 스승님께 향한다.",
        "이제 사람들은 아틀리에의 주인이 어디에 있는지 아무도 모를 것이다…. ",
        "Bad Ending. 실패한 가르침"};


    private string[] imagePathTrueMagic = { "ending/trueMagic1", "ending/trueMagic2" };
    private string[] imagePathTrueReligion = { "ending/trueReligion1", "ending/trueReligion2" };
    private string[] imagePathGood = { "ending/good" };
    private string[] imagePathNormal = { "ending/normal" };
    private string[] imagePathBad = { "ending/bad" };

    void Awake()
    {


    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.SetResolution();
        int i = 0;
        fadeout = GameObject.Find("FadeObject");
        fadeout1 = GameObject.Find("FadeObject (1)");
        cutSceneImage = GameObject.Find("Image").GetComponent<Image>();
        message = GameObject.Find("StoryText").GetComponent<Text>();


        //True Ending: 스테이지 2개 다 트루엔딩
        if (DataController.Instance.gameData.trueEnding > 1 && DataController.Instance.gameData.badEnding == 0)//True Ending
        {
            if (DataController.Instance.gameData.religion > DataController.Instance.gameData.magic)
            {
                //종교 엔딩
                initDialog(TrueReligionText, imagePathTrueReligion);

            }
            else if (DataController.Instance.gameData.religion < DataController.Instance.gameData.magic)
            {
                //마도학 엔딩
                initDialog(TrueMagicText, imagePathTrueMagic);
            }
            else//예외 처리
            {
                Debug.Log("religion, magic ending error!");
            }

        }
        //Bad Ending: 스테이지 2개 중 배드엔딩 2개 보기
        else if (DataController.Instance.gameData.badEnding > 1 || (DataController.Instance.gameData.badEnding == 1 && DataController.Instance.gameData.normalEnding == 1))//Bad Ending
        {
            initDialog(BadText, imagePathBad);
        }
        else//Normal Ending
        {
            //Good Ending: 2개 중 1개는 True Ending
            if (DataController.Instance.gameData.trueEnding == 1)
            {
                //good ending
                initDialog(GoodText, imagePathGood);

            }
            //Normal Ending: 
            else
            {
                Debug.Log("normal ending");
                initDialog(NormalText, imagePathNormal);
            }


        }

        StartCoroutine(EndingSceneCoroutine());


        //화면(을 가장한 버튼) 클릭 시
        GameObject.Find("ConvertBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            //True Ending: 스테이지 2개 다 트루엔딩
            if (DataController.Instance.gameData.trueEnding > 1 && DataController.Instance.gameData.badEnding == 0)//True Ending
            {
                if (DataController.Instance.gameData.religion > DataController.Instance.gameData.magic)
                {
                    //종교 엔딩
                    nextDialog(TrueReligionText, imagePathTrueReligion);
                    i = 1;

                }
                else if (DataController.Instance.gameData.religion < DataController.Instance.gameData.magic)
                {
                    //마도학 엔딩
                    nextDialog(TrueMagicText, imagePathTrueMagic);
                    i = 1;
                }
                else//예외 처리
                {
                    Debug.Log("religion, magic ending error!");
                }

            }
            //Bad Ending: 스테이지 2개 중 배드엔딩 2개 보기
            else if (DataController.Instance.gameData.badEnding > 1 || (DataController.Instance.gameData.badEnding == 1 && DataController.Instance.gameData.normalEnding == 1))//Bad Ending
            {
                nextDialog(BadText, imagePathBad);
                i = 1;
            }
            else//Normal Ending
            {
                //Good Ending: 2개 중 1개는 True Ending
                if (DataController.Instance.gameData.trueEnding == 1 && DataController.Instance.gameData.normalEnding == 1)
                {
                    //good ending
                    nextDialog(GoodText, imagePathGood);
                    i = 1;

                }
                //Normal Ending: 
                else
                {
                    Debug.Log("normal ending");
                    nextDialog(NormalText, imagePathNormal);
                    i = 1;
                }
            }



        });
    }

    void initDialog(string[] storyText, string[] imagePath)
    {
        message.text = storyText[i];
        i++;
        cutSceneImage.sprite = Resources.Load<Sprite>(imagePath[j]) as Sprite;
    }

    void nextDialog(string[] storyText, string[] imagePath)
    {
        Debug.Log("다음 클릭!");
        if (i < storyText.Length)
        {
            fadeout1.SetActive(true);
            fadeout1.GetComponent<TransitionEffect>().FadeOutFlow();
            message.text = storyText[i];
            if (storyText[i] == " ")
            {
                j++;
                fadeout.SetActive(true);
                fadeout.GetComponent<TransitionEffect>().FadeOutFlow();
                fadeout.GetComponent<TransitionEffect>().FadeInFlow();
            }

            cutSceneImage.sprite = Resources.Load<Sprite>(imagePath[j]) as Sprite;
            if (storyText[i] == " ")
            {
                fadeout.GetComponent<TransitionEffect>().FadeInFlow();
            }
            fadeout1.GetComponent<TransitionEffect>().FadeInFlow();
            i++;

        }
        else
        {
            Debug.Log("스크립트 전체출력 완료!");
            DataController.Instance.gameData.isClear5 = true;
            DataController.Instance.gameData.trueEnding = 0;
            DataController.Instance.gameData.normalEnding = 0;
            DataController.Instance.gameData.badEnding = 0;
            DataController.Instance.gameData.religion = 0;
            DataController.Instance.gameData.magic = 0;
            DataController.Instance.SaveGameData();
            fadeout.SetActive(true);
            fadeout.GetComponent<Transform>().SetSiblingIndex(4);
            fadeout.GetComponent<TransitionEffect>().FadeOut(1);
        }
    }

    IEnumerator EndingSceneCoroutine()
    {
        yield return new WaitForSeconds(1f);
        fadeout.GetComponent<Transform>().SetSiblingIndex(3);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DataController.Instance.gameData.isClear5 = true;
            DataController.Instance.gameData.trueEnding = 0;
            DataController.Instance.gameData.normalEnding = 0;
            DataController.Instance.gameData.badEnding = 0;
            DataController.Instance.gameData.religion = 0;
            DataController.Instance.gameData.magic = 0;
            DataController.Instance.SaveGameData();
            fadeout.SetActive(true);
            fadeout.GetComponent<Transform>().SetSiblingIndex(4);
            fadeout.GetComponent<TransitionEffect>().FadeOut(1);
        }
    }
}
