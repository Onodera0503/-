using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour　// 破壊命令、生成命令作る
{
    public enum MINIGAMESTEP
    {
        ONE,
        TWO,
        THREE,
        FOUR,
        FIVE,
        SIX,
        SEVEN,
    }
    public MINIGAMESTEP Step;   // ミニゲームの進行状況管理変数

    public enum UILIST
    {
        CLIENT,
        PAYMENT,
        PRODUCTION,
        RESULTCLIENT,
        RESULTPRODUCTION,
        WIN,
        LOSE,
    }
    public Image[] UIimage;

    public UIManager[] UImanager;

    public int RequestData;
    public bool RequestSend;

    public int ProductionData;
    public bool ProductionSend;

    public int AmountData;
    public bool AmountSend;

    public GameObject Game;
    public GameObject Player;
    public GameObject Enemy;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i < UImanager.Length;i++)
        {
            UImanager[i] = UImanager[i].GetComponent<UIManager>();

        }
        UImanager[0].UICreate(UIimage[(int)UILIST.CLIENT]);
        UImanager[1].UICreate(UIimage[(int)UILIST.PRODUCTION]);
        Step = MINIGAMESTEP.ONE;
    }

    // Update is called once per frame
    void Update()
    {
        switch(Step)
        {
            case MINIGAMESTEP.ONE:
                one();
                break;

            case MINIGAMESTEP.TWO:
                two();
                break;

            case MINIGAMESTEP.THREE:
                three();
                break;

            case MINIGAMESTEP.FOUR:
                four();
                break;

            case MINIGAMESTEP.FIVE:
                five();
                break;

            case MINIGAMESTEP.SIX:
                six();
                break;

            case MINIGAMESTEP.SEVEN:
                seven();
                break;

            default:
                Debug.Log("どの状態にも属していない");
                break;
        }


    }

    void one()
    {
        if (RequestSend)
        {
            UImanager[0].UIDestroy();
            UImanager[0].UICreate(UIimage[(int)UILIST.PAYMENT]);
            Step = MINIGAMESTEP.TWO;
        }
    }

    void two()
    {
        if((ProductionSend)&&(AmountSend))
        {
            for (int i = 0; i < UImanager.Length; i++)
            {
                UImanager[i].UIDestroy();
                Step = MINIGAMESTEP.THREE;
                Game.SetActive(true);
            }
        }
    }

    void three()
    {
        if(!Player)
        {
            UImanager[0].UICreate(UIimage[(int)UILIST.RESULTCLIENT]);
            UImanager[1].UICreate(UIimage[(int)UILIST.RESULTPRODUCTION]);
            UImanager[2].UICreate(UIimage[(int)UILIST.LOSE]);
            Step = MINIGAMESTEP.FOUR;
            Game.SetActive(false);
        }
        if(!Enemy)
        {
            UImanager[0].UICreate(UIimage[(int)UILIST.RESULTCLIENT]);
            UImanager[1].UICreate(UIimage[(int)UILIST.RESULTPRODUCTION]);
            UImanager[2].UICreate(UIimage[(int)UILIST.WIN]);
            Step = MINIGAMESTEP.FIVE;
            Game.SetActive(false);
        }
    }

    void four() // プレイヤー敗北
    {
        if (Input.GetKeyDown("space")) // スペース押したら現地画面へ遷移
        {
            SceneManager.LoadScene("Genchi");
        }
    }

    void five() // プレイヤー勝利
    {

    }

    void six()
    {

    }

    void seven()
    {

    }

    private void SetStep(MINIGAMESTEP nextstep)
    {
        Step = nextstep;
    }
}
