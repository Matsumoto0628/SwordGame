using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class ColliderOfAnim : MonoBehaviour
{
    [SerializeField]
    PlayerController myPC;
    
    HitSlow hitSlow;

    CameraEffect ce;

    private void Start()
    {
        hitSlow = GameObject.FindGameObjectWithTag("GameController").GetComponent<HitSlow>();

        ce = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraEffect>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            PlayerController collisionPC = collision.gameObject.GetComponentInParent<PlayerController>();
            Dummy collisionDummy = collision.gameObject.GetComponentInParent<Dummy>();

            //pc処理
            if (collisionPC != null)
            {
                //右
                if (myPC.IsRight())
                    collisionPC.KnockBack(Vector2.right);

                //左
                if (!myPC.IsRight())
                    collisionPC.KnockBack(Vector2.left);

                hitSlow.Slow();
                ce.ShakeCamera();

                return;
            }
            
            //dummy処理
            if (collisionDummy != null)
            {
                //右
                if (myPC.IsRight())
                    collisionDummy.KnockBack(Vector2.right);

                //左
                if (!myPC.IsRight())
                    collisionDummy.KnockBack(Vector2.left);

                hitSlow.Slow();
                ce.ShakeCamera();
            }
        }

        if (collision.gameObject.CompareTag("Body"))
        {
            PlayerController collisionPC = collision.gameObject.GetComponentInParent<PlayerController>();
            Dummy collisionDummy = collision.gameObject.GetComponentInParent<Dummy>();

            //pc処理
            if (collisionPC != null)
            {
                //倒れたら攻撃できない
                if (collisionPC.IsDie())
                    return;

                //右
                if (myPC.IsRight())
                    collisionPC.Hit(Vector2.right);

                //左
                if (!myPC.IsRight())
                    collisionPC.Hit(Vector2.left);

                hitSlow.Slow();
                ce.ShakeCamera();
                ce.BackRed();
            }

            //dummy処理
            if (collisionDummy != null)
            {
                //倒れたら攻撃できない
                if (collisionDummy.IsDie())
                    return;

                //右
                if (myPC.IsRight())
                    collisionDummy.Hit(Vector2.right);

                //左
                if (!myPC.IsRight())
                    collisionDummy.Hit(Vector2.left);

                hitSlow.Slow();
                ce.ShakeCamera();
                ce.BackRed();
            }
        }
    }
}
