using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : Shooter
{
    // Controla a cena
    SceneManager SceneManager;

    [SerializeField]
    // Nome do level que será carregado
    private string levelName;

    // Morte do boss 
    [SerializeField]
    AudioClip bossDeath;

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (gameManager.bossVida > 0)
        {
            
            Shooting();
        } else if (gameManager.bossVida <= 0)
        {
            AudioManager.Instance.PlaySFX(bossDeath, 0.6f);
            gameManager.pontuacao++;
            SceneManager.LoadScene(levelName);
            Destroy(gameObject);
            gameManager.bossVida = gameManager.bossVidaMax;
            gameManager.vidaPlayer = gameManager.vidaMaxima;
            gameManager.pontuacao = 0;
           
        }
    }



}
