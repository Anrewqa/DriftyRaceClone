using UnityEngine;

namespace Gameplay
{
    public class BoostTrigger : TriggerBase
    {
        protected override TriggerType TriggerType { get; } = TriggerType.Boost;
        
        private void OnTriggerEnter(Collider other)
        {
            gameObject.SetActive(false);
        }
    }
}