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

            //pcèàóù
            if (collisionPC != null)
            {
                //âE
                if (myPC.IsRight())
                    collisionPC.KnockBack(Vector2.right);

                //ç∂
                if (!myPC.IsRight())
                    collisionPC.KnockBack(Vector2.left);

                hitSlow.Slow();
                ce.ShakeCamera();

                return;
            }
            
            //dummyèàóù
            if (collisionDummy != null)
            {
                //âE
                if (myPC.IsRight())
                    collisionDummy.KnockBack(Vector2.right);

                //ç∂
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

            //pcèàóù
            if (collisionPC != null)
            {
                //ì|ÇÍÇΩÇÁçUåÇÇ≈Ç´Ç»Ç¢
                if (collisionPC.IsDie())
                    return;

                //âE
                if (myPC.IsRight())
                    collisionPC.Hit(Vector2.right);

                //ç∂
                if (!myPC.IsRight())
                    collisionPC.Hit(Vector2.left);

                hitSlow.Slow();
                ce.ShakeCamera();
                ce.BackRed();
            }

            //dummyèàóù
            if (collisionDummy != null)
            {
                //ì|ÇÍÇΩÇÁçUåÇÇ≈Ç´Ç»Ç¢
                if (collisionDummy.IsDie())
                    return;

                //âE
                if (myPC.IsRight())
                    collisionDummy.Hit(Vector2.right);

                //ç∂
                if (!myPC.IsRight())
                    collisionDummy.Hit(Vector2.left);

                hitSlow.Slow();
                ce.ShakeCamera();
                ce.BackRed();
            }
        }
    }
}
