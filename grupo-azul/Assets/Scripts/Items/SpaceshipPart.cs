using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    public class SpaceshipPart : MonoBehaviour
    {
        [SerializeField] private Id id;

        public enum Id
        {
            HudNavigator,
            FluxCondensor,
            IaNeuralInterface
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            if (!other.TryGetComponent<Inventory>(out Inventory inventory)) return;
            inventory.AddSpaceshipPart(id);
            gameObject.SetActive(false);
        }
    }
}