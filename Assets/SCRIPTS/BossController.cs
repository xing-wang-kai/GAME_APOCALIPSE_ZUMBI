using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent boss;
    public float Speed = 4;
    public float LifePoint = 100;
    private int damager;
    private Animator anim;
    private Rigidbody rig;
    public Slider SliderOfLife;
    public Image fill;
    public Color MaxLifeColor, MinLifeColor;

    public GameObject BloodParticule;

    void Start()
    {
        this.player = GameObject.FindWithTag("jogador").transform;
        this.anim = GetComponent<Animator>();
        this.boss = GetComponent<NavMeshAgent>();
        this.rig = GetComponent<Rigidbody>();
        this.boss.speed = this.Speed;
        this.SliderOfLife.maxValue = this.LifePoint;
    }

    // Update is called once per frame
    void Update()
    {
        this.boss.SetDestination(this.player.position);
        this.anim.SetFloat("Movendo", this.boss.velocity.magnitude);
        if(boss.hasPath)
        {
            bool nextToPlayer = this.boss.remainingDistance <= this.boss.stoppingDistance;
            if(nextToPlayer)
            {
                this.anim.SetBool("Atacando", true);
                Vector3 direction = this.player.position - this.transform.position;
                Quaternion newRotation = Quaternion.LookRotation(direction);
                this.rig.MoveRotation(newRotation);

            }else
            {
                this.anim.SetBool("Atacando", false);
            }
        }
        this.UpdateSliderLife();
        
    }
    public void AtacarJogador()
    {
        this.damager = Random.Range(20, 30);
        this.player.GetComponent<ControlarJogador>().TomarDano(this.damager);
    }

    public void setDead(int damager)
    {
        this.LifePoint -= damager;
        if(this.LifePoint <= 0)
        {
            this.Death();
        }
    }

    public void Death()
    {
        anim.SetTrigger("Morrer");

        this.enabled = false;
        this.boss.enabled = false;

        Destroy(gameObject, 3);
    }

    public void UpdateSliderLife()
    {
        this.SliderOfLife.value = this.LifePoint;
        float porcent = (float)this.SliderOfLife.value / (float)this.SliderOfLife.maxValue;
        Color sliderColor = Color.Lerp(this.MinLifeColor, this.MaxLifeColor, porcent);
        this.fill.color = sliderColor;
        if (this.LifePoint <= 0)
        {
            this.SliderOfLife.value = 0;
        }
        
    }

    public void setBloodParticle(Vector3 position, Quaternion rotation)
    {
        Instantiate(this.BloodParticule, position, rotation);
    }
}
