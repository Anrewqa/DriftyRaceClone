namespace Gameplay
{
    public class CheckpointTrigger : TriggerBase
    {
        public override TriggerType TriggerType => TriggerType.Checkpoint;
        
        public override ITriggerData GetTriggerInfo()
        {
            return new CheckpointTriggerData(TriggerType.Checkpoint, transform.position, transform.rotation);
        }
    }
}