using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int rutina; // Variable para almacenar la rutina actual del enemigo
    public float cronometro; // Variable para contar el tiempo transcurrido
    public Animator ani; // Referencia al componente Animator del enemigo
    public Quaternion angulo; // Ángulo de rotación para la rutina de rotación
    public float grado; // Grado de rotación para la rutina de rotación

    public GameObject target; // Objeto objetivo al que el enemigo persigue
    public bool atacando; // Indicador de si el enemigo está atacando

    void Start()
    {
        ani = GetComponent<Animator>(); // Obtener el componente Animator del objeto enemigo
        target = GameObject.Find("Vanguard By T. Choonyung"); // Buscar el objeto objetivo por su nombre en la escena
    }

    public void Comportamiento_Enemigo()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 8)
        {
            // Si la distancia entre el enemigo y el objetivo es mayor a 5 unidades, realizar rutina de patrulla

            ani.SetBool("Run", false); // Desactivar la animación de carrera del enemigo

            cronometro += 1 * Time.deltaTime; // Incrementar el cronómetro

            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2); // Generar un número aleatorio para seleccionar una rutina
                cronometro = 0; // Reiniciar el cronómetro
            }
   switch (rutina)
    {
    case 0:
        // Acción de la rutina 0: detenerse y desactivar la animación de caminar
        ani.SetBool("Walk", false);
        break;
    case 1:
        // Acción de la rutina 1: generar un ángulo aleatorio y rotar gradualmente hacia él
        grado = Random.Range(0, 360);
        angulo = Quaternion.Euler(0, grado, 0);
        rutina++;
        break;
    case 2:
        // Acción de la rutina 2: rotar hacia el ángulo objetivo y moverse hacia adelante
        transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
        transform.Translate(Vector3.forward * 1 * Time.deltaTime);
        ani.SetBool("Walk", true);
        break;
    }
    }
    else
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 1 && !atacando)
    {
        // Si el enemigo no está cerca del objetivo y no está atacando, realizar acciones de aproximación al objetivo
        
        var lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);

        ani.SetBool("Walk", false);

        ani.SetBool("Run", true);
        transform.Translate(Vector3.forward * 4 * Time.deltaTime);

        ani.SetBool("Attack", false);
    }
    else
    {
       // Si el enemigo está cerca del objetivo, realizar rutina de ataque

        ani.SetBool("Walk", false); // Desactivar la animación de caminar
        ani.SetBool("Run", false); // Desactivar la animación de correr

        ani.SetBool("Attack", true); // Activar la animación de ataque
        atacando = true; // Establecer el estado de ataque como verdadero
    }
    }
}
    public void Final_Ani()
{
    // Método llamado al finalizar la animación de ataque

    ani.SetBool("Attack", false); // Desactivar la animación de ataque
    atacando = false; // Establecer el estado de ataque como falso
}


    void Update()
    {
        Comportamiento_Enemigo(); // Llamar al comportamiento del enemigo en cada actualización
    }
}
