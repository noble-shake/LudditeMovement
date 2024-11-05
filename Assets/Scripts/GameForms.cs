using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ObjectType
{
    Player,
    Enemy,
    Bullet,
    Wall,
    Item,
}

[System.Serializable]
public enum enumDifficulty
{
    EASY,
    NORMAL,
    HARD,
    LUNARTIC,
    EXTRA,
    None,
}

[System.Serializable]
public enum PlayableCharacter
{
    REIMU, // Yukari
    MARISA, // Okina
    SUMIREKO, // Kasen
    RIN,
}

[System.Serializable]
public enum SubPlayableCharacter
{
    YUKARI,
    OKINA,
    KASEN,
    MARIA,
}