using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookInteraction : MonoBehaviour
{
    //settingPanel ��ư(�ݱ��ư)
    Button closeBtn;
    Text choice1Text;
    Text choice2Text;
    Text contentsText;

    //��ȣ�ۿ� ���� ���� ���� �Է��ϱ�
    [SerializeField]
    private string[] choice1;
    [SerializeField]
    private string[] choice2;

    //��ȣ�ۿ� �Ұ��� ���� ���� �Է��ϱ�
    [SerializeField]
    private string[] contents;

    void Awake()
    {
        //�ݱ� ��ư
        closeBtn = transform.GetChild(2).GetComponent<Button>();

        closeBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        /*

        choice1Text= transform.GetChild(0).GetChild(0).GetComponent<Text>();
        choice1Text.text = choice1[0];

        choice2Text = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        choice2Text.text = choice2[0];

        contentsText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        contentsText.text = contents[0];
        */
    }

    
}
