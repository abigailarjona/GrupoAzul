using System;
using UnityEngine;

namespace Scenario
{
    public class RestrictedArea : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                UIManager.ShowMessage("Zona no disponible");
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                UIManager.HideMessage();
        }
    }
}