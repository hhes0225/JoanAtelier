using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

//ToolDropHandler.cs ��ũ��Ʈ ���� �� ����
public class PictureInteraction : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    private Image image;
    string objectName;

    string slotAddContents;
    [SerializeField]
    private GameObject scrollViewContent;
    private List<GameObject> evidenceSlots;
    Transform[] allChildren; //ScrollView�� Content���� ��� �ڽ� ��ȸ�ϱ� ����

    public static bool isCoroutineCalled = false;

    GameObject pictureChange;


    void Awake()
    {
        isCoroutineCalled = false;
        image = transform.GetComponent<Image>();
        objectName = gameObject.name;
        allChildren = scrollViewContent.transform.GetComponentsInChildren<Transform>();
        evidenceSlots = new List<GameObject>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("objectName: " + gameObject.name+" / objectParentName"+gameObject.transform.parent.name);
        if(objectName !="Picture1"&& objectName != "Picture2" && objectName!="Picture")
            image.color = new Color(0f, 0f, 0f, 100f/255f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (objectName != "Picture1"&& objectName != "Picture2" && objectName != "Picture")
            image.color = new Color(0f, 0f, 0f, 0f);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("isCoroutineCalled ����? " + isCoroutineCalled);

        //���� ���� ����Ʈ �����
        //(instantiate�� ������ �ֵ��� Content�� ��ü�ǰ� ��¥���� SubmitSlot�� ���� Destroy�� ���� �����Ƿ� �������� ����)
        evidenceSlots.Clear();
        allChildren = scrollViewContent.transform.GetComponentsInChildren<Transform>();

        Debug.Log("contents ũ��: "+allChildren.Length);

        foreach (Transform child in allChildren)
        {

            // Debug.Log("transform name: "+transform.name);
            // �ڱ� �ڽ��� ��쿣 ���� 
            //if (child.name == transform.name)
            //    break;

            if (child.tag == "EvidenceSlot")
            {
                evidenceSlots.Add(child.gameObject);
                Debug.Log(child.name + " EvidenceSlot List�� �߰���");
            }
        }

        //���������� ���� �ٸ� ���� drop ��ȣ�ۿ�
        if (SceneManager.GetActiveScene().name == "Chapter1Scene")
            FirstStageDragSetting(eventData);
        else if (SceneManager.GetActiveScene().name == "Chapter2Scene") {
            pictureChange = SecondStagePicture();

            //Debug.Log("���� Ȱ��ȭ �׸� �� : " + pictureChange.transform.GetChild(0).name);
            //Debug.Log("�ι�° ���� bool ���: " + (pictureChange.transform.GetChild(1).name == "Picture2" && pictureChange.transform.GetChild(1).gameObject.activeSelf));

            if (pictureChange.transform.GetChild(0).name == "Picture1" && pictureChange.transform.GetChild(0).gameObject.activeSelf)
                SecondStageDragSettingPicture1(eventData);
            else if (pictureChange.transform.GetChild(1).name == "Picture2" && pictureChange.transform.GetChild(1).gameObject.activeSelf) {
                SecondStageDragSettingPicture2(eventData);
            }
        }
        else if (SceneManager.GetActiveScene().name == "Chapter3Scene")
            ThridStageDragSetting(eventData);


        Debug.Log("isCoroutineCalled: " + PictureInteraction.isCoroutineCalled);

        #region ���ֱ� �ڷ�ƾ ���
        /*
        //�ѹ� ���� ��� �ް� �� Ʋ���� ������ �ٽ� isCoroutineTrue=false�� �ʱ�ȭ
        if (GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter1Manager>().wrongDragCount > 3 && PictureInteraction.isCoroutineCalled)
        {
            PictureInteraction.isCoroutineCalled = false;
        }

        //wrongDragCount�� 0 �ʰ��鼭 ��ⷯ 3�� 0�� ��(�������� 0�� ��->3�� ���) �����ƿ�
        Debug.Log("PictureInteraction����.. wrongcount = " + GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().wrongDragCount);
        Debug.Log("PictureInteraction����.. wrongcount%3 = " + GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().wrongDragCount % 3);
        if (GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().wrongDragCount > 0 && GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().wrongDragCount % 3 == 0 && !isCoroutineCalled) { 
            StartCoroutine(ThreeOut());
            Debug.Log("���ֱ� �ڷ�ƾ ȣ���..");
            Debug.Log("�ڷ�ƾ �� isCoroutineCalled ����? " + PictureInteraction.isCoroutineCalled);
        }
        */
        #endregion

        //�ѹ� ���� ��� �ް� �� Ʋ���� ������ �ٽ� isCoroutineTrue=false�� �ʱ�ȭ
        if (GameManager.wrongDragCount > 3 && PictureInteraction.isCoroutineCalled)
        {
            PictureInteraction.isCoroutineCalled = false;
        }

        //wrongDragCount�� 0 �ʰ��鼭 ��ⷯ 3�� 0�� ��(�������� 0�� ��->3�� ���) �����ƿ�
        Debug.Log("PictureInteraction����.. wrongcount = " + GameManager.wrongDragCount);
        Debug.Log("PictureInteraction����.. wrongcount%3 = " + GameManager.wrongDragCount % 3);
        if (GameManager.wrongDragCount > 0 && GameManager.wrongDragCount % 3 == 0 && !isCoroutineCalled)
        {
            StartCoroutine(ThreeOut());
            Debug.Log("���ֱ� �ڷ�ƾ ȣ���..");
            Debug.Log("�ڷ�ƾ �� isCoroutineCalled ����? " + PictureInteraction.isCoroutineCalled);
        }

        //�� ���Կ� ���
        foreach (GameObject obj in evidenceSlots)
        {
            //string tmpText = obj.transform.GetChild(0).GetComponent<Text>().text;
            if (obj.transform.GetChild(0).GetComponent<Text>().text.Contains("EMPTY") && slotAddContents!="wrong")
            {
                obj.transform.GetChild(0).GetComponent<Text>().text = slotAddContents;

                Debug.Log("���� ��� ���?: " + obj.transform.parent.parent.parent);
                Debug.Log("���� ���빰: "+slotAddContents);
                Debug.Log("���Կ� ��ϵ�");
                break;
            }
            else
            {
                continue;
            }
        }


    }

    //�߸��� ������ 3�� ������ �� ��� ���
    IEnumerator ThreeOut()
    {
        if (SceneManager.GetActiveScene().name == "Chapter1Scene")
        {
            GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().dialogManager.StartDialog(GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().dialogList[14]);
            PictureInteraction.isCoroutineCalled = true;
        }
        else if (SceneManager.GetActiveScene().name == "Chapter2Scene")
        {
            GameObject.Find("2StageScene").GetComponent<Chapter2Manager>().dialogManager.StartDialog(GameObject.Find("2StageScene").GetComponent<Chapter2Manager>().dialogList[10]);
            PictureInteraction.isCoroutineCalled = true;
        }
        else if (SceneManager.GetActiveScene().name == "Chapter3Scene")
        {
            //��Ƴ��� Ȱ��ȭ�Ǿ��ִٸ� ��Ƴ��� ����
            if (GameObject.Find("Characters").transform.GetChild(0).gameObject.activeSelf)
            {
                GameObject.Find("3StageScene").GetComponent<Chapter3Manager>().dialogManager.StartDialog(GameObject.Find("3StageScene").GetComponent<Chapter3Manager>().dialogList[5]);
                PictureInteraction.isCoroutineCalled = true;
            }
            //�ĸ��ϰ� Ȱ��ȭ�Ǿ��ִٸ� ��Ƴ��� ����
            else if (GameObject.Find("Characters").transform.GetChild(1).gameObject.activeSelf)
            {
                GameObject.Find("3StageScene").GetComponent<Chapter3Manager>().dialogManager.StartDialog(GameObject.Find("3StageScene").GetComponent<Chapter3Manager>().dialogList[8]);
                PictureInteraction.isCoroutineCalled = true;
            }
            
        }

        yield return null;
    }

    //�ٸ� ��ũ��Ʈ���� �����ؼ� ���Կ� ����� �� �ִ� �Լ�(CharacterClick �� �ܼ� ����� �� ���)
    public void registerSlot(string communicationInfo)
    {
        if (evidenceSlots == null)
        {
            evidenceSlots = new List<GameObject>();
        }
        Debug.Log(evidenceSlots);

        //���� ���� ����Ʈ �����
        //(instantiate�� ������ �ֵ��� Content�� ��ü�ǰ� ��¥���� SubmitSlot�� ���� Destroy�� ���� �����Ƿ� �������� ����)
        evidenceSlots.Clear();
        allChildren = scrollViewContent.transform.GetComponentsInChildren<Transform>();

        //Debug.Log("contents ũ��: " + allChildren.Length);

        foreach (Transform child in allChildren)
        {
            /*
            Debug.Log("transform name: "+transform.name);
            // �ڱ� �ڽ��� ��쿣 ���� 
            if (child.name == transform.name)
                break;
            */
            if (child.tag == "EvidenceSlot")
            {
                evidenceSlots.Add(child.gameObject);
                Debug.Log(child.name + " EvidenceSlot List�� �߰���");
            }
        }


        //�� ���Կ� ���
        foreach (GameObject obj in evidenceSlots)
        {
            if (obj.transform.GetChild(0).GetComponent<Text>().text.Contains("EMPTY") && communicationInfo != "wrong")
            {
                obj.transform.GetChild(0).GetComponent<Text>().text = communicationInfo;
                Debug.Log("���Կ� ��ϵ�");
                break;
            }
            else
            {
                continue;
            }
        }

    }


    void FirstStageDragSetting(PointerEventData eventData)
    {
        #region ù ��° ����
        //ù ��° ����
        if (Chapter1Manager.quizNum == 0)
        {
            if (eventData.pointerDrag.name == "Image_machine1")
            {
                Debug.Log("�ȷ��������");
                slotAddContents = "�񸮵�Ÿ������ ���� ��� ����.\n�ϼ����� ���� �����ϴ�.";
            }
            else if (eventData.pointerDrag.gameObject.name == "Image_machine2")
            {
                Debug.Log("�����������");
                slotAddContents = "�߶�Ʈ�ν� �� ��ǰ���� ������";
            }

            //����, ���� drop�� ���� ����
            switch (objectName)
            {
                case "Point1Zone"://�������
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("�ȷ� or ���� ���� ���");
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("���� �� ���ɰ� �Ŵ��� ����"))
                    {
                        Debug.Log("������ ���� 2");
                        slotAddContents = "������ ���� ������ ������ ��¡�� �Ŵ��� ��� �����̴�.";
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("��Ÿ������"))
                    {
                        Debug.Log("�Ź� ���� 2");
                        slotAddContents = "��ȭ���� ���� ����� ������ �ǰ�, �¾��� �Ҳ��� ��������� ���� ���� �Ǿ���.";
                    }
                    else
                    {
                        Debug.Log("Ʋ�� ��ġ�� Drop!");
                        GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().increase();
                        slotAddContents = "wrong";
                    }

                    Debug.Log("Point1Zone�� drop"); break;

                case "Point2Zone"://����
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("�ȷ� or ���� ���� ���");
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("����ȭ�� �־�"))
                    {
                        Debug.Log("������ ���� 1");
                        slotAddContents = "������ ź���� ��¡�ϴ� ������ '��'�̴�.";
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("��Ÿ������"))
                    {
                        Debug.Log("�Ź� ���� 2");
                        slotAddContents = "��ȭ���� ���� ����� ������ �ǰ�, �¾��� �Ҳ��� ��������� ���� ���� �Ǿ���.";
                    }
                    else
                    {
                        Debug.Log("Ʋ�� ��ġ�� Drop!");
                        GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().increase();
                        slotAddContents = "wrong";
                    }

                    Debug.Log("Point2Zone�� drop"); break;

                case "Point3Zone"://�����ٶ�
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("�ȷ� or ���� ���� ���");
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("������ ���"))
                    {
                        Debug.Log("�Ź� ���� 1");
                        slotAddContents = "������ ������ ������ �ٶ��� �Ǿ���.";
                    }
                    else
                    {
                        slotAddContents = "wrong";
                        Debug.Log("Ʋ�� ��ġ�� Drop!");
                        GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().increase();
                    }

                    Debug.Log("Point3Zone�� drop"); break;

                default://picture �����̸�
                    if (eventData.pointerDrag.name != "Image_machine1" && eventData.pointerDrag.name != "Image_machine2")
                    {
                        GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().increase();
                        slotAddContents = "wrong";
                        Debug.Log("������ Ư�� �������� Drop ����");
                    }
                    Debug.Log("�׸� ĵ������ drop"); break;
            }
        }
        #endregion

        #region �� ��° ����
        //ù ��° ����
        else if (Chapter1Manager.quizNum == 1)
        {

            if (eventData.pointerDrag.name == "Image_machine1")
            {
                Debug.Log("�ȷ��������");
                slotAddContents = "ũ�����콺 �������� ���� �ݺ� �ȷᰡ �ַ� ����.";
            }
            else if (eventData.pointerDrag.gameObject.name == "Image_machine2")
            {
                Debug.Log("�����������");
                slotAddContents = "600��� �� ��ǰ���� ������.";
            }

            //����, ���� drop�� ���� ����
            switch (objectName)
            {
                case "Point1Zone"://å�� ��
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("�ȷ� or ���� ���� ���");
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("������ ���� ū"))
                    {
                        Debug.Log("�Ź� ���� 1");
                        slotAddContents = "å�� ������ �߽��ϴ� ���������� ��¡�̴�.";
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("������"))
                    {
                        Debug.Log("�Ź� ���� 2");
                        slotAddContents = "�������ڴ� �Ĺ��� �������� �������� �� �߿���Ѵ�.";
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("���ǵ��"))
                    {
                        Debug.Log("������ ���� 1");
                        slotAddContents = "���ǵ�� �� �ô뿡�� ����Ƽ�Ƹ� �����ϴ� ���Ŀ� ���� ����Ƽ���� ������ ��¡�� ���Ǿ���.";
                    }
                    else
                    {
                        Debug.Log("Ʋ�� ��ġ�� Drop!");
                        GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().increase();
                        slotAddContents = "wrong";
                    }

                    Debug.Log("Point1Zone�� drop"); break;

                case "Point2Zone"://�Ǻ�
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("�ȷ� or ���� ���� ���");
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("������ ��������"))
                    {
                        Debug.Log("������ ���� 2");
                        slotAddContents = "������ �������ڵ��� ����Ƽ���� ��¡�� ����Ͽ���.";
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("Ȳ�ݺ� �Ǻ�"))
                    {
                        Debug.Log("�Ź� ���� 1");
                        slotAddContents = "Ȳ�ݺ� �Ǻ��� ���������� ��¡�̴�.";
                    }
                    else
                    {
                        Debug.Log("Ʋ�� ��ġ�� Drop!");
                        GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().increase();
                        slotAddContents = "wrong";
                    }

                    Debug.Log("Point2Zone�� drop"); break;

                case "Point3Zone"://�Ӹ��α� 
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("�ȷ� or ���� ���� ���");
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("������ ��������"))
                    {
                        Debug.Log("������ ���� 2");
                        slotAddContents = "������ �������ڵ��� ����Ƽ���� ��¡�� ����Ͽ���.";
                    }
                    else
                    {
                        slotAddContents = "wrong";
                        Debug.Log("Ʋ�� ��ġ�� Drop!");
                        GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().increase();
                    }

                    Debug.Log("Point3Zone�� drop"); break;

                default://picture �����̸�
                    if (eventData.pointerDrag.name != "Image_machine1" && eventData.pointerDrag.name != "Image_machine2")
                    {
                        GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().increase();
                        slotAddContents = "wrong";
                        Debug.Log("������ Ư�� �������� Drop ����");
                    }
                    Debug.Log("�׸� ĵ������ drop"); break;
            }
        }
        #endregion
    }

    //�� ���� �׸� �� Ȱ��ȭ�Ǿ��ִ� �׸� getter
    GameObject SecondStagePicture()
    {
        GameObject pictureChange = GameObject.Find("PictureChangeArea");

        return pictureChange;
    }

    #region �������� 2 �׸� 1
    void SecondStageDragSettingPicture1(PointerEventData eventData)
    {

        
        //GameObject pictureChange = GameObject.Find("PictureChangeArea");
        //Debug.Log("pictureChange.transform.GetChild(0).name: " + pictureChange.transform.GetChild(0).name);
        //Debug.Log("pictureChange.transform.GetChild(1).name: " + pictureChange.transform.GetChild(1).name);

        #region ��������2 �巡�� ��ȣ�ۿ�
        if (eventData.pointerDrag.name == "Image_machine1")
        {
            Debug.Log("�ȷ��������");
            slotAddContents = "������ ���罺 ��� �ȷᰡ ����";
        }
        else if (eventData.pointerDrag.gameObject.name == "Image_machine2")
        {
            Debug.Log("�����������");
            slotAddContents = "400��� �߹� �׸����� ������";
        }

        //Debug.Log("�� �̸���... "+objectName);

        Debug.Log("***objectName: " + gameObject.name + " / objectParentName: " + gameObject.transform.parent.name);

        //����, ���� drop�� ���� ����
        switch (objectName)
        {
            case "Point1Zone"://Ȳ�� ������
                Debug.Log("�� �̸���... " + objectName);
                if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                {
                    Debug.Log("�ȷ� or ���� ���� ���");
                }

                else
                {
                    Debug.Log("Ʋ�� ��ġ�� Drop!");
                    GameManager.increase();
                    slotAddContents = "wrong";
                }

                Debug.Log("Point1Zone�� drop"); break;

            case "Point2Zone"://�����
                Debug.Log("�� �̸���... " + objectName);
                Debug.Log("�� �̸���... " + eventData.pointerDrag.name);
                if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                {
                    Debug.Log("�ȷ� or ���� ���� ���");
                }
                else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("�к���"))
                {
                    Debug.Log("�Ź� ���� 2");
                    slotAddContents = "�絵 �˷�Ƽ�ƴ� ����������\n����Ǳ⵵ �Ѵ�.";
                }
                else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("���"))
                {
                    Debug.Log("������ ���� 1");
                    slotAddContents = "�絵 �˷�Ƽ�ƴ� �ַ�\n���������� ����ȴ�.";
                }
                else
                {
                    Debug.Log("Ʋ�� ��ġ�� Drop!");
                    GameManager.increase();
                    slotAddContents = "wrong";
                }

                Debug.Log("Point2Zone�� drop"); break;

            case "Point3Zone"://�þ� ������ ��������
                Debug.Log("�� �̸���... " + objectName);
                if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                {
                    Debug.Log("�ȷ� or ���� ���� ���");
                }
                else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("��ݺ�"))
                {
                    Debug.Log("�Ź� ���� 1");
                    slotAddContents = "���Ǵ��� ��� ȭ������ ���� ������\nǥ���ϱ� ���� ����ߴ�.";
                }
                else
                {
                    slotAddContents = "wrong";
                    Debug.Log("Ʋ�� ��ġ�� Drop!");
                    GameManager.increase();
                }

                Debug.Log("Point3Zone�� drop"); break;

            default://picture �����̸�
                if (eventData.pointerDrag.name != "Image_machine1" && eventData.pointerDrag.name != "Image_machine2")
                {
                    GameManager.increase();
                    slotAddContents = "wrong";
                    Debug.Log("������ Ư�� �������� Drop ����");
                }
                Debug.Log("�׸� ĵ������ drop"); break;
        }
        #endregion

    }
    #endregion

    #region �������� 2 �׸� 2
    void SecondStageDragSettingPicture2(PointerEventData eventData)
    {
        //GameObject pictureChange = GameObject.Find("PictureChangeArea");
        //Debug.Log("pictureChange.transform.GetChild(0).name: " + pictureChange.transform.GetChild(0).name);
        //Debug.Log("pictureChange.transform.GetChild(1).name: " + pictureChange.transform.GetChild(1).name);

        Debug.Log("***objectName: " + gameObject.name + " / objectParentName: " + gameObject.transform.parent.name);

        #region ��������2 �巡�� ��ȣ�ۿ�
        if (eventData.pointerDrag.name == "Image_machine1")
        {
            Debug.Log("�ȷ��������");
            slotAddContents = "������ �������Ʈ ���� �ȷᰡ ����";
        }
        else if (eventData.pointerDrag.gameObject.name == "Image_machine2")
        {
            Debug.Log("�����������");
            slotAddContents = "�� 230�� �� �׸����� ������";
        }

        //Debug.Log("�� �̸���... "+objectName);

        //����, ���� drop�� ���� ����
        switch (objectName)
        {
            case "Point1Zone"://Ȳ�� ������
                Debug.Log("�� �̸���... " + objectName);
                if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                {
                    Debug.Log("�ȷ� or ���� ���� ���");
                }

                else
                {
                    Debug.Log("Ʋ�� ��ġ�� Drop!");
                    GameManager.increase();
                    slotAddContents = "wrong";
                }

                Debug.Log("Point1Zone�� drop"); break;

            case "Point2Zone"://�����
                Debug.Log("�� �̸���... " + objectName);
                Debug.Log("�� �̸���... " + eventData.pointerDrag.name);
                if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                {
                    Debug.Log("�ȷ� or ���� ���� ���");
                }
                else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("������ ����"))
                {
                    Debug.Log("������ ���� 1");
                    slotAddContents = "�˷�Ƽ�ƴ� ������ ���� �ñ⿡\n���������� ����Ǵ� �絵�̴�.";
                }
                else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("�γ�"))
                {
                    Debug.Log("������ ���� 2");
                    slotAddContents = "�Ŀ�Ƽ�ƴ� �̳׸��� �¿� ���������� ����Ǵ� �絵�̴�.";
                }
                else
                {
                    Debug.Log("Ʋ�� ��ġ�� Drop!");
                    GameManager.increase();
                    slotAddContents = "wrong";
                }

                Debug.Log("Point2Zone�� drop"); break;

            case "Point3Zone"://�þ� ������ ��������
                Debug.Log("�� �̸���... " + objectName);
                if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                {
                    Debug.Log("�ȷ� or ���� ���� ���");
                }
                else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("��ݺ�"))
                {
                    Debug.Log("�Ź� ���� 1");
                    slotAddContents = "���Ǵ��� ��� ȭ������ ���� ������ ǥ���ϱ� ���� ����ߴ�.";
                }
                else
                {
                    slotAddContents = "wrong";
                    Debug.Log("Ʋ�� ��ġ�� Drop!");
                    GameManager.increase();
                }

                Debug.Log("Point3Zone�� drop"); break;

            default://picture �����̸�
                if (eventData.pointerDrag.name != "Image_machine1" && eventData.pointerDrag.name != "Image_machine2")
                {
                    GameManager.increase();
                    slotAddContents = "wrong";
                    Debug.Log("������ Ư�� �������� Drop ����");
                }
                Debug.Log("�׸� ĵ������ drop"); break;
        }
        #endregion


    }
    #endregion

    #region �������� 2 ���
    void SecondStageDragSetting(PointerEventData eventData)
    {
        GameObject pictureChange = GameObject.Find("PictureChangeArea");
        Debug.Log("pictureChange.transform.GetChild(0).name: " + pictureChange.transform.GetChild(0).name);
        Debug.Log("pictureChange.transform.GetChild(1).name: " + pictureChange.transform.GetChild(1).name);

        //�׸� 1�� Ȱ��ȭ�Ǿ��� ��
        if (pictureChange.transform.GetChild(0).name=="Picture1" && pictureChange.transform.GetChild(0).gameObject.activeSelf)
        {
            #region ��������2 �巡�� ��ȣ�ۿ�
            if (eventData.pointerDrag.name == "Image_machine1")
            {
                Debug.Log("�ȷ��������");
                slotAddContents = "������ ���罺 ��� �ȷᰡ ����";
            }
            else if (eventData.pointerDrag.gameObject.name == "Image_machine2")
            {
                Debug.Log("�����������");
                slotAddContents = "400��� �߹� �׸����� ������";
            }

            //Debug.Log("�� �̸���... "+objectName);

            //����, ���� drop�� ���� ����
            switch (objectName)
            {
                case "Point1Zone"://Ȳ�� ������
                    Debug.Log("�� �̸���... " + objectName);
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("�ȷ� or ���� ���� ���");
                    }
 
                    else
                    {
                        Debug.Log("Ʋ�� ��ġ�� Drop!");
                        GameManager.increase();
                        slotAddContents = "wrong";
                    }

                    Debug.Log("Point1Zone�� drop"); break;

                case "Point2Zone"://�����
                    Debug.Log("�� �̸���... " + objectName);
                    Debug.Log("�� �̸���... " + eventData.pointerDrag.name);
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("�ȷ� or ���� ���� ���");
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("�к���"))
                    {
                        Debug.Log("�Ź� ���� 2");
                        slotAddContents = "<size=27>�絵 �˷�Ƽ�ƴ� ���������� ����Ǳ⵵ �Ѵ�.</size>";
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("���"))
                    {
                        Debug.Log("������ ���� 1");
                        slotAddContents = "�׸� �� �ι��� �絵 �˷�Ƽ���̴�.";
                    }
                    else
                    {
                        Debug.Log("Ʋ�� ��ġ�� Drop!");
                        GameManager.increase();
                        slotAddContents = "wrong";
                    }

                    Debug.Log("Point2Zone�� drop"); break;

                case "Point3Zone"://�þ� ������ ��������
                    Debug.Log("�� �̸���... " + objectName);
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("�ȷ� or ���� ���� ���");
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("��ݺ�"))
                    {
                        Debug.Log("�Ź� ���� 1");
                        slotAddContents = "���Ǵ��� ��� ȭ������ ���� ������ ǥ���ϱ� ���� ����ߴ�.";
                    }
                    else
                    {
                        slotAddContents = "wrong";
                        Debug.Log("Ʋ�� ��ġ�� Drop!");
                        GameManager.increase();
                    }

                    Debug.Log("Point3Zone�� drop"); break;

                default://picture �����̸�
                    if (eventData.pointerDrag.name != "Image_machine1" && eventData.pointerDrag.name != "Image_machine2")
                    {
                        GameManager.increase();
                        slotAddContents = "wrong";
                        Debug.Log("������ Ư�� �������� Drop ����");
                    }
                    Debug.Log("�׸� ĵ������ drop"); break;
            }
            #endregion
        }

        //�׸� 2�� Ȱ��ȭ�Ǿ��� ��
        else if (pictureChange.transform.GetChild(1).name == "Picture2" && pictureChange.transform.GetChild(1).gameObject.activeSelf)
        {
            #region ��������2 �巡�� ��ȣ�ۿ�
            if (eventData.pointerDrag.name == "Image_machine1")
            {
                Debug.Log("�ȷ��������");
                slotAddContents = "������ �������Ʈ ���� �ȷᰡ ����";
            }
            else if (eventData.pointerDrag.gameObject.name == "Image_machine2")
            {
                Debug.Log("�����������");
                slotAddContents = "�� 230�� �� �׸����� ������";
            }

            //Debug.Log("�� �̸���... "+objectName);

            //����, ���� drop�� ���� ����
            switch (objectName)
            {
                case "Point1Zone"://Ȳ�� ������
                    Debug.Log("�� �̸���... " + objectName);
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("�ȷ� or ���� ���� ���");
                    }

                    else
                    {
                        Debug.Log("Ʋ�� ��ġ�� Drop!");
                        GameManager.increase();
                        slotAddContents = "wrong";
                    }

                    Debug.Log("Point1Zone�� drop"); break;

                case "Point2Zone"://�����
                    Debug.Log("�� �̸���... " + objectName);
                    Debug.Log("�� �̸���... " + eventData.pointerDrag.name);
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("�ȷ� or ���� ���� ���");
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("�к���"))
                    {
                        Debug.Log("�Ź� ���� 2");
                        slotAddContents = "<size=27>�絵 �˷�Ƽ�ƴ� ���������� ����Ǳ⵵ �Ѵ�.</size>";
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("���"))
                    {
                        Debug.Log("������ ���� 1");
                        slotAddContents = "�׸� �� �ι��� �絵 �Ŀ�Ƽ���� ���� �ִ�.";
                    }
                    else
                    {
                        Debug.Log("Ʋ�� ��ġ�� Drop!");
                        GameManager.increase();
                        slotAddContents = "wrong";
                    }

                    Debug.Log("Point2Zone�� drop"); break;

                case "Point3Zone"://�þ� ������ ��������
                    Debug.Log("�� �̸���... " + objectName);
                    if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                    {
                        Debug.Log("�ȷ� or ���� ���� ���");
                    }
                    else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("��ݺ�"))
                    {
                        Debug.Log("�Ź� ���� 1");
                        slotAddContents = "���Ǵ��� ��� ȭ������ ���� ������ ǥ���ϱ� ���� ����ߴ�.";
                    }
                    else
                    {
                        slotAddContents = "wrong";
                        Debug.Log("Ʋ�� ��ġ�� Drop!");
                        GameManager.increase();
                    }

                    Debug.Log("Point3Zone�� drop"); break;

                default://picture �����̸�
                    if (eventData.pointerDrag.name != "Image_machine1" && eventData.pointerDrag.name != "Image_machine2")
                    {
                        GameManager.increase();
                        slotAddContents = "wrong";
                        Debug.Log("������ Ư�� �������� Drop ����");
                    }
                    Debug.Log("�׸� ĵ������ drop"); break;
            }
            #endregion
        }


    }
    #endregion

    void ThridStageDragSetting(PointerEventData eventData)
    {
        #region ��������3 �巡�� ��ȣ�ۿ�
        if (eventData.pointerDrag.name == "Image_machine1")
        {
            Debug.Log("�ȷ��������");
            slotAddContents = "��ο� ������ �������Ʈ ���� �ȷᰡ ����";
        }
        else if (eventData.pointerDrag.gameObject.name == "Image_machine2")
        {
            Debug.Log("�����������");
            slotAddContents = "�̳׸��� �¿� ���۵� �׸�";
        }

        //Debug.Log("�� �̸���... "+objectName);

        //����, ���� drop�� ���� ����
        switch (objectName)
        {
            case "Point1Zone"://�� ���� ������
                Debug.Log("�� �̸���... " + objectName);
                if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                {
                    Debug.Log("�ȷ� or ���� ���� ���");
                }
                else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("������ �̱���"))
                {
                    Debug.Log("�Ź� ���� 2");
                    slotAddContents = "<size=27>������Ÿ���� �� ���� �ұ���� ��ƿø���, �̸� ����� ������� �̱����� �Ǿ���.</size>";
                }
                else
                {
                    Debug.Log("Ʋ�� ��ġ�� Drop!");
                    GameManager.increase();
                    slotAddContents = "wrong";
                }

                Debug.Log("Point1Zone�� drop"); break;

            case "Point2Zone"://ū ������
                Debug.Log("�� �̸���... " + objectName);
                Debug.Log("�� �̸���... " + eventData.pointerDrag.name);
                if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                {
                    Debug.Log("�ȷ� or ���� ���� ���");
                }
                else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("������ ��ǰ"))
                {
                    Debug.Log("������ ���� 2");
                    slotAddContents = "���� '�ΰ� ź��'�� �ǹ̸� ������, ������ ������ ��ǰ�� ���� �������� ǥ���Ѵ�.";
                }
                else
                {
                    Debug.Log("Ʋ�� ��ġ�� Drop!");
                    GameManager.increase();
                    slotAddContents = "wrong";
                }

                Debug.Log("Point2Zone�� drop"); break;

            case "Point3Zone"://��ȯ�Ͻ�
                Debug.Log("�� �̸���... " + objectName);
                if (eventData.pointerDrag.name == "Image_machine1" || eventData.pointerDrag.name == "Image_machine2")
                {
                    Debug.Log("�ȷ� or ���� ���� ���");
                }
                else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("��Ƴ� �׳׽ý�"))
                {
                    Debug.Log("�Ź� ���� 1");
                    slotAddContents = "������Ÿ���� �ǽ�����, �׸��ڰ� �¾��� ���� �� ���㿡�� �¾��.";
                }
                else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("������ ȥ��"))
                {
                    Debug.Log("������ ���� 1");
                    slotAddContents = "��ȯ�Ͻ��� ���� �ƽ�Ʈ���̾��� ��¡ �� �ϳ��̸�, ������ �Ͻ��� ȥ���� �������.";
                }
                else
                {
                    slotAddContents = "wrong";
                    Debug.Log("Ʋ�� ��ġ�� Drop!");
                    GameManager.increase();
                }

                Debug.Log("Point3Zone�� drop"); break;

            default://picture �����̸�
                if (eventData.pointerDrag.name != "Image_machine1" && eventData.pointerDrag.name != "Image_machine2")
                {
                    GameManager.increase();
                    slotAddContents = "wrong";
                    Debug.Log("������ Ư�� �������� Drop ����");
                }
                Debug.Log("�׸� ĵ������ drop"); break;
        }
        #endregion
    }


}
