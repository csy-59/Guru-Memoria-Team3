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
            "안녕! 난 유니콘이야!\n넌 여기가 처음이구나!",
            "정말 반가워!\n초면에 미안한데 혹시...",
            "내게 유니콘 뿔을 찾아줄 수 있겠니?",
            "정말 정말 중요한 물건인데 이 세상 어딘가에 숨겨져있어...!",
            "지금의 나는 다리를 다쳐서 이 앞의 떠다니는 섬을 건널 수 없지만,\n너라면 가능할 거야!",
            "...그리고 혹시라도 토끼 인형이 있다면 바로...",
            "없에버려",
            "꼭 유니콘 뿔은 나에게 줘야해!!!",
            "그럼 행운을 빌어!"
        });
        talkData.Add(100, new string[] { "상자이다.", "물약이 들어있다!" });
        talkData.Add(101, new string[] { "상자이다.", "유니콘 뿔이 들어있다!" });
        talkData.Add(500, new string[] { "토끼 인형이다.",
            "...없애버릴까?",
            "인형을 들었다.",
            "인형 밑에 노트가 있다!",
            "[마우스 클릭으로 공격. 뿔달린 짐승을 믿지 마라\n-익명-]"
        });
        talkData.Add(501, new string[] { "토끼 인형이다.",
            "....없긿촗싈ㅈ",
            "머리를 뜯어버려!!!!!!!!!!!!!!!!!!!!!!",
            "인형을 들었다.",
            "인형 밑에 노트가 있다!",
            "[뿔은 열쇠다. 절대 놓지지마라]",
            "[뿔 달린 짐승을 믿지 마라. 뿔 달린 짐승을 믿지 마라. \n" +
            "뿔 달린 짐승을 믿지 마라. 뿔 달린 짐승을 믿지 마라. \n" +
            "뿔 달린 짐승을 믿지 마라. 뿔 달린 짐승을 믿지 마라. \n-익명-]"
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
