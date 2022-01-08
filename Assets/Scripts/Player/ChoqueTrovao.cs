using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoqueTrovao : MonoBehaviour
{
    // Dano que o raio irá causar
    [SerializeField]
    int damage;

    // Referência ao game manager
    GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.gameManager;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
            gameManager.bossVida -= damage;
        else if (collision.gameObject.CompareTag("Inimigo"))
        {
            gameManager.pontuacao++;
            Destroy(collision.gameObject);
        }
        
    }


}
