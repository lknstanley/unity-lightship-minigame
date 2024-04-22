using Core.Gameplay;
using UnityEngine;

namespace Core
{
    public class BeatGameManager : MonoBehaviour
    {
        #region Singleton
        
        private static BeatGameManager _instance;
        public static BeatGameManager Instance => _instance;
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion

        [ SerializeField ]
        private AudioAnalyzer audioAnalyzer;
        public AudioAnalyzer GetAudioAnalyzer() => audioAnalyzer;

        [ SerializeField ]
        private CubeGenerator cubeGenerator;
        public CubeGenerator GetCubeGenerator() => cubeGenerator;
        
        [ SerializeField ]
        private AudioVisualizer audioVisualizer;
        public AudioVisualizer GetAudioVisualizer() => audioVisualizer;

        [ SerializeField ]
        private ARCursor arCursor;
        public ARCursor GetARCursor() => arCursor;

        [ SerializeField ]
        private ARPlacer arPlacer;
        public ARPlacer GetARPlacer() => arPlacer;

    }
}
