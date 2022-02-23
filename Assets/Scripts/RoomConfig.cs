using Assets.Scripts.Enemies;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class RoomConfig
    {
        public List<EnemyConfig> Enemies { get; set; } = new List<EnemyConfig>();

        public bool Completed { get; set; }

        public static RoomConfig CreateRandomRoom(float playerX, float playerY, float xMax, float yMax, float margin)
        {
            var room = new RoomConfig();

            var playerVector = new Vector3(playerX, playerY, Constants.EntityZValue);
            var effectiveXMax = xMax - (2 * margin);
            var effectiveYMax = yMax - (2 * margin);
            for (var i = 0; i < 5; i++)
            {
                Vector3 spawnPoint;
                do
                {
                    spawnPoint = new Vector3((Random.value) * effectiveXMax + margin, (Random.value * effectiveYMax) + margin, Constants.EntityZValue);
                } while (Vector3.Distance(playerVector, spawnPoint) < 3);

                room.Enemies.Add(new EnemyConfig { Entity = Resources.Load("Prefabs/Enemy", typeof(GameObject)) as GameObject, XPosition = spawnPoint.x, YPosition = spawnPoint.y });
            }

            return room;
        }
    }
}
