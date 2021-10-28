using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hpbar : MonoBehaviour
{

    Slider Hpslider;

    public GameObject obj;
    Player script0;
    Enemy script1;


    // Start is called before the first frame update
    void Start()
    {
        Hpslider = this.GetComponent<Slider>();

        if(obj.gameObject.tag == "Player")
        {
            script0 = obj.GetComponent<Player>();
            Hpslider.maxValue = script0.m_Hp;
            Hpslider.value = script0.m_Hp;
        }
        if (obj.gameObject.tag == "Enemy")
        {
            script1 = obj.GetComponent<Enemy>();
            Hpslider.maxValue = script1.m_Hp;
            Hpslider.value = script1.m_Hp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (obj.gameObject.tag == "Player")
        {
            Hpslider.value = script0.m_Hp;
        }
        if (obj.gameObject.tag == "Enemy")
        {
            Hpslider.value = script1.m_Hp;
        }
    }
}
