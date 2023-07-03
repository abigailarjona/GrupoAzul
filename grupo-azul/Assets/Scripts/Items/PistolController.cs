using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolController : MonoBehaviour
{
    public Transform shootPoint;

    public Transform bulletPrefab;
    //sight
    Transform sight;

    private void tart()
    {
        sight = GetComponentInChildren<Canvas>().transform;
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
    }

    public void DrawSight(Transform camera)
    {
        RaycastHit hit;

        if(Physics.Raycast(camera.position, camera.forward, out hit))
        {
            shootPoint.LookAt(hit.point);
        }
        else
        {
            Vector3 end =camera.position + camera.forward;
            shootPoint.LookAt(end);
        }
    }

}
