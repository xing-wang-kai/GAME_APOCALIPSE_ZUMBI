using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerarInimigos : MonoBehaviour
{

    public GameObject inimigo;
    public GameObject player;
    private float tempoGerarZumbi = 0;
    public float TempoMaxGerarZumbi = 2;
    public float rangeGeneration = 3;
    public float distanceToPlayerForGeneration = 20;
    
    public LayerMask ZumbieLayer;
    public LayerMask ConstructionLayer;

    public int qtdZombieGenerated;
    public int maxQtdZombie = 3;
    public float TimeToGenerationDificult = 30;

    public void Start()
    {
        this.player = GameObject.FindWithTag("jogador");
        for(int i = 0; i < maxQtdZombie; i++)
        {
            StartCoroutine(this.GenerationZumbie());
        }
    }

    void Update()
    {
        float DistanceToPlayer = Vector3.Distance(transform.position, this.player.transform.position);
        if(DistanceToPlayer > distanceToPlayerForGeneration && maxQtdZombie > qtdZombieGenerated)
        {
            tempoGerarZumbi += Time.deltaTime;
            if (tempoGerarZumbi > TempoMaxGerarZumbi)
            {

                StartCoroutine(this.GenerationZumbie());
                tempoGerarZumbi = 0;
            }
        }

        if (Time.timeSinceLevelLoad >= this.TimeToGenerationDificult)
        {
            maxQtdZombie++;
            this.TimeToGenerationDificult = Time.timeSinceLevelLoad + this.TimeToGenerationDificult;
        }
    }
    IEnumerator GenerationZumbie()
    {
        Vector3 aleatoriePosition = GenerationAleatoriePosition();
        Collider[] Zumbiecollideres = Physics.OverlapSphere(aleatoriePosition, 1, this.ZumbieLayer);
        Collider[] ConstructionsCollideres = Physics.OverlapSphere(aleatoriePosition, 1, this.ConstructionLayer);
        while(Zumbiecollideres.Length > 0 && ConstructionsCollideres.Length >0)
        {
            aleatoriePosition = GenerationAleatoriePosition();
            Zumbiecollideres = Physics.OverlapSphere(aleatoriePosition, 1, this.ZumbieLayer);
            ConstructionsCollideres = Physics.OverlapSphere(aleatoriePosition, 1, this.ConstructionLayer);
            yield return null;
        }
        ControlarZumbi zumbieController = Instantiate(inimigo, aleatoriePosition, transform.rotation).GetComponent<ControlarZumbi>();
        this.qtdZombieGenerated++;
        zumbieController.myGeneration = this;
    }
    public Vector3 GenerationAleatoriePosition()
    {
        Vector3 position = Random.insideUnitSphere * 4;
        position += transform.position;
        position.y = transform.position.y;
        return position;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, this.rangeGeneration);
    }

    public void reduceZombieDeadCounter()
    {
        this.qtdZombieGenerated--;
    }
}
