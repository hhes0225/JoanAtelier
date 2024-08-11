using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrologueManager : MonoBehaviour
{
    public Text message;
    public Image cutSceneImage;
    GameObject fadeout;

    int i = 1;
    private string[] storyText = {"기나긴 전쟁의 시대가 끝나고, 안정된 세상에서 문화가 부흥하는 에스펠라 왕국.",
                                    "왕국과 교단은 여신 아스트라이아와 여신의 사도들을 섬기는 아스테르 교를 지지하고,",
                                    "한편, 여신의 가호라고만 여겨졌던 초월적 현상의 원인을 마력으로 분석하는\n마도학이 싹트기 시작한다.",
                                    "마도학자들은 신을 부정하여, 성직자들과 마찰한다.",
                                    "변화의 소용돌이 속에서, 아틀리에를 운영하는 스승님과 조안.",
                                    "시간이 흘러, 조안은 스승님의 아틀리에를 물려받게 된다."};
    private string[] imagePath = { "prologue/cut1", "prologue/cut2", "prologue/cut3", "prologue/cut4", "prologue/cut5", "prologue/cut6" };

    // Start is called before the first frame update
    void Start()
    {
        GameManager.SetResolution();
        int i = 1;
        fadeout = GameObject.Find("FadeObject");
        message = GameObject.Find("StoryText").GetComponent<Text>();
        cutSceneImage = GameObject.Find("Image").GetComponent<Image>();

        StartCoroutine(PrologueSceneCoroutine());

        //화면(을 가장한 버튼) 클릭 시
        GameObject.Find("ConvertBtn").GetComponent<Button>().onClick.AddListener(() => {
            Debug.Log("화면 넘기기 버튼 클릭, 스크립트 번호 - "+i);

            if (i < storyText.Length)
            {
                fadeout.SetActive(true);
                fadeout.GetComponent<TransitionEffect>().FadeOutFlow();
                message.text = storyText[i];
                cutSceneImage.sprite = Resources.Load<Sprite>(imagePath[i]) as Sprite;
                i++;
                fadeout.GetComponent<TransitionEffect>().FadeInFlow();
            }
            else
            {
                Debug.Log("스크립트 전체출력 완료!");
                fadeout.SetActive(true);
                fadeout.GetComponent<Transform>().SetSiblingIndex(4);
                fadeout.GetComponent<TransitionEffect>().FadeOut(3);
            }

        });
    }

    IEnumerator PrologueSceneCoroutine()
    {
        yield return new WaitForSeconds(1f);
        fadeout.GetComponent<Transform>().SetSiblingIndex(3);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            fadeout.SetActive(true);
            fadeout.GetComponent<Transform>().SetSiblingIndex(4);
            fadeout.GetComponent<TransitionEffect>().FadeOut(3);
        }
    }
}
