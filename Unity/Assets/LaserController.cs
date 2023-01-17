using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject firePoint;

    // Update is called once per frame
    void Update()
    {

    }

    public void EnableLaser()
    {
        lineRenderer.enabled = true;
    }

    public void UpdateLaser(GameObject target)
    {
        lineRenderer.SetPosition(0, firePoint.transform.position);

        lineRenderer.SetPosition(1, target.transform.position);
    }

    public void DisableLaser()
    {
        lineRenderer.enabled = false;
    }
}