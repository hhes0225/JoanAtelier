using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public struct Speaker
{
    public Image spriteRenderer; //캐릭터 이미지
    public Text textName;//대사중인 캐릭터 이름 출력 text UI
    public Text textDialog;//대사출력 text UI
    public GameObject objectArrow;//대사 완료되었을 때 출력되는 커서 오브젝트
}

[System.Serializable]
public struct DialogData
{
    public int effect;//이름, 대사 출력할 현재 DialogSystem의 speakers 배열 순번
    public string name;//캐릭터 이름
    [TextArea(3, 5)]
    public string dialog;//대사
}

public class DialogSystem : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private int branch;
    [SerializeField]
    private DialogDB dialogDB;

    [SerializeField]
    private Speaker[] speakers;//대화에 참여하는 캐릭터들 UI 배열
    [SerializeField]
    private DialogData[] dialogs;//현재 분기의 대사 목록 배열
    DialogData testDialog;
    [SerializeField]
    private bool isAutoStart = true;//자동 시작 여부
    private bool isFirst = true; //최초 1회만 호출하기 위한 변수
    private int currentDialogIndex = -1;//현재 대사 순번
    private int currentSpeakerIndex = 0;//현재 말하는 화자의 speakers 배열 순번

    //텍스트 타이핑 효과
    private float typingSpeed = 0.05f;//재생 속도
    private bool isTypingEffect = false;//효과 재생중인가?

    //캐릭터 등장 시 페이드 인
    GameObject fadein;

    private void Awake()
    {
        int index = 0;
        
        for (int i = 0; i < dialogDB.Entities.Count; i++)
        {
            

            //엑셀 정보 가져오기(분기 1, 2, 3.... 분기마다 엑셀 정보를 가져온다)
            if (dialogDB.Entities[i].branch == branch)
            {
                dialogs[index].effect = dialogDB.Entities[i].effect;
                dialogs[index].name = dialogDB.Entities[i].name;
                dialogs[index].dialog = dialogDB.Entities[i].dialog;
                index++;
            }
           

         } 


        Setup();    
    }

    private void Setup()
    {
        //모든 대화 관련 게임오브젝트 비활성화
        for(int i = 0; i < speakers.Length; ++i)
        {
            SetActiveObjects(speakers[i], false);
            if (speakers[i].spriteRenderer != null) { 
            //캐릭터 이미지도 안보이도록 설정
            if(speakers[i].spriteRenderer!=null)
                speakers[i].spriteRenderer.gameObject.SetActive(false);
            }
        }
    }

    public bool UpdateDialog()
    {
        //대사 분기 시작될 때 1회만 호출
        if (isFirst == true)
        {
            //초기화, 캐릭터 이미지는 활성화하고 대사 관련 UI 비활성화
            Setup();

            //자동 재생(isAutoStart==true)으로 설정되어있으면 첫 번째 대사 재생
            if (isAutoStart) SetNextDialog();
            isFirst = false;
        }

        //Debug.Log("UpdateDialog가 호출되는가?");
        if (dialogs.Length <= currentDialogIndex + 1) {
            Debug.Log("여기서 true됨 ㅅㅂ");
            return true;
        }
        else {
            //Debug.Log("여기서 false됨 ㅅㅂ");
            return false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //텍스트 타이핑 효과 재생중일때 마우스 왼쪽 클릭하면 타이핑 효과 종료
        if (isTypingEffect == true)
        {
            isTypingEffect = false;

            //타이핑 효과 중지, 현재 대사 전체 출력
            StopAllCoroutines();
            //StopCoroutine("OnTypingText");
            Debug.Log("텍스트 코루틴 종료");

            speakers[currentSpeakerIndex].textDialog.text = dialogs[currentDialogIndex].dialog;
            Debug.Log("텍스트 다시 세팅");

            //대사가 완료되었을 때 출력되는 커서 활성화
            speakers[currentSpeakerIndex].objectArrow.SetActive(true);
            Debug.Log("커서 활성화");
            return;
        }
        else
        {
            //대사가 남아있을 경우 다음 대사 진행
            if (dialogs.Length > currentDialogIndex + 1)
            {
                SetNextDialog();
            }
            //대사가 더 이상 없을 경우 모든 오브젝트 비활성화하고 true 변환
            else
            {
                //현재 대화에 참여했던 모든 캐릭터, 대화 관련 UI 보이지 않게 비활성화
                for (int i = 0; i < speakers.Length; ++i)
                {
                    SetActiveObjects(speakers[i], false);
                    //SetActiveObjects()에 캐릭터 이미지 보이지 않게 하는 부분이 없기 때문에 별도 호출
                    //speakers[i].spriteRenderer.gameObject.SetActive(false);
                }
                //isFirst = true;
            }
        }
    }


    private void SetNextDialog()
    {
        //이전 화자의 대화 관련 오브젝트 비활성화
        SetActiveObjects(speakers[currentSpeakerIndex], false);

        //다음 대사 진행
        currentDialogIndex++;

        //현재 화자 순번 설정
        currentSpeakerIndex = dialogs[currentDialogIndex].effect;

        //현재 화자의 대화 관련 오브젝트 활성화
        SetActiveObjects(speakers[currentSpeakerIndex], true);

        if (dialogs[currentDialogIndex].effect == 1)
        {
            speakers[currentSpeakerIndex].spriteRenderer.gameObject.SetActive(true);
            StartCoroutine("FadeInFlowEffect");
        }

        //현재 화자 이름 텍스트 설정
        speakers[currentSpeakerIndex].textName.text = dialogs[currentDialogIndex].name;



        //현재 화자의 대사 텍스트 지정
        speakers[currentSpeakerIndex].textDialog.text = dialogs[currentDialogIndex].dialog;
        //StartCoroutine("OnTypingText");
    }

    private void SetActiveObjects(Speaker speaker, bool visible)
    {
        speaker.textName.gameObject.SetActive(visible);
        speaker.textDialog.gameObject.SetActive(visible);

        //화살표는 대사가 종료되었을 때만 활성화하기 때문에 항상 false
        speaker.objectArrow.SetActive(false);
    }

    IEnumerator OnTypingText()
    {
        int index = 0;

        //텍스트를 한 글자씩 타이핑치듯 재생
        while(index < dialogs[currentDialogIndex].dialog.Length){
            speakers[currentSpeakerIndex].textDialog.text = dialogs[currentDialogIndex].dialog.Substring(0, index);
            index++;

            yield return new WaitForSeconds(typingSpeed);
        }

        isTypingEffect = false;

        //대사가 완료되었을 때 출력되는 커서 활성화
        speakers[currentSpeakerIndex].objectArrow.SetActive(true);
    }

    IEnumerator FadeInFlowEffect()
    {
        float time = 0f;
        float F_time = 0.5f;
        
        Color color = speakers[currentSpeakerIndex].spriteRenderer.color;

        while (color.a < 1f)
        {
            time += Time.deltaTime / F_time;
            color.a = Mathf.Lerp(0, 1, time);
            speakers[currentSpeakerIndex].spriteRenderer.color = color;
            yield return null;
            //dialogs[currentDialogIndex].effect = 0;
        }
        //yield return new WaitForSeconds(0.2f);
        //EffectPanel.SetActive(false);
        //Debug.Log("씬 전환 없는 페이드 인");
    }

}
