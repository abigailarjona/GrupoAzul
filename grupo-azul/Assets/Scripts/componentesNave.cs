using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class componenteNave : MonoBehaviour
{
    public UIManager manager;
    public Transform posicionAparecerItem1;
    public Transform posicionAparecerItem2;
    public Transform posicionAparecerItem3;

    public bool agarreItem1;
    public bool agarreItem2;
    public bool agarreItem3;

    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject item4;
    public GameObject item5;
    public GameObject item6;
    public GameObject item7;

    private bool rangoitem1 = false; // Indica si el jugador está dentro del rango de interacción
    private bool rangoitem2 = false; // Indica si el jugador está dentro del rango de interacción
    private bool rangoitem3 = false; // Indica si el jugador está dentro del rango de interacción
    private bool parte1 = false;
    private bool parte2 = false;
    private bool parte3 = false;

    void Update()
    {
        if (rangoitem1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("El HUD de navegacion fue colocado de manera correcta");
                item1.SetActive(true);
                //cambiar la posicion del objeto que agarre
                item1.transform.position = posicionAparecerItem1.position;
                parte1 = true;
            }
        }

        if (rangoitem2)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("El condensador de flujo fue colocado de manera correcta");
                item2.SetActive(true);
                //cambiar la posicion del objeto que agarre
                item2.transform.position = posicionAparecerItem2.position;
                parte2 = true;
            }
        }

        if (rangoitem3)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("La interfaz de inteligencia artificial fue colocada de manera correcta");
                item3.SetActive(true);
                //cambiar la posicion del objeto que agarre
                item3.transform.position = posicionAparecerItem3.position;
                parte3 = true;
            }
        }

        if (parte1)
        {
            if (parte2)
            {
                if (parte3)
                {
                    item4.SetActive(true);
                    item5.SetActive(true);
                    item7.SetActive(true);
                }
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("item-1"))
        {
            Debug.Log("Has tomado el HUD de navegaciom");
            agarreItem1 = true;
            item1 = col.gameObject;
            col.gameObject.SetActive(false);
        }

        if (col.gameObject.CompareTag("item-2"))
        {
            agarreItem2 = true;
            Debug.Log("Has tomado el condensador de flujo");
            item2 = col.gameObject;
            col.gameObject.SetActive(false);
        }

        if (col.gameObject.CompareTag("item-3"))
        {
            agarreItem3 = true;
            Debug.Log("Has tomado la interfaz de inteligencia artificial");
            item3 = col.gameObject;
            col.gameObject.SetActive(false);
        }


        if (col.gameObject.CompareTag("DejarItem1"))
        {
            if (agarreItem1)
            {
                rangoitem1 = true; // Indica que el jugador está en rango de interacción
                manager.ActivarMenuItems();
                Debug.Log("DejarItem 1");

                // }
            }
        }

        if (col.gameObject.CompareTag("DejarItem2"))
        {
            if (agarreItem2)
            {
                rangoitem2 = true; // Indica que el jugador está en rango de interacción
                manager.ActivarMenuItems();
                Debug.Log("DejarItem 2");
            }
        }

        if (col.gameObject.CompareTag("DejarItem3"))
        {
            if (agarreItem3)
            {
                rangoitem3 = true; // Indica que el jugador está en rango de interacción
                manager.ActivarMenuItems();
                Debug.Log("DejarItem 3");
            }
        }
    }


    void OnTriggerExit(Collider col)
    {
        if (col.transform.CompareTag("DejarItem1"))
        {
            manager.DesactivarMenuItems(); // Desactiva la interfaz de usuario de los ítems si sale del rango
            rangoitem1 = false; // Indica que el jugador está en rango de interacción
        }

        if (col.transform.CompareTag("DejarItem2"))
        {
            manager.DesactivarMenuItems(); // Desactiva la interfaz de usuario de los ítems si sale del rango
            rangoitem2 = false; // Indica que el jugador está en rango de interacción
        }

        if (col.transform.CompareTag("DejarItem3"))
        {
            manager.DesactivarMenuItems(); // Desactiva la interfaz de usuario de los ítems si sale del rango
            rangoitem3 = false; // Indica que el jugador está en rango de interacción
        }
    }
}