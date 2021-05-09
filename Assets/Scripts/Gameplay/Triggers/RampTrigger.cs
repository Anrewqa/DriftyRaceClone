using UnityEngine;

namespace Gameplay
{
    public class RampTrigger : TriggerBase
    {
        protected override TriggerType TriggerType { get; } = TriggerType.Ramp;
    }
}