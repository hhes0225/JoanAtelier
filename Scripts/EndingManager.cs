using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*�������� �߰��� ��� ���� ���� �����ϱ�*/

public class EndingManager : MonoBehaviour
{
    public Text message;
    public Image cutSceneImage;
    GameObject fadeout, fadeout1;

    int i = 0;
    int j = 0;
    private string[] TrueMagicText = {"\"�� �׸��� �������ְ�!\"",
        "\"���� �Ƿ��� �׸��� � ���ڸ� �׸� ��ǰ�̾��°�!\"",
        "\"�� �ʻ�ȭ�� � ��¡�� ������ ���� �����̸� ������ �� �ְڴ°�?\"",
        "�׵��� ����� ���������� �ذ��� ������, �ҹ��� Ÿ�� ��Ʋ������ �������� ���� ����������.",
        "�����ε��� �����̰�, �ٸ� ���������� �Ƿ��ϱ� ���� ã�ƿԴ�.",
        "���´��� ��Ʋ������ �� �̲�� ���ٴ�, �ڶ������� ���ڸ� Ī���ϴ� ������ �����Դ�.",
        "�������� ������ ��ϴ� ��Ʋ������ ��� �ñ��ϴٸ�, ������ ã�ư��ڴٴ� ���뵵 �־���.",
        "����, ��Ƴ�, �ĸ���, �׸��� �ٸ� �Ƿ��ε鵵 �׸� ������ �Ƿ��Ϸ� ���ų�, ��Ȳ�� ���ϰ� ��Ҹ� ������ ���� ã�ƿԴ�.",
        "",
        "��Ʋ������ ���������� �� �� ã�ƿ��� �Ƿ��ε��� �ַ� �����а��� ������̾���.",
        "�������ڿ� ��ī������ �����к� �л����� �Ƿڸ� �ϱ� ���� ã�ƿ�����,",
        "��Ʋ���� �ֺ������� ������ ��ġ�� ������ ������ ���� ���ظ� ����ϴ� ����� �����δ�.",
        "������� ��ü�� ���� ���ź��� ���� ������ �����ϴ� �� Ÿ���� �����п� ��̸� ������ �����Ѵ�.",
        " ",
        "������ �׵��� ���� ��Ƴ��� ������ ������ ������ �д´�.",
        "�׵��� ���� �׸��� �����ϸ� �����п� ū ������ �޾Ұ�, �ź��ٴ� �ΰ� �߽��� �ð����� �׸��� �ٶ󺸰� �Ǿ���.",
        "������ ���õ� �׸��� �м��ϸ�, �����а� ���ݼ��� �����ϰ� �����ε� �׷� ���̴�.",
        "���´��� ��Ʋ����, �׸��� ������ ������ ��Ʋ������ ����.",
        "True Ending 1. ���°� �������� ����, �׸��� ������ ��Ʋ����"};
    private string[] TrueReligionText = {"\"�� �׸��� �������ְ�!\"",
        "\"���� �Ƿ��� �׸��� � ���ڸ� �׸� ��ǰ�ΰ�!\"",
        "\"�� �ʻ�ȭ�� ���� � ��¡�� ������ �� �����־� ���̰ڴ°�!\"",
        "�׵��� ����� ���������� �ذ��� ������, �ҹ��� Ÿ�� ��Ʋ������ �������� ���� ����������.",
        "�����ε��� �����̰�, �ٸ� ���������� �Ƿ��ϱ� ���� ã�ƿԴ�.",
        "���´��� ��Ʋ������ �� �̲�� ���ٴ�, �ڶ������� ���ڸ� Ī���ϴ� ������ �����Դ�.",
        "�������� ������ ��ϴ� ��Ʋ������ ��� �ñ��ϴٸ�, ������ ã�ư��ڴٴ� ���뵵 �־���.",
        "����, ��Ƴ�, �ĸ���, �׸��� �ٸ� �Ƿ��ε鵵 �׸� ������ �Ƿ��Ϸ� ���ų�,  ��Ҹ� ������ ���� ã�ƿԴ�.",
        "",
        "��Ʋ������ ���������� �� �� ã�ƿ��� �Ƿ��ε��� �ַ� �������� ������̾���.",
        "�����ڿ� �Ž��� �����ε��� �Ƿڸ� �ϱ� ���� ã�ƿ�����,",
        "��Ʋ���� �ֺ������� ������ �ؼ��ϰ� �ƽ��׸� ���� ������ �����ϴ� ����� �����δ�.",
        "������� �� ���� ������ �����ִ� ������ �ູ�� �����Ѵ�.",
        " ",
        "������ �׵��� ���� �ĸ��ϰ� ������ �ƽ��׸� �� �̼� ������ �д´�.",
        "�׵��� ���� �׸��� �����ϸ� ������ ū ������ �޾Ұ�, �ΰ����ٴ� ���Ŵ԰� ���� �絵�� �߽��� �ð����� �׸��� �ٶ󺸰� �Ǿ���.",
        "������ ���õ� �׸��� �м��ϸ�, �ƽ��׸� ���� �����ϰ� �����ε� �׷� ���̴�.",
        "���´��� ��Ʋ����, �׸��� ������ ������ ��Ʋ������ ����.",
        "True Ending 2. ���Ű� �ƽ�Ʈ������ ����, �׸��� ������ ��Ʋ����"};
    private string[] GoodText = { "\"�� �ǹ��� ���� �ϴ� �ǹ��ϱ�?\"",
        "\"�Ƹ� �׸� �м����ִ� ���ϰ�?\"",
        "\"������ ��Ʋ������� �˰� �־�!\"",
        "������ ��Ʋ������ �ƴ� ����� �ƴ�, ���� ����� ���� �׷� ���� �Ǿ���.",
        "�׸��� ��Ȯ�� �м����� �մԵ��� �ٽ� �׸��� �Ƿ��Ϸ� ã�ƿ�����, �߸� �м��� �׸��� �մԵ��� ������ �ٽ� ã�ƿ��� �ʾҴ�.",
        "�׷��� ������ ����, ���ο� �մ��� ���Ե��� �ʰ� �ܰ� �մ� ������ ������ �̷������.",
        "�׷��� ������ ��ǵ��� �λ�������, ������� ������ ��Ʋ������ ����ϰ� �־���.",
        "�Ƹ� �׸� �����̳� �м��� �ʿ��ϰ� �ȴٸ� ������� �ѹ��� ������ ��Ʋ������ ���ø��� ã�ư� ���̴�.",
        "������ ��Ʋ������ ������ �ʾҴ�.",
        "������ ��Ʋ���� ������ ���� �� ������ ���̴�.",
        "Good Ending. ������ ��Ʋ����"};
    private string[] NormalText = {"\"�� �ǹ��� ���� �ϴ� �ǹ��ϱ�?\"",
        "\"�׷���, �� �𸣰ڳ�.\"",
        "������ ��Ʋ������ �ƴ� ����� �ƴ�, ���� ����� ���� �׷� ���� �Ǿ���.",
        "�׸��� ��Ȯ�� �м����� �մԵ��� �ٽ� �׸��� �Ƿ��Ϸ� ã�ƿ�����, �߸� �м��� �׸��� �մԵ��� �ٽô� ã�ƿ��� �ʾҴ�.",
        "�׷��� ������ ����, ���ο� �մ��� ���Ե��� �ʰ� �ܰ� �մ� ������ ������ �̷������.",
        "������� ��Ʋ���� �ǹ��� ������ �ϴ� ������ �� �𸣰� �����ƴ�.",
        "�׷��� ��Ʋ������ ���� �� �ڸ��� �帴�ϰ� ������ ���̴�.",
        "�ִ� �� ���� ��, ������ ������ó��.",
        "Normal Ending. ������ ��Ʋ����"};
    private string[] BadText = { "\"���͸�!\"", "\"�� ��Ʋ���� ������ �����̾�!\"",
        "\"���⿡ �Ƿ��ߴ� �׸��� ��ǰ�̶�� �ؼ� ó���ߴµ� ��ǰ�̾��ٰ�!\"",
        "\"�� ���Ŵ��� �׸��� �� �˰� �Ĵ� ���� �� �׸��� �̱��� �׸��̾���!\"",
        "\"�� �������� �� ������!\"",
        "�߸��� ���� ����� ���Ȱ� ��Ʋ������ �Ǹ��� ���� �׿�����.",
        "�Ϻ� �����ε��� ��Ʋ������ ����ߴ�.",
        "�� �����, ��Ʋ������ ���ֿ��� �������ϰ�, ������ ��Ʋ�������� �Ѱܳ���.",
        "���´��� ��Ʋ�������� ���ο� ����� �Ӹ�Ǿ���.",
        "�������� ��� ����� ��������, ������ ������ �ʾҴ�.",
        "�׷��� ��, �㿡 �ǹ��� ���� ���� ���� �߰��� ����� ���� ���� ������������� �Ű��߰�, �� ��Ʋ������ ������� �������� �����ߴ�.",
        "�Ѱܳ� ������ �������� �ʾҴ�.",
        "������ ó������ �ٽ� ���� ���� ���´Բ� ���Ѵ�.",
        "���� ������� ��Ʋ������ ������ ��� �ִ��� �ƹ��� �� ���̴١�. ",
        "Bad Ending. ������ ����ħ"};


