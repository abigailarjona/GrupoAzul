using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
   public float power = 100f;

   public float lifeTime = 5f;

   float deltatime = 0f;

   Rigidbody Rigidbody;
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();

        Rigidbody.velocity = this.transform.forward * power;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       deltatime += Time.deltaTime;

       if(deltatime >= lifeTime)
       {
        Destroy(this.gameObject);
       } 
    }
}
