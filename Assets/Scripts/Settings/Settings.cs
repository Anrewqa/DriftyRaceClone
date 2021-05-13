using UnityEngine;

namespace GameSettings
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Assets/Settings", order = 0)]
    public class Settings : ScriptableObject
    {
        public static string PathInResources = "Settings";
        
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _angularSpeed;
        
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private AnimationCurve _deflectionAngle;
        [SerializeField] private float _deflectionSpeed;
        
        [SerializeField] private float _baseBoostSpeed;
        [SerializeField] private float _collisionSpeedDrop;

        [SerializeField] private float _baseBoostForce;

        [SerializeField] private float _finishBoostForceMultiplier;
        [SerializeField] private float _rampBoostForceMultiplier;
        
        public float MaxSpeed => _maxSpeed;
        public float Acceleration => _acceleration;
        public float BaseBoostSpeed => _baseBoostSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float CollisionSpeedDrop => _collisionSpeedDrop;
        public float AngularSpeed => _angularSpeed;

        public AnimationCurve DeflectionAngle => _deflectionAngle;

        public float DeflectionSpeed => _deflectionSpeed;

        public float BaseBoostForce => _baseBoostForce;

        public float FinishBoostForceMultiplier => _finishBoostForceMultiplier;

        public float RampBoostForceMultiplier => _rampBoostForceMultiplier;
    }
}