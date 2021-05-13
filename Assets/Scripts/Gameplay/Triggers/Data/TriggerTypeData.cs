namespace Gameplay
{
    public struct TriggerTypeData : ITriggerData
    {
        public TriggerTypeData(TriggerType type)
        {
            Type = type;
        }
        
        public TriggerType Type { get; }
    }
}