using System;
using CustomInput;
using Gameplay;
using UnityEngine;

namespace Car
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private InputController _inputController;
        [SerializeField] private CarCollisionDetector _collisionDetector;
        [SerializeField] private bool _isManualMovement;

        private CarMover _carMover;
        private CarRotator _carRotator;
        
        private CheckpointTracker _checkpointTracker;

        public Settings Settings { get; private set; }
        public CarMoveData MoveData { get; private set; }
        public Transform Transform { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        
        private void Awake()
        {
            Settings = Resources.Load<Settings>(Settings.PathInResources);
            
            Rigidbody = GetComponent<Rigidbody>();
            Transform = transform;

            MoveData = new CarMoveData(Settings, Transform.forward);
            
            _carMover = new CarMover(this);
            _carRotator = new CarRotator(this);
            
            _checkpointTracker = new CheckpointTracker();
            
            OnCheckpointTriggered(Transform.position);
        }

        private void Start()
        {
            _inputController.TapInput.Tap += OnTap;
            _collisionDetector.GroundedChange += OnGroundedChanged;
            _collisionDetector.CarCollided += OnCarCollided;
            _collisionDetector.TriggeredSomething += OnTriggeredSomething;
            
            
        }
        
        private void OnDestroy()
        {
            _inputController.TapInput.Tap -= OnTap;
            _collisionDetector.GroundedChange -= OnGroundedChanged;
            _collisionDetector.CarCollided -= OnCarCollided;
            _collisionDetector.TriggeredSomething -= OnTriggeredSomething;
        }
        
        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetCar();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                AddForce(1);
            }

            UpdateMovementData();
            
            if (_isManualMovement)
            {
                _carMover.MoveForward();
                _carRotator.Rotate();
            }
        }
        
        private void UpdateMovementData()
        {
            MoveData.UpdateData();
        }

        private void OnTap()
        {
            MoveData.Turn();
        }
        
        private void OnGroundedChanged(bool isGrounded)
        {
            _isManualMovement = isGrounded;
        }

        private void OnCarCollided()
        {
            MoveData.DropSpeed();
        }

        private void OnCheckpointTriggered(Vector3 position)
        {
            _checkpointTracker.SaveCheckpoint(position, MoveData.IsMovingRight);
        }

        private void ResetCar()
        {
            transform.position = _checkpointTracker.LastCheckpointPosition;
            MoveData.ResetData(_checkpointTracker.WasMovingRight);
        }

        private void OnTriggeredSomething(TriggerData triggerData)
        {
            switch (triggerData.TriggerType)
            {
                case TriggerType.Checkpoint:
                    OnCheckpointTriggered(triggerData.Vector);
                    break;
                case TriggerType.Boost:
                    MoveData.BoostSpeed(1);
                    break;
                case TriggerType.Finish:
                    _isManualMovement = false;
                    AddForce(2, triggerData.Vector);
                    enabled = false;
                    break;
                case TriggerType.Ramp:
                    _isManualMovement = false;
                    AddForce(1);
                    break;
                case TriggerType.Default:
                    break;
                case TriggerType.Reset:
                    ResetCar();
                    break;
                case TriggerType.Coin:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AddForce(float forceMultiplier, Vector3 direction)
        {
            Rigidbody.AddForce(direction * (forceMultiplier * Settings.BaseBoostForce), ForceMode.Force);
        }

        private void AddForce(float forceMultiplier)
        {
            Rigidbody.AddForce(MoveData.DirectionForTurn * (forceMultiplier * Settings.BaseBoostForce), ForceMode.Force);
        }
    }
}