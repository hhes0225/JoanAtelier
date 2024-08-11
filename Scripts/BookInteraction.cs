using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookInteraction : MonoBehaviour
{
    //settingPanel 버튼(닫기버튼)
    Button closeBtn;
    Text choice1Text;
    Text choice2Text;
    Text contentsText;

    //상호작용 가능 슬롯 내용 입력하기
    [SerializeField]
    private string[] choice1;
    [SerializeField]
    private string[] choice2;

    //상호작용 불가능 슬롯 내용 입력하기
    [SerializeField]
    private string[] contents;

    void Awake()
    {
        //닫기 버튼
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
