using System;
using Gameplay;
using UnityEngine;

namespace Car
{
    public class CarCollisionDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask _roadMask;
        [SerializeField] private float _rayDistance = 5f;
        
        public event Action CarCollided;
        public event Action<bool> GroundedChange;
        public event Action<ITriggerData> TriggeredSomething;

        private bool _isGrounded;

        private void Update()
        {
            var position = transform.position;
            var isGrounded = Physics.Raycast(position + Vector3.up, Vector3.down, out var hit, _rayDistance, _roadMask);
            
            /*Debug.DrawLine(position + Vector3.up, position + Vector3.down * 5, Color.yellow);
            Debug.DrawLine(position, hit.point, isGrounded ? Color.green : Color.red);*/

            if (isGrounded != _isGrounded)
            {
                _isGrounded = isGrounded;
                GroundedChange?.Invoke(isGrounded);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if ((_roadMask & (1 << other.gameObject.layer)) == 0)
            {
                Debug.Log($"{name} collided with {other.gameObject.name}");
                CarCollided?.Invoke();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out ITrigger trigger))
            {
                TriggeredSomething?.Invoke(trigger.GetTriggerInfo());
            }
        }
    }
}