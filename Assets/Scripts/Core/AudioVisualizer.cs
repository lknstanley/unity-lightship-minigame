using System.Collections.Generic;
using Core.Gameplay;
using UnityEngine;

namespace Core
{
    public class AudioVisualizer : MonoBehaviour
    {
        public VisualizerCube visualizerPrefab;
        public List< VisualizerCube > visualizerObjects;

        private void Start()
        {
            int visualizerCount = BeatGameManager.Instance.GetAudioAnalyzer().feqBandCount;
            Transform container = transform;
            float startX = -visualizerPrefab.transform.localScale.x * visualizerCount / 2;
            for ( int i = 0; i < visualizerCount; i++ )
            {
                VisualizerCube go = Instantiate( visualizerPrefab, container, false );
                go.transform.localPosition = new Vector3( startX + i * (go.transform.localScale.x + 0.5f), 0, 0 );
                go.Initialize();
                visualizerObjects.Add( go );
            }
        }

        private void FixedUpdate()
        {
            float[] spectrum = BeatGameManager.Instance.GetAudioAnalyzer().previousFeqs;
            for ( var i = 0; i < spectrum.Length; i++ )
            {
                var val = spectrum[ i ] * 8f;
                var visualizerObject = visualizerObjects[ i ];
                visualizerObject.transform.localScale = new Vector3( 
                    visualizerObject.transform.localScale.x, 
                    val, 
                    visualizerObject.transform.localScale.z );
            }
        }

        public Color GetBandColor( int index )
        {
            return visualizerObjects[ index ].GetColor();
        }

        public Quaternion GetBandRotation()
        {
            return transform.rotation;
        }

        public Vector3 GetBandScale( int index )
        {
            return visualizerObjects[ index ].transform.lossyScale;
        }

        public Vector3 GetBandPosition( int index )
        {
            return visualizerObjects[ index ].transform.position;
        }
    }
}
