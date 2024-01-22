using DG.Tweening;
using UnityEngine;

namespace _PrceduralShaders.Data
{
    public class UiiacHelp : MonoBehaviour
    {
        private bool isSequenceCompleted;
        private int currentIndex;
        private int totalElements;

        private void Awake()
        {
            int one = 1;
            double myNa = 2;

            var res = Mathf.Lerp(one, (float)myNa, 3);

            print(res);
        }

        public static void FadeCanvasGroup(GameObject canvasObject, bool fadeIn)
        {
            canvasObject.SetActive(true);
            CanvasGroup canvasGroup = canvasObject.GetComponent<CanvasGroup>();
            float targetAlpha = fadeIn ? 1f : 0f;

            canvasGroup.DOFade(targetAlpha, 0.5f).OnComplete(() =>
            {
                if (!fadeIn)
                {
                    canvasObject.SetActive(false);
                }
            });
        }
    }
}