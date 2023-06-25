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

            //pc����
            if (collisionPC != null)
            {
                //�E
                if (myPC.IsRight())
                    collisionPC.KnockBack(Vector2.right);

                //��
                if (!myPC.IsRight())
                    collisionPC.KnockBack(Vector2.left);

                hitSlow.Slow();
                ce.ShakeCamera();

                return;
            }
            
            //dummy����
            if (collisionDummy != null)
            {
                //�E
                if (myPC.IsRight())
                    collisionDummy.KnockBack(Vector2.right);

                //��
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

            //pc����
            if (collisionPC != null)
            {
                //�|�ꂽ��U���ł��Ȃ�
                if (collisionPC.IsDie())
                    return;

                //�E
                if (myPC.IsRight())
                    collisionPC.Hit(Vector2.right);

                //��
                if (!myPC.IsRight())
                    collisionPC.Hit(Vector2.left);

                hitSlow.Slow();
                ce.ShakeCamera();
                ce.BackRed();
            }

            //dummy����
            if (collisionDummy != null)
            {
                //�|�ꂽ��U���ł��Ȃ�
                if (collisionDummy.IsDie())
                    return;

                //�E
                if (myPC.IsRight())
                    collisionDummy.Hit(Vector2.right);

                //��
                if (!myPC.IsRight())
                    collisionDummy.Hit(Vector2.left);

                hitSlow.Slow();
                ce.ShakeCamera();
                ce.BackRed();
            }
        }
    }
}
