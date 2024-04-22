using System;
using System.Collections;
using UnityEngine;

namespace Core
{
    public class AudioAnalyzer : MonoBehaviour
    {
        [ Header( "Visualizer Settings" ) ]
        public float scaleTo = 100f;

        public int feqBandCount = 8;

        [ Header( "Sample Settings" ) ]
        [ SerializeField ]
        private int sampleCount = 512;

        public static float[] feqs;
        public static float[] samples;
        private AudioSource audioSource;
        private int feqDivider = 0;
        private int[] feqSampleCounts = new int[8] { 2, 4, 8, 33, 23, 24, 46, 372 };

        [ Header( "Beat Game Settings" ) ]
        [ Range( 0.1f, 100f ) ]
        public float sensitivity = 0.5f;

        [ Range( 0.03f, 1f ) ]
        public float cooltimeSetting = 0.1f;

        private bool _band1HandingCooldown = false;
        private bool _band2HandingCooldown = false;
        private bool _band3HandingCooldown = false;
        private bool _band4HandingCooldown = false;
        private bool _band5HandingCooldown = false;
        private bool _band6HandingCooldown = false;
        private bool _band7HandingCooldown = false;
        private bool _band8HandingCooldown = false;

        // Action Callback Methods
        public float[] previousFeqs;
        public Action OnBassTrigger;
        public Action OnBand2Trigger;
        public Action OnBand3Trigger;
        public Action OnBand4Trigger;
        public Action OnBand5Trigger;
        public Action OnBand6Trigger;
        public Action OnBand7Trigger;
        public Action OnBand8Trigger;

        // buffer feqs
        public static float[] BufferFeqs;
        float[] _decreaseBufferFeqs;

        void Awake()
        {
            audioSource = GetComponent< AudioSource >();
        }

        // Use this for initialization
        void Start()
        {
            feqs = new float[feqBandCount];
            previousFeqs = new float[feqBandCount];
            BufferFeqs = new float[feqBandCount];
            _decreaseBufferFeqs = new float[feqBandCount];
            samples = new float[sampleCount];
            feqDivider = ( int ) ( sampleCount / feqBandCount );
        }

        // Update is called once per frame
        void Update()
        {
            GetSpectrum();
            CreateFeqArray();
            CreateBufferFeq();
        }

        public void StartMusic()
        {
            audioSource.Play();
        }
        
        public void StopMusic()
        {
            audioSource.Stop();
        }

        public bool IsPlaying()
        {
            return audioSource.isPlaying;
        }
        
        #region Audio Analzyer
        
        void GetSpectrum()
        {
            audioSource.GetSpectrumData( samples, 0, FFTWindow.BlackmanHarris );
        }

        void CreateFeqArray()
        {
            previousFeqs = feqs;
            feqs = new float[feqBandCount];

            /*
                We have 512 samples and each sameple represent (22050Hz / 512) ~= 43Hz
                20 - 50 Hz (Sub bass)
                60 - 250 Hz (Bass)
                250 - 500 Hz (Low Midrange)
                500 - 2000 Hz (Midrange)
                2000 - 3000 Hz (1st Upper Midrange)
                3000 - 4000 Hz (2nd Upper Midrange)
                4000 - 6000 Hz (Presence)
                6000 - 20000 Hz (Brilliance)

                How can we get the fequency band from samples like we listed above?
                The result is we are going to use N sample(s) to fill one band.
                20 - 50 Hz needs 2 samples to cover the fequency range (43Hz * 2 = 86Hz > 50Hz) 0~86Hz
                60 - 250 Hz needs 4 samples to cover the fequency range (86Hz + 43Hz * 4 = 258Hz > 250Hz) 87~258Hz
                250 - 500 Hz needs 8 samples to cover the fequency range (258Hz + 43Hz * 8 = 602Hz > 500Hz) 259~602Hz
                500 - 2000 Hz needs 33 samples to cover the fequency range (602Hz + 43Hz * 33 = 2021Hz > 2000Hz) 603~2021Hz
                2000 - 3000 Hz needs 23 samples to cover the fequency range (2021Hz + 43Hz * 23 = 3010Hz > 3000Hz) 2022~3010Hz
                3000 - 4000 Hz needs 24 samples to cover the fequency range (3010Hz + 43Hz * 24 = 4042Hz > 4000Hz) 3011~4042Hz
                4000 - 6000 Hz needs 46 samples to cover the fequency range (4042Hz + 43Hz * 46 = 6020Hz > 6000Hz) 4043~6020Hz
                6000 - 20000Hz needs 326 samples to cover the fequency range (6020Hz + 43Hz * 326 = 20038Hz > 20000Hz) 6021~20038Hz
                *remain 46 samples -> add to the last band*
            */

            int sampleLength = 0;
            int count = 0;
            float average = 0f;
            for ( int i = 0; i < feqBandCount; i++ )
            {
                average = 0f;
                sampleLength = feqSampleCounts[ i ];
                for ( int j = 0; j < sampleLength; j++ )
                {
                    average += samples[ count ] * ( count + 1 );
                    count++;
                }

                average /= count;
                feqs[ i ] = average * 10f;
            }

            for ( int i = 0; i < feqBandCount; i++ )
            {
                if ( previousFeqs[ i ] > feqs[ i ] )
                {
                    continue;
                }
                else if ( feqs[ i ] - previousFeqs[ i ] >= sensitivity )
                {
                    switch ( i )
                    {
                        case 0:
                            if ( !_band1HandingCooldown )
                            {
                                StartCoroutine( ShowBand1Note() );
                            }

                            break;
                        case 1:
                            if ( !_band2HandingCooldown )
                            {
                                StartCoroutine( ShowBand2Note() );
                            }

                            break;
                        case 2:
                            if ( !_band3HandingCooldown )
                            {
                                StartCoroutine( ShowBand3Note() );
                            }

                            break;
                        case 3:
                            if ( !_band4HandingCooldown )
                            {
                                StartCoroutine( ShowBand4Note() );
                            }

                            break;
                        case 4:
                            if ( !_band5HandingCooldown )
                            {
                                StartCoroutine( ShowBand5Note() );
                            }

                            break;
                        case 5:
                            if ( !_band6HandingCooldown )
                            {
                                StartCoroutine( ShowBand6Note() );
                            }

                            break;
                        case 6:
                            if ( !_band7HandingCooldown )
                            {
                                StartCoroutine( ShowBand7Note() );
                            }

                            break;
                        case 7:
                            if ( !_band8HandingCooldown )
                            {
                                StartCoroutine( ShowBand8Note() );
                            }

                            break;
                    }
                }
            }
        }

        void CreateBufferFeq()
        {
            for ( int i = 0; i < feqBandCount; i++ )
            {
                if ( feqs[ i ] > BufferFeqs[ i ] )
                {
                    BufferFeqs[ i ] = feqs[ i ];
                    _decreaseBufferFeqs[ i ] = 0.005f;
                }
                else if ( feqs[ i ] < BufferFeqs[ i ] )
                {
                    BufferFeqs[ i ] -= _decreaseBufferFeqs[ i ];
                    _decreaseBufferFeqs[ i ] *= 1.2f;
                }
            }
        }

        #endregion

        #region Trigger Events

        IEnumerator ShowBand1Note()
        {
            _band1HandingCooldown = true;
            if ( OnBassTrigger != null )
            {
                OnBassTrigger();
            }

            yield return new WaitForSeconds( cooltimeSetting );
            _band1HandingCooldown = false;
        }

        IEnumerator ShowBand2Note()
        {
            _band2HandingCooldown = true;
            if ( OnBand2Trigger != null )
            {
                OnBand2Trigger();
            }

            yield return new WaitForSeconds( cooltimeSetting );
            _band2HandingCooldown = false;
        }

        IEnumerator ShowBand3Note()
        {
            _band3HandingCooldown = true;
            if ( OnBand3Trigger != null )
            {
                OnBand3Trigger();
            }

            yield return new WaitForSeconds( cooltimeSetting );
            _band3HandingCooldown = false;
        }

        IEnumerator ShowBand4Note()
        {
            _band4HandingCooldown = true;
            if ( OnBand4Trigger != null )
            {
                OnBand4Trigger();
            }

            yield return new WaitForSeconds( cooltimeSetting );
            _band4HandingCooldown = false;
        }

        IEnumerator ShowBand5Note()
        {
            _band5HandingCooldown = true;
            if ( OnBand5Trigger != null )
            {
                OnBand5Trigger();
            }

            yield return new WaitForSeconds( cooltimeSetting );
            _band5HandingCooldown = false;
        }

        IEnumerator ShowBand6Note()
        {
            _band6HandingCooldown = true;
            if ( OnBand6Trigger != null )
            {
                OnBand6Trigger();
            }

            yield return new WaitForSeconds( cooltimeSetting );
            _band6HandingCooldown = false;
        }

        IEnumerator ShowBand7Note()
        {
            _band7HandingCooldown = true;
            if ( OnBand7Trigger != null )
            {
                OnBand7Trigger();
            }

            yield return new WaitForSeconds( cooltimeSetting );
            _band7HandingCooldown = false;
        }

        IEnumerator ShowBand8Note()
        {
            _band8HandingCooldown = true;
            if ( OnBand8Trigger != null )
            {
                OnBand8Trigger();
            }

            yield return new WaitForSeconds( cooltimeSetting );
            _band8HandingCooldown = false;
        }

        #endregion
    }
}