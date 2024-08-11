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

        //���ư��� Ŭ�� ��
        GameObject.Find("BackButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            fadeout.SetActive(true);
            fadeout.GetComponent<TransitionEffect>().FadeOut(0);
            //SceneManager.LoadScene("TitleScene");
        });

        //�������� 1 Ŭ�� ��
        GameObject.Find("StageButton1").GetComponent<Button>().onClick.AddListener(() =>
        {
            fadeout.SetActive(true);
            fadeout.GetComponent<TransitionEffect>().FadeOut(2);
        });

        //�������� 2 Ŭ�� ��
        GameObject.Find("StageButton2").GetComponent<Button>().onClick.AddListener(() =>
        {
            fadeout.SetActive(true);
            fadeout.GetComponent<TransitionEffect>().FadeOut(4);
        });
        //�������� 3 Ŭ�� ��
        GameObject.Find("StageButton3").GetComponent<Button>().onClick.AddListener(() =>
        {
            fadeout.SetActive(true);
            fadeout.GetComponent<TransitionEffect>().FadeOut(6);
        });
        //�������� 4 Ŭ�� ��
        GameObject.Find("StageButton4").GetComponent<Button>().onClick.AddListener(() =>
        {
            //fadeout.SetActive(true);
            //fadeout.GetComponent<TransitionEffect>().FadeOut(4);
        });
        //�������� 5 Ŭ�� ��
        GameObject.Find("StageButton5").GetComponent<Button>().onClick.AddListener(() =>
        {
            fadeout.SetActive(true);
            fadeout.GetComponent<TransitionEffect>().FadeOut(8);
        });


        Debug.Log("Before GameData_clearStages: " + clearData.gameData.clearStages);

        if (!clearData.gameData.isClear1)
        {
            Debug.Log("stage 1�� �� ����!");
            
            GameObject.Find("StageButton2").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton3").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton4").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton5").GetComponent<Button>().interactable = false;
        }
        else if (!clearData.gameData.isClear2)
        {
            Debug.Log("stage 2�� �� ����!");

            GameObject.Find("StageButton1").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton2").GetComponent<Button>().interactable = true;
            GameObject.Find("StageButton3").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton4").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton5").GetComponent<Button>().interactable = false;
        }
        else if (!clearData.gameData.isClear3)
        {
            Debug.Log("stage 3�� �� ����!");

            GameObject.Find("StageButton1").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton2").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton3").GetComponent<Button>().interactable = true;
            GameObject.Find("StageButton4").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton5").GetComponent<Button>().interactable = false;
        }
        else if (!clearData.gameData.isClear5)
        {
            Debug.Log("stage 4�� �� ����!...���� �������� �ٷ� ��");

            GameObject.Find("StageButton1").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton2").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton3").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton4").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton5").GetComponent<Button>().interactable = true;
        }
        /*
        else if (!clearData.gameData.isClear5)
        {
            Debug.Log("stage 5�� �� ����!");

            GameObject.Find("StageButton1").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton2").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton3").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton4").GetComponent<Button>().interactable = false;
            GameObject.Find("StageButton5").GetComponent<Button>().interactable = true;
        }
        */
        else if (clearData.gameData.isClear5)
        {
            Debug.Log("Ŭ���� �Ϸ�!");

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
