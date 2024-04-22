using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Core
{
    public class ARCursor : MonoBehaviour
    {
        [ SerializeField ]
        private GameObject cursor;
        private Camera _camera;
        private bool _isDetectingGround;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void FixedUpdate()
        {
            // Use camera position and direction to fire a ray and detect hit point and move the cursor
            Ray ray = new Ray( _camera.transform.position, _camera.transform.forward );
            if ( Physics.Raycast( ray, out RaycastHit hit, 1000f, LayerMask.GetMask( "ARGround" ) ) )
            {
                _isDetectingGround = true;
                cursor.SetActive( true );
                cursor.transform.position = hit.point;
                cursor.transform.LookAt( new Vector3( _camera.transform.position.x, hit.point.y, _camera.transform.position.z ) );
            }
            else
            {
                _isDetectingGround = false;
                cursor.SetActive( false );
            }
        }

        public Vector3 GetCursorPosition()
        {
            return cursor.transform.position;
        }
        
        public bool IsDetectingGround()
        {
            return _isDetectingGround;
        }
    }
}
