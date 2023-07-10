using System;
using Player;
using UnityEngine;

namespace Items
{
    public class SpaceshipPartContainer : MonoBehaviour
    {
        [SerializeField] private SpaceshipPart.Id id;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private Material intalledMaterial;

        private bool _isInstalled;
        private bool _canInstall;
        private MeshRenderer _meshRenderer;

        public static Action<SpaceshipPart.Id> onPartInstalled;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _isInstalled = false;
        }

        // Agregar un event listener para detectar cuando se ejecutar la action Interact en en el New Input System
        private void OnEnable()
        {
            Inputs.onInteractInput += OnInteractInput;
        }

        private void OnDisable()
        {
            Inputs.onInteractInput -= OnInteractInput;
        }

        // Evento disparado la presionar el boton Intereact en el New Input System, E por defecto
        private void OnInteractInput()
        {
            if (!_canInstall) return;

            _isInstalled = true;
            _canInstall = false;
            _meshRenderer.material = intalledMaterial;

            onPartInstalled?.Invoke(id);
            uiManager.DesactivarMostrarMensaje();

            // Desuscribirse del evento Interact
            Inputs.onInteractInput -= OnInteractInput;
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
                UIManager.onShowMessage("Presiona \"E\" para dejar los objetos");
                _canInstall = true;
            }
            else
            {
                UIManager.onShowMessage("Pieza faltante\nEncuentrala en alguna parte del mapa");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            UIManager.onHideMessage();
            _canInstall = false;
        }
    }
}