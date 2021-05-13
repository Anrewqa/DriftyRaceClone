using UnityEngine;

namespace Gameplay
{
    public class BoostTrigger : TriggerBase
    {
        public override TriggerType TriggerType => TriggerType.Boost;
        
        private void OnTriggerEnter(Collider other)
        {
            gameObject.SetActive(false);
        }
    }
}