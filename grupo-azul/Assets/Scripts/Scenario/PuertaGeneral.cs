using UnityEngine;

namespace Scenario
{
    public class PuertaGeneral : MonoBehaviour
    {
        public Animator puerta;
        private bool enZona;
        private bool activa;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && enZona == true)
            {
                activa = !activa;

                if (activa == true)
                {
                    puerta.SetBool("puertaActiv", true);
                }

                if (activa == false)
                {
                    puerta.SetBool("puertaActiv", false);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                enZona = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                enZona = false;
            }
        }
    }
}