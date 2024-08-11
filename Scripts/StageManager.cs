using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    DataController clearData;

    void Start()
    {
        GameManager.SetResolution();
        //StartCoroutine(SaveDataCoroutine());
        clearData = GameObject.Find("DataController").GetComponent<DataController>();
        GameObject fadeout = GameObject.Find("FadeObject");

        //돌아가기 클릭 시
        GameObject.Find("BackButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            fadeout.SetActive(true);
            fadeout.GetComponent<TransitionEffect>().FadeOut(0);
            //SceneManager.LoadScene("TitleScene");
        });

        //스테이지 1 클릭 시
        GameObject.Find("StageButton1").GetComponent<Button>().onClick.AddListener(() =>
        {
            fadeout.SetActive(true);
            fadeout.GetComponent<TransitionEffect>().FadeOut(2);
        });

        //스테이지 2 클릭 시
        GameObject.Find("StageButton2").GetComponent<Button>().onClick.AddListener(() =>
        {
            fadeout.SetActive(true);
            fadeout.GetComponent<TransitionEffect>().FadeOut(4);
        });
        //스테이지 3 클릭 시
        GameObject.Find("StageButton3").GetComponent<Button>().onClick.AddListener(() =>
        {
            fadeout.SetActive(true);
            fadeout.GetComponent<TransitionEffect>().FadeOut(6);
        });
        //스테이지 4 클릭 시
        GameObject.Find("StageButton4").GetComponent<Button>().onClick.AddListener(() =>
        {
            //fadeout.SetActive(true);
            //fadeout.GetComponent<TransitionEffect>().FadeOut(4);
        });
        //스테이지 5 클릭 시
        GameObject.Find("StageButton5").GetComponent<Button>().onClick.AddListener(() =>
        {
            fadeout.SetActive(true);
            fadeout.GetComponent<TransitionEffect>().FadeOut(8);
        });


        Debug.Log("Before GameData_clearStages: " + clearData.gameData.clearStages);

        if (!clearData.gameData.isClear1)
        {
            Debug.Log("stage 1을 할 차례!");
            
            GameObject.Find("StageButton2").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton3").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton4").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton5").GetComponent<Button>().interactable = false;
        }
        else if (!clearData.gameData.isClear2)
        {
            Debug.Log("stage 2를 할 차례!");

            GameObject.Find("StageButton1").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton2").GetComponent<Button>().interactable = true;
            GameObject.Find("StageButton3").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton4").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton5").GetComponent<Button>().interactable = false;
        }
        else if (!clearData.gameData.isClear3)
        {
            Debug.Log("stage 3을 할 차례!");

            GameObject.Find("StageButton1").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton2").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton3").GetComponent<Button>().interactable = true;
            GameObject.Find("StageButton4").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton5").GetComponent<Button>().interactable = false;
        }
        else if (!clearData.gameData.isClear5)
        {
            Debug.Log("stage 4를 할 차례!...지만 엔딩으로 바로 고");

            GameObject.Find("StageButton1").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton2").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton3").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton4").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton5").GetComponent<Button>().interactable = true;
        }
        /*
        else if (!clearData.gameData.isClear5)
        {
            Debug.Log("stage 5를 할 차례!");

            GameObject.Find("StageButton1").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton2").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton3").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton4").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton5").GetComponent<Button>().interactable = true;
        }
        */
        else if (clearData.gameData.isClear5)
        {
            Debug.Log("클리어 완료!");

            GameObject.Find("StageButton1").GetComponent<Button>().interactable = true;
            GameObject.Find("StageButton2").GetComponent<Button>().interactable = true;
            GameObject.Find("StageButton3").GetComponent<Button>().interactable = true;
            GameObject.Find("StageButton4").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton5").GetComponent<Button>().interactable = true;
        }

    }

    /*
    IEnumerator SaveDataCoroutine()
    {
        clearData = GameObject.Find("DataController").GetComponent<DataController>();
        yield return new WaitForSeconds(1f);

    }
    */

}
