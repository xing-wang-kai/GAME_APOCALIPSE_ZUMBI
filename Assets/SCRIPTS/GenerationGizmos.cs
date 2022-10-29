using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationGizmos : MonoBehaviour
{
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 4);
    }
}
