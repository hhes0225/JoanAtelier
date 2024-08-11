using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


//�� ��ũ��Ʈ�� PictureInteraction.cs�� ��ü�ؼ� �� �̻� ������ �ʴ� ��ũ��Ʈ��
public class ToolDropHandler : MonoBehaviour, IDropHandler
{
    string slotAddContents;

    [SerializeField]
    private GameObject[] evidenceSlots;
    
    //�׸� ������ ���� ������� �� 1ȸ ȣ��
    public void OnDrop(PointerEventData eventData)
    {
        if (Chapter1Manager.quizNum == 0)
        {
            if (eventData.pointerDrag.gameObject.name == "Image_machine1")
            {
                Debug.Log("�ȷ��������");
                slotAddContents = "�񸮵�Ÿ������ ���� ��� ����.\n�ϼ����� ���� �����ϴ�.";
            }
            else if (eventData.pointerDrag.gameObject.name == "Image_machine2")
            {
                Debug.Log("�����������");
                slotAddContents = "�߶�Ʈ�ν� �� ��ǰ���� ������";
            }
            else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("������ ���"))
            {
                Debug.Log("�Ź� ���� 1");
                slotAddContents = "������ ������ Ź�� ������ ��ȭ���״�.";
            }
            else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("Ƣ�����"))
            {
                Debug.Log("�Ź� ���� 2");
                slotAddContents = "��ȭ���� ���� ����� ������ �ǰ�, ���ϼ��� ������ �Ѹ��� �Ǿ���.";
            }
            else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("����ȭ�� �־�"))
            {
                Debug.Log("������ ���� 1");
                slotAddContents = "������ ź���� ��¡�ϴ� ������ '��'�̴�.";
            }
            else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("�Ŵ��� ���� �� �ѱ׷�"))
            {
                Debug.Log("������ ���� 2");
                slotAddContents = "������ ���� ������ ������ ��¡�� �Ŵ��� Ȳ�� ��� �����̴�.";
            }
        }
        //�� ��° ������ ����
        else if (Chapter1Manager.quizNum == 1)
        {
            if (eventData.pointerDrag.gameObject.name == "Image_machine1")
            {
                Debug.Log("�ȷ��������");
                slotAddContents = "ũ�����콺 �������� ���� �ݺ� �ȷᰡ �ַ� ����.";
            }
            else if (eventData.pointerDrag.gameObject.name == "Image_machine2")
            {
                Debug.Log("�����������");
                slotAddContents = "600��� �� ��ǰ���� ������.";
            }
            else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("������ ���"))
            {
                Debug.Log("�Ź� ���� 1");
                slotAddContents = "å�� ������ �߽��ϴ� ���������� ��¡�̴�.";
            }
            else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("Ƣ�����"))
            {
                Debug.Log("�Ź� ���� 2");
                slotAddContents = "�������ڴ� �Ĺ��� �������� �������� �� �߿���Ѵ�.";
            }
            else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("����ȭ�� �־�"))
            {
                Debug.Log("������ ���� 1");
                slotAddContents = "���ǵ�� �� �ô뿡�� ����Ƽ�Ƹ� �����ϴ� ���Ŀ� ���� ����Ƽ���� ��¡�� ���Ǿ���.";
            }
            else if (eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Text>().text.Contains("�Ŵ��� ���� �� �ѱ׷�"))
            {
                Debug.Log("������ ���� 2");
                slotAddContents = "������ �������ڵ��� ����Ƽ���� ��¡�� ����Ͽ���.";
            }
        }
        

        //�� ���Կ� ���
        foreach (GameObject i in evidenceSlots)
        {
            //Debug.Log("��ϵ� ������ �̸��� "+i.name);
            if (i.name.Contains("Clone")){
                Debug.Log("��ŵ�� ������ �̸��� " + i.name);
                continue;
            }
            if (i.transform.GetChild(0).GetComponent<Text>().text.Contains("EMPTY"))
            {
                i.transform.GetChild(0).GetComponent<Text>().text = slotAddContents;
                Debug.Log("���Կ� ��ϵ�");
                break;
            }
            else
            {
                continue;
            }
        }
    }
}
