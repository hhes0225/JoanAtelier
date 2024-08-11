using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


//이 스크립트는 PictureInteraction.cs가 대체해서 더 이상 쓰이지 않는 스크립트임
public class ToolDropHandler : MonoBehaviour, IDropHandler
{
    string slotAddContents;

    [SerializeField]
    private GameObject[] evidenceSlots;
    
    //그림 영역에 도구 드롭했을 때 1회 호출
    public void OnDrop(PointerEventData eventData)
    {
        if (Chapter1Manager.quizNum == 0)
        {
            if (eventData.pointerDrag.gameObject.name == "Image_machine1")
            {
                Debug.Log("안료측정기기");
                slotAddContents = "비리디타늄으로 만든 녹빛 물감.\n암석에서 추출 가능하다.";
            }
            else if (eventData.pointerDrag.gameObject.name == "Image_machine2")
            {
                Debug.Log("연대측정기기");
                slotAddContents = "발란트로스 력 작품으로 추정됨";
            }
            else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("공허의 기운"))
            {
                Debug.Log("신문 슬롯 1");
                slotAddContents = "여신의 숨결이 탁한 구슬을 정화시켰다.";
            }
            else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("튀어나오매"))
            {
                Debug.Log("신문 슬롯 2");
                slotAddContents = "정화되지 못한 공허는 대지가 되고, 은하수는 나무의 뿌리가 되었다.";
            }
            else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("종교화에 있어"))
            {
                Debug.Log("도상학 슬롯 1");
                slotAddContents = "세계의 탄생을 상징하는 계절은 '봄'이다.";
            }
            else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("거대한 나무 단 한그루"))
            {
                Debug.Log("도상학 슬롯 2");
                slotAddContents = "여신의 만든 최초의 나무의 상징은 거대한 황금 사과 나무이다.";
            }
        }
        //두 번째 도상학 퀴즈
        else if (Chapter1Manager.quizNum == 1)
        {
            if (eventData.pointerDrag.gameObject.name == "Image_machine1")
            {
                Debug.Log("안료측정기기");
                slotAddContents = "크리세우스 광석으로 만든 금빛 안료가 주로 사용됨.";
            }
            else if (eventData.pointerDrag.gameObject.name == "Image_machine2")
            {
                Debug.Log("연대측정기기");
                slotAddContents = "600년대 초 작품으로 추정됨.";
            }
            else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("공허의 기운"))
            {
                Debug.Log("신문 슬롯 1");
                slotAddContents = "책은 지혜를 중시하는 마도학자의 상징이다.";
            }
            else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("튀어나오매"))
            {
                Debug.Log("신문 슬롯 2");
                slotAddContents = "마도학자는 식물과 무생물을 동물보다 더 중요시한다.";
            }
            else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("종교화에 있어"))
            {
                Debug.Log("도상학 슬롯 1");
                slotAddContents = "오판디오 력 시대에서 센텐티아를 숭배하는 계파에 의해 센텐티아의 상징이 사용되었다.";
            }
            else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("거대한 나무 단 한그루"))
            {
                Debug.Log("도상학 슬롯 2");
                slotAddContents = "현대의 마도학자들은 센텐티아의 상징을 사용하였다.";
            }
        }
        

        //빈 슬롯에 등록
        foreach (GameObject i in evidenceSlots)
        {
            //Debug.Log("등록될 슬롯의 이름은 "+i.name);
            if (i.name.Contains("Clone")){
                Debug.Log("스킵된 슬롯의 이름은 " + i.name);
                continue;
            }
            if (i.transform.GetChild(0).GetComponent<Text>().text.Contains("EMPTY"))
            {
                i.transform.GetChild(0).GetComponent<Text>().text = slotAddContents;
                Debug.Log("슬롯에 등록됨");
                break;
            }
            else
            {
                continue;
            }
        }
    }
}
