using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllerCanvas : MonoBehaviour
{
    [Header("Attributes : ")]
    private ControlarJogador controlerPlayer;
    public Slider SliderVidaJogador;
    public GameObject GameOverPainel;
    public Text GameOverText;
    public Text MaxPointText;
    public Text qtdZombieDead;
    public float ZombieDead;
    public float maxPoint;
    public GameObject BossWarningMessage;
    
    void Start()
    {
        this.controlerPlayer = GameObject.FindWithTag("jogador")
            .GetComponent<ControlarJogador>();

        this.SliderVidaJogador.maxValue = this.controlerPlayer.vida;
        this.AtualizarSliderVidaJogador();
        Time.timeScale = 1;
        this.maxPoint = PlayerPrefs.GetFloat("MaxPoint");
    }

    public void Update()
    {
        this.AtualizarSliderVidaJogador();
    }

    public void AtualizarSliderVidaJogador()
    {
        this.SliderVidaJogador.value = this.controlerPlayer.vida;
        if(this.controlerPlayer.vida <= 0)
        {
            this.SliderVidaJogador.value = 0;
        }
    }

    public void GameOver()
    {
        this.GameOverPainel.SetActive(true);
        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int seconds = (int)(Time.timeSinceLevelLoad % 60);
        this.GameOverText.text = string.Format("Você Sobrevideu por {0} minutos e {1} segundos", minutos, seconds);
        this.CompareMaxPoint(minutos, seconds);
        Time.timeScale = 0;
    }

    public void CompareMaxPoint(int minute, int seconds)
    {
        if(Time.timeSinceLevelLoad > this.maxPoint)
        {
            this.MaxPointText.text = string.Format("Seu Tempo Record foi {0} minutos e {1} segundos.", minute, seconds);
            PlayerPrefs.SetFloat("MaxPoint", Time.timeSinceLevelLoad);
        }
        if(this.MaxPointText.text == "")
        {
            int minutos = (int)(this.maxPoint / 60);
            int sec = (int)(this.maxPoint % 60);

            this.MaxPointText.text = string.Format("Seu Tempo Record foi {0} minutos e {1} segundos.", minutos, sec);
        }
    }
    public void ResetarGame()
    {
       SceneManager.LoadScene("game");
    }

    public void countZomibieDead()
    {
        this.ZombieDead++;
        qtdZombieDead.text = string.Format(" X {0}", this.ZombieDead);
    }

    public void SetBossWarningMessage(bool acction)
    {
        this.BossWarningMessage.SetActive(acction);
    }
   
}
