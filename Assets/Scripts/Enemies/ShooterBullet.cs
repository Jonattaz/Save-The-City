using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBullet : Bullet
{
    // Referência ao jogador
    protected Player target;

    // direção da qual a bala irá se mover
    protected Vector2 moveDirection;

    // Referência ao rigidBody do componente
    protected Rigidbody2D rb;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<Player>();
        moveDirection = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);

    }

    // Update is called once per frame
    protected override void Update()
    {

    }  

}
