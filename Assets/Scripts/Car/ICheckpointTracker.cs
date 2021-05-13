using UnityEngine;

namespace Car
{
    public interface ICheckpointTracker
    {
        Vector3 LastCheckpointPosition { get; }
        bool WasMovingRight { get; }

        void SaveCheckpoint(Vector3 position, Quaternion rotation,  bool isMovingRight);
    }
}