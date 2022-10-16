using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlarJogador : MonoBehaviour
    
{
    public float velocidade = 10;
    Vector3 direcao;

    void Start()
    {
        
    }
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        if( direcao != Vector3.zero )
        {
            GetComponent<Animator>().SetBool("Movendo", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Movendo", false);
        }
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition( 
            GetComponent<Rigidbody>().position + (direcao * velocidade * Time.deltaTime)
            );
    }

}
