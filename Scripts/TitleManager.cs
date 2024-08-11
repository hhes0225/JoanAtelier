using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    bool isMakerBtnOpen = false;
    bool isSettingBtnOpen = false;


    // Start is called before the first frame update
    void Start()
    {
        GameObject fadeout = GameObject.Find("FadeObject");

        //시작하기 버튼 클릭 시
        GameObject.Find("StartBtn").GetComponent<Button>().onClick.AddListener(() => {
            fadeout.SetActive(true);
            fadeout.GetComponent<TransitionEffect>().FadeOut(1);
        });

        //종료하기 버튼 클릭 시
        GameObject.Find("EndBtn").GetComponent<Button>().onClick.AddListener(() => {
            Application.Quit();
        });

        //만든사람들 클릭 시
        var makerBtn = GameObject.Find("MakerBtn").GetComponent<Button>();
        var makerPanel = GameObject.Find("TitleScene").GetComponent<Transform>().GetChild(5);

        makerPanel.gameObject.SetActive(false);
        makerBtn.onClick.AddListener(() => {
            if (!isMakerBtnOpen)
            {
                Debug.Log("isMakerBtnOpen==false 이고 방금 true가 됐습니당");
                makerPanel.gameObject.SetActive(true);
                isMakerBtnOpen = true;

                GameObject.Find("MakerCloseBtn").GetComponent<Button>().onClick.AddListener(() => {
                    if (isMakerBtnOpen)
                    {
                        Debug.Log("isMakerBtnOpen==true 이고 방금 false가 됐습니당");
                        makerPanel.gameObject.SetActive(false);
                        isMakerBtnOpen = false;
                    }
                });
            }
        });

        //환경설정 클릭 시
        var settingBtn = GameObject.Find("SettingBtn").GetComponent<Button>();
        var settingPanel = GameObject.Find("TitleScene").GetComponent<Transform>().GetChild(6);

        settingPanel.gameObject.SetActive(false);
        settingBtn.onClick.AddListener(() => {
            if (!isSettingBtnOpen)
            {
                
                Debug.Log("isSettingBtnOpen==false 이고 방금 true가 됐습니당");
                settingPanel.gameObject.SetActive(true);
                isSettingBtnOpen = true;

                GameObject.Find("SettingCloseBtn").GetComponent<Button>().onClick.AddListener(() => {
                    if (isSettingBtnOpen)
                    {
                        Debug.Log("isSettingBtnOpen==true 이고 방금 false가 됐습니당");
                        settingPanel.gameObject.SetActive(false);
                        isSettingBtnOpen = false;
                    }
                });
            }
        });



    }
}
