using UnityEngine;

namespace Gameplay
{
    public struct FinishTriggerData : ITriggerData
    {
        public FinishTriggerData(TriggerType type, Vector3 direction, Vector3 normal)
        {
            Type = type;
            Direction = direction;
            Normal = normal;
        }
        
        public TriggerType Type { get; }
        public Vector3 Direction{ get; }
        public Vector3 Normal{ get; }
    }
}