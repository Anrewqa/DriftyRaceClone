using UnityEngine;

namespace Gameplay
{
    public class CheckpointTrigger : MonoBehaviour, ITrigger
    {
        public TriggerData GetTriggerInfo()
        {
            return new TriggerData {TriggerType = TriggerType.Checkpoint};
        }

        public void RespondToTrigger()
        {
        }
    }
}