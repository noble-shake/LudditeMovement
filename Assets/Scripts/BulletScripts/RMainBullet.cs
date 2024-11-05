using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RMainBullet : Bullet
{
    // Á÷¼± ºÎÀûÅº

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        speed = 5f;
    }

    private void Update()
    {
        Fire();
    }

    public override void Init(int _factor = 0)
    {
        
    }

    public override void Forward(Vector3 _direction)
    {
        
    }

    public override void Fire()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ObjectType.Enemy.ToString()))
        {
            // hit
        }
        else if (collision.CompareTag(ObjectType.Wall.ToString()))
        { 
            // destroy
        }
    }

}