    private string[] imagePathTrueMagic = { "ending/trueMagic1", "ending/trueMagic2" };
    private string[] imagePathTrueReligion = { "ending/trueReligion1", "ending/trueReligion2" };
    private string[] imagePathGood = { "ending/good" };
    private string[] imagePathNormal = { "ending/normal" };
    private string[] imagePathBad = { "ending/bad" };

    void Awake()
    {


    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.SetResolution();
        int i = 0;
        fadeout = GameObject.Find("FadeObject");
        fadeout1 = GameObject.Find("FadeObject (1)");
        cutSceneImage = GameObject.Find("Image").GetComponent<Image>();
        message = GameObject.Find("StoryText").GetComponent<Text>();


        //True Ending: �������� 2�� �� Ʈ�翣��
        if (DataController.Instance.gameData.trueEnding > 1 && DataController.Instance.gameData.badEnding == 0)//True Ending
        {
            if (DataController.Instance.gameData.religion > DataController.Instance.gameData.magic)
            {
                //���� ����
                initDialog(TrueReligionText, imagePathTrueReligion);

            }
            else if (DataController.Instance.gameData.religion < DataController.Instance.gameData.magic)
            {
                //������ ����
                initDialog(TrueMagicText, imagePathTrueMagic);
            }
            else//���� ó��
            {
                Debug.Log("religion, magic ending error!");
            }

        }
        //Bad Ending: �������� 2�� �� ��忣�� 2�� ����
        else if (DataController.Instance.gameData.badEnding > 1 || (DataController.Instance.gameData.badEnding == 1 && DataController.Instance.gameData.normalEnding == 1))//Bad Ending
        {
            initDialog(BadText, imagePathBad);
        }
        else//Normal Ending
        {
            //Good Ending: 2�� �� 1���� True Ending
            if (DataController.Instance.gameData.trueEnding == 1)
            {
                //good ending
                initDialog(GoodText, imagePathGood);

            }
            //Normal Ending: 
            else
            {
                Debug.Log("normal ending");
                initDialog(NormalText, imagePathNormal);
            }


        }

        StartCoroutine(EndingSceneCoroutine());


        //ȭ��(�� ������ ��ư) Ŭ�� ��
        GameObject.Find("ConvertBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            //True Ending: �������� 2�� �� Ʈ�翣��
            if (DataController.Instance.gameData.trueEnding > 1 && DataController.Instance.gameData.badEnding == 0)//True Ending
            {
                if (DataController.Instance.gameData.religion > DataController.Instance.gameData.magic)
                {
                    //���� ����
                    nextDialog(TrueReligionText, imagePathTrueReligion);
                    i = 1;

                }
                else if (DataController.Instance.gameData.religion < DataController.Instance.gameData.magic)
                {
                    //������ ����
                    nextDialog(TrueMagicText, imagePathTrueMagic);
                    i = 1;
                }
                else//���� ó��
                {
                    Debug.Log("religion, magic ending error!");
                }

            }
            //Bad Ending: �������� 2�� �� ��忣�� 2�� ����
            else if (DataController.Instance.gameData.badEnding > 1 || (DataController.Instance.gameData.badEnding == 1 && DataController.Instance.gameData.normalEnding == 1))//Bad Ending
            {
                nextDialog(BadText, imagePathBad);
                i = 1;
            }
            else//Normal Ending
            {
                //Good Ending: 2�� �� 1���� True Ending
                if (DataController.Instance.gameData.trueEnding == 1 && DataController.Instance.gameData.normalEnding == 1)
                {
                    //good ending
                    nextDialog(GoodText, imagePathGood);
                    i = 1;

                }
                //Normal Ending: 
                else
                {
                    Debug.Log("normal ending");
                    nextDialog(NormalText, imagePathNormal);
                    i = 1;
                }
            }



        });
    }

    void initDialog(string[] storyText, string[] imagePath)
    {
        message.text = storyText[i];
        i++;
        cutSceneImage.sprite = Resources.Load<Sprite>(imagePath[j]) as Sprite;
    }

    void nextDialog(string[] storyText, string[] imagePath)
    {
        Debug.Log("���� Ŭ��!");
        if (i < storyText.Length)
        {
            fadeout1.SetActive(true);
            fadeout1.GetComponent<TransitionEffect>().FadeOutFlow();
            message.text = storyText[i];
            if (storyText[i] == " ")
            {
                j++;
                fadeout.SetActive(true);
                fadeout.GetComponent<TransitionEffect>().FadeOutFlow();
                fadeout.GetComponent<TransitionEffect>().FadeInFlow();
            }

            cutSceneImage.sprite = Resources.Load<Sprite>(imagePath[j]) as Sprite;
            if (storyText[i] == " ")
            {
                fadeout.GetComponent<TransitionEffect>().FadeInFlow();
            }
            fadeout1.GetComponent<TransitionEffect>().FadeInFlow();
            i++;

        }
        else
        {
            Debug.Log("��ũ��Ʈ ��ü��� �Ϸ�!");
            DataController.Instance.gameData.isClear5 = true;
            DataController.Instance.gameData.trueEnding = 0;
            DataController.Instance.gameData.normalEnding = 0;
            DataController.Instance.gameData.badEnding = 0;
            DataController.Instance.gameData.religion = 0;
            DataController.Instance.gameData.magic = 0;
            DataController.Instance.SaveGameData();
            fadeout.SetActive(true);
            fadeout.GetComponent<Transform>().SetSiblingIndex(4);
            fadeout.GetComponent<TransitionEffect>().FadeOut(1);
        }
    }

    IEnumerator EndingSceneCoroutine()
    {
        yield return new WaitForSeconds(1f);
        fadeout.GetComponent<Transform>().SetSiblingIndex(3);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DataController.Instance.gameData.isClear5 = true;
            DataController.Instance.gameData.trueEnding = 0;
            DataController.Instance.gameData.normalEnding = 0;
            DataController.Instance.gameData.badEnding = 0;
            DataController.Instance.gameData.religion = 0;
            DataController.Instance.gameData.magic = 0;
            DataController.Instance.SaveGameData();
            fadeout.SetActive(true);
            fadeout.GetComponent<Transform>().SetSiblingIndex(4);
            fadeout.GetComponent<TransitionEffect>().FadeOut(1);
        }
    }
}
