using System;
using Core.ObjectPool;
using UnityEngine;

namespace Core.Gameplay
{
    public class PoolableObject : MonoBehaviour, IPoolable
    {
        private float _currentTimeLeft = 0.0f;

        [ Header( "Pool Object Settings" ) ]
        public float lifeTime = 3.0f;

        protected virtual void Awake()
        {
            
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            // By default, this pooled object should be inactive
            gameObject.SetActive( false );
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            // Calculate how long this object will last
            if ( gameObject.activeSelf )
            {
                // Calculate the time left
                _currentTimeLeft -= Time.deltaTime;

                // When the timer is less than zero, this object is going to deactivate
                if ( _currentTimeLeft <= 0.0f )
                {
                    gameObject.SetActive( false );
                }
            }
        }

        public virtual void Spawn()
        {
            // Set the time left to the life time
            _currentTimeLeft = lifeTime;
            // Show the object to the screen
            gameObject.SetActive( true );
        }

        public virtual void Despawn()
        {
            // Force set the time left to zero, in case the life time is not finish
            _currentTimeLeft = 0.0f;
            // Hide this object from screen
            gameObject.SetActive( false );
        }
    }
}