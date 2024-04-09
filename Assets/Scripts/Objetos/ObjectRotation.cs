using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectRotation : MonoBehaviour
{
    private float velocityRotation= 70.0f;

    void Update()
    {
        transform.Rotate(Vector3.up * velocityRotation* Time.deltaTime);        
    }
}
