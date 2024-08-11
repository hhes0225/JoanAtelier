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
    //UI가 소속된 최상단 Canvas Transform

    private Transform prevParent;
    //해당 오브젝트 직전에 소속되어 있던 부모 Transform
    private RectTransform rect;
    //UI 위치 제어 위한 RectTransform
    private CanvasGroup canvasGroup;
    //UI 알파값, 상호작용 제어 위한 canvasGroup

    private Vector3 originalPosition;

    //public GameObject

    void Awake()
    {
        canvas = FindObjectOfType<Canvas>().transform;
        originalPosition = this.transform.position;
              
    }

    //drag 시작 시 1회 호출되는 인터페이스
    public void OnBeginDrag(PointerEventData eventData)
    {
        rect = this.GetComponent<RectTransform>();
        canvasGroup = this.GetComponent<CanvasGroup>();

        //드래그 직전 소속된 부모 transform 정보 저장
        prevParent = transform.parent;

        //현재 드래그 중인 UI가 화면 최상단에 출력되도록 하기 위해
        //부모 obj를 Canvas로 설정
        this.transform.SetParent(canvas);
        //가장 앞에 보이도록 마지막 자식으로 설정
        transform.SetAsLastSibling();

        //드래그 가능한 오브젝트가 자식 가지고 있을 수 있어 Canvasgroup사용
        //알파값 0.6으로 설정, 광선 충돌처리 되지 않도록 함
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    //드래그 중일 때 매 프레임마다 호출
    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;

    }

    //드래그 종료 시 1회 호출
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
