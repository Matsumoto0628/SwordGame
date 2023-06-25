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

        //���C���[�Ń_�b�V�����̓����蔻���ς���
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
        //�A�j���[�^�[�̃p�����[�^��ύX
        animator.SetBool("isHit", true);

        //����̍U�������ɂӂ��Ƃ�
        rb.AddForce(dir * hitForce, ForceMode2D.Impulse);

        //�ړ��s�ɂ���
        isDie = true;

        generator.Generate();

        Destroy(gameObject, 2f);
    }

    public void KnockBack(Vector2 dir)
    {
        //����̍U�������ɂӂ��Ƃ�
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
