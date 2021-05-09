using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class TriggerBase : MonoBehaviour, ITrigger
    {
        protected virtual TriggerType TriggerType => TriggerType.Default;
        
        public virtual TriggerData GetTriggerInfo()
        {
            return new TriggerData
            {
                TriggerType = TriggerType,
                Vector = transform.position,
            };
        }
    }
}