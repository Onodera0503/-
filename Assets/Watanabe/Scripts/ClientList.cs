using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientList : MonoBehaviour // Requestlistに子のRequestを格納し、どれかが押された際に他のボタンを押せないようにする。
{
    public Request[] Requestlist;
    public bool isSearch;
    public bool isSend; // 情報送信の確認

    public int debug; // 受け取ったデータを確認する為の仮変数
    // Start is called before the first frame update
    void Start()
    {
        isSearch = true;
        isSend = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSearch)
        {
            // 要求物の情報を取得していない状態

            for (int i = 0; i < Requestlist.Length; i++)
            {
                if (Requestlist[i].Choice)
                {
                    Debug.Log("要求物を検出");
                    debug = Requestlist[i].request;
                    isSearch = false;
                }
            }
        }
        else
        {
            // 要求物の情報を取得している状態

            if (!isSend)
            {
                // 情報未送信
                GameObject Minimana = GameObject.Find("MinigameManager");
                MinigameManager script = Minimana.GetComponent<MinigameManager>();
                script.RequestData = debug;

                isSend = true;
                script.RequestSend = isSend;
            }
            else
            {
                // 情報送信完了
            }
        }
    }
}
