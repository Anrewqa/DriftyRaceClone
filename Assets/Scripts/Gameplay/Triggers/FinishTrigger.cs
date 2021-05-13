namespace Gameplay
{
    public class FinishTrigger : TriggerBase
    {
        public override TriggerType TriggerType => TriggerType.Finish;

        public override ITriggerData GetTriggerInfo()
        {
            return new FinishTriggerData(TriggerType, transform.forward, transform.up);
        }
    }
}