using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableScripts : MonoBehaviour
{

    public void EnableScripts(GameObject targetGameObject, bool enable)
    {
        // Get all the MonoBehaviour components on the target game object
        MonoBehaviour[] scripts = targetGameObject.GetComponents<MonoBehaviour>();

        // Iterate through the scripts and disable them
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = enable;
        }
    }
}
