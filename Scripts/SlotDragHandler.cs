using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject _slotBeingDragged;
    Transform _startParent;
    private int childidx;
    private int totalChild;
    private bool isDragged = false;
    public bool onRightPlace = false;

    private Transform canvas;
    //UI�� �Ҽӵ� �ֻ�� Canvas Transform
    private Transform prevParent;
    //�ش� ������Ʈ ������ �ҼӵǾ� �ִ� �θ� Transform
    private RectTransform rect;
    //UI ��ġ ���� ���� RectTransform
    private CanvasGroup canvasGroup;
    //UI ���İ�, ��ȣ�ۿ� ���� ���� CanvasGroup

    public GameObject prefab;
    public GameObject prefabResult;

    void Awake()
    {
        /*
        canvas = FindObjectOfType<Canvas>().transform;
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        */
        canvas = FindObjectOfType<Canvas>().transform;
        prefab = this.gameObject;
        childidx = transform.GetSiblingIndex();
        totalChild = transform.childCount;
    }

    //drag ���� �� 1ȸ ȣ��
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isDragged)
        {
            prefabResult = Instantiate(prefab);
            prefabResult.name = prefab.name;
            //Debug.Log("prefabResult: " + prefabResult.name);
            rect = prefabResult.GetComponent<RectTransform>();
            canvasGroup = prefabResult.GetComponent<CanvasGroup>();

            //�巡�� ���� �Ҽӵ� �θ� transform ���� ����
            prevParent = transform.parent;
            //Debug.Log("�� ��ȣ�� : " +(childidx));
            //Debug.Log("Begin - isDragged : " + isDragged);

            //���� �巡�� ���� UI�� ȭ�� �ֻ�ܿ� ��µǵ��� �ϱ� ����
            //�θ� obj�� Canvas�� ����
            prefabResult.transform.SetParent(canvas);
            //���� �տ� ���̵��� ������ �ڽ����� ����
            prefabResult.transform.SetAsLastSibling();

            //�巡�� ������ ������Ʈ�� �ڽ� ������ ���� �� �־� CanvasGroup ���
            //���İ��� 0.6 ����, ���� �浹ó�� ���� �ʵ��� ��
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
        }
        
    }

    //�巡�� ���� �� �� �����Ӹ��� ȣ��
    public void OnDrag(PointerEventData eventData)
    {
        if(!isDragged)
            rect.position = eventData.position;
    }


    //�巡�� ���� �� 1ȸ ȣ��
    public void OnEndDrag(PointerEventData eventData)
    {
       if (!isDragged)
        {

            /*�巡�� �����ϸ� �θ� Canvas�� �����Ǳ� ������
             �巡�׸� ������ �� �θ� Canvas�� ������ ������ �ƴ� ������ ����
            ����� �ߴٴ� ���̱� ������ �巡�� ������ �ҼӵǾ� �ִ� �������� ���� �̵�*/
            if (prefabResult.transform.parent == canvas)
            {
                //������ �ҼӵǾ��ִ� prevParent�� �ڽ����� �����ϰ�, �ش� ��ġ�� ����
                prefabResult.transform.SetParent(prevParent);
                //Debug.Log("�� �̸���: " + this.gameObject.name);
                //Debug.Log("������ �̸���: " + prefabResult.gameObject.name);
              
                if (this.gameObject.name.Contains("ScrollSlot_Conclusion1"))
                {
                    prefabResult.transform.SetAsFirstSibling();
                }
                else if(this.gameObject.name.Contains("ScrollSlot_Conclusion2"))
                {
                    prefabResult.transform.SetSiblingIndex(1);
                }
                else if (this.gameObject.name.Contains("ScrollSlot_Evidence1"))
                {
                    prefabResult.transform.SetSiblingIndex(2);
                }
                else if (this.gameObject.name.Contains("ScrollSlot_Evidence2"))
                {
                    prefabResult.transform.SetSiblingIndex(3);
                }
                else if (this.gameObject.name.Contains("ScrollSlot_Evidence3"))
                {
                    prefabResult.transform.SetSiblingIndex(4);
                }
                else if (this.gameObject.name.Contains("ScrollSlot_Evidence4"))
                {
                    prefabResult.transform.SetSiblingIndex(5);
                }

                //prefabResult.transform.SetSiblingIndex(childidx-1);
                //Debug.Log("�ٲ� �� ��ȣ�� : " + prefabResult.transform.GetSiblingIndex());

                if(!onRightPlace)
                    Destroy(prefab);
            }

            //���İ��� 1�� ����, ���� �浹ó�� �ǵ��� ����
            canvasGroup.alpha = 1.0f;
            canvasGroup.blocksRaycasts = true;
            isDragged = true;
            //Debug.Log("End - isDragged : " + isDragged);
        }
        
    }


}
