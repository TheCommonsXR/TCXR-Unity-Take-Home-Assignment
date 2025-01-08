using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameMode", menuName = "GameModes/New Game Mode")]
public class GameMode : ScriptableObject
{
    public string modeName;
    public int currentHp;
    public Vector3 startingPosition;
    public float playerSpeed;
    public int bulletDamage;
    public GameObject bulletPreFab;
    internal GameObject bulletPrefab;
}
