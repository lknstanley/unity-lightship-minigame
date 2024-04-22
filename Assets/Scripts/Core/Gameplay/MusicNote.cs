using UnityEngine;

namespace Core.Gameplay
{
    public class MusicNote : PoolableObject
    {
        [ SerializeField ]
        private MeshRenderer meshRenderer;

        private float _speed = 5f;
        
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
            transform.position += -transform.forward * _speed * Time.deltaTime;
        }
        
        public override void Spawn()
        {
            base.Spawn();
        }

        public void Initialize( Vector3 position, Vector3 scale, Quaternion rotation, Color color, float speed = 5f)
        {
            transform.position = position;
            transform.localScale = scale;
            transform.rotation = rotation;
            // meshRenderer.material.color = new Color( color.r, color.g, color.b, 0.95f );
            _speed = speed;
        }

        public override void Despawn()
        {
            base.Despawn();
        }
    }
}