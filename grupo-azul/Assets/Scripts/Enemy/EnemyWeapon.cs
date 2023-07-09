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
        [SerializeField] [Min(1f)] private float attackSpeed = 1f;
        [SerializeField] [Min(1f)] private int attackDamage = 20;

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
            damageableTarget.TakeDamage(transform, attackDamage);
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
            // A partir de que momento de la animación el collider puede hacer daño
            float attackAnimationOffset = attackAnimation.length / 3f * attackSpeed;
            animator.speed = attackSpeed;
            animator.SetTrigger(AttackTrigger);
            yield return new WaitForSeconds(attackAnimationOffset);
            _canDamage = true;
            yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length -
                                            attackAnimationOffset);
            _canDamage = false;
            animator.speed = 1f;
        }

        public float GetAttackCooldown()
        {
            return attackAnimation.length * attackSpeed;
        }
    }
}