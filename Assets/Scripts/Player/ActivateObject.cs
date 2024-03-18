using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    public string objetoTag; 

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(objetoTag))
        {
            IActivable activable = other.GetComponent<IActivable>();
            if (activable != null)
            {
                activable.ActivateObject();
            }
        }
    }
}
