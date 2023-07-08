using System;
using System.Collections;
using HealthSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class EnemyWeapon : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private AnimationClip attackAnimation;

        private bool _isPlayingAttackAnimation;
        private bool _canDamage;

        private static readonly int AttackTrigger = Animator.StringToHash("Attack");

        private void OnEnable()
        {
            _canDamage = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_canDamage || !IsPlayer(other)) return;
            if (!other.TryGetComponent<IDamageable>(out IDamageable damageableTarget)) return;
            damageableTarget.TakeDamage(transform, 20);
            _canDamage = false;
        }

        private static bool IsPlayer(Component component)
        {
            return component.CompareTag("Player");
        }

        public void Attack()
        {
            StartCoroutine(PlayAttackAnimation());
        }

        private IEnumerator PlayAttackAnimation()
        {
            const float attackAnimationOffset = 1.2f;
            animator.SetTrigger(AttackTrigger);
            yield return new WaitForSeconds(attackAnimationOffset);
            _canDamage = true;
            yield return new WaitForSeconds(attackAnimation.length);
            _canDamage = false;
        }

        public float GetAttackAnimationLength()
        {
            return attackAnimation.length;
        }
    }
}