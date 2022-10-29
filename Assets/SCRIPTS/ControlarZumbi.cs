using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlarZumbi : MonoBehaviour
{

    public GameObject jogador;
    public float velocidade = 5;
    private Vector3 direcao;
    private Vector3 randoPosition;
    private Animator animatorZumbie;
    private Rigidbody rigidBodyZumbie;
    private int dano;
    private float counterTimeZumbieWalker;
    private float initialCounterTimeZumbieWalker = 4f;
    
    private float maxGenerationMedicKit = 0.5f;

    public GerarInimigos myGeneration;
    public GameObject medicKit;
    public GameObject BloodParticule;
    

    private void Start()
    {
        jogador = GameObject.FindWithTag("jogador");

        int choice = Random.Range(1, 28);
        transform.GetChild(choice).gameObject.SetActive(true);

        rigidBodyZumbie = GetComponent<Rigidbody>();
        animatorZumbie = GetComponent<Animator>();

    }

    private void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, jogador.transform.position);

        if (distancia > 15)
        {
            this.RandomizeWalk();
        }
        else if (distancia > 3)
        {
            this.FallowPlayer();
        }
        else
        {
            animatorZumbie.SetBool("Atacando", true);
        }
        

    }

    public void RandomizeWalk()
    {
        counterTimeZumbieWalker -= Time.deltaTime;
        if(counterTimeZumbieWalker <= 0)
        {
            randoPosition = RandomizePosition();
            counterTimeZumbieWalker = initialCounterTimeZumbieWalker;
        }
        bool isPointZeroWalk = Vector3.Distance(transform.position, randoPosition) <= 0.05;
        if(isPointZeroWalk == false)
        {
            direcao = randoPosition - transform.position;
            rigidBodyZumbie.MovePosition(
                    rigidBodyZumbie.position + (direcao.normalized * velocidade * Time.deltaTime)
               );
            Quaternion novaRotacao = Quaternion.LookRotation(direcao);

            rigidBodyZumbie.MoveRotation(novaRotacao);
            animatorZumbie.SetFloat("Movendo", direcao.magnitude);
        }

    }
    public Vector3 RandomizePosition()
    {
        Vector3 position = Random.insideUnitSphere * 10;
        position += transform.position;
        position.y = transform.position.y;
        return position; 
    }

    public void FallowPlayer()
    {
        direcao = jogador.transform.position - transform.position;

        rigidBodyZumbie.MovePosition(
            rigidBodyZumbie.position + (direcao.normalized * velocidade * Time.deltaTime)
       );
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);

        rigidBodyZumbie.MoveRotation(novaRotacao);
        animatorZumbie.SetFloat("Movendo", direcao.magnitude);

        animatorZumbie.SetBool("Atacando", false);
    }
    void AtacarJogador()
    {
        dano = Random.Range(5, 20);
        jogador.GetComponent<ControlarJogador>().TomarDano(dano);
      
    }
    public void setDead()
    {
        animatorZumbie.SetTrigger("Morrer");
        Destroy(gameObject, 3);
        this.enabled = false;
        
        this.dropMedicKit();
        this.myGeneration.reduceZombieDeadCounter(); 
    }
    public void setBloodParticle(Vector3 position, Quaternion rotation)
    {
        Instantiate(this.BloodParticule, position, rotation);
    }

    public void dropMedicKit()
    {
        if(Random.value <= this.maxGenerationMedicKit)
        {
            try
            {
                Instantiate(medicKit, transform.position, transform.rotation);
            }
            catch
            {

            }
            
        }
    }
}
