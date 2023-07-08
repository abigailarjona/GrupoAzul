using UnityEngine;

namespace HealthSystem
{
    public class Daño : MonoBehaviour
    {
        public GameObject player;
        public int cantidad;
        public float damageTime;

        float currentDamageTime;

        // Start is called before the first frame update
        private void Start()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                currentDamageTime -= Time.deltaTime;
                if (currentDamageTime < damageTime)
                {
                    other.gameObject.GetComponent<IDamageable>().TakeDamage(transform, cantidad);
                    currentDamageTime = 0.0f;
                }
            }
        }
    }
}