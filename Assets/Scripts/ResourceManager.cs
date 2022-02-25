using Assets.Scripts.Enemies;
using Assets.Scripts.Weapons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public static class ResourceManager
    {
        public static Dictionary<EnemyType, GameObject> EnemyPrefabs { get; } = new Dictionary<EnemyType, GameObject>();

        public static Dictionary<ProjectileType, GameObject> ProjectilePrefabs { get; } = new Dictionary<ProjectileType, GameObject>();

        public static void Load()
        {
            EnemyPrefabs.Add(EnemyType.Basic, Resources.Load<GameObject>("Prefabs/Enemy"));
            EnemyPrefabs.Add(EnemyType.Projectile, Resources.Load<GameObject>("Prefabs/ProjectileEnemy"));

            ProjectilePrefabs.Add(ProjectileType.Basic, Resources.Load<GameObject>("Prefabs/Bullet"));
        }
    }
}
