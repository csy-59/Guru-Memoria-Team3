using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    // Start is called before the first frame update
    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] {
            "�ȳ�! �� �������̾�!\n �� ���Ⱑ ó���̱���!",
            "���� �ݰ���!",
            "���� ������ ���� ã���� �� �ְڴ�?",
        });
        talkData.Add(100, new string[] { "�����̴�.", "���ʰ� ����ִ�!" });
        talkData.Add(101, new string[] { "�����̴�.", "������ ����ִ�!" });
        talkData.Add(102, new string[] { "�����̴�.", "������ ����ִ�!" });
        talkData.Add(103, new string[] { "�����̴�.", "������ ���� ����ִ�!" });
        talkData.Add(104, new string[] { "�����̴�.", "������ ���� ����ִ�!" });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][talkIndex];
        }
    }
}
