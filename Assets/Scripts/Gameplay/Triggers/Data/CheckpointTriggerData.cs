using UnityEngine;

namespace Gameplay
{
    public struct CheckpointTriggerData : ITriggerData
    {
        public CheckpointTriggerData(TriggerType type, Vector3 position, Quaternion rotation)
        {
            Type = type;
            Position = position;
            Rotation = rotation;
        }

        public TriggerType Type { get; }
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
    }
}