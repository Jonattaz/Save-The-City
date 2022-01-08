using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // Velocidade de movimentação do inimigo
    [SerializeField]
     float speed;

    [SerializeField]
    protected float fireRate;
    [SerializeField]
    protected float nextFire;

    [SerializeField]
    // Pontos dos quais o inimigo irá passar
    Transform[] waypoints;

    [SerializeField]
    // Referência o shotspawner
    protected Transform shotSpawner;

    [SerializeField]
    // Escolhe quando dano este objeto irá dar no jogador
    protected int damage;

    // Index dos pontos
     int waypointIndex;

    [SerializeField]
    // Referêcia ao game object do tiro
    protected GameObject bullet;

    // Representa o game manager
    protected GameManager gameManager;

    [SerializeField]
    //Bool auxiliar
    protected bool waypointPermission;

    // Tiro Sound
    [SerializeField]
    protected AudioClip shotSound;
   
    
    // Dano Sound
    [SerializeField]
    protected AudioClip danoSound;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        gameManager = GameManager.gameManager;
        nextFire = Time.time;
        if (waypointPermission) 
        {
            transform.position = waypoints[waypointIndex].transform.position;
        }
         
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Movement();
        Shooting();
    }


    // Responsável pela movimentação do inimigo
    void Movement()
    {
        transform.position = Vector2.MoveTowards(transform.position,
            waypoints[waypointIndex].transform.position, speed * Time.deltaTime);

        if (transform.position == waypoints[waypointIndex].transform.position)
            waypointIndex += 1;
        if (waypointIndex == waypoints.Length)
            waypointIndex = 0;
    }

    // Controla o tiro do inimigo
    protected void Shooting()
    {
        if (Time.time > nextFire)
        {
            AudioManager.Instance.PlaySFX(shotSound, 0.6f);
            Instantiate(bullet, shotSpawner.position, bullet.GetComponent<Transform>().rotation);
            nextFire = Time.time + fireRate;

        }

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!gameManager.invincible)  
                gameManager.LevouDano(damage);
                AudioManager.Instance.PlaySFX(danoSound, 0.6f);
        }
    }

}
















