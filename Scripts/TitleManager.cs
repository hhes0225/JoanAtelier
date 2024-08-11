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

        //�����ϱ� ��ư Ŭ�� ��
        GameObject.Find("StartBtn").GetComponent<Button>().onClick.AddListener(() => {
            fadeout.SetActive(true);
            fadeout.GetComponent<TransitionEffect>().FadeOut(1);
        });

        //�����ϱ� ��ư Ŭ�� ��
        GameObject.Find("EndBtn").GetComponent<Button>().onClick.AddListener(() => {
            Application.Quit();
        });

        //�������� Ŭ�� ��
        var makerBtn = GameObject.Find("MakerBtn").GetComponent<Button>();
        var makerPanel = GameObject.Find("TitleScene").GetComponent<Transform>().GetChild(5);

        makerPanel.gameObject.SetActive(false);
        makerBtn.onClick.AddListener(() => {
            if (!isMakerBtnOpen)
            {
                Debug.Log("isMakerBtnOpen==false �̰� ��� true�� �ƽ��ϴ�");
                makerPanel.gameObject.SetActive(true);
                isMakerBtnOpen = true;

                GameObject.Find("MakerCloseBtn").GetComponent<Button>().onClick.AddListener(() => {
                    if (isMakerBtnOpen)
                    {
                        Debug.Log("isMakerBtnOpen==true �̰� ��� false�� �ƽ��ϴ�");
                        makerPanel.gameObject.SetActive(false);
                        isMakerBtnOpen = false;
                    }
                });
            }
        });

        //ȯ�漳�� Ŭ�� ��
        var settingBtn = GameObject.Find("SettingBtn").GetComponent<Button>();
        var settingPanel = GameObject.Find("TitleScene").GetComponent<Transform>().GetChild(6);

        settingPanel.gameObject.SetActive(false);
        settingBtn.onClick.AddListener(() => {
            if (!isSettingBtnOpen)
            {
                
                Debug.Log("isSettingBtnOpen==false �̰� ��� true�� �ƽ��ϴ�");
                settingPanel.gameObject.SetActive(true);
                isSettingBtnOpen = true;

                GameObject.Find("SettingCloseBtn").GetComponent<Button>().onClick.AddListener(() => {
                    if (isSettingBtnOpen)
                    {
                        Debug.Log("isSettingBtnOpen==true �̰� ��� false�� �ƽ��ϴ�");
                        settingPanel.gameObject.SetActive(false);
                        isSettingBtnOpen = false;
                    }
                });
            }
        });



    }
}
