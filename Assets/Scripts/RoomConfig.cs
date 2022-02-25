using Assets.Scripts.Enemies;
using System.Collections.Generic;
using UnityEngine;
using Serializable = System.SerializableAttribute;

namespace Assets.Scripts
{
    [Serializable]
    public class RoomConfig
    {
        public List<EnemyConfig> Enemies = new List<EnemyConfig>();

        public bool Completed;

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

                room.Enemies.Add(
                    new EnemyConfig {
                        Type = Random.value > 0.5
                                        ? EnemyType.Basic
                                        : EnemyType.Projectile,
                        MaxHealth = 5,
                        CurrentHealth = 5,
                        XPosition = spawnPoint.x,
                        YPosition = spawnPoint.y
                    });
            }

            return room;
        }

        public static RoomConfig GetRoomFromFile(string fileName)
        {
            var file = Resources.Load<TextAsset>(fileName);
            return JsonUtility.FromJson<RoomConfig>(file.text);
        }
    }
}
