using UnityEngine;

namespace Gameplay
{
    public class FinishTrigger : TriggerBase
    {
        protected override TriggerType TriggerType { get; } = TriggerType.Finish;

        public override TriggerData GetTriggerInfo()
        {
            return new TriggerData
            {
                TriggerType = TriggerType,
                Vector = Vector3.up
            };
        }
    }
}