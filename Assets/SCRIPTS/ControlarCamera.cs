using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlarCamera : MonoBehaviour
{
    public GameObject jogador;
    private Vector3 compensarDistancia;
    
    void Start()
    {
        compensarDistancia = transform.position - jogador.transform.position;
    }

     void Update()
    {
        transform.position = jogador.transform.position + compensarDistancia;

    }


}
