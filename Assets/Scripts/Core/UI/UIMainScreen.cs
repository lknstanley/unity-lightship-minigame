using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class UIMainScreen : MonoBehaviour
    {
        public Slider diffSlider;
        public Slider speedSlider;

        private void Start()
        {
            diffSlider.SetValueWithoutNotify( BeatGameManager.Instance.GetAudioAnalyzer().sensitivity );
            speedSlider.SetValueWithoutNotify( BeatGameManager.Instance.GetCubeGenerator().GetNoteSpeed() );
        }

        public void OnStartClicked()
        {
            if ( BeatGameManager.Instance.GetARPlacer()
                .RequestPlacement( BeatGameManager.Instance.GetAudioVisualizer().gameObject ) )
            {
                if ( !BeatGameManager.Instance.GetAudioAnalyzer().IsPlaying() )
                {
                    BeatGameManager.Instance.GetAudioAnalyzer().StartMusic();
                }
            }
        }

        public void OnStopClicked()
        {
            BeatGameManager.Instance.GetAudioAnalyzer().StopMusic();
        }

        public void OnDiffSliderValueChanged()
        {
            BeatGameManager.Instance.GetAudioAnalyzer().sensitivity = diffSlider.value;
        }

        public void OnNoteSpeedSliderValueChanged()
        {
            BeatGameManager.Instance.GetCubeGenerator().SetNoteSpeed( speedSlider.value );
        }
    }
}