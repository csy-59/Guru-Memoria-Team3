using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    PlayerMove player;

    //시작 UI 관련
    public GameObject startUI;
    MainMenu start;
    public GameObject startPanel;
    public GameObject howToPlayPanel;

    //사망 UI 관련
    public GameObject DeathUI;
    DeathMenu death;
    public bool isDeath;

    //InGame UI 관련
    public GameObject inGameUI;
    public Text uniconCount;
    public Image potion;
    public Text potionCount;
    public Image doll;
    public Text dollcount;
    public Image playerHeart;
    public Sprite[] hearts;
    int heartCount;
    public GameObject LoadingPanel;

    //Pause UI 관련
    public GameObject PauseUI;
    PauseMenu pause;
    public GameObject pauseButtonPanel;
    public GameObject howToPlayPanel_PauseUI;
    public bool isPauseOn;

    //Talk UI 관련
    public GameObject talkPanel;
    public Text talkText;
    GameObject scanObject;
    public bool isTextOn = false;
    TalkManager tm;
    int talkIndex;
    public bool hadTalked;

    //아이템 관련
    public int Doll = 0;            //layer 15
    public int unicornHorns = 0;    //layer 16
    //public int candle = 0;          //layer 17
    public int medicine = 0;        //layer 18

    //스테이지 전환 관련
    public int nowStage;
    MainCamara Camara1;

    // Start is called before the first frame update
    void Start()
    {
        tm = GameObject.FindObjectOfType<TalkManager>();
        player = GameObject.FindObjectOfType<PlayerMove>();
        start = startUI.GetComponent<MainMenu>();
        death = DeathUI.GetComponent<DeathMenu>();
        pause = PauseUI.GetComponent<PauseMenu>();

        inGameUI.SetActive(false);
        talkPanel.SetActive(false);
        startPanel.SetActive(true);
        howToPlayPanel.SetActive(false);
        DeathUI.SetActive(false);
        PauseUI.SetActive(false);
        pauseButtonPanel.SetActive(false);
        howToPlayPanel_PauseUI.SetActive(false);
        LoadingPanel.SetActive(false);

        isPauseOn = false;
        heartCount = hearts.Length;
        hadTalked = false;
        
        Camara1 = GameObject.FindObjectOfType<MainCamara>();

        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어 사망
        if (player.currentHp == 0)
        {
            Time.timeScale = 0;
            isDeath = true;
            DeathUI.SetActive(true);
        }

        //###UI관련
        //Start UI;
        startPanel.SetActive(!start.isClickHowToPlay);
        howToPlayPanel.SetActive(start.isClickHowToPlay);
        if (start.isGameStart)
        {
            startUI.SetActive(false);
            inGameUI.SetActive(true);
            Time.timeScale = 1;
        }
        //Death UI;
        if(death.isClickRestart)
        {
            isDeath = false;
            player.currentHp = player.maxHp;
            DeathUI.SetActive(false);
            death.isClickRestart = false;
        }
        else if (death.isQuit)
        {
            isDeath = false;
            startUI.SetActive(true);
            DeathUI.SetActive(false);
            death.isQuit = false;
            GameOver();
        }
        //Pause UI
        if (Input.GetKeyDown(KeyCode.Escape))   //esc 키 누를시
        {
            isPauseOn = true;
        }
        PauseUI.SetActive(isPauseOn);
        pauseButtonPanel.SetActive(!pause.isClickHowToPlay);
        howToPlayPanel_PauseUI.SetActive(pause.isClickHowToPlay);
        Time.timeScale = isPauseOn ? 0 : 1;
        if (pause.isClickResume)    //일시정지 풀기
        {
            isPauseOn = false;
            pause.isClickResume = false;
        }
        else if (pause.isQuit)
        {
            startUI.SetActive(true);
            isPauseOn = false;
            pause.isQuit = false;
            GameOver();
        }
        //item UI
        uniconCount.text = "" + unicornHorns + " / 10";
        potion.color = medicine > 0 ? new Color(potion.color.r, potion.color.g, potion.color.b, 1f) : new Color(potion.color.r, potion.color.g, potion.color.b, 0f);
        potionCount.text = medicine > 0 ? "" + medicine : "";
        doll.color = Doll > 0 ? new Color(doll.color.r, doll.color.g, doll.color.b, 1f) : new Color(doll.color.r, doll.color.g, doll.color.b, 0f);
        dollcount.text = Doll > 0 ? "" + Doll : "";
        playerHeart.sprite = player.currentHp > 0 ? hearts[player.currentHp - 1] : null;

        //##


        //아이템 사용 관련
        if (Input.GetKeyDown(KeyCode.Alpha1) && medicine > 0 && player.currentHp < player.maxHp)
        {
            medicine--;
            player.currentHp++;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && Doll > 0)
        {
            Doll--;
        }
    }
    public void StageChange()
    {
        nowStage++;
        LoadingPanel.SetActive(true);
        Invoke("OutLoadingPanel", 1);
    }

    public void NPCText(GameObject scanObj)
    {
        scanObject = scanObj;
        InteractiveCode objCode = scanObject.GetComponent<InteractiveCode>();
        if (objCode.codeNumber == 10000 && unicornHorns > 2)
        {
            StageChange();
            return;
        }
        Talk(objCode.codeNumber, objCode.isNPC, scanObj);

        talkPanel.SetActive(isTextOn);
    }

    void Talk(int id, bool isNPC, GameObject scanObj)
    {
        string talkData = tm.GetTalk(id, talkIndex);

        if (talkData == null)
        {
            isTextOn = false;
            talkIndex = 0;
            if (id == 1000)
                hadTalked = true;
            if (!isNPC)
            {
                switch (id)
                {
                    case 100:
                        medicine++;
                        break;
                    case 101:
                        unicornHorns++;
                        break;
                    case 500:
                        Doll++;
                        break;
                }
                Destroy(scanObj);
            }
            return;
        }
        talkText.text = talkData;
        isTextOn = true;
        talkIndex++;
    }

    void GameOver()
    {
        print("Game Over");
    }

    public void GameSave()
    {

    }
    public void GameLoad()
    {

    }

    void OutLoadingPanel()
    {
        LoadingPanel.SetActive(false);
    }
}
