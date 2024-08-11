using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    SetVolume slider;
    GameObject pausePanel;
    GameObject settingPanel;
    GameObject fadeout;

    //pausePanel 버튼
    Button resumeBtn;
    Button replayBtn;
    Button settingBtn;
    Button exitBtn;
    
    //settingPanel 버튼(닫기버튼)
    Button closeBtn;

    
    void Awake()
    {
        int lastchild = GameObject.FindGameObjectWithTag("StageManager").GetComponent<Transform>().childCount - 1;
        //Debug.Log("lastchild: " + lastchild);

        pausePanel = GameObject.FindGameObjectWithTag("StageManager").GetComponent<Transform>().GetChild(lastchild-2).gameObject;
        settingPanel = GameObject.FindGameObjectWithTag("StageManager").GetComponent<Transform>().GetChild(lastchild-1).gameObject;

        resumeBtn = pausePanel.GetComponent<Transform>().GetChild(1).GetComponent<Button>();
        replayBtn = pausePanel.GetComponent<Transform>().GetChild(2).GetComponent<Button>();
        settingBtn = pausePanel.GetComponent<Transform>().GetChild(3).GetComponent<Button>();
        exitBtn = pausePanel.GetComponent<Transform>().GetChild(4).GetComponent<Button>();

        closeBtn = settingPanel.GetComponent<Transform>().GetChild(1).GetComponent<Button>();

        //판넬들은 처음에 다 비활성화 상태로 초기화
        //pausePanel.SetActive(false);
        settingPanel.SetActive(false);

        //씬 전환용 페이드 아웃
        fadeout = GameObject.FindGameObjectWithTag("StageManager").GetComponent<Transform>().GetChild(lastchild).gameObject;

        Debug.Log("PauseController awake 세팅 끝");
    }

    void Start()
    {
        resumeBtn.onClick.AddListener(() =>
        {
            pausePanel.SetActive(false);
        });

        /*
        replayBtn.onClick.AddListener(() =>
        {
            StopAllCoroutines();
            GameObject.Find("1StageScene").GetComponent<Chapter1Manager>().StartTutorialCoroutine();
        });
        */

        settingBtn.onClick.AddListener(() => {
            pausePanel.SetActive(false);
            settingPanel.SetActive(true);
        });

        exitBtn.onClick.AddListener(() => {
            fadeout.SetActive(true);
            fadeout.GetComponent<TransitionEffect>().FadeOut(1);
        });

        closeBtn.onClick.AddListener(() =>
        {
            settingPanel.SetActive(false);
            pausePanel.SetActive(true);
        });

        Debug.Log("PauseController start 세팅 끝");
    }

}
