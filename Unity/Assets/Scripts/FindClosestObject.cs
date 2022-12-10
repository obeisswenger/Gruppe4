using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestObject : MonoBehaviour
{

    public GameObject empty;
    public GameObject closestGameObject;

    public GameObject nearestObjectWithTag(string tag)
    {
        // Initialize the distance and closest game object to maximum values
        float closestDistance = float.MaxValue;
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        if (gameObjects.Length != 0)
        {
            closestGameObject = gameObjects[0];
        }
        else
        {
            closestGameObject = empty;
        }

        // Loop through all the game objects
        foreach (GameObject gameObject in gameObjects)
        {
            // Get the current position of the player
            Vector3 playerPosition = transform.position;

            // Get the position of the game object
            Vector3 gameObjectPosition = gameObject.transform.position;

            // Calculate the distance between the player and the game object
            float distance = Vector3.Distance(playerPosition, gameObjectPosition);

            // If the distance is less than the closest distance so far, update the closest distance and game object
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestGameObject = gameObject;
            }
        }

        return closestGameObject;
    }
}
