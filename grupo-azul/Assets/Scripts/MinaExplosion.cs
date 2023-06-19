using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinaExplosion : MonoBehaviour
{
    public GameObject mina;
    private ParticleSystem particleObject;
    // Start is called before the first frame update
    void Start()
    {
        //Desactivar particulas al comienzo del juego
        particleObject = GetComponent<ParticleSystem>();
        particleObject.Stop();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        //Si el juegador toca la mina se activan las particulas y luego se destruye la mina en determinado tiempo
        if(other.tag == "Player")
        {
            particleObject.Play();
            Destroy(mina, 0.9f);
           
            
        }
    }
}
