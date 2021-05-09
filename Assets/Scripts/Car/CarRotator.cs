using UnityEngine;

namespace Car
{
    public class CarRotator
    {
        private readonly Settings _settings;
        private readonly CarMoveData _moveData;
        private readonly Rigidbody _rigidbody;
        
        public CarRotator(CarController controller)
        {
            _rigidbody = controller.Rigidbody;
            _settings = controller.Settings;
            _moveData = controller.MoveData;
        }

        public void Rotate()
        {
            if (_moveData.CurrentVelocity != Vector3.zero)
            {
                Quaternion velocityRotation = Quaternion.LookRotation(_moveData.CurrentVelocity, Vector3.up);

                var deflectionSign = Mathf.Sign(_moveData.DeltaRotationAngle);
                var angularSpeed = Mathf.Abs(_moveData.DeltaRotationAngle);

                var deflectionAngle = Mathf.Lerp(0, _settings.DeflectionSpeed, angularSpeed);

                Quaternion deflection = Quaternion.AngleAxis(deflectionAngle * deflectionSign, Vector3.up);

                Debug.DrawRay(Vector3.zero, deflection * Vector3.forward, Color.blue);
                Quaternion direction = velocityRotation * deflection;

                Debug.DrawRay(Vector3.zero, direction * Vector3.forward, Color.yellow);

                var deltaRotation = _settings.RotationSpeed * Time.fixedDeltaTime;

                _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, direction, deltaRotation));
            }
        }
    }
}