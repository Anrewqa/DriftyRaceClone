using UnityEngine;

namespace Car
{
    public class CheckpointTracker
    {
        private int _checkpointIndex;
        
        public Vector3 LastCheckpointPosition { get; private set; }
        public bool WasMovingRight { get; private set; }

        public void SaveCheckpoint(Vector3 position, bool isMovingRight)
        {
            LastCheckpointPosition = position;
            WasMovingRight = isMovingRight;
            _checkpointIndex++;
        }
    }
}