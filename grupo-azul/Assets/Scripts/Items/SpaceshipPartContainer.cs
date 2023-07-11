using System;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    public class SpaceshipPartContainer : MonoBehaviour
    {
        [SerializeField] private SpaceshipPart.Id id;
        [SerializeField] private Material installedMaterial;

        private bool _isInstalled;
        private bool _canInstall;
        private MeshRenderer _meshRenderer;

        public static Action<SpaceshipPart.Id> OnSpaceshipPartInstalled;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _isInstalled = false;
        }

        // Agregar un event listener para detectar cuando se ejecutar la action Interact en en el New Input System
        private void OnEnable()
        {
            Inputs.OnInteractInput += OnInteractInput;
        }

        private void OnDisable()
        {
            Inputs.OnInteractInput -= OnInteractInput;
        }

        // Evento disparado la presionar el boton Intereact en el New Input System, E por defecto
        private void OnInteractInput()
        {
            if (!_canInstall) return;

            _isInstalled = true;
            _canInstall = false;
            _meshRenderer.material = installedMaterial;

            OnSpaceshipPartInstalled?.Invoke(id);
            UIManager.HideMessage();

            // Desuscribirse del evento Interact
            Inputs.OnInteractInput -= OnInteractInput;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Comprobar que la pieza no este instalada
            if (_isInstalled) return;

            // Comprobar que el jugador haya entrado en collider
            if (!other.CompareTag("Player")) return;

            // Verificar si la pieza fue recogida
            if (other.TryGetComponent(out Inventory inventory) && inventory.WasCollected(id))
            {
                UIManager.ShowMessage("Presiona \"E\" para dejar los objetos");
                _canInstall = true;
            }
            else
            {
                UIManager.ShowMessage("Pieza faltante\nEncuentrala en alguna parte del mapa");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            UIManager.HideMessage();
            _canInstall = false;
        }
    }
}