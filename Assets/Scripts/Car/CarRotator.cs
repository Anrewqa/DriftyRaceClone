using GameSettings;
using UnityEngine;

namespace Car
{
    public class CarRotator : ICarRotator
    {
        private readonly Settings _settings;
        private readonly CarMoveData _moveData;
        private readonly Rigidbody _rigidbody;

        public ICarController Controller { get; }

        public CarRotator(ICarController controller)
        {
            Controller = controller;
            _rigidbody = controller.Rigidbody;
            _settings = controller.Settings;
            _moveData = controller.MoveData;
        }

        public void Rotate()
        {
            if (_moveData.CurrentVelocity == Vector3.zero)
            {
                return;
            }

            Quaternion velocityRotation = Quaternion.LookRotation(_moveData.CurrentVelocity, Vector3.up);

            var direction = CalculateDesiredRotation(velocityRotation);

            var deltaRotation = _settings.RotationSpeed * Time.fixedDeltaTime;

            _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, direction, deltaRotation));
        }

        private Quaternion CalculateDesiredRotation(Quaternion velocityRotation)
        {
            var angularSpeed = Mathf.Abs(_moveData.DeltaRotationAngle);
            var deflectionAngle = Mathf.Lerp(0, _settings.DeflectionSpeed, angularSpeed);
            
            var deflectionSign = Mathf.Sign(_moveData.DeltaRotationAngle);

            Quaternion deflection = Quaternion.AngleAxis(deflectionAngle * deflectionSign, Vector3.up);

            Quaternion direction = velocityRotation * deflection;
            return direction;
        }
    }
}