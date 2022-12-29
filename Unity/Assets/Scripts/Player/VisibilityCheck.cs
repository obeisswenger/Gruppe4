using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityCheck : MonoBehaviour
{
    public Camera cam;

    public bool IsVisible(GameObject og)
    {
        // Get the renderer of the object
        Renderer renderer = og.GetComponent<Renderer>();
        if (renderer == null) return false;
        // Check if the renderer is visible from the camera
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
