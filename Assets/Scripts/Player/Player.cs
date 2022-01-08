using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Player : MonoBehaviour
{

    // Rigidbody do jogador
    Rigidbody2D playerRb;

    [SerializeField]
    // Representa a velocidade do jogador 
    float speed;

    [SerializeField]
    // Representa a posição do tiro
    Transform shotSpawner;

    // Representa o laser do personagem
    GameObject laser;

    [SerializeField]
    // Representa o choque do trovão
    GameObject choqueTrovao;

    [SerializeField]
    // Representa o escudo
    GameObject escudo;

    // Representa o game manager
    GameManager gameManager;

    // Controla a desativação do choque
    bool choqueDesativar;

    // Controla a desativação do escudo
    bool escudoDesativar;

    // Som do tiro do jogador
    [SerializeField]
    AudioClip laserSound;

    // Som do escudo
    [SerializeField]
    AudioClip escudoSound;


    // Som do choque do trovão
    [SerializeField]
    AudioClip trovaoSound;

    // Som de coletavel
    [SerializeField]
    AudioClip coletavelSound;

    // Controla a cena
    SceneManager SceneManager;

    [SerializeField]
    // Nome do level que será carregado
    private string levelName;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        gameManager = GameManager.gameManager;
        gameManager.escudoAtivador = false;
        gameManager.choqueAtivador = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        SceneControl();
        Attack();
        EscudoEletrico();
        ChoqueDoTrovao();
    }
    private void SceneControl()
    {
        if (gameManager.vidaPlayer <= 0)
        {
            SceneLoader();
            gameManager.vidaPlayer = gameManager.vidaMaxima;
        }

    }

    private void SceneLoader()
    {
        SceneManager.LoadScene(levelName);
    }


    private void FixedUpdate()
    {
        Movimentação();
    }

    // Método responsável pela movimentação do jogador
    private void Movimentação()
    {
        // Movimenta o jogador no eixo horizontal
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        playerRb.velocity = new Vector2(horizontalInput * speed, playerRb.velocity.y);

        // Movimenta o jogador no eixo vertical
        float verticalInput = Input.GetAxisRaw("Vertical");
        playerRb.velocity = new Vector2(playerRb.velocity.x, verticalInput * speed);
    }

    // Método responsável pelo ataque basico do personagem
    private void TiroLaser()
    {
        laser = ObjectPool.instance.GetPooledObject();
        if (laser != null)
        {
            laser.transform.position = shotSpawner.transform.position;
            laser.transform.rotation = shotSpawner.transform.rotation;
            laser.SetActive(true);
        }
    }
    
    // Método responsável pela habilidade do escudo
    private void EscudoEletrico() 
    {
      escudo.SetActive(gameManager.escudoAtivador);
    }
    
    // Método responsável pela habilidade especial do personagem
    private void ChoqueDoTrovao()
    {
        choqueTrovao.SetActive(gameManager.choqueAtivador);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trovão"))
        {
            AudioManager.Instance.PlaySFX(coletavelSound, 0.6f);
            gameManager.choqueTrovaoAtivador = true;
            Destroy(collision.gameObject);
        }
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            AudioManager.Instance.PlaySFX(laserSound, 0.6f);
            TiroLaser();
        }

        if (Input.GetKeyDown(KeyCode.C) && !gameManager.cooldown)
        {
            AudioManager.Instance.PlaySFX(escudoSound,0.6f);
            gameManager.invincible = true;
            gameManager.escudoAtivador = true;
            gameManager.cooldown = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && gameManager.escudoAtivador)
        {
            gameManager.invincible = false;
            gameManager.escudoAtivador = false;
            gameManager.cooldown = false;
        }

        if (Input.GetKeyDown(KeyCode.Z) && gameManager.choqueTrovaoAtivador && !gameManager.cooldown)
        {
            AudioManager.Instance.PlaySFX(trovaoSound, 0.6f);
            gameManager.invincible = true;
            gameManager.choqueAtivador = true;
            gameManager.cooldown = true;
        }
        else if (Input.GetKeyDown(KeyCode.Z) && gameManager.choqueAtivador)
        {
            gameManager.invincible = false;
            gameManager.choqueAtivador = false;
            gameManager.cooldown = false;
        }
    }

}
