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

        //レイヤーでダッシュ中の当たり判定を変える
        bodyObj.layer = 8;
    }

    private void Update()
    {
        //自機だけ動かす
        if (!photonView.IsMine)
            return;

        //経過時間処理ダッシュ
        if (isDash)
        {
            pushTimer += Time.unscaledDeltaTime;

            if (pushTimer >= limitTime)
                isMaxDash = true;
        }

        //倒れたら動きを制限
        if (isDie)
            return;

        Move();

        //攻撃モーション中は回転・ダッシュ・攻撃を制限
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Blue_Attack")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("White_Attack"))
            return;

        Rotate();

        Dash();

        Attack();
    }

    private void Move()
    {
        //入力受付
        inputX = Input.GetAxisRaw("Horizontal");

        //アニメーターのパラメータを変更
        if (inputX > 0 || inputX < 0)
            animator.SetBool("isRun", true);
        else
            animator.SetBool("isRun", false);

        //移動方向に力を加える
        moveDir = new Vector2(inputX, 0);

        rb.AddForce(moveDir * speed, ForceMode2D.Force);
    }

    private void Rotate()
    {
        //スプライトの向きを変える
        //向きを保存
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
    /// 経過時間とボタンを押す長さでダッシュ切り替え
    /// </summary>
    private void Dash()
    {
        //入力判定
        //スペースを押しているかつダッシュしていない
        if (Input.GetKey(KeyCode.Space) && !isDash)
        {
            pushTimer = 0;

            isDash = true;            
        }

        //最大押下していたらリセットする
        if (isMaxDash)
            ResetDash();

        //スペースを押していなかったら押下判定含めリセットする
        if (!Input.GetKey(KeyCode.Space))
        {
            ResetDash();

            isMaxDash = false;
        }

        //入力判定後、力を加える
        if (isDash)
        {
            //右向き
            if (isRight)
                rb.AddForce(Vector2.right * dashForce, ForceMode2D.Force);

            //左向き
            if (!isRight)
                rb.AddForce(Vector2.left * dashForce, ForceMode2D.Force);

            //ダッシュ中貫通する
            bodyObj.layer = 9;

            //アニメーターのパラメータを変更
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
            //アニメーターのキーフレームとコライダー判定を合わせておく
            //アニメーターのパラメータを変更
            animator.SetTrigger("isAttack");
        }
    }

    public void Hit(Vector2 dir)
    {
        //アニメーターのパラメータを変更
        animator.SetBool("isHit", true);

        audioSource.PlayOneShot(audioClips[1]);

        //相手の攻撃方向にふっとぶ
        rb.AddForce(dir * hitForce, ForceMode2D.Impulse);

        //移動不可にする
        isDie = true;
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
