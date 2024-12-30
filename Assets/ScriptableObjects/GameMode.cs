using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    [CreateAssetMenu(fileName = "GameMode", menuName = "ScriptableObjects/GameMode/GameMode", order = 2)]
    public class GameMode : ScriptableObject
    {

        public Vector3 start;
        public PlayerCharacterSettings playerCharacterSettings;
        public int enemyHealth;
        public int enemyDamage;
    }

}