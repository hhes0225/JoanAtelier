using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionEffect : MonoBehaviour
{
    GameObject EffectPanel; //판넬 오브젝트
    Image image;    //판넬 이미지
    int currentScene;

    float time = 0f;
    float F_time = 1f;

    //씬 전환 후 페이드 인
    public void FadeIn()
    {
        StartCoroutine(FadeInEffect());
    }

    //씬 전환 전 페이드 아웃 + 실제 씬 전환
    public void FadeOut(int prevNext)
    {
        StartCoroutine(FadeOutEffect(prevNext));
    }
    
    //실제 씬 전환 X, 그냥 한 씬에서 페이드 인아웃
    public void FadeInFlow()
    {
        StartCoroutine(FadeInFlowEffect());
    }

    //씬 전환X
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
        Debug.Log("씬 전환 없는 페이드 인");
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
        Debug.Log("씬 전환 없는 페이드  아웃");
        yield return new WaitForSeconds(0.7f);
    }

    void Awake()
    {
        EffectPanel = this.gameObject;
        //스크립트 참조된 오브젝트. 이 오브젝트가 이펙트 적용할 판넬이다.
        image = EffectPanel.GetComponent<Image>();
        //판넬 오브젝트 이미지 참조
        Color color = image.color;
        color.a = 1f;
        image.color = color;
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    private void Start()
    {
        //Debug.Log("현재 씬 번호: " + currentScene);
        
        FadeIn();
       
    }
}
