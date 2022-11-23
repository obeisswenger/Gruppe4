using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        //reload button
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene( SceneManager.GetActiveScene().name );
        }
    }
}
