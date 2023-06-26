using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    // Referencia al script de la barra de vida
    public BarraVIda barraVida;

    // Referencia al objeto que se va a respawnear
    public GameObject objetoARespawnear;

    // Tiempo de respawn en segundos
    public float tiempoRespawn = 3f;

    // Posición de respawn predefinida
    public Vector3 respawnPosition;

    

    // Método para respawnear el objeto
    void Start()
    {
         // Llamar al método Respawn después de un cierto tiempo
    Invoke("Respawn", tiempoRespawn);
        
    }

 
    public void Respawn()
    {
        // Reproducir la animación de transición antes del respawn
        


        // Restablecer la vida utilizando el método RestablecerVida del script de la barra de vida
        barraVida.RestablecerVida();

        // Desactivar el objeto
        objetoARespawnear.SetActive(false);

        // Invocar el método de respawn después de un cierto tiempo
        Invoke("ActivarObjeto", tiempoRespawn);

        // Reposicionar el objeto a la posición de respawn predefinida
        objetoARespawnear.transform.position = respawnPosition;
    }

    // Método para activar el objeto después del tiempo de respawn
    private void ActivarObjeto()
    {
         // Reactivar el objeto que contiene el script RespawnController
    gameObject.SetActive(true);
        
    }
}
