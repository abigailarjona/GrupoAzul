using UnityEngine;

namespace HealthSystem
{
    public interface IDamageable
    {
        public void TakeDamage(Transform attacker, int damageTaken);
    }
}