using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct PlayerMoveInput : IComponentData
{
    public Vector2 _moveVector;
}

public struct PlayerMoveSpeed : IComponentData
{
    public float _moveSpeed;
}

public struct PlayerShoot : IComponentData
{
    public int _projectiles;
    public int _shootTerm;
    public int _character;
    public int _subCharacter;
    public Entity _bullet;
}

public struct PlayerSubShoot : IComponentData
{
    public int _projectiles;
    public float _shootTerm;
    public int _character;
    public int _subCharacter;
    public Entity _bullet;
}

public struct PlayerBomb : IComponentData
{
    public float _shootTerm;
    public Entity _Bomb;
}

public struct PlayerOverflow : IComponentData
{
    public int _projectiles;
    public int _character;
    public int _subCharacter;
    public Entity _bullet;
}

public struct PlayerPasue : IComponentData
{
    bool _pauseReady;
}

public struct PlayerSlow : IComponentData
{
    public bool _Slow;
}

public struct PlayerTag : IComponentData 
{
}