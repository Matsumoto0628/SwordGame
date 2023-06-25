using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSlow : MonoBehaviour
{
    [SerializeField]
    float slowTimeScale;

    [SerializeField]
    float slowTime;

    float elapsedTime;

    bool isSlow;

    private void Update()
    {
        //�o�ߎ��ԏ����q�b�g�X�g�b�v
        if (isSlow)
        {
            elapsedTime += Time.unscaledDeltaTime;

            if (elapsedTime >= slowTime)
                SetNormalTime();
        }
    }

    public void Slow()
    {
        //���łɃX���E��ԂȂ珈�����Ȃ�
        if (isSlow)
            return;

        elapsedTime = 0;

        Time.timeScale = slowTimeScale;

        isSlow = true;
    }

    private void SetNormalTime()
    {
        Time.timeScale = 1f;

        isSlow = false;
    }
}
