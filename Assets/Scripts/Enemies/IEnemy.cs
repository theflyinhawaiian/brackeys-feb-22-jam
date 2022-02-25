using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public interface IEnemy
    {
        event BaseEnemyController.EnemyDeath OnDeath;

        public int MaxHealth { get; }

        public int CurrentHealth { get; }

        void Configure(int currentHealth, int maxHealth);

        EnemyType Type { get; set; }
    }
}