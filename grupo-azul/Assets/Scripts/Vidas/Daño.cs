using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Daño : MonoBehaviour
{
    

    public GameObject player;
    public int cantidad;
    public float damageTime;
    float currentDamgeTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            currentDamgeTime -= Time.deltaTime;
            if (currentDamgeTime < damageTime)
            {
                player.GetComponent<BarraVIda>().vidaActual -= cantidad;
                currentDamgeTime = 0.0f;
                

            }
            
        }
    }
}
