using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Instância do game manager
    public static GameManager gameManager;

    // Controla se o cooldown foi ativado
    public bool cooldown;

    // Controla se o jogador ja pegou a habilidade especial
    public bool choqueTrovaoAtivador;

    // Tempo de reset do cooldown
    public float cooldownReset;

    // Timer do cooldown
    public float cooldownTimer;

    // Bool que controla o escudo
    public bool escudoAtivador;

    // Bool que controla o choque Do Trovão
    public bool choqueAtivador;

    // Vida do jogador
    public int vidaPlayer;

    // Vida máxima do jogador
    public int vidaMaxima;

    // Pontuação do jogador;
    public int pontuacao;

    // Vida do boss
    public int bossVida;
    
    // Vida do boss
    public int bossVidaMax;

    // Bool que deixa o jogador indestrutivel enquanto estiver usando o escudo
    public bool invincible;

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
        vidaPlayer = vidaMaxima;
        gameManager.bossVida = gameManager.bossVidaMax;
        DontDestroyOnLoad(gameObject);
        
    }

    public void LevouDano(int dano)
    {
        vidaPlayer -= dano;
    }

    public void LevouDanoCannon(int dano)
    {
        bossVida -= dano;
            
    }

}






