using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SmallEndingManager : MonoBehaviour
{
    [SerializeField]
    private DialogDB2 dialogDB2;

    [SerializeField]
    private DialogDB3 dialogDB3;

    public List<NewDialog> dialogList;

    private NewDialog tmpDialog;
    public NewDialogManager dialogManager;
    GameObject fadeout;

    int i = 0;
    int branchNum = 0;
    bool isFirst = false;//엑셀 파싱할 때 쓰임

    string thisSceneName;

    int finalScore=0;

    

    // Start is called before the first frame update
    void awake()
    {
        GameManager.SetResolution();
        dialogManager = NewDialogManager.Instance;

        Debug.Log("SmallEndingManager awake 세팅 끝");
    }

    void Start()
    {
        fadeout = GameObject.Find("FadeObject");
        thisSceneName = SceneManager.GetActiveScene().name;
        Debug.Log(thisSceneName);
        switch (thisSceneName)
        {
            case "Chapter2Ending":
                excelParsing(dialogDB2);
                StartCoroutine("Chapter2EndingFlow");
                break;
            case "Chapter3Ending":
                excelParsing(dialogDB3);
                StartCoroutine("Chapter3EndingFlow");
                break;
            default:
                Debug.Log("스몰엔딩 씬 맞음?");
                break;
        }
        
    }


    //스테이지 2 파싱
    void excelParsing(DialogDB2 dialogDB2)
    {

        while (dialogDB2.Entities[i].branch >= 0)
        {
            //Debug.Log("branch: " + dialogDB2.Entities[i].branch);

            //맨 처음에 리스트 생성
            if (!isFirst)
            {
                isFirst = true;

                tmpDialog = new NewDialog(0, new DialogForm(dialogDB2.Entities[i].name, dialogDB2.Entities[i].dialog));

                dialogList.Add(tmpDialog);
                //Debug.Log("첫 배열 추가");
            }
            //branch가 i보다 크면 i를 다음 branch로 넘기고 다음 dialogForm 배열 세팅 
            else if (dialogDB2.Entities[i].branch > branchNum)
            {
                branchNum++;

                tmpDialog = new NewDialog(0, new DialogForm(dialogDB2.Entities[i].name, dialogDB2.Entities[i].dialog));

                dialogList.Add(tmpDialog);
                //Debug.Log("중간 정산: " + dialogList[branchNum-1].dialogForm.Count);
                //Debug.Log("다음 branch element");
            }
            else
            {
                dialogList[branchNum].dialogForm.Add(new DialogForm(dialogDB2.Entities[i].name, dialogDB2.Entities[i].dialog));
                //Debug.Log("branch 변동 X, 대사 추가 "+branchNum);
            }

            i++;
        }
    }


    IEnumerator Chapter2EndingFlow()
    {
        finalScore = DataController.Instance.gameData.stage2score;

        Debug.Log("점수는? " + DataController.Instance.gameData.stage2score);
        if (finalScore == 100)
        {
            //true ending
            dialogManager.StartDialog(dialogList[6]);
            yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);
            DataController.Instance.gameData.trueEnding++;
        }
        else if (finalScore >= 75)
        {
            //normal ending
            dialogManager.StartDialog(dialogList[7]);
            yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);
            DataController.Instance.gameData.normalEnding++;
        }
        else
        {
            //bad ending
            dialogManager.StartDialog(dialogList[8]);
            yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);
            DataController.Instance.gameData.badEnding++;
        }

        Debug.Log("true:" + DataController.Instance.gameData.trueEnding + ", normal:" + DataController.Instance.gameData.normalEnding + ", bad:" + DataController.Instance.gameData.badEnding);

        DataController.Instance.SaveGameData();

        fadeout.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        //스테이지 선택화면으로 이동
        fadeout.GetComponent<TransitionEffect>().FadeOut(1);

    }


    //스테이지 3 파싱
    void excelParsing(DialogDB3 dialogDB3)
    {

        while (dialogDB3.Entities[i].branch >= 0)
        {
            //Debug.Log("branch: " + dialogDB3.Entities[i].branch);

            //맨 처음에 리스트 생성
            if (!isFirst)
            {
                isFirst = true;

                tmpDialog = new NewDialog(0, new DialogForm(dialogDB3.Entities[i].name, dialogDB3.Entities[i].dialog));

                dialogList.Add(tmpDialog);
                //Debug.Log("첫 배열 추가");
            }
            //branch가 i보다 크면 i를 다음 branch로 넘기고 다음 dialogForm 배열 세팅 
            else if (dialogDB3.Entities[i].branch > branchNum)
            {
                branchNum++;

                tmpDialog = new NewDialog(0, new DialogForm(dialogDB3.Entities[i].name, dialogDB3.Entities[i].dialog));

                dialogList.Add(tmpDialog);
                //Debug.Log("중간 정산: " + dialogList[branchNum-1].dialogForm.Count);
                //Debug.Log("다음 branch element");
            }
            else
            {
                dialogList[branchNum].dialogForm.Add(new DialogForm(dialogDB3.Entities[i].name, dialogDB3.Entities[i].dialog));
                //Debug.Log("branch 변동 X, 대사 추가 "+branchNum);
            }

            i++;
        }
    }


    IEnumerator Chapter3EndingFlow()
    {
        finalScore = DataController.Instance.gameData.stage3score;

        Debug.Log("점수는? "+DataController.Instance.gameData.stage3score);
        if (finalScore == 100)
        {
            //true ending
            dialogManager.StartDialog(dialogList[13]);
            yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);
            DataController.Instance.gameData.trueEnding++;

        }
        else if (finalScore >= 75)
        {
            //normal ending
            dialogManager.StartDialog(dialogList[14]);
            yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);
            DataController.Instance.gameData.normalEnding++;
        }
        else
        {
            //bad ending
            dialogManager.StartDialog(dialogList[15]);
            yield return new WaitWhile(() => dialogManager.chatState == NewDialogManager.ChatState.Chat);
            DataController.Instance.gameData.badEnding++;
        }

        Debug.Log("true:" + DataController.Instance.gameData.trueEnding + ", normal:" + DataController.Instance.gameData.normalEnding + ", bad:" + DataController.Instance.gameData.badEnding);

        DataController.Instance.SaveGameData();
        fadeout.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        //스테이지 선택화면으로 이동
        fadeout.GetComponent<TransitionEffect>().FadeOut(1);

    }

}
