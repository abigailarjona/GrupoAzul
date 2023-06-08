using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento2 : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;      // Velocidad de movimiento del personaje

    private Rigidbody rb;       // Referencia al componente Rigidbody del personaje
    private Animator animator; // Referencia al componente Animator del personaje
    private CharacterController characterController;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();      // Obtener la referencia al componente Rigidbody
        animator = GetComponent<Animator>(); // Obtener la referencia al componente Animator
        characterController = GetComponent<CharacterController>();

        // Configurar la fricción en el Rigidbody para evitar el deslizamiento
        //rb.drag = 10f;         // Fricción lineal
        //rb.angularDrag = 10f;  // Fricción angular
    }

    void Update()
    {
        Mover();    // Llamar al método Mover() en cada frame
    }

    void Mover()
    {
        float hor = Input.GetAxis("Horizontal");    // Obtener el valor de entrada horizontal (eje X)
        float ver = Input.GetAxis("Vertical");      // Obtener el valor de entrada vertical (eje Z)
        
        if (hor != 0 || ver != 0)
        {
            Vector3 direction = new Vector3(hor, 0, ver).normalized; // Normalizar la dirección de movimiento
            
            animator.SetFloat("XSpeed", direction.x);                // Configurar el parámetro de velocidad X en el Animator
            animator.SetFloat("YSpeed", direction.z);                // Configurar el parámetro de velocidad Y en el Animator
            //rb.velocity = direction * moveSpeed;                     // Aplicar la velocidad al Rigidbody en la dirección y magnitud adecuadas
        }
        else
        {
            animator.SetFloat("XSpeed", 0);     // Configurar el parámetro de velocidad X en el Animator a cero (personaje quieto)
            animator.SetFloat("YSpeed", 0);     // Configurar el parámetro de velocidad Y en el Animator a cero (personaje quieto)
            //rb.velocity = Vector3.zero;         // Detener el movimiento del Rigidbody estableciendo la velocidad a cero
        }
        
        var velocidadHorizontal = new Vector3(hor, 0, ver) * moveSpeed;
        var gravedad = Physics.gravity;
        characterController.Move((velocidadHorizontal + gravedad) * Time.deltaTime);
    }
}
