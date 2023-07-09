using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    public class SpaceshipPartContainer : MonoBehaviour
    {
        [SerializeField] private SpaceshipPart.Id id;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private Material intalledMaterial;

        private bool IsInstalled { get; set; }
        private Spaceship _spaceship;
        private bool _canInstall;
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _spaceship = GetComponentInParent<Spaceship>();
            IsInstalled = false;
        }

        private void Update()
        {
            if (!_canInstall || !Input.GetKeyDown(KeyCode.E)) return;
            IsInstalled = true;
            _canInstall = false;
            _meshRenderer.material = intalledMaterial;
            _spaceship.InstallPart(id);
            uiManager.DesactivarMostrarMensaje();
        }

        private void OnTriggerEnter(Collider other)
        {
            // Comprobar que la pieza no este instalada
            if (IsInstalled) return;

            // Comprobar que el jugador haya entrado en collider
            if (!other.CompareTag("Player")) return;

            // Verificar si la pieza fue recogida
            if (other.TryGetComponent(out Inventory inventory) && inventory.WasCollected(id))
            {
                uiManager.ActivarMostrarMensaje("Presiona \"E\" para dejar los objetos");
                _canInstall = true;
            }
            else
            {
                uiManager.ActivarMostrarMensaje("Pieza faltante\nEncuentrala en alguna parte del mapa");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            uiManager.DesactivarMostrarMensaje();
            _canInstall = false;
        }
    }
}