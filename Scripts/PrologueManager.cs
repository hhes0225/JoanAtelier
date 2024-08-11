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
    private string[] storyText = {"�⳪�� ������ �ô밡 ������, ������ ���󿡼� ��ȭ�� �����ϴ� ������� �ձ�.",
                                    "�ձ��� ������ ���� �ƽ�Ʈ���̾ƿ� ������ �絵���� ����� �ƽ��׸� ���� �����ϰ�,",
                                    "����, ������ ��ȣ��� �������� �ʿ��� ������ ������ �������� �м��ϴ�\n�������� ��Ʈ�� �����Ѵ�.",
                                    "�������ڵ��� ���� �����Ͽ�, �����ڵ�� �����Ѵ�.",
                                    "��ȭ�� �ҿ뵹�� �ӿ���, ��Ʋ������ ��ϴ� ���´԰� ����.",
                                    "�ð��� �귯, ������ ���´��� ��Ʋ������ �����ް� �ȴ�."};
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

        //ȭ��(�� ������ ��ư) Ŭ�� ��
        GameObject.Find("ConvertBtn").GetComponent<Button>().onClick.AddListener(() => {
            Debug.Log("ȭ�� �ѱ�� ��ư Ŭ��, ��ũ��Ʈ ��ȣ - "+i);

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
                Debug.Log("��ũ��Ʈ ��ü��� �Ϸ�!");
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
