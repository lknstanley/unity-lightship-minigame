using UnityEngine;

namespace Core.Gameplay
{
    public class ARPlacer : MonoBehaviour
    {
        public bool RequestPlacement( GameObject objectToPlace )
        {
            // Check if ARCursor is detecting ground
            if ( BeatGameManager.Instance.GetARCursor().IsDetectingGround() )
            {
                // Get cursor position
                Vector3 cursorPosition = BeatGameManager.Instance.GetARCursor().GetCursorPosition();
                // Place object at cursor position
                objectToPlace.transform.position = cursorPosition;
                // Copy rotation
                objectToPlace.transform.rotation = BeatGameManager.Instance.GetARCursor().transform.rotation;
                // Reset x rotation to zero
                objectToPlace.transform.eulerAngles = new Vector3( 0, objectToPlace.transform.eulerAngles.y, objectToPlace.transform.eulerAngles.z );
                // Kickstart the object
                objectToPlace.SetActive( true );

                return true;
            }

            return false;
        }
    }
}