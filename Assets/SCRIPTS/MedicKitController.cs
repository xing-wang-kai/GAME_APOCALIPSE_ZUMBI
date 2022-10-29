using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicKitController : MonoBehaviour
{
    private int cure = 15;
    private int destructionTime = 5;

    public void Start()
    {
        Destroy(gameObject, this.destructionTime);
    }
    private void OnTriggerEnter(Collider colliderObject)
    {
        if(colliderObject.tag == "jogador")
        {
            colliderObject.gameObject.GetComponent<ControlarJogador>().setCure(this.cure);
            Destroy(gameObject);
        }
        
    }
}
