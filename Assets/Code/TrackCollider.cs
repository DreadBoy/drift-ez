using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCollider : MonoBehaviour
{
    private void OnTriggerStay(Collider collider)
    {
        Debug.Log("Stay " + collider.name);
    }
}
