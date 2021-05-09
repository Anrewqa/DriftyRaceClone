using UnityEngine;

namespace Car
{
    public class CarModelMover : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _speed;
        [SerializeField] private float _maxDelta;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            var speed = _speed * Time.fixedDeltaTime;
            var currentPosition = transform.position;
            var nextPosition = Vector3.MoveTowards(currentPosition, _target.position, speed);

            _rb.AddForce((_target.position - currentPosition) * speed, ForceMode.Acceleration);
            
            // _rb.MovePosition(nextPosition);
            // _rb.MoveRotation(quaternion.LookRotation(nextPosition - currentPosition, Vector3.up));
        }
    }
}
