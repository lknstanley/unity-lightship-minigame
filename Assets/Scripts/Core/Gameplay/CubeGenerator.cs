using Core.ObjectPool;
using UnityEngine;

namespace Core.Gameplay
{
    public class CubeGenerator : MonoBehaviour
    {
        private ObjectPool< MusicNote > _notePool;

        [ SerializeField ]
        private MusicNote musicNoteTemplate;
        private float _musicNoteSpeed = 2f;

        [ Header( "Pool Settings" ) ]
        [ SerializeField ]
        private int maxNumberOfNotesOnScreen = 50;

        private void Awake()
        {
            _notePool = new ObjectPool< MusicNote >(
                maxNumberOfNotesOnScreen,
                musicNoteTemplate,
                ( item, index ) => { item.gameObject.transform.SetParent( transform ); } );
        }

        private void Start()
        {
            // Mapping Action from audio analyzer to the spawn method
            BeatGameManager.Instance.GetAudioAnalyzer().OnBassTrigger += OnBassTrigger;
            BeatGameManager.Instance.GetAudioAnalyzer().OnBand2Trigger += OnBand2Trigger;
            BeatGameManager.Instance.GetAudioAnalyzer().OnBand3Trigger += OnBand3Trigger;
            BeatGameManager.Instance.GetAudioAnalyzer().OnBand4Trigger += OnBand4Trigger;
            BeatGameManager.Instance.GetAudioAnalyzer().OnBand5Trigger += OnBand5Trigger;
            BeatGameManager.Instance.GetAudioAnalyzer().OnBand6Trigger += OnBand6Trigger;
            BeatGameManager.Instance.GetAudioAnalyzer().OnBand7Trigger += OnBand7Trigger;
            BeatGameManager.Instance.GetAudioAnalyzer().OnBand8Trigger += OnBand8Trigger;
        }

        private void OnDestroy()
        {
            // Unmapping Actions from audio analyzer
            BeatGameManager.Instance.GetAudioAnalyzer().OnBassTrigger -= OnBassTrigger;
            BeatGameManager.Instance.GetAudioAnalyzer().OnBand2Trigger -= OnBand2Trigger;
            BeatGameManager.Instance.GetAudioAnalyzer().OnBand3Trigger -= OnBand3Trigger;
            BeatGameManager.Instance.GetAudioAnalyzer().OnBand4Trigger -= OnBand4Trigger;
            BeatGameManager.Instance.GetAudioAnalyzer().OnBand5Trigger -= OnBand5Trigger;
            BeatGameManager.Instance.GetAudioAnalyzer().OnBand6Trigger -= OnBand6Trigger;
            BeatGameManager.Instance.GetAudioAnalyzer().OnBand7Trigger -= OnBand7Trigger;
            BeatGameManager.Instance.GetAudioAnalyzer().OnBand8Trigger -= OnBand8Trigger;
        }

        void OnBassTrigger()
        {
            Debug.Log( "bass trigger" );
            var note = _notePool.GetObjectFromPool();
            note.Despawn();
            var av = BeatGameManager.Instance.GetAudioVisualizer();
            note.Initialize( av.GetBandPosition( 0 ), av.GetBandScale( 0 ), av.GetBandRotation(), av.GetBandColor( 0 ), _musicNoteSpeed );
            note.Spawn();
        }

        void OnBand2Trigger()
        {
            Debug.Log( "band 2 trigger" );
            var note = _notePool.GetObjectFromPool();
            note.Despawn();
            var av = BeatGameManager.Instance.GetAudioVisualizer();
            note.Initialize( av.GetBandPosition( 1 ), av.GetBandScale( 1 ), av.GetBandRotation(), av.GetBandColor( 1 ), _musicNoteSpeed );
            note.Spawn();
        }

        void OnBand3Trigger()
        {
            Debug.Log( "band 3 trigger" );
            var note = _notePool.GetObjectFromPool();
            note.Despawn();
            var av = BeatGameManager.Instance.GetAudioVisualizer();
            note.Initialize( av.GetBandPosition( 2 ), av.GetBandScale( 2 ), av.GetBandRotation(), av.GetBandColor( 2 ), _musicNoteSpeed );
            note.Spawn();
        }

        void OnBand4Trigger()
        {
            Debug.Log( "band 4 trigger" );
            var note = _notePool.GetObjectFromPool();
            note.Despawn();
            var av = BeatGameManager.Instance.GetAudioVisualizer();
            note.Initialize( av.GetBandPosition( 3 ), av.GetBandScale( 3 ), av.GetBandRotation(), av.GetBandColor( 3 ), _musicNoteSpeed );
            note.Spawn();
        }

        void OnBand5Trigger()
        {
            Debug.Log( "band 5 trigger" );
            var note = _notePool.GetObjectFromPool();
            note.Despawn();
            var av = BeatGameManager.Instance.GetAudioVisualizer();
            note.Initialize( av.GetBandPosition( 4 ), av.GetBandScale( 4 ), av.GetBandRotation(), av.GetBandColor( 4 ), _musicNoteSpeed );
            note.Spawn();
        }

        void OnBand6Trigger()
        {
            Debug.Log( "band 6 trigger" );
            var note = _notePool.GetObjectFromPool();
            note.Despawn();
            var av = BeatGameManager.Instance.GetAudioVisualizer();
            note.Initialize( av.GetBandPosition( 5 ), av.GetBandScale( 5 ), av.GetBandRotation(), av.GetBandColor( 5 ), _musicNoteSpeed );
            note.Spawn();
        }

        void OnBand7Trigger()
        {
            Debug.Log( "band 7 trigger" );
            var note = _notePool.GetObjectFromPool();
            note.Despawn();
            var av = BeatGameManager.Instance.GetAudioVisualizer();
            note.Initialize( av.GetBandPosition( 6 ), av.GetBandScale( 6 ), av.GetBandRotation(), av.GetBandColor( 6 ), _musicNoteSpeed );
            note.Spawn();
        }

        void OnBand8Trigger()
        {
            Debug.Log( "band 8 trigger" );
            var note = _notePool.GetObjectFromPool();
            note.Despawn();
            var av = BeatGameManager.Instance.GetAudioVisualizer();
            note.Initialize( av.GetBandPosition( 7 ), av.GetBandScale( 7 ), av.GetBandRotation(), av.GetBandColor( 7 ), _musicNoteSpeed );
            note.Spawn();
        }

        public void SetNoteSpeed( float val )
        {
            _musicNoteSpeed = val;
        }

        public float GetNoteSpeed() => _musicNoteSpeed;
    }
}