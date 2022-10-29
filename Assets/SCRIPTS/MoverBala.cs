using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverBala : MonoBehaviour
{
    public float velocidade = 25;
    public AudioClip EnimyZumibieDeadSound;
    public ControllerCanvas canvasInterface;
    public int Damager;

    public void Start()
    {
        this.canvasInterface = GameObject.FindWithTag("Canvas").GetComponent<ControllerCanvas>();
    }

    void Update()
    {
        GetComponent<Rigidbody>().MovePosition(
               GetComponent<Rigidbody>().position + (transform.forward * velocidade * Time.deltaTime)
        );
    }

    void OnTriggerEnter(Collider objetoDeColisao)
    {
        Quaternion bulletRotation = Quaternion.LookRotation(-transform.forward);
        if (objetoDeColisao.tag == "Inimigo")
        {
            ControlarZumbi Enimy = objetoDeColisao.gameObject.GetComponent<ControlarZumbi>();
            Enimy.setDead();
            Enimy.setBloodParticle(transform.position, bulletRotation);
            AudioController.currentAdioSource.PlayOneShot(EnimyZumibieDeadSound);
            this.canvasInterface.countZomibieDead();
        }
        if(objetoDeColisao.tag == "EnimyBoss")
        {
            this.Damager = Random.Range(10, 15);
            BossController boss = objetoDeColisao.gameObject.GetComponent<BossController>();
            boss.setDead(this.Damager);
            boss.setBloodParticle(transform.position, bulletRotation);
            AudioController.currentAdioSource.PlayOneShot(EnimyZumibieDeadSound);
            this.canvasInterface.countZomibieDead();
        }

        Destroy(gameObject);
    }
    
}
