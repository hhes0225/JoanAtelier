using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ToolClickHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField]
    private GameObject popupBubble;

    [SerializeField]
    private GameObject bookDetail;

    //상호작용 가능 슬롯 내용 입력하기
    [SerializeField]
    private string[] choice1;
    [SerializeField]
    private string[] choice2;

    //상호작용 불가능 슬롯 내용 입력하기
    [SerializeField]
    private string[] contents;

    void Awake()
    {
        popupBubble.SetActive(false);
        if (gameObject.tag == "Book")
            bookDetail.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        popupBubble.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        popupBubble.SetActive(false);

        if (SceneManager.GetActiveScene().name == "Chapter1Scene")
            FirstStageClickSetting();
        else if (SceneManager.GetActiveScene().name == "Chapter2Scene")
            SecondStageClickSetting();
        else if (SceneManager.GetActiveScene().name == "Chapter3Scene")
            ThirdStageClickSetting();
        
    }

    void FirstStageClickSetting()
    {
        #region 첫 번째 퀴즈
        if (Chapter1Manager.quizNum==0)
        {
            if (gameObject.tag == "InteractableBook" || gameObject.tag == "UninteractableBook")
            {

                //bookDetail.transform.GetChild(1).gameObject.SetActive(false);
                //bookDetail.transform.GetChild(2).gameObject.SetActive(false);

                bookDetail.SetActive(true);
                if (gameObject.tag == "InteractableBook")
                {
                    //상호작용 가능한 책이면 설명 슬롯 활성화, 설명 이미지 비활성화
                    bookDetail.transform.GetChild(0).gameObject.SetActive(true);
                    bookDetail.transform.GetChild(1).gameObject.SetActive(false);
                    Debug.Log("Interactable book");

                    if (gameObject.name == "Image_book1")//도상학 서적이면
                    {
                        bookDetail.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "종교화에 있어서 봄을 그린 그림은 세계의 탄생을 표현한 그림이다.";
                        bookDetail.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "붉은 색 봄꽃과 거대한 나무 단 한 그루가 사과나무라면, 이는 여신의 만든 최초의 나무를 의미한다.";
                    }
                    else//신문이면
                    {
                        //bookDetail.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().fontSize = 30;
                        //bookDetail.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().fontSize = 30;
                        bookDetail.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "<size=27><b>성서 한구절- 이 모든 게 여신의 축복이라!</b>\n여신 아스트라이아가 공허의 기운을 모아 숨결을 불어넣으매, 구름과 바람이 되어 혼탁한 기운은 가시고 맑고 깨끗한 구슬이 되었다.</size>";
                        bookDetail.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "<size=27>여신이 미처 없애지 못한 공허는 가라앉아 대지가 되고, 여신이 태양으로 만든 불꽃을 대지에 묻으매 거대한 나무가 자랐다. 대지에서 태양의 빛이 불타오르매, 이는 꽃이요 사과 과실이라.</size>";
                    }
                }
                else
                {
                    //상호작용 불가능한 책이면 설명 슬롯 비활성화, 설명 이미지 활성화
                    bookDetail.transform.GetChild(1).gameObject.SetActive(true);
                    bookDetail.transform.GetChild(0).gameObject.SetActive(false);
                    Debug.Log("Uninteractable book");

                    if (gameObject.name == "Image_memo")//스승님 메모면
                    {
                        bookDetail.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "<size=27>시대에 따라 쓰이는 안료도 변화해 왔단다.\n발란트로스 력(342~527): 전쟁에 혼란스러운 상황 속에서 쉽게 구할 수 있는 <b>식물</b>을 안료로 사용함.\n오판디오 력(528~664): 신학자들이 대지의 축복을 연구하며 <b>금속과 암석 안료</b>가 발달함.\n이네르스 력(665~): 마도학이 싹트며 <b>보석과 결정</b>으로 만들어진 안료가 발달함.\n</size>";
                    }
                    else//왕국 역사서면
                    {
                        bookDetail.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "발란트로스 력(342~527): 군사 정복기\n오판디오 력(528~664): 종교 부흥기\n이네르스 력(665~): 마도학 발전기, 종교와 마도학의 충돌기\n";
                    }
                }
            }
        }

        #endregion

        #region 두 번째 퀴즈
        else if (Chapter1Manager.quizNum == 1)
        {
            if (gameObject.tag == "InteractableBook" || gameObject.tag == "UninteractableBook")
            {

                //bookDetail.transform.GetChild(1).gameObject.SetActive(false);
                //bookDetail.transform.GetChild(2).gameObject.SetActive(false);

                bookDetail.SetActive(true);
                if (gameObject.tag == "InteractableBook")
                {
                    //상호작용 가능한 책이면 설명 슬롯 활성화, 설명 이미지 비활성화
                    bookDetail.transform.GetChild(0).gameObject.SetActive(true);
                    bookDetail.transform.GetChild(1).gameObject.SetActive(false);
                    Debug.Log("Interactable book");

                    if (gameObject.name == "Image_book1")//도상학 서적이면
                    {
                        bookDetail.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "오판디오 력 시기, 종교계에서는 사도 센텐티아를 중시하는 계파가 권력을 잡아 종교화에도 영향을 미쳤다. 특히 사도의 동물적 상징을 자주 사용하였다.";
                        bookDetail.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "현대의 마도학자는 마도학의 가치를 위배하지 않는 선에서 사도 센텐티아의 상징을 차용하기도 한다.";
                    }
                    else//신문이면
                    {
                        bookDetail.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "마도학자의 상징은 황금빛 의복이다. 초상화에도 항상 책을 함께 그릴 만큼 지혜를 가장 큰 가치로 여긴다.";
                        bookDetail.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "<size=27>마도학자는 양분을 얻기 위해 다른 생물을 해하는 동물을 싫어한다. 움직임 없이 스스로 양분을 만들어내는 식물에 더 관심을 가지며, <b>식물 혹은 무생물</b>에서 생명력의 원천을 찾으려고 한다.</size>";
                    }
                }
                else
                {
                    //상호작용 불가능한 책이면 설명 슬롯 비활성화, 설명 이미지 활성화
                    bookDetail.transform.GetChild(1).gameObject.SetActive(true);
                    bookDetail.transform.GetChild(0).gameObject.SetActive(false);
                    Debug.Log("Uninteractable book");

                    if (gameObject.name == "Image_memo")//스승님 메모면
                    {
                        bookDetail.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "<size=27>시대에 따라 쓰이는 안료도 변화해 왔단다.\n발란트로스 력(342~527): 전쟁에 혼란스러운 상황 속에서 쉽게 구할 수 있는 <b>식물</b>을 안료로 사용함.\n오판디오 력(528~664): 신학자들이 대지의 축복을 연구하며 <b>금속과 암석 안료</b>가 발달함.\n이네르스 력(665~): 마도학이 싹트며 <b>보석과 결정</b>으로 만들어진 안료가 발달함.\n</size>";
                    }
                    else//왕국 역사서면
                    {
                        bookDetail.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "<size=27>사도 센텐티아 – 여신의 머리에 맴돌던 빛. 상징색은 금색이다. 상징 동물은 새이다.\n사도 알렌티아 – 여신의 손목에 맴돌던 빛. 상징색은 붉은색이다. 상징 동물은 소 또는 말이다.\n사도 파엔티아 – 여신의 발치에 맴돌던 빛. 상징색은 푸른색이다. 상징 동물은 물고기이다.</size>";
                    }
                }
            }
        }
        #endregion
    }

    void SecondStageClickSetting()
    {
        #region 스테이지 2 퀴즈
        if (gameObject.tag == "InteractableBook" || gameObject.tag == "UninteractableBook")
        {

            bookDetail.SetActive(true);
            if (gameObject.tag == "InteractableBook")
            {
                //상호작용 가능한 책이면 설명 슬롯 활성화, 설명 이미지 비활성화
                bookDetail.transform.GetChild(0).gameObject.SetActive(true);
                bookDetail.transform.GetChild(1).gameObject.SetActive(false);
                Debug.Log("Interactable book");

                if (gameObject.name == "Image_book1")//도상학 서적이면
                {
                    bookDetail.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "<size=26>종교적으로 알렌티아는 용맹과 열정을 상징한다. 여신의 손목에 맴돌던 빛에서 탄생하였으며, 여성형으로 묘사된다. 하반신이 발굽동물로 묘사되기도 한다. 전쟁이 잦은 지역과 시대에 특히 더 숭상되었다. 망치와 같은 금속 도구를 상징으로서 같이 그리기도 한다.</size>";
                    bookDetail.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "<size=26>파엔티아는 인내를 상징한다. 여신의 발목에 맴돌던 빛에서 탄생하였으며, 여성형으로 묘사된다. 하반신이 어류로 그려질 수도 있다. 오판디오 시대는 사도 센텐티아가 숭상되었지만, 마도학계에서 그 상징을 차용하기 시작한 이네르스 시대부터 파엔티아의 상징이 사용되기 시작했다.</size>";
                }
                else//신문이면
                {
                    bookDetail.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "<size=27><b>미술계의 대격변?\n</b>1개월 전, 선대 화가들이 사용했던 안료, 루피늄이 발견되었다는 소식을 보도했다. 이 안료는 입자가 커 당시 화가들은 나무의 거친 질감을 표현하기 위해 사용했을 것이라고 왕실 연구 관계자는 전했다.</size>";
                    bookDetail.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "<size=27>(중략) 또한 사도 알렌티아가 최근 남성형으로도 묘사되었다는 설이 제기되었으며 학계의 정설로 받아들여졌다. 격변의 시대 속에서 이론이 바뀌기 전에 발행된 서적의 내용을 맹신하기보다 분별력있게 판단하는 자세가 필요하다.</size>";
                }
            }
            else
            {
                //상호작용 불가능한 책이면 설명 슬롯 비활성화, 설명 이미지 활성화
                bookDetail.transform.GetChild(1).gameObject.SetActive(true);
                bookDetail.transform.GetChild(0).gameObject.SetActive(false);
                Debug.Log("Uninteractable book");

                if (gameObject.name == "Image_memo")//스승님 메모면
                {
                    bookDetail.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "<size=27>시대에 따라 쓰이는 안료도 변화해 왔단다.\n발란트로스 력(342~527): 전쟁에 혼란스러운 상황 속에서 쉽게 구할 수 있는 <b>식물</b>을 안료로 사용함.\n오판디오 력(528~664): 신학자들이 대지의 축복을 연구하며 <b>금속과 암석 안료</b>가 발달함.\n이네르스 력(665~): 마도학이 싹트며 <b>보석과 결정</b>으로 만들어진 안료가 발달함.\n</size>";
                }
                else//미술 감정 협회 공지문이면
                {
                    bookDetail.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "<size=27><b><align=center>공  지  문</align></b>\n\n본 학술 협회 회원들께 알려드립니다. 이번 왕국 역사문화부서의 연구에서 새로운 안료가 발견되었습니다. 소식을 받은 즉시 공문을 보내드렸사오니 양해 부탁드립니다. 안료의 이름은 루피늄이며, 재료가 암석임에도 발란트로스 시대에도 사용되었습니다. 이 안료는 갈색 빛을 표현하는 데 쓰였습니다. 기존 감정 도구를 사용했을 때, 디오나이트 광물 안료, 퀼루스 토양 안료로 혼동된 결과가 나왔다고 합니다. 감정에 있어 참고하시기 바랍니다.\n<align=right>777년 7월 7일\n에스펠라 왕립 미술 감정 협회 드림.</align></size>";
                }
            }
        }
        #endregion
    }

    void ThirdStageClickSetting()
    {
        #region 스테이지 3 퀴즈
        if (gameObject.tag == "InteractableBook" || gameObject.tag == "UninteractableBook")
        {

            bookDetail.SetActive(true);
            if (gameObject.tag == "InteractableBook")
            {
                //상호작용 가능한 책이면 설명 슬롯 활성화, 설명 이미지 비활성화
                bookDetail.transform.GetChild(0).gameObject.SetActive(true);
                bookDetail.transform.GetChild(1).gameObject.SetActive(false);
                Debug.Log("Interactable book");

                if (gameObject.name == "Image_book1")//도상학 서적이면
                {
                    bookDetail.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "<size=27>금환일식은 아스트라이아의 상징 중 하나이다. 태초의 혼란에 그림자가 태양을 가렸지만, 여신과 세 사도들의 힘으로 몰아냈다는 신화에서 비롯되었다. 이러한 일식 현상은 여신을 묘사할 때 자주 사용되는 상징이기도 하다.</size>";
                    bookDetail.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "<size=27>별은 아스테르 교리에서는 인간을 탄생시키는 매개체이자 성물의 의미가 될 수 있다. 여신의 따스한 성품을 돋보이게 하기 위해 별의 색감을 이용하며, 이를 중요시한다.</size>";
                }
                else//신문이면
                {
                    bookDetail.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "<size=25><b>초상화 사건–말리니타스는 누구인가?</b></size>\n<size=27>디아나 테네시스(마도학 연구원)양의 초상화 사건이 조사 중이다. 논란의 ‘말리니타스’는 누구인가. 말리니타스는 여신이 태어나고 남은 공허의 찌꺼기에서 태어났으며, 이 때 그림자가 태양을 가려 그 빛이 반지 같았다고 한다.</size>";
                    bookDetail.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "<size=27>또한 말리니타스는 이교도를 탄생시켰다. 어렵게 입수한 정보에 따르면, 말리니타스는 여신의 뜻에 반하여 세 개의 불기둥을 쏘아올렸다. 이를 본 일부 인간들이 여신을 저버리고 말리니타스를 따랐고, 이들이 최초의 이교도이다.</size>";
                }
            }
            else
            {
                //상호작용 불가능한 책이면 설명 슬롯 비활성화, 설명 이미지 활성화
                bookDetail.transform.GetChild(1).gameObject.SetActive(true);
                bookDetail.transform.GetChild(0).gameObject.SetActive(false);
                Debug.Log("Uninteractable book");

                if (gameObject.name == "Image_memo")//스승님 메모면
                {
                    bookDetail.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "<size=27>시대에 따라 쓰이는 안료도 변화해 왔단다.\n발란트로스 력(342~527): 전쟁에 혼란스러운 상황 속에서 쉽게 구할 수 있는 <b>식물</b>을 안료로 사용함.\n오판디오 력(528~664): 신학자들이 대지의 축복을 연구하며 <b>금속과 암석 안료</b>가 발달함.\n이네르스 력(665~): 마도학이 싹트며 <b>보석과 결정</b>으로 만들어진 안료가 발달함.\n</size>";
                }
                else//아스테라 교 성서
                {
                    bookDetail.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "<size=27><b><align=center>~사도의 탄생~</align></b>\n여신이 돌아오니 푸른 구슬이 검고 붉게 물들어 있으매, 타락한 이교도가 생명을 약탈하고 있더라.여신이 곁을 맴돌던 세 줄기의 별빛을 뽑아내어 세계에 내리어 보내니, 이는 유성우요, 최초의 사도라.\n<b><align=center>~인간의 탄생~</b></align>\n세 사도가 커다란 별 조각을 대지에 뿌리매 사도의 형상과 비슷한 생명체가 탄생하였으니, 이교도와는 다른 성스러운 인간이다. 세 사도에 의해 탄생한 인간은 이교도를 몰아내고, 이를 축복하며 사도 알렌티아의 푸른 나뭇가지를 가장 척박한 땅에 꽂아넣었다.</size>";
                }
            }
        }
        #endregion
    }

}
