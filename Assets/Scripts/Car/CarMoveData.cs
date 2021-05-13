using System;
using Gameplay;
using GameSettings;
using UnityEngine;

namespace Car
{
    [Serializable]
    public class CarMoveData
    {
        private Settings _settings;

        public bool IsMovingRight { get; private set; }
        public float Speed { get; private set; }
        public Vector3 PreviousVelocity { get; private set; }
        public Vector3 CurrentVelocity { get; private set; }
        public float DeltaRotationAngle { get; private set; }
        
        public Vector3 DirectionForTurn => IsMovingRight ? Vector3.right : Vector3.forward;

        public CarMoveData(Settings settings, Vector3 forward)
        {
            _settings = settings;
            IsMovingRight = false;
            Speed = 0;
            CurrentVelocity = forward;
            PreviousVelocity = forward;
            DeltaRotationAngle = 0;
        }

        public void ResetData(bool isMovingRight)
        {
            IsMovingRight = isMovingRight;
            Speed = 0;
            CurrentVelocity = DirectionForTurn;
            PreviousVelocity = DirectionForTurn;
            DeltaRotationAngle = 0;
        }

        public void Turn()
        {
            IsMovingRight = !IsMovingRight;
        }

        public void BoostSpeed(float multiplier)
        {
            Speed += _settings.BaseBoostSpeed * multiplier;
        }
        
        public void DropSpeed()
        {
            Speed -= _settings.CollisionSpeedDrop;
        }
        
        public void UpdateData()
        {
            Speed = Mathf.MoveTowards(Speed, PlayerDataHolder.Speed, _settings.Acceleration * Time.deltaTime);
            PreviousVelocity = CurrentVelocity;

            var relativeAngularSpeed = _settings.AngularSpeed * Time.deltaTime;
            CurrentVelocity = Vector3.Slerp(CurrentVelocity, DirectionForTurn, relativeAngularSpeed);
            CurrentVelocity.Normalize();

            DeltaRotationAngle = Vector3.SignedAngle(PreviousVelocity, CurrentVelocity, Vector3.up);
        }
    }
}