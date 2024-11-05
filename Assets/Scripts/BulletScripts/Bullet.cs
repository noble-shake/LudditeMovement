using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected float damage;
    protected float speed;
    protected int projectiles;
    protected int projectileFactor;
    protected Collider2D coll;
    protected SpriteRenderer spr;

    public virtual void Init() { }
    public virtual void Init(int _factor) { }
    public virtual void Forward(Vector3 _direction) { }
    public virtual void Fire() { }

}
