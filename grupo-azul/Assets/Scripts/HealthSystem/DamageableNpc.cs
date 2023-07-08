using Enemy;
using UnityEngine;

namespace HealthSystem
{
    public class DamageableNpc : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth = 100f;

        private EnemyController _target;
        private float _currentHealth;
        private bool _isAlive;

        private void Awake()
        {
            _target = GetComponent<EnemyController>();
            _currentHealth = maxHealth;
            _isAlive = true;
        }

        public void TakeDamage(Transform attacker, int damageTaken)
        {
            if (!_isAlive) return;
            _target.OnHurt(attacker);
            _currentHealth -= damageTaken;
            
            if (_currentHealth > 0) return;
            _isAlive = false;
            _target.OnDeath();
        }
    }
}