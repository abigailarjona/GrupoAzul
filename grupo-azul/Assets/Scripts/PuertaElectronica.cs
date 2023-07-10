using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaElectronica : MonoBehaviour
{
    public GameObject PuertaIzquierda;
    public GameObject PuertaDerecha;
    public bool itempuerta;
    public GameObject Boton;
    


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
           
           if (itempuerta)                //aca mati este es el if que abre la puerta no se como editarlo

        {
            PuertaIzquierda.GetComponent<Animator>().enabled = true;
            PuertaDerecha.GetComponent<Animator>().enabled = true;
        }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Boton.SetActive(false);
        }
    }


    public void AbrirPuerta()
    {
        PuertaIzquierda.GetComponent<Animator>().enabled = true;
        PuertaDerecha.GetComponent<Animator>().enabled = true;
    }
}
