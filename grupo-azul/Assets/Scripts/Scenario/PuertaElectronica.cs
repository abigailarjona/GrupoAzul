using Items;
using UnityEngine;

namespace Scenario
{
    public class PuertaElectronica : MonoBehaviour
    {
        public GameObject puertaIzquierda;
        public GameObject puertaDerecha;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Inventory inventory) && inventory.HudNavigator)
            {
                puertaIzquierda.GetComponent<Animator>().enabled = true;
                puertaDerecha.GetComponent<Animator>().enabled = true;
            }
            else
            {
                UIManager.ShowMessage("Debes recoger la parte faltante de la nave antes de poder continuar");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            UIManager.HideMessage();
        }

        public void AbrirPuerta()
        {
            puertaIzquierda.GetComponent<Animator>().enabled = true;
            puertaDerecha.GetComponent<Animator>().enabled = true;
        }
    }
}