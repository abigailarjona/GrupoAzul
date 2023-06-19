using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVIda : MonoBehaviour
{

    public Image barraDeVida;

    public float vidaActual;    

    public static BarraVIda instance;

    private void Awake()
    {
        //instanciar objeto, si la instancia no existe entonces lo instancia
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //valor de vida, si esta baja o sube que se refleje en la barra de vida, para que no sea negativo se pone 0 y el 100 es el valor máximo
        vidaActual = Mathf.Clamp(vidaActual, 0, 100);

        barraDeVida.fillAmount = vidaActual / 100;
    }
}
