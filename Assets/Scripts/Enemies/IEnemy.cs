using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public interface IEnemy
    {
        event BaseEnemyController.EnemyDeath OnDeath;

        GameObject GetPrototype();
    }
}