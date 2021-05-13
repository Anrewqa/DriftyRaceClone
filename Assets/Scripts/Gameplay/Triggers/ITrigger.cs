namespace Gameplay
{
    public interface ITrigger
    {
        public TriggerType TriggerType { get; }
        public ITriggerData GetTriggerInfo();
    }
}