using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    float limitTime;

    [SerializeField]
    float dashForce;

    [SerializeField]
    float hitForce;

    [SerializeField]
    bool isRight;

    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject bodyObj;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip[] audioClips;

    float inputX;

    Vector2 moveDir;

    float pushTimer;

    bool isDash;

    bool isMaxDash;

    bool isDie;

    Rigidbody2D rb;

    PhotonView photonView;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        photonView = GetComponent<PhotonView>();

        //���C���[�Ń_�b�V�����̓����蔻���ς���
        bodyObj.layer = 8;
    }

    private void Update()
    {
        //���@����������
        if (!photonView.IsMine)
            return;

        //�o�ߎ��ԏ����_�b�V��
        if (isDash)
        {
            pushTimer += Time.unscaledDeltaTime;

            if (pushTimer >= limitTime)
                isMaxDash = true;
        }

        //�|�ꂽ�瓮���𐧌�
        if (isDie)
            return;

        Move();

        //�U�����[�V�������͉�]�E�_�b�V���E�U���𐧌�
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Blue_Attack")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("White_Attack"))
            return;

        Rotate();

        Dash();

        Attack();
    }

    private void Move()
    {
        //���͎�t
        inputX = Input.GetAxisRaw("Horizontal");

        //�A�j���[�^�[�̃p�����[�^��ύX
        if (inputX > 0 || inputX < 0)
            animator.SetBool("isRun", true);
        else
            animator.SetBool("isRun", false);

        //�ړ������ɗ͂�������
        moveDir = new Vector2(inputX, 0);

        rb.AddForce(moveDir * speed, ForceMode2D.Force);
    }

    private void Rotate()
    {
        //�X�v���C�g�̌�����ς���
        //������ۑ�
        if (inputX > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);

            isRight = true;
        }

        if (inputX < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);

            isRight = false;
        }
    }

    /// <summary>
    /// �o�ߎ��Ԃƃ{�^�������������Ń_�b�V���؂�ւ�
    /// </summary>
    private void Dash()
    {
        //���͔���
        //�X�y�[�X�������Ă��邩�_�b�V�����Ă��Ȃ�
        if (Input.GetKey(KeyCode.Space) && !isDash)
        {
            pushTimer = 0;

            isDash = true;            
        }

        //�ő剟�����Ă����烊�Z�b�g����
        if (isMaxDash)
            ResetDash();

        //�X�y�[�X�������Ă��Ȃ������牟������܂߃��Z�b�g����
        if (!Input.GetKey(KeyCode.Space))
        {
            ResetDash();

            isMaxDash = false;
        }

        //���͔����A�͂�������
        if (isDash)
        {
            //�E����
            if (isRight)
                rb.AddForce(Vector2.right * dashForce, ForceMode2D.Force);

            //������
            if (!isRight)
                rb.AddForce(Vector2.left * dashForce, ForceMode2D.Force);

            //�_�b�V�����ђʂ���
            bodyObj.layer = 9;

            //�A�j���[�^�[�̃p�����[�^��ύX
            animator.SetBool("isRun", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))        
            audioSource.PlayOneShot(audioClips[0]);        
    }

    private void ResetDash()
    {
        isDash = false;

        bodyObj.layer = 8;
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //�A�j���[�^�[�̃L�[�t���[���ƃR���C�_�[��������킹�Ă���
            //�A�j���[�^�[�̃p�����[�^��ύX
            animator.SetTrigger("isAttack");
        }
    }

    public void Hit(Vector2 dir)
    {
        //�A�j���[�^�[�̃p�����[�^��ύX
        animator.SetBool("isHit", true);

        audioSource.PlayOneShot(audioClips[1]);

        //����̍U�������ɂӂ��Ƃ�
        rb.AddForce(dir * hitForce, ForceMode2D.Impulse);

        //�ړ��s�ɂ���
        isDie = true;
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
