using System;
using UnityEngine;

namespace HealthSystem
{
    public class AcidFluid : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Acid entered");
            IDamageable target = other.transform.GetComponentInParent<IDamageable>();
            target?.TakeDamage(transform, 999);
        }
    }
}