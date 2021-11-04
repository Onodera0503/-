using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionList : MonoBehaviour
{
    public Production[] Productionlist;
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

            for (int i = 0; i < Productionlist.Length; i++)
            {
                if (Productionlist[i].Choice)
                {
                    Debug.Log("生産者を検出");
                    debug = Productionlist[i].production;
                    isSearch = false;
                }
            }
        }
        else
        {
            // 要求物の情報を取得している状態

            if (!isSend)
            {
                GameObject Minimana = GameObject.Find("MinigameManager");
                MinigameManager script = Minimana.GetComponent<MinigameManager>();
                script.ProductionData = debug;
                
                isSend = true;
                script.ProductionSend = isSend;
            }
            else
            {
                // 情報送信完了
            }
        }
    }
}
