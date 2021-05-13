using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class TriggerBase : MonoBehaviour, ITrigger
    {
        public virtual TriggerType TriggerType => TriggerType.Default;
        
        public virtual ITriggerData GetTriggerInfo()
        {
            return new TriggerTypeData(TriggerType);
        }
    }
}