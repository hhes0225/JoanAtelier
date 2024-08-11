using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToolDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private bool isDragged = false;
    public bool onRightPlace = false;


    private Transform canvas;
    //UI�� �Ҽӵ� �ֻ�� Canvas Transform

    private Transform prevParent;
    //�ش� ������Ʈ ������ �ҼӵǾ� �ִ� �θ� Transform
    private RectTransform rect;
    //UI ��ġ ���� ���� RectTransform
    private CanvasGroup canvasGroup;
    //UI ���İ�, ��ȣ�ۿ� ���� ���� canvasGroup

    private Vector3 originalPosition;

    //public GameObject

    void Awake()
    {
        canvas = FindObjectOfType<Canvas>().transform;
        originalPosition = this.transform.position;
              
    }

    //drag ���� �� 1ȸ ȣ��Ǵ� �������̽�
    public void OnBeginDrag(PointerEventData eventData)
    {
        rect = this.GetComponent<RectTransform>();
        canvasGroup = this.GetComponent<CanvasGroup>();

        //�巡�� ���� �Ҽӵ� �θ� transform ���� ����
        prevParent = transform.parent;

        //���� �巡�� ���� UI�� ȭ�� �ֻ�ܿ� ��µǵ��� �ϱ� ����
        //�θ� obj�� Canvas�� ����
        this.transform.SetParent(canvas);
        //���� �տ� ���̵��� ������ �ڽ����� ����
        transform.SetAsLastSibling();

        //�巡�� ������ ������Ʈ�� �ڽ� ������ ���� �� �־� Canvasgroup���
        //���İ� 0.6���� ����, ���� �浹ó�� ���� �ʵ��� ��
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    //�巡�� ���� �� �� �����Ӹ��� ȣ��
    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;

    }

    //�巡�� ���� �� 1ȸ ȣ��
    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent == canvas)
        {
            transform.SetParent(prevParent);
            transform.position = originalPosition;

            if (this.gameObject.name.Contains("Choice1"))
            {
                gameObject.transform.SetAsFirstSibling();
            }
            else if (this.gameObject.name.Contains("Choice2"))
            {
                gameObject.transform.SetAsLastSibling();
            }
        }
        
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
        isDragged = true;
    }


}
