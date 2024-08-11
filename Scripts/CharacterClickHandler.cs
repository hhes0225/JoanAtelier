using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CharacterClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool teacherSaid = false;
    bool mullerSaid = false;
    bool dianaSaid = false;
    bool farhaSaid = false;

    string communicationInfo;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (SceneManager.GetActiveScene().name == "Chapter1Scene")
        {
            FirstStageChange();
        }
        else if (SceneManager.GetActiveScene().name == "Chapter2Scene")
        {
            SecondStageChange();
        }
        else if (SceneManager.GetActiveScene().name == "Chapter3Scene")
        {
            ThirdStageChange();
        }

    }

    #region ��������2
    void FirstStageChange()
    {
        StartCoroutine(FirstStageCoroutine());
    }

    IEnumerator FirstStageCoroutine()
    {
        Debug.Log(this.name);
        if (GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter1Manager>().dialogManager.chatState != NewDialogManager.ChatState.Chat)
        {
            Debug.Log("���� Ŭ����");
            if (this.name.Contains("Character"))
            {
                Debug.Log("���� Ŭ���ǰ� Chararcter contain");
                if (!teacherSaid)
                {
                    GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter1Manager>().dialogManager.StartDialog(GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter1Manager>().dialogList[5]);
                    teacherSaid = true;
                    Debug.Log("���� Ŭ���ǰ� ������ true�� �ٲ�");
                }
            }

        }

        yield return null;
    }
    #endregion

    #region ��������2
    void SecondStageChange()
    {
        StartCoroutine(SecondStageCoroutine());
    }

    IEnumerator SecondStageCoroutine()
    {
        GameObject pictureChange = GameObject.Find("PictureChangeArea");

        Debug.Log(this.name);
        if (GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter2Manager>().dialogManager.chatState != NewDialogManager.ChatState.Chat)
        {
            if (this.name.Contains("muller"))
            {
                if (!mullerSaid)
                {
                    GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter2Manager>().dialogManager.StartDialog(GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter2Manager>().dialogList[2]);
                    mullerSaid = true;

                    //������ ���Կ� ���
                    communicationInfo = "��ǰ���� Ư���� ��¦�̴�\n���� ǥ���� �ִ�.";
                    pictureChange.transform.GetChild(0).GetComponent<PictureInteraction>().registerSlot(communicationInfo);
                    pictureChange.transform.GetChild(1).GetComponent<PictureInteraction>().registerSlot(communicationInfo);
                }
                else
                {
                    //�̹� Ŭ���ؼ� ��ȭ�ϰ� ���� ��ϵǾ��ٸ� ��ȭ �ź� ��Ʈ ����
                    GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter2Manager>().dialogManager.StartDialog(GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter2Manager>().dialogList[3]);
                }
            }

        }

        yield return null;
    }
    #endregion

    #region ��������3
    void ThirdStageChange()
    {
        StartCoroutine(ThirdStageCoroutine());
    }

    IEnumerator ThirdStageCoroutine()
    {
        Debug.Log(this.name);
        if(GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter3Manager>().dialogManager.chatState != NewDialogManager.ChatState.Chat) { 
            if (this.name.Contains("diana"))
            {
                if (!dianaSaid)
                {
                    GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter3Manager>().dialogManager.StartDialog(GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter3Manager>().dialogList[3]);
                    dianaSaid = true;

                    //������ ���Կ� ���
                    communicationInfo = "�ĸ��ϴ� ������ �ƽ��׶� ���� �����̰�, ��Ƴ��� ������ �ݴ�Ǵ� ������ �������̴�.";
                    GameObject.Find("Picture").GetComponent<PictureInteraction>().registerSlot(communicationInfo);
                }
                else
                {
                    GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter3Manager>().dialogManager.StartDialog(GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter3Manager>().dialogList[4]);
                }
            }
            else if (this.name.Contains("farha"))
            {
                if (!farhaSaid)
                {
                    GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter3Manager>().dialogManager.StartDialog(GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter3Manager>().dialogList[6]);
                    farhaSaid = true;

                    //������ ���Կ� ���
                    communicationInfo = "�������� ������ ���� â���� �Ű�ü�̴�.";
                    GameObject.Find("Picture").GetComponent<PictureInteraction>().registerSlot(communicationInfo);
                }
                else
                {
                    GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter3Manager>().dialogManager.StartDialog(GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter3Manager>().dialogList[7]);
                }
            }
        }

        yield return null;
    }
    #endregion
}
