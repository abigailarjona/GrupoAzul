using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class EnemyControllerOld : MonoBehaviour
{
    [Header("Enemy NPC")]
    [SerializeField] private int rutina; // Variable para almacenar la rutina actual del enemigo
    [SerializeField] private float cronometro; // Variable para contar el tiempo transcurrido
    [SerializeField] private Animator ani; // Referencia al componente Animator del enemigo
    [SerializeField] private Quaternion angulo; // Ángulo de rotación para la rutina de rotación

    [SerializeField] private float grado; // Grado de rotación para la rutina de rotación

    [SerializeField] private bool atacando; // Indicador de si el enemigo está atacando

    
    private GameObject _target; // Objeto objetivo al que el enemigo persigue
    
    // Id de los parámetros utilizados por el Animator
    private static readonly int RunParamId = Animator.StringToHash("Run");
    private static readonly int WalkParamId = Animator.StringToHash("Walk");
    private static readonly int AttackParamId = Animator.StringToHash("Attack");

    private void Start()
    {
        if (ani == null)
        {
            Debug.LogError("Enemy animator component is missing.");
        }

        _target = GameObject.FindGameObjectWithTag("Player");
        if (_target == null)
        {
            Debug.LogError("Target not found. The scene must have a GameObject with tag \"Player\"");
        }
    }

    private void Comportamiento_Enemigo()
    {
        // Rango de detección del enemigo
        const float detectionRange = 8f;
        const float approximationRange = 1f;

        if (Vector3.SqrMagnitude(_target.transform.position - transform.position) > detectionRange * detectionRange)
        {
            // Si la distancia entre el enemigo y el objetivo es mayor a detectionRange, realizar rutina de patrulla

            ani.SetBool(RunParamId, false); // Desactivar la animación de carrera del enemigo

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
                    ani.SetBool(WalkParamId, false);
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
                    transform.Translate(Vector3.forward * (1 * Time.deltaTime));
                    ani.SetBool(WalkParamId, true);
                    break;
            }
        }
        else
        {
            if (Vector3.SqrMagnitude(_target.transform.position - transform.position) >
                approximationRange * approximationRange &&
                !atacando)
            {
                // Si el enemigo no está cerca del objetivo y no está atacando, realizar acciones de aproximación al objetivo

                var lookPos = _target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);

                ani.SetBool(WalkParamId, false);

                ani.SetBool(RunParamId, true);
                transform.Translate(Vector3.forward * (4 * Time.deltaTime));

                ani.SetBool(AttackParamId, false);
            }
            else
            {
                // Si el enemigo está cerca del objetivo, realizar rutina de ataque

                ani.SetBool(WalkParamId, false); // Desactivar la animación de caminar
                ani.SetBool(RunParamId, false); // Desactivar la animación de correr

                ani.SetBool(AttackParamId, true); // Activar la animación de ataque
                atacando = true; // Establecer el estado de ataque como verdadero
            }
        }
    }

    public void Final_Ani()
    {
        // Método llamado al finalizar la animación de ataque

        ani.SetBool(AttackParamId, false); // Desactivar la animación de ataque
        atacando = false; // Establecer el estado de ataque como falso
    }


    private void Update()
    {
        Comportamiento_Enemigo(); // Llamar al comportamiento del enemigo en cada actualización
    }
}