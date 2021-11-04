using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{

    public enum EnemyState  // エネミーの状態
    {
        ONE,
        TWO,
        THREE,
    }

    public EnemyState State = EnemyState.ONE;
    public EnemyState NextState = EnemyState.ONE;

    public Transform m_Transform;
    public Bullet[] m_BulletPrefab; // 弾のプレハブ
    public int Bulletnumber;        // 発射する弾の番号
    public float m_BulletSpeed;     // 弾のスピード
    public float m_ShotTimer;       // 射撃タイミング管理タイマー
    public float m_ShotInterval;    // 射撃間隔
    public float m_MoveTimer;       // 往復移動管理タイマー
    public float m_MoveInterval;    // 往復移動間隔
    float m_Move = 1;        // 反転用に1突っ込んどくﾔﾂ
    public float m_JumpTimer;       // ジャンプタイミング管理タイマー
    public float m_JumpInterval;    // ジャンプ間隔
    public float m_speed; // 移動の速さ
    public float m_jump;  // ジャンプ力
    public bool isGround; // 接地判定
    public int m_Hp;      // 体力

    public GameObject target;
    public GameObject barrel;

    // Start is called before the first frame update
    void Start()
    {
        m_ShotInterval = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case EnemyState.ONE:
                one();
                break;

            case EnemyState.TWO:
                two();
                break;

            case EnemyState.THREE:
                three();
                break;

            default:
                Debug.Log("どのの状態にも属していない");
                break;
        }

        // 以下、射撃処理

        // 弾の発射タイミングを管理するタイマーを更新する
        m_ShotTimer += Time.deltaTime;

        // まだ弾の発射タイミングではない場合は、ここで処理を終える
        if (m_ShotTimer < m_ShotInterval) return;

        // 弾の発射タイミングを管理するタイマーをリセットする
        m_ShotTimer = 0;

        var pos = m_Transform.position;
        var dir = target.transform.position - pos;

        var angle = Utils.GetAngle(Vector3.zero, dir);
        Shoot(angle, m_BulletSpeed);

        if (m_Hp <= 0)
            Destroy(this.gameObject);
    }



    private void one()
    {
        if (m_Hp <= 10)
        {
            SetState(EnemyState.TWO);
            m_ShotInterval = 1.5f;
        }
    }

    private void two()
    {
        // 往復移動タイマー更新
        m_MoveTimer += Time.deltaTime;
        // 反転処理
        if (m_MoveTimer >= m_MoveInterval)
        {
            // 往復移動を管理するタイマーをリセットし反転
            m_MoveTimer = 0;
            m_Move *= -1;
        }
        transform.Translate(m_Move * Vector3.left * Time.deltaTime);

        if (m_Hp <= 5)
        {
            SetState(EnemyState.THREE);
            m_ShotInterval = 0.5f;
        }
    }

    private void three()
    {
        // 往復移動タイマー更新
        m_MoveTimer += Time.deltaTime;
        // 反転処理
        if (m_MoveTimer >= m_MoveInterval)
        {
            // 往復移動を管理するタイマーをリセットし反転
            m_MoveTimer = 0;
            m_Move *= -1;
        }
        transform.Translate(m_Move * Vector3.left * Time.deltaTime);

        if(isGround)
        {
            // ジャンプタイミングを管理するタイマーを更新する
            m_JumpTimer += Time.deltaTime;

            // ジャンプタイミングだった場合ジャンプする。
            if (m_JumpTimer >= m_JumpInterval)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, m_jump, 0);
                // ジャンプタイミングを管理するタイマーをリセットする
                m_JumpTimer = 0;
            }
        }

    }

    private void Shoot(float angle, float speed)
    {
        var pos = barrel.transform.position; // プレイヤーの位置
        var rot = transform.localRotation; // プレイヤーの向き

        Bulletnumber = Random.Range(0, m_BulletPrefab.Length);

        // 発射する弾を生成する(種類はランダム)
        var Bullet = Instantiate(m_BulletPrefab[Bulletnumber], pos, rot);

        // 弾を発射する方向と速さを設定し生成
        Bullet.Init(angle, speed, Bulletnumber);
    }

    private void SetState(EnemyState nextstate)
    {
        State = nextstate;
    }

    void OnCollisionStay(Collision collision)
    {
        // 地面接地判定
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // 地面離脱判定
        if (collision.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }

}
