using UnityEngine;

namespace Car
{
    public class CheckpointTracker : ICheckpointTracker
    {
        private int _checkpointIndex;
        
        public Vector3 LastCheckpointPosition { get; private set; }
        public Quaternion LastCheckpointRotation { get; private set; }
        public bool WasMovingRight { get; private set; }

        public void SaveCheckpoint(Vector3 position, Quaternion rotation, bool isMovingRight)
        {
            LastCheckpointPosition = position;
            LastCheckpointRotation = rotation;
            WasMovingRight = isMovingRight;
            _checkpointIndex++;
        }
    }
}