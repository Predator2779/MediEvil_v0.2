using System.Collections.Generic;
using UnityEngine;

namespace Character.Configs
{
    [CreateAssetMenu(menuName = "Configs/Characters", fileName = "New CharacterConfig", order = 0)]
    public class CharacterConfig : ScriptableObject
    {
        [Header("About")] [Space]
        public string Name;
        
        [Space] [Header("Movement")] 
        public float SpeedMove;
        public float SpeedRun;
        public float JumpForce;
        public float FallSpeed;
        public float RollHeight;
        public float RollDistance;
        public float SlideLimitVelocity;
        public float SlideSpeed;

        [Space] [Header("Parameters")] [Space]
        
        [Header("Attack")] 
        public float Damage;
        public float DamageThrowDamage;
        public float ThrowForce;
        public float CombatSlideDamage;
        public float ComboAttackDamageModificator;
        [Tooltip("MilliSeconds")] public int ComboInterval;
        [Tooltip("Frames")] public int SecondStrikeDelay;
        
        [Header("Health")]
        public int MaxHealth;
        public int CurrentHealth;
        
        [Header("Stamina")]
        public int MaxStamina;
        public int CurrentStamina;
        public float StaminaRecovery;
        public float StaminaUsage;
        public float StaminaAttackUsageCoef;
        [Tooltip("Milliseconds")] public int StaminaRestoreDelay;
        
        [Header("Mana")] 
        public int MaxMana;
        public int CurrentMana;
        public float ManaUsage;
        
        [Header("Death")] 
        [Tooltip("Milliseconds")] public int TimeToRespawn;
        public List<Vector2> SavePoints = new List<Vector2>();
        
        [Header("For AI")]
        public bool CanRoll;
        public int ComboChanceAI = 10;   // 1 из 10 шанс совершения комбо
        public int RollChanceAI = 35;    // 1 из 35 шанс совершения переката
    }
}