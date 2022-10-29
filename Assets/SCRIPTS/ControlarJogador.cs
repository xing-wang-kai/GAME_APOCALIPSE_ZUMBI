using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlarJogador : MonoBehaviour
{
    public float velocidade = 10;
    public LayerMask camadaChao;
    public int vida = 100;
    public int maxLife = 100;
    public AudioClip damageSound;

    private Vector3 direcao;
    private Rigidbody rigidBodyJogador;
    private Animator animatorJogador;
    private ControllerCanvas controllerCanvas;
    public GameObject BloodParticule;


    void Start()
    {
        rigidBodyJogador = GetComponent<Rigidbody>();
        animatorJogador = GetComponent<Animator>();
        controllerCanvas = GameObject.FindWithTag("Canvas")
            .GetComponent<ControllerCanvas>();
    }
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);
        animatorJogador.SetFloat("Movendo", direcao.magnitude);
    }

    private void FixedUpdate()
    {
        rigidBodyJogador.MovePosition(
            rigidBodyJogador.position + (direcao * velocidade * Time.deltaTime)
        );

        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        RaycastHit impacto;

        if(Physics.Raycast(raio, out impacto, 100, camadaChao))
        {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;

            posicaoMiraJogador.y = 0;

            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);
            rigidBodyJogador.MoveRotation(novaRotacao);
        }

       
    }

    public void TomarDano(int dano)
    {
        vida -= dano;
        this.setBloodParticle(transform.position, transform.rotation);
        AudioController.currentAdioSource.PlayOneShot(damageSound);
        if (this.vida <= 0)
        {
            this.controllerCanvas.GameOver();
        }
       
    }

    public void setCure(int cure)
    {
        this.vida += cure;
        if(this.vida >= maxLife)
        {
            this.vida = maxLife; 
        }
    }

    public void setBloodParticle(Vector3 position, Quaternion rotation)
    {
        Instantiate(this.BloodParticule, position, rotation);
    }


}
