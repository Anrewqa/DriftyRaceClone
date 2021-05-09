using UnityEngine;

namespace Gameplay
{
    public class CoinTrigger : TriggerBase
    {
        [SerializeField] private ParticleSystem _particleSystem;
        
        protected override TriggerType TriggerType { get; } = TriggerType.Coin;

        private void OnTriggerEnter(Collider other)
        {
            Instantiate(_particleSystem, transform.position, Quaternion.identity);

            gameObject.SetActive(false);
        }
    }
}