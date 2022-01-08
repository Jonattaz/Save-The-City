using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    // Controla a velocidade da bala
    protected float speed;

    [SerializeField]
    // Controla quem irá levar o dano
    protected string damageTag;

    [SerializeField]
    protected string destruction;

    [SerializeField]
    protected int damage;


    // Representa o game manager
    GameManager gameManager;

    [SerializeField]
    // Som de destruição
    protected AudioClip destructionSound;

    [SerializeField]
    // Ativa o som de destruição
    protected bool destrActive;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        gameManager = GameManager.gameManager;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

   protected private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Boss") && damageTag == "Inimigo")
        {
            gameManager.bossVida -= damage;
            gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Inimigo") && damageTag == "Inimigo")
        {
            if (destrActive)
            {
                AudioManager.Instance.PlaySFX(destructionSound, 0.6f);
            }
            gameManager.pontuacao++;
            Destroy(collision.gameObject);            
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag(destruction))
        {
            if (damageTag != "Inimigo")
                Destroy(gameObject, 0.5f);
            else
                gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Player") && damageTag == "Player")
        {
            if (!gameManager.invincible)
            {
                gameManager.LevouDano(damage);
            }
            if (destrActive)
            {
                AudioManager.Instance.PlaySFX(destructionSound, 0.6f);
            }
            Destroy(gameObject);
        }

        if (damageTag == "Player")
            Destroy(gameObject, 1.5f);
        else if(damageTag == "Inimigo")
            Invoke("Deactivator", 1.5f);
    }
    void Deactivator()
    {
        if (gameObject)
            gameObject.SetActive(false);
       
    }
}
