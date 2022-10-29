using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGeneratorController : MonoBehaviour
{
    public GameObject Boss;
    public GameObject player;
    public float TimeToGenerationBoss = 20;
    public float InitalTimeToGenerationBoss;
    public GameObject[] GenerationPoints;
    private GameObject generationPoint;
    private ControllerCanvas canvas;

    void Start()
    {
        this.InitalTimeToGenerationBoss = this.TimeToGenerationBoss;
        this.canvas = GameObject.FindObjectOfType<ControllerCanvas>();
        this.generationPoint = this.GenerationPoints[0];
    }

    void Update()
    {
        this.TimeToGenerationBoss -= Time.deltaTime;
        if(this.TimeToGenerationBoss <= 0)
        {
            this.GeneratePoint();
            StartCoroutine(setBossMessage());
            Instantiate(this.Boss, this.generationPoint.transform.position, transform.rotation);
            this.TimeToGenerationBoss = this.InitalTimeToGenerationBoss;
            this.InitalTimeToGenerationBoss *= 2;
        }
    }

    public void GeneratePoint()
    {
        
        foreach(GameObject point in GenerationPoints)
        {
            float distance = Vector3.Distance(point.transform.position, this.player.transform.position);
            if(distance >= Vector3.Distance(this.generationPoint.transform.position, this.player.transform.position))
            {
                this.generationPoint = point;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 6);
    }

    private IEnumerator setBossMessage()
    {
        this.canvas.SetBossWarningMessage(true);
        yield return new WaitForSeconds(5);
        this.canvas.SetBossWarningMessage(false);
    }
}
