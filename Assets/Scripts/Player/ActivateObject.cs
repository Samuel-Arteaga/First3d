using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.TryGetComponent<IActivable>(out IActivable activable))
        {
            activable.ActivateObject();
        }
    }
}

