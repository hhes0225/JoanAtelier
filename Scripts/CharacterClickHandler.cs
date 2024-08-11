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

    #region 스테이지2
    void FirstStageChange()
    {
        StartCoroutine(FirstStageCoroutine());
    }

    IEnumerator FirstStageCoroutine()
    {
        Debug.Log(this.name);
        if (GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter1Manager>().dialogManager.chatState != NewDialogManager.ChatState.Chat)
        {
            Debug.Log("스승 클릭됨");
            if (this.name.Contains("Character"))
            {
                Debug.Log("스승 클릭되고 Chararcter contain");
                if (!teacherSaid)
                {
                    GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter1Manager>().dialogManager.StartDialog(GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter1Manager>().dialogList[5]);
                    teacherSaid = true;
                    Debug.Log("스승 클릭되고 변수도 true로 바뀜");
                }
            }

        }

        yield return null;
    }
    #endregion

    #region 스테이지2
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

                    //정보를 슬롯에 등록
                    communicationInfo = "진품에는 특유의 반짝이는\n질감 표현이 있다.";
                    pictureChange.transform.GetChild(0).GetComponent<PictureInteraction>().registerSlot(communicationInfo);
                    pictureChange.transform.GetChild(1).GetComponent<PictureInteraction>().registerSlot(communicationInfo);
                }
                else
                {
                    //이미 클릭해서 대화하고 슬롯 등록되었다면 대화 거부 멘트 띄우기
                    GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter2Manager>().dialogManager.StartDialog(GameObject.FindGameObjectWithTag("StageManager").GetComponent<Chapter2Manager>().dialogList[3]);
                }
            }

        }

        yield return null;
    }
    #endregion

    #region 스테이지3
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

                    //정보를 슬롯에 등록
                    communicationInfo = "파르하는 독실한 아스테라 교의 신자이고, 디아나는 교리에 반대되는 마도학 연구자이다.";
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

                    //정보를 슬롯에 등록
                    communicationInfo = "수정구는 여신의 세계 창조의 매개체이다.";
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
