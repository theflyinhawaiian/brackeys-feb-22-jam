using Assets.Scripts.Enemies;
using System;

namespace Assets.Scripts
{
    [Serializable]
    public class EnemyConfig
    {
        public EnemyType Type;

        public int MaxHealth;

        public int CurrentHealth;

        public float XPosition;

        public float YPosition;
    }
}
