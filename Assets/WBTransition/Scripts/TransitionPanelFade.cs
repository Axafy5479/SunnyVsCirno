using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WBTransition
{
    internal class TransitionPanelFade : TransitionPanelBase
    {
        [SerializeField] private CanvasGroup canvasGroup;
        //[SerializeField] private GameObject loading;
        [SerializeField] private float openTime = 1;
        [SerializeField] private float waitTime = 2;
        [SerializeField] private float closeTime = 1;


        private void Awake()
        {
            //�}�X�N�����؂�܂ł́A���[�f�B���O�̃N���N���͌����Ȃ�
            //loading.SetActive(false);

            //���߁A�}�X�N�̐F�͓���(���X�ɕs�����x���グ��)
            canvasGroup.alpha = 0;
        }

        /// <summary>
        /// ��ʂ����A�j���[�V����
        /// </summary>
        /// <returns></returns>
        protected override IEnumerator Close()
        {
            //1�b�Ԃ����āA�s�����x��0����1�܂ŏ㏸������
            for (int i = 0; i < (int)(20*closeTime); i++)
            {
                canvasGroup.alpha = (float)i / (int)(20*closeTime-1);
                yield return new WaitForSecondsRealtime(1f/20);
            }

            //�}�X�N�����؂����̂ŁA���[�f�B���O�̃N���N����\��
            //loading.SetActive(true);
        }

        /// <summary>
        /// �}�X�N���J���A�j���[�V����
        /// </summary>
        /// <returns></returns>
        protected override IEnumerator Open()
        {
            //�}�X�N���J���O�ɁA���[�f�B���O�̃N���N�����\��
            //loading.SetActive(false);

            //1�b�Ԃ����āA�s�����x��1����0�܂ŉ�����
            for (int i = (int)(20 * openTime-1); i >=0; i--)
            {
                canvasGroup.alpha = (float)i / (int)(20f * openTime - 1);
                yield return new WaitForSecondsRealtime(1f / 20);
            }
        }

        /// <summary>
        /// �Œ���A���[�h��ʂ�������������(�b)
        /// </summary>
        protected override float WaitTime => waitTime;
    }
}
