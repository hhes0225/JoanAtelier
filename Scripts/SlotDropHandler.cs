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

    //���콺 �����Ͱ� ���� ������ ���� ���� ���η� �� �� 1ȸ ȣ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        //���� ���� ��Ӱ� ����
        image.color = new Color(150f / 255f, 120f / 255f, 100f / 255f);
    }

    //���콺 �����Ͱ� ���� ���� �������� �� 1ȸ ȣ��
    public void OnPointerExit(PointerEventData eventData)
    {
        //���� ���� ��� ����
        image.color = new Color(210f / 255f, 180f / 255f, 160f / 255f);
    }

    //���� ������ ���� ���� ���ο��� ������� �� 1ȸ ȣ��
    public void OnDrop(PointerEventData eventData)
    {
        //eventData.pointerDrag�� ���� �巡������ ���(=����)
        if (eventData.pointerDrag != null && CompareTag(eventData.pointerDrag.gameObject.tag))
        {
            if (SceneManager.GetActiveScene().name=="Chapter2Scene")
            {
                GameObject submitArea = GameObject.Find("SubmitArea");
                if (submitArea.transform.GetChild(0).childCount != 0) {
                    Debug.Log(submitArea.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text);
                    if (submitArea.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("ù��° �׸�")) 
                    {
                        if (eventData.pointerDrag.transform.parent.tag == "Picture2Slots" && eventData.pointerDrag.gameObject.tag=="EvidenceSlot") {
                            Debug.Log("�׸� 1 ���� ���Ը� �������� ");
                            GameObject.Find("2StageScene").GetComponent<Chapter2Manager>().showWrongSlotAreaNotice();
                            return;
                        }
                    }
                    else if (submitArea.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text.Contains("�ι�° �׸�"))
                    {
                        if (eventData.pointerDrag.transform.parent.tag == "Picture1Slots" && eventData.pointerDrag.gameObject.tag == "EvidenceSlot")
                        {
                            Debug.Log("�׸� 2 ���� ���Ը� �������� ");
                            GameObject.Find("2StageScene").GetComponent<Chapter2Manager>().showWrongSlotAreaNotice();
                            return;
                        }
                            
                    }
                }
                //���� Contents �� �±׿� ���� ������ ���� ����
                //if()
            }
            eventData.pointerDrag.GetComponent<SlotDragHandler>().onRightPlace = true;
            Debug.Log("���� ���� �Ϸ�, �̸�: "+eventData.pointerDrag.name);

            //���� SubmitSlot ������Ʈ�� �ڽ� ������Ʈ�� �ִٸ� �̹� ������ ������ �����ϴ� ����̹Ƿ� ���� ���� ���� ����
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

            //�巡������ ����� �θ� ���� ������Ʈ�� �����ϰ�, ��ġ�� ���� ������Ʈ ��ġ�� �����ϰ� ����
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.transform.GetChild(0).GetComponent<Text>().text = "<size=25>"+ eventData.pointerDrag.transform.GetChild(0).GetComponent<Text>().text+"</size>";
            eventData.pointerDrag.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 87);
            //eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
            eventData.pointerDrag.GetComponent<RectTransform>().position = new Vector2(rect.position.x, rect.position.y + 43);
        }

        else { 
            Debug.Log("���� ���� ����");
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
