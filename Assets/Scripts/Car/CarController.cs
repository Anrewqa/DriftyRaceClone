using System;
using CustomInput;
using Gameplay;
using GameSettings;
using UnityEngine;

namespace Car
{
    public class CarController : MonoBehaviour, ICarController
    {
        [SerializeField] private InputController _inputController;
        [SerializeField] private CarCollisionDetector _collisionDetector;
        
        private bool _isManualMovement;
        private bool _isGrounded;
        private bool _inJump;
        private ICheckpointTracker _checkpointTracker;

        public ICarMover CarMover { get; private set; }
        public ICarRotator CarRotator { get; private set; }

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
            
            CarMover = new CarMover(this);
            CarRotator = new CarRotator(this);
            
            _checkpointTracker = new CheckpointTracker();
            
            _checkpointTracker.SaveCheckpoint(Transform.position, Transform.rotation, MoveData.IsMovingRight);
        
            _collisionDetector.GroundedChange += OnGroundedChanged;
            _collisionDetector.CarCollided += OnCarCollided;
            _collisionDetector.TriggeredSomething += OnTriggeredSomething;

            GameEventsManager.Subscribe(GameEventType.LevelLoaded, OnLevelLoaded);
            GameEventsManager.Subscribe(GameEventType.RaceStarted, StartManualMovement);
            GameEventsManager.Subscribe(GameEventType.RaceEnded, StopManualMovement);
            GameEventsManager.Subscribe(GameEventType.PlayerFall, ResetCar);
        }

        private void OnDestroy()
        {
            _inputController.TapInput.Tap -= OnTap;
            _collisionDetector.GroundedChange -= OnGroundedChanged;
            _collisionDetector.CarCollided -= OnCarCollided;
            _collisionDetector.TriggeredSomething -= OnTriggeredSomething;
            
            GameEventsManager.Unsubscribe(GameEventType.LevelLoaded, OnLevelLoaded);
            GameEventsManager.Unsubscribe(GameEventType.RaceStarted, StartManualMovement);
            GameEventsManager.Unsubscribe(GameEventType.RaceEnded, StopManualMovement);
            GameEventsManager.Unsubscribe(GameEventType.PlayerFall, ResetCar);
        }

        private void OnLevelLoaded()
        {
            Rigidbody.velocity = Vector3.zero;
            Rigidbody.angularVelocity = Vector3.zero;
            ResetCar();
            StopManualMovement();
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
            
            if (_isManualMovement)
            {
                UpdateMovementData();
                
                if (_isGrounded)
                {
                    CarMover.Move();
                }

                CarRotator.Rotate();
            }
        }

        private void StartManualMovement()
        {
            _isManualMovement = true;
            _inputController.enabled = true;
            _inputController.TapInput.Tap += OnTap;
        }
        
        private void StopManualMovement()
        {
            _isManualMovement = false;
            _inputController.TapInput.Tap -= OnTap;
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
            if (_inJump && isGrounded)
            {
                StartManualMovement();
                _inJump = false;
            }
            _isGrounded = isGrounded;
        }

        private void OnCarCollided()
        {
            MoveData.DropSpeed();
        }

        private void OnCheckpointTriggered(ITriggerData triggerData)
        {
            var data = (CheckpointTriggerData) triggerData;
            _checkpointTracker.SaveCheckpoint(data.Position, data.Rotation, MoveData.IsMovingRight);
        }

        private void ResetCar()
        {
            transform.position = _checkpointTracker.LastCheckpointPosition;
            MoveData.ResetData(_checkpointTracker.WasMovingRight);
        }

        private void OnTriggeredSomething(ITriggerData triggerData)
        {
            switch (triggerData.Type)
            {
                case TriggerType.Checkpoint:
                    OnCheckpointTriggered(triggerData);
                    break;
                case TriggerType.Boost:
                    MoveData.BoostSpeed(1);
                    break;
                case TriggerType.Finish:
                    OnFinishTrigger(triggerData);
                    break;
                case TriggerType.Ramp:
                    StopManualMovement();
                    _inJump = true;
                    AddForce(Settings.RampBoostForceMultiplier);
                    break;
                case TriggerType.Default:
                    break;
                case TriggerType.Fall:
                    GameEventsManager.CallEvent(GameEventType.PlayerFall);
                    break;
                case TriggerType.Coin:
                    OnTriggerCoin();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnTriggerCoin()
        {
            PlayerDataHolder.AddCoins();
            GameEventsManager.CallEvent(GameEventType.CoinObtained);
        }

        private void OnFinishTrigger(ITriggerData triggerData)
        {
            GameEventsManager.CallEvent(GameEventType.RaceEnded);
            
            StopManualMovement();
            
            var data = (FinishTriggerData) triggerData;
            transform.rotation = Quaternion.LookRotation(data.Direction, data.Normal);
            Rigidbody.velocity = Vector3.zero;
            AddForce(Settings.FinishBoostForceMultiplier, data.Direction);
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