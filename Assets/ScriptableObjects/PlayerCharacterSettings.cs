using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharacterSettings", menuName = "ScriptableObjects/GameMode/PlayerCharacterSettings", order = 1)]
public class PlayerCharacterSettings : ScriptableObject
{

    //movement settings
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    
    //combat and health settings
    public int startingHP;
    public int maxHP;

    public float invincibleTime;

    public int bulletDamage;

    

}
