using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform m_Transform;
    public Bullet[] m_BulletPrefab; // 弾のプレハブ
    public int Bulletnumber; // 発射する弾の番号
    public float m_BulletSpeed;// 弾のスピード
    public float m_speed; // 移動の速さ
    public float m_jump;  // ジャンプ力
    public int m_Hp;      // 体力

    public GameObject enemy;
    public GameObject barrel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if (Input.GetKey("d"))
      {
            transform.position += transform.right * m_speed * Time.deltaTime;
      }
      if (Input.GetKey("a"))
       {
            transform.position -= transform.right * m_speed * Time.deltaTime;
      }
      if (Input.GetKeyDown("e"))
      {
         if (Bulletnumber == 2)
          {
                Bulletnumber = 0;
          }
          else
           {
                Bulletnumber++;
           }
       }
      if (Input.GetKeyDown("space"))
        {
            var pos = m_Transform.position;
            var dir = enemy.transform.position - pos;
            var angle = Utils.GetAngle(Vector3.zero, dir);
            Shoot(angle, m_BulletSpeed);
       }

        // 回転しないように角度を固定する
        m_Transform = this.transform;
        Vector3 localAngle = m_Transform.localEulerAngles;
        localAngle.x = 0.0f;
        localAngle.y = 0.0f;
        localAngle.z = 0.0f;
        m_Transform.localEulerAngles = localAngle;

        if(m_Hp <= 0)
        Destroy(this.gameObject);
    }

    void OnCollisionStay(Collision collision)
    {
        // 地面(Ground)に接触している場合wでジャンプできる
        if (collision.gameObject.tag == "Ground")
        {
          if (Input.GetKey("w"))
          {
                GetComponent<Rigidbody>().velocity = new Vector3(0, m_jump, 0);
          }
        }
    }

    private void Shoot(float angle,float speed)
    {
        var pos = barrel.transform.position; // プレイヤーの位置
        var rot = transform.localRotation; // プレイヤーの向き

        // 発射する弾を生成する
        var Bullet = Instantiate(m_BulletPrefab[Bulletnumber], pos, rot);

        // 弾を発射する方向と速さを設定する
        Bullet.Init(angle, speed, Bulletnumber);
    }
}
