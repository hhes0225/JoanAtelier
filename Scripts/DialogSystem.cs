using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public struct Speaker
{
    public Image spriteRenderer; //ĳ���� �̹���
    public Text textName;//������� ĳ���� �̸� ��� text UI
    public Text textDialog;//������ text UI
    public GameObject objectArrow;//��� �Ϸ�Ǿ��� �� ��µǴ� Ŀ�� ������Ʈ
}

[System.Serializable]
public struct DialogData
{
    public int effect;//�̸�, ��� ����� ���� DialogSystem�� speakers �迭 ����
    public string name;//ĳ���� �̸�
    [TextArea(3, 5)]
    public string dialog;//���
}

public class DialogSystem : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private int branch;
    [SerializeField]
    private DialogDB dialogDB;

    [SerializeField]
    private Speaker[] speakers;//��ȭ�� �����ϴ� ĳ���͵� UI �迭
    [SerializeField]
    private DialogData[] dialogs;//���� �б��� ��� ��� �迭
    DialogData testDialog;
    [SerializeField]
    private bool isAutoStart = true;//�ڵ� ���� ����
    private bool isFirst = true; //���� 1ȸ�� ȣ���ϱ� ���� ����
    private int currentDialogIndex = -1;//���� ��� ����
    private int currentSpeakerIndex = 0;//���� ���ϴ� ȭ���� speakers �迭 ����

    //�ؽ�Ʈ Ÿ���� ȿ��
    private float typingSpeed = 0.05f;//��� �ӵ�
    private bool isTypingEffect = false;//ȿ�� ������ΰ�?

    //ĳ���� ���� �� ���̵� ��
    GameObject fadein;

    private void Awake()
    {
        int index = 0;
        
        for (int i = 0; i < dialogDB.Entities.Count; i++)
        {
            

            //���� ���� ��������(�б� 1, 2, 3.... �б⸶�� ���� ������ �����´�)
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
        //��� ��ȭ ���� ���ӿ�����Ʈ ��Ȱ��ȭ
        for(int i = 0; i < speakers.Length; ++i)
        {
            SetActiveObjects(speakers[i], false);
            if (speakers[i].spriteRenderer != null) { 
            //ĳ���� �̹����� �Ⱥ��̵��� ����
            if(speakers[i].spriteRenderer!=null)
                speakers[i].spriteRenderer.gameObject.SetActive(false);
            }
        }
    }

    public bool UpdateDialog()
    {
        //��� �б� ���۵� �� 1ȸ�� ȣ��
        if (isFirst == true)
        {
            //�ʱ�ȭ, ĳ���� �̹����� Ȱ��ȭ�ϰ� ��� ���� UI ��Ȱ��ȭ
            Setup();

            //�ڵ� ���(isAutoStart==true)���� �����Ǿ������� ù ��° ��� ���
            if (isAutoStart) SetNextDialog();
            isFirst = false;
        }

        //Debug.Log("UpdateDialog�� ȣ��Ǵ°�?");
        if (dialogs.Length <= currentDialogIndex + 1) {
            Debug.Log("���⼭ true�� ����");
            return true;
        }
        else {
            //Debug.Log("���⼭ false�� ����");
            return false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //�ؽ�Ʈ Ÿ���� ȿ�� ������϶� ���콺 ���� Ŭ���ϸ� Ÿ���� ȿ�� ����
        if (isTypingEffect == true)
        {
            isTypingEffect = false;

            //Ÿ���� ȿ�� ����, ���� ��� ��ü ���
            StopAllCoroutines();
            //StopCoroutine("OnTypingText");
            Debug.Log("�ؽ�Ʈ �ڷ�ƾ ����");

            speakers[currentSpeakerIndex].textDialog.text = dialogs[currentDialogIndex].dialog;
            Debug.Log("�ؽ�Ʈ �ٽ� ����");

            //��簡 �Ϸ�Ǿ��� �� ��µǴ� Ŀ�� Ȱ��ȭ
            speakers[currentSpeakerIndex].objectArrow.SetActive(true);
            Debug.Log("Ŀ�� Ȱ��ȭ");
            return;
        }
        else
        {
            //��簡 �������� ��� ���� ��� ����
            if (dialogs.Length > currentDialogIndex + 1)
            {
                SetNextDialog();
            }
            //��簡 �� �̻� ���� ��� ��� ������Ʈ ��Ȱ��ȭ�ϰ� true ��ȯ
            else
            {
                //���� ��ȭ�� �����ߴ� ��� ĳ����, ��ȭ ���� UI ������ �ʰ� ��Ȱ��ȭ
                for (int i = 0; i < speakers.Length; ++i)
                {
                    SetActiveObjects(speakers[i], false);
                    //SetActiveObjects()�� ĳ���� �̹��� ������ �ʰ� �ϴ� �κ��� ���� ������ ���� ȣ��
                    //speakers[i].spriteRenderer.gameObject.SetActive(false);
                }
                //isFirst = true;
            }
        }
    }


    private void SetNextDialog()
    {
        //���� ȭ���� ��ȭ ���� ������Ʈ ��Ȱ��ȭ
        SetActiveObjects(speakers[currentSpeakerIndex], false);

        //���� ��� ����
        currentDialogIndex++;

        //���� ȭ�� ���� ����
        currentSpeakerIndex = dialogs[currentDialogIndex].effect;

        //���� ȭ���� ��ȭ ���� ������Ʈ Ȱ��ȭ
        SetActiveObjects(speakers[currentSpeakerIndex], true);

        if (dialogs[currentDialogIndex].effect == 1)
        {
            speakers[currentSpeakerIndex].spriteRenderer.gameObject.SetActive(true);
            StartCoroutine("FadeInFlowEffect");
        }

        //���� ȭ�� �̸� �ؽ�Ʈ ����
        speakers[currentSpeakerIndex].textName.text = dialogs[currentDialogIndex].name;



        //���� ȭ���� ��� �ؽ�Ʈ ����
        speakers[currentSpeakerIndex].textDialog.text = dialogs[currentDialogIndex].dialog;
        //StartCoroutine("OnTypingText");
    }

    private void SetActiveObjects(Speaker speaker, bool visible)
    {
        speaker.textName.gameObject.SetActive(visible);
        speaker.textDialog.gameObject.SetActive(visible);

        //ȭ��ǥ�� ��簡 ����Ǿ��� ���� Ȱ��ȭ�ϱ� ������ �׻� false
        speaker.objectArrow.SetActive(false);
    }

    IEnumerator OnTypingText()
    {
        int index = 0;

        //�ؽ�Ʈ�� �� ���ھ� Ÿ����ġ�� ���
        while(index < dialogs[currentDialogIndex].dialog.Length){
            speakers[currentSpeakerIndex].textDialog.text = dialogs[currentDialogIndex].dialog.Substring(0, index);
            index++;

            yield return new WaitForSeconds(typingSpeed);
        }

        isTypingEffect = false;

        //��簡 �Ϸ�Ǿ��� �� ��µǴ� Ŀ�� Ȱ��ȭ
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
        //Debug.Log("�� ��ȯ ���� ���̵� ��");
    }

}
