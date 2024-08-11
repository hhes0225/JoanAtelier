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
    //UI가 소속된 최상단 Canvas Transform
    private Transform prevParent;
    //해당 오브젝트 직전에 소속되어 있던 부모 Transform
    private RectTransform rect;
    //UI 위치 제어 위한 RectTransform
    private CanvasGroup canvasGroup;
    //UI 알파값, 상호작용 제어 위한 CanvasGroup

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

    //drag 시작 시 1회 호출
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isDragged)
        {
            prefabResult = Instantiate(prefab);
            prefabResult.name = prefab.name;
            //Debug.Log("prefabResult: " + prefabResult.name);
            rect = prefabResult.GetComponent<RectTransform>();
            canvasGroup = prefabResult.GetComponent<CanvasGroup>();

            //드래그 직전 소속된 부모 transform 정보 저장
            prevParent = transform.parent;
            //Debug.Log("내 번호는 : " +(childidx));
            //Debug.Log("Begin - isDragged : " + isDragged);

            //현재 드래그 중인 UI가 화면 최상단에 출력되도록 하기 위해
            //부모 obj를 Canvas로 설정
            prefabResult.transform.SetParent(canvas);
            //가장 앞에 보이도록 마지막 자식으로 설정
            prefabResult.transform.SetAsLastSibling();

            //드래그 가능한 오브젝트가 자식 가지고 있을 수 있어 CanvasGroup 사용
            //알파값을 0.6 설정, 광선 충돌처리 되지 않도록 함
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
        }
        
    }

    //드래그 중일 때 매 프레임마다 호출
    public void OnDrag(PointerEventData eventData)
    {
        if(!isDragged)
            rect.position = eventData.position;
    }


    //드래그 종료 시 1회 호출
    public void OnEndDrag(PointerEventData eventData)
    {
       if (!isDragged)
        {

            /*드래그 시작하면 부모가 Canvas로 설정되기 때문에
             드래그를 종료할 때 부모가 Canvas면 아이템 슬롯이 아닌 엉뚱한 곳에
            드롭을 했다는 뜻이기 떄문에 드래그 직전에 소속되어 있던 슬롯으로 슬롯 이동*/
            if (prefabResult.transform.parent == canvas)
            {
                //마지막 소속되어있던 prevParent의 자식으로 설정하고, 해당 위치로 설정
                prefabResult.transform.SetParent(prevParent);
                //Debug.Log("내 이름은: " + this.gameObject.name);
                //Debug.Log("프리팹 이름은: " + prefabResult.gameObject.name);
              
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
                //Debug.Log("바뀐 내 번호는 : " + prefabResult.transform.GetSiblingIndex());

                if(!onRightPlace)
                    Destroy(prefab);
            }

            //알파값을 1로 설정, 광선 충돌처리 되도록 설정
            canvasGroup.alpha = 1.0f;
            canvasGroup.blocksRaycasts = true;
            isDragged = true;
            //Debug.Log("End - isDragged : " + isDragged);
        }
        
    }


}
