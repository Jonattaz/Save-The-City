using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Referência gameManager
    GameManager gameManager;

    // Variável que serve de referência a barra de vida
    public Slider powerBar;

    // Variável que serve de referência ao texto da vida
    public Text healthText;

    // Variável que referência o texto das moedas
    public Text pointsText;

    // Bool de controle da habilidade especial
    bool aux;

    [SerializeField]
    // Musica que toca no jogo
    private AudioClip GameSoundtrack;


    private void Awake()
    {
        gameManager = GameManager.gameManager;
        UpdateLife();
        UpdatePoints();


    }

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic(GameSoundtrack);
        gameManager.cooldownTimer = gameManager.cooldownReset;
        aux = false;
    }

    // Update is called once per frame
    void Update()
    {
        powerBar.value = gameManager.cooldownTimer / gameManager.cooldownReset;
        EspecialBar();
        UpdateLife();
        UpdatePoints();
    }

    private void EspecialBar()
    {
        if (gameManager.cooldownTimer > 0 && gameManager.cooldown)
        {
            gameManager.cooldownTimer -= Time.deltaTime;
        }

        if (gameManager.cooldownTimer < 0)
        {
            aux = true;
        }

        if (aux && gameManager.cooldownTimer < gameManager.cooldownReset)
        {
            gameManager.invincible = false;
            gameManager.choqueAtivador = false;
            gameManager.escudoAtivador = false;
            gameManager.cooldownTimer += Time.deltaTime;
            gameManager.cooldown = false;

        }

        if (aux && gameManager.cooldownTimer >= gameManager.cooldownReset)
        {
            aux = false;
        }


    }


    public void UpdateLife()
    {
        healthText.text = "Vida: " + gameManager.vidaPlayer.ToString();
    }

   public void UpdatePoints()
    {
        pointsText.text = gameManager.pontuacao.ToString();
    }


}











