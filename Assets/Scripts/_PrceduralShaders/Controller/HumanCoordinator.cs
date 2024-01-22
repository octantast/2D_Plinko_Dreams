using UnityEngine;

namespace _PrceduralShaders.Controller
{
    public class HumanCoordinator : MonoBehaviour
    {
        public void InitiateHumanInteraction()
        {
            UniWebView.SetAllowInlinePlay(true);

            var audioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (var source in audioSources)
            {
                source.Stop();
            }

            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
            Screen.orientation = ScreenOrientation.AutoRotation;
        }
    }
}