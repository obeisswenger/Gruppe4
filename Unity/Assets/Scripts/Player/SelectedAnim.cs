using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedAnim : MonoBehaviour
{
    public GameObject selected;
    public Camera cam; // The main camera

    void Update()
    {
        
        // Get the screen position of the GameObject
        Vector2 screenPos = cam.GetComponent<Camera>().WorldToScreenPoint(selected.transform.position);

        // Set the position of the UI image to the screen position of the GameObject
        transform.position = screenPos;
        if(!selected.activeSelf || selected.tag == "Empty")
        {
            gameObject.SetActive(false);
        }
    }
}
