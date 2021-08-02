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
            "�ȳ�! �� �������̾�!\n�� ���Ⱑ ó���̱���!",
            "���� �ݰ���!\n�ʸ鿡 �̾��ѵ� Ȥ��...",
            "���� ������ ���� ã���� �� �ְڴ�?",
            "���� ���� �߿��� �����ε� �� ���� ��򰡿� �������־�...!",
            "������ ���� �ٸ��� ���ļ� �� ���� ���ٴϴ� ���� �ǳ� �� ������,\n�ʶ�� ������ �ž�!",
            "...�׸��� Ȥ�ö� �䳢 ������ �ִٸ� �ٷ�...",
            "��������",
            "�� ������ ���� ������ �����!!!",
            "�׷� ����� ����!"
        });
        talkData.Add(100, new string[] { "�����̴�.", "������ ����ִ�!" });
        talkData.Add(101, new string[] { "�����̴�.", "������ ���� ����ִ�!" });
        talkData.Add(500, new string[] { "�䳢 �����̴�.",
            "...���ֹ�����?",
            "������ �����.",
            "���� �ؿ� ��Ʈ�� �ִ�!",
            "[���콺 Ŭ������ ����. �Դ޸� ������ ���� ����\n-�͸�-]"
        });
        talkData.Add(501, new string[] { "�䳢 �����̴�.",
            "....�����U�ˤ�",
            "�Ӹ��� ������!!!!!!!!!!!!!!!!!!!!!!",
            "������ �����.",
            "���� �ؿ� ��Ʈ�� �ִ�!",
            "[���� �����. ���� ����������]",
            "[�� �޸� ������ ���� ����. �� �޸� ������ ���� ����. \n" +
            "�� �޸� ������ ���� ����. �� �޸� ������ ���� ����. \n" +
            "�� �޸� ������ ���� ����. �� �޸� ������ ���� ����. \n-�͸�-]"
        });
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
