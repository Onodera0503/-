using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Vector3 m_velocity; // 速度

    public int Bulletnumber;    // 弾の番号

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Init(float angle, float speed , int number)
    {
        Bulletnumber = number;

        // 弾の発射角度をベクトルに変換する
        var direction = Utils.GetDirection(angle);

        // 発射角度と速さから速度を求める
        m_velocity = direction * speed;

        // 弾が進行方向を向くようにする
        var angles = transform.localEulerAngles;
        angles.z = angle - 90;
        transform.localEulerAngles = angles;

        // 2 秒後に削除する
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        // 移動する
        transform.localPosition += m_velocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            GameObject obj = collision.gameObject;
            Bullet script = obj.GetComponent<Bullet>();
            int number = script.Bulletnumber;

            if (Bulletnumber == number)
            {
                Debug.Log("同属性なのでお互い消去");
                Destroy(this.gameObject);
                return;
            }
            if ((Bulletnumber == 0) && (number == 2))
            {
                 Debug.Log("不利属性なので消去");
                 Destroy(this.gameObject);
                 return;
            }
            if ((Bulletnumber == 1) && (number == 0))
            {
                Debug.Log("不利属性なので消去");
                Destroy(this.gameObject);
                return;
            }
            if ((Bulletnumber == 2) && (number == 1))
            {
                Debug.Log("不利属性なので消去");
                Destroy(this.gameObject);
                return;
            }
        }

        if(collision.gameObject.tag == "Player")
        {
            GameObject obj = collision.gameObject;
            Player script = obj.GetComponent<Player>();
            script.m_Hp += -1;

            Debug.Log("プレイヤーに命中");
            Destroy(this.gameObject);
            return;
        }
        
        if(collision.gameObject.tag == "Enemy")
        {
            GameObject obj = collision.gameObject;
            Enemy script = obj.GetComponent<Enemy>();
            script.m_Hp += -1;

            Debug.Log("敵に命中");
            Destroy(this.gameObject);
            return;
        }
    }

}
