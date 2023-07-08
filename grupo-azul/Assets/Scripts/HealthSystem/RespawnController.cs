using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace HealthSystem
{
    public class RespawnController : MonoBehaviour
    {
        // Referencia al script de la barra de vida
        public BarraVida barraVida;

        // Referencia al objeto que se va a respawnear
        public GameObject objetoARespawnear;

        // Tiempo de respawn en segundos
        public float tiempoRespawn = 3f;

        // Posición de respawn predefinida
        public Vector3 respawnPosition;


        // Método para respawnear el objeto
        private void Start()
        {
        }


        public IEnumerator Respawn()
        {
            // Reproducir la animación de transición antes del respawn


            // Restablecer la vida utilizando el método RestablecerVida del script de la barra de vida

            // Desactivar el objeto
            objetoARespawnear.SetActive(false);

            yield return new WaitForSeconds(tiempoRespawn);

            // Reposicionar el objeto a la posición de respawn predefinida
            objetoARespawnear.transform.position = respawnPosition;

            // Invocar el método de respawn después de un cierto tiempo
            ActivarObjeto();
        }

        // Método para activar el objeto después del tiempo de respawn
        private void ActivarObjeto()
        {
            // Reactivar el objeto que contiene el script RespawnController
            gameObject.SetActive(true);
        }
    }
}