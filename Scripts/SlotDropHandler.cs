using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SlotDropHandler : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    private Image image;
    private RectTransform rect;

    private void Awake()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }

    //마우스 포인터가 현재 아이템 슬롯 영역 내부로 들어갈 때 1회 호출
    public void OnPointerEnter(PointerEventData eventData)
    {
        //슬롯 색상 어둡게 변경
        image.color = new Color(150f / 255f, 120f / 255f, 100f / 255f);
    }

    //마우스 포인터가 슬롯 영역 빠져나갈 때 1회 호출
    public void OnPointerExit(PointerEventData eventData)
    {
        //슬롯 색상 밝게 변경
        image.color = new Color(210f / 255f, 180f / 255f, 160f / 255f);
    }

    //현재 아이템 슬롯 영역 내부에서 드롭했을 때 1회 호출
    public void OnDrop(PointerEventData eventData)
    {
        //eventData.pointerDrag는 현재 드래그중인 대상(=슬롯)
        if (eventData.pointerDrag != null && CompareTag(eventData.pointerDrag.gameObject.tag))
        {
            if (SceneManager.GetActiveScene().name=="Chapter2Scene")
            {
                GameObject submitArea = GameObject.Find("SubmitArea");
                if (submitArea.transform.GetChild(0).childCount != 0) {
                    Debug.Log(submitArea.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text);
                    if (submitArea.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("첫번째 그림")) 
                    {
                        if (eventData.pointerDrag.transform.parent.tag == "Picture2Slots" && eventData.pointerDrag.gameObject.tag=="EvidenceSlot") {
                            Debug.Log("그림 1 관련 슬롯만 놓으셈요 ");
                            GameObject.Find("2StageScene").GetComponent<Chapter2Manager>().showWrongSlotAreaNotice();
                            return;
                        }
                    }
                    else if (submitArea.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("두번째 그림"))
                    {
                        if (eventData.pointerDrag.transform.parent.tag == "Picture1Slots" && eventData.pointerDrag.gameObject.tag == "EvidenceSlot")
                        {
                            Debug.Log("그림 2 관련 슬롯만 놓으셈요 ");
                            GameObject.Find("2StageScene").GetComponent<Chapter2Manager>().showWrongSlotAreaNotice();
                            return;
                        }
                            
                    }
                }
                //만약 Contents 의 태그에 따라 놓을지 말지 결정
                //if()
            }
            eventData.pointerDrag.GetComponent<SlotDragHandler>().onRightPlace = true;
            Debug.Log("슬롯 장착 완료, 이름: "+eventData.pointerDrag.name);

            //만약 SubmitSlot 오브젝트에 자식 오브젝트가 있다면 이미 장착된 슬롯을 수정하는 경우이므로 기존 장착 슬롯 삭제
            if (this.transform.childCount > 0)
            {
                Transform[] childList = this.transform.GetComponentsInChildren<Transform>();

                if (childList != null)
                {
                    for(int i = 1; i < childList.Length; i++)
                    {
                        if (childList[i] != transform)
                            Destroy(childList[i].gameObject);
                    }
                }
            }

            //드래그중인 대상의 부모를 현재 오브젝트로 설정하고, 위치를 현재 오브젝트 위치와 동일하게 설정
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.transform.GetChild(0).GetComponent<Text>().text = "<size=25>"+ eventData.pointerDrag.transform.GetChild(0).GetComponent<Text>().text+"</size>";
            eventData.pointerDrag.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 87);
            //eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
            eventData.pointerDrag.GetComponent<RectTransform>().position = new Vector2(rect.position.x, rect.position.y + 43);
        }

        else { 
            Debug.Log("슬롯 장착 실패");
            eventData.pointerDrag.GetComponent<SlotDragHandler>().onRightPlace = false;
            //eventData.pointerDrag.SetActive(false);
        }

    }

    public void DeleteSubmitSlot()
    {
        if (this.transform.childCount > 0)
            Destroy(this.transform.GetChild(0).gameObject);
    }
}
