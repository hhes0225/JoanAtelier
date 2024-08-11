using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataController : MonoBehaviour
{
    #region 싱글톤
    //싱글톤 선언
    static GameObject _container;
    static GameObject Container
    {
        get { return _container; }
    }

    static DataController _instance;
    public static DataController Instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }
    #endregion

    //게임 데이터 파일이름 설정
    public string GameDataFileName = "SaveFile.json";

    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            //게임 시작 시 자동으로 실행
            if (_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    private void Awake()
    {
        Debug.Log(Application.persistentDataPath);
        Debug.Log("시작 두근두근");
        LoadGameData();
        Debug.Log("로드 성공");
        SaveGameData();
        Debug.Log("세이브 성공");
    }

    [SerializeField]
    //저장된 게임 불러오기
    public void LoadGameData()
    {
        string filePath = Application.dataPath + "/" + GameDataFileName;

        //저장된 게임이 있다면
        if (File.Exists(filePath)){
            Debug.Log("불러오기 성공");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }

        //저장된 게임이 없다면
        else
        {
            Debug.Log("새로운 파일 생성");
            _gameData = new GameData();
        }
    }

    //게임 저장하기
    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.dataPath + "/" + GameDataFileName;

        //이미 저장된 파일이 있다면 덮어쓰기
        File.WriteAllText(filePath, ToJsonData);

        //올바르게 저장되었는지 확인
        Debug.Log("저장완료");
        Debug.Log("클리어한 스테이지 수: " + gameData.clearStages);
        Debug.Log("stage 1 clear: " + gameData.isClear1);
        Debug.Log("stage 2 clear: " + gameData.isClear2);
        Debug.Log("stage 3 clear: " + gameData.isClear3);
        Debug.Log("stage 4 clear: " + gameData.isClear4);
        Debug.Log("stage 5 clear: " + gameData.isClear5);
    }

    /*
    //게임 종료 시 자동저장
    private void OnApplicationQuit()
    {
        SaveGameData();
    }
    */
}
