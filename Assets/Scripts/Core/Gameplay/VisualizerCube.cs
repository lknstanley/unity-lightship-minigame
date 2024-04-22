using UnityEngine;

namespace Core.Gameplay
{
    public class VisualizerCube : MonoBehaviour
    {
        [ SerializeField ]
        private MeshRenderer meshRenderer;

        public void Initialize()
        {
            meshRenderer.material.color = new Color( 
                Random.Range( 0f, 1f ),
                Random.Range( 0f, 1f ), 
                Random.Range( 0f, 1f ) );
        }

        public Color GetColor()
        {
            return meshRenderer.material.color;
        }
    }
}
