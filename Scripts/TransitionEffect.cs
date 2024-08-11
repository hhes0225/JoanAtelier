using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionEffect : MonoBehaviour
{
    GameObject EffectPanel; //�ǳ� ������Ʈ
    Image image;    //�ǳ� �̹���
    int currentScene;

    float time = 0f;
    float F_time = 1f;

    //�� ��ȯ �� ���̵� ��
    public void FadeIn()
    {
        StartCoroutine(FadeInEffect());
    }

    //�� ��ȯ �� ���̵� �ƿ� + ���� �� ��ȯ
    public void FadeOut(int prevNext)
    {
        StartCoroutine(FadeOutEffect(prevNext));
    }
    
    //���� �� ��ȯ X, �׳� �� ������ ���̵� �ξƿ�
    public void FadeInFlow()
    {
        StartCoroutine(FadeInFlowEffect());
    }

    //�� ��ȯX
    public void FadeOutFlow()
    {
        StartCoroutine(FadeOutFlowEffect());
    }

    IEnumerator FadeInEffect()
    {
        time = 0f;
        Color color = image.color;

        while (color.a > 0f)
        {
            time += Time.deltaTime / F_time;
            color.a = Mathf.Lerp(1, 0, time);
            image.color = color;
            yield return null;
        }

        time = 0f;
        yield return new WaitForSeconds(0.2f);
        EffectPanel.SetActive(false);
    }

    IEnumerator FadeOutEffect(int prevNext)
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        time = 0f;

        Color color = image.color;

        while (color.a < 1f)
        {
            time += Time.deltaTime / F_time;
            color.a = Mathf.Lerp(0, 1, time);
            image.color = color;
            yield return null;
        }

        time = 0f;
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadSceneAsync(prevNext);
    }

    IEnumerator FadeInFlowEffect()
    {
        time = 0f;
        Color color = image.color;

        while (color.a > 0f)
        {
            time += Time.deltaTime / F_time;
            color.a = Mathf.Lerp(1, 0, time);
            image.color = color;
            yield return null;
        }

        time = 0f;
        //yield return new WaitForSeconds(0.2f);
        EffectPanel.SetActive(false);
        Debug.Log("�� ��ȯ ���� ���̵� ��");
    }

    IEnumerator FadeOutFlowEffect()
    {
        time = 0f;
        Color color = image.color;

        while (color.a < 1f)
        {
            time += Time.deltaTime / F_time;
            color.a = Mathf.Lerp(0, 1, time);
            image.color = color;
            yield return null;
        }

        time = 0f;
        Debug.Log("�� ��ȯ ���� ���̵�  �ƿ�");
        yield return new WaitForSeconds(0.7f);
    }

    void Awake()
    {
        EffectPanel = this.gameObject;
        //��ũ��Ʈ ������ ������Ʈ. �� ������Ʈ�� ����Ʈ ������ �ǳ��̴�.
        image = EffectPanel.GetComponent<Image>();
        //�ǳ� ������Ʈ �̹��� ����
        Color color = image.color;
        color.a = 1f;
        image.color = color;
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    private void Start()
    {
        //Debug.Log("���� �� ��ȣ: " + currentScene);
        
        FadeIn();
       
    }
}
