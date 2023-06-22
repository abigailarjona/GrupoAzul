using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorCollision : MonoBehaviour
{
    public UIManager manager;       // Referencia al UIManager

    private bool estaEnRango = false;    // Indica si el jugador está dentro del rango de interacción
    private int numerito = 0;       // Variable de control para los ítems

    public GameObject item1;    // Referencia al ítem 1
    public GameObject item2;    // Referencia al ítem 2
    public GameObject item3;    // Referencia al ítem 3

    void Update()
    {
        if (estaEnRango)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                numerito++;    // Incrementa el valor de numerito cuando se presiona la tecla E
            }
        }

        switch (numerito)
        {
            case 0:
                // No se activa ningún ítem
                break;

            case 1:
                item1.SetActive(true);    // Activa el ítem 1
                break;

            case 2:
                item2.SetActive(true);    // Activa el ítem 2
                break;

            case 3:
                item3.SetActive(true);    // Activa el ítem 3
                break;

            default:
                // No se activa ningún ítem
                break;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Enemigo"))
        {
            manager.ActivarMenuPerdiste();    // Activa el menú de pérdida si colisiona con un enemigo
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.transform.CompareTag("DejarItems"))
        {
            manager.ActivarMenuItems();    // Activa la interfaz de usuario de los ítems si permanece en el rango

            estaEnRango = true;    // Indica que el jugador está en rango de interacción
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.transform.CompareTag("DejarItems"))
        {
            manager.DesactivarMenuItems();    // Desactiva la interfaz de usuario de los ítems si sale del rango
        }
    }
}
