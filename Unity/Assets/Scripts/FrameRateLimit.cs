using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateLimit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 144;
    }
}
