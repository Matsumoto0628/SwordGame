using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    [SerializeField]
    float speed;

    //[SerializeField]
    //Transform target;

    [SerializeField]
    float hitForce;

    [SerializeField]
    float attackRange;

    [SerializeField]
    bool isRight;

    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject bodyObj;

    bool isDie;

    Rigidbody2D rb;

    DummyGenerator generator;

    Transform target;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //レイヤーでダッシュ中の当たり判定を変える
        bodyObj.layer = 8;

        target = GameObject.FindWithTag("Player").transform;

        generator = GameObject.FindWithTag("GameController").GetComponent<DummyGenerator>();
    }

    private void Update()
    {
        animator.SetBool("isRun", true);

        if (!isDie)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }

        //float distance = Vector3.Distance(transform.position, target.position);

        //if (Mathf.Abs(distance) < attackRange)
        //    Attack();

    }

    private void Attack()
    {
        animator.SetTrigger("isAttack");
    }

    public void Hit(Vector2 dir)
    {
        //アニメーターのパラメータを変更
        animator.SetBool("isHit", true);

        //相手の攻撃方向にふっとぶ
        rb.AddForce(dir * hitForce, ForceMode2D.Impulse);

        //移動不可にする
        isDie = true;

        generator.Generate();

        Destroy(gameObject, 2f);
    }

    public void KnockBack(Vector2 dir)
    {
        //相手の攻撃方向にふっとぶ
        rb.AddForce(dir * hitForce, ForceMode2D.Impulse);
    }

    public bool IsRight()
    {
        return isRight;
    }

    public bool IsDie()
    {
        return isDie;
    }
}
