using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
//����ȭ : 

public class GameData
{
    //�� é���� ��ݿ���
    public bool isClear1;
    public bool isClear2;
    public bool isClear3;
    public bool isClear4;
    public bool isClear5;

    //é�ͺ� ����
    public int stage1score;
    public int stage2score;
    public int stage3score;
    public int stage4score;
    public int stage5score;

    public int religion;
    public int magic;

    public int trueEnding;
    public int normalEnding;
    public int badEnding;

    public int clearStages = 0;
}
