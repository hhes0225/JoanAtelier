using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataController : MonoBehaviour
{
    #region �̱���
    //�̱��� ����
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

    //���� ������ �����̸� ����
    public string GameDataFileName = "SaveFile.json";

    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            //���� ���� �� �ڵ����� ����
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
        Debug.Log("���� �αٵα�");
        LoadGameData();
        Debug.Log("�ε� ����");
        SaveGameData();
        Debug.Log("���̺� ����");
    }

    [SerializeField]
    //����� ���� �ҷ�����
    public void LoadGameData()
    {
        string filePath = Application.dataPath + "/" + GameDataFileName;

        //����� ������ �ִٸ�
        if (File.Exists(filePath)){
            Debug.Log("�ҷ����� ����");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }

        //����� ������ ���ٸ�
        else
        {
            Debug.Log("���ο� ���� ����");
            _gameData = new GameData();
        }
    }

    //���� �����ϱ�
    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.dataPath + "/" + GameDataFileName;

        //�̹� ����� ������ �ִٸ� �����
        File.WriteAllText(filePath, ToJsonData);

        //�ùٸ��� ����Ǿ����� Ȯ��
        Debug.Log("����Ϸ�");
        Debug.Log("Ŭ������ �������� ��: " + gameData.clearStages);
        Debug.Log("stage 1 clear: " + gameData.isClear1);
        Debug.Log("stage 2 clear: " + gameData.isClear2);
        Debug.Log("stage 3 clear: " + gameData.isClear3);
        Debug.Log("stage 4 clear: " + gameData.isClear4);
        Debug.Log("stage 5 clear: " + gameData.isClear5);
    }

    /*
    //���� ���� �� �ڵ�����
    private void OnApplicationQuit()
    {
        SaveGameData();
    }
    */
}
