using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + "ASDASD");
        if (other.TryGetComponent<IActivable>(out IActivable activable))
        {
            Debug.Log(other.gameObject.name);
            activable.ActivateObject();

        }
    }
}

