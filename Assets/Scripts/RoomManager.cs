using System.Collections.Generic;
using Assets.Scripts.Enemies;
using UnityEngine;

namespace Assets.Scripts
{
    class RoomManager
    {
        private Transform playerPosition;

        private float xOffset;
        private float yOffset;
        private float roomMargin;

        RoomConfig ActiveRoom;
        List<GameObject> activeEnemies = new List<GameObject>();

        Dictionary<(int x, int y), RoomConfig> _generatedRooms = new Dictionary<(int x, int y), RoomConfig>();

        public RoomManager(Transform playerPos, float xOffset, float yOffset, float margin)
        {
            this.xOffset = xOffset;
            this.yOffset = yOffset;

            playerPosition = playerPos;
            roomMargin = margin;
        }

        public void ActivateRoom(int prevX, int prevY, int newX, int newY)
        {
            if(ActiveRoom != null)
            {
                ActiveRoom.Enemies.Clear();
                foreach(var obj in activeEnemies)
                {
                    var enemy = obj.GetComponent<IEnemy>();
                    var savedX = (obj.transform.position.x + (xOffset / 2)) % xOffset;
                    var savedY = (obj.transform.position.y + (yOffset / 2)) % yOffset;

                    // I have no idea why I need to do this... but I need to do this.
                    if (savedX < 0)
                        savedX = xOffset + savedX;

                    if (savedY < 0)
                        savedY = yOffset + savedY;

                    ActiveRoom.Enemies.Add(new EnemyConfig { Entity = enemy.GetPrototype(), XPosition = savedX, YPosition = savedY });
                    Object.Destroy(obj);
                }

                activeEnemies.Clear();
                _generatedRooms[(prevX, prevY)] = ActiveRoom;
            }

            RoomConfig roomConfig;

            if (_generatedRooms.ContainsKey((newX, newY)))
                roomConfig = _generatedRooms[(newX, newY)];
            else
            {
                var playerX = playerPosition.position.x;
                var playerY = playerPosition.position.y;
                roomConfig = RoomConfig.CreateRandomRoom(playerX % xOffset, playerY % yOffset, xOffset, yOffset, roomMargin);
                _generatedRooms.Add((newX, newY), roomConfig);
            }

            if (roomConfig.Completed)
                return;

            foreach(var enemy in roomConfig.Enemies)
            {
                var loadedX = (newX * xOffset) + enemy.XPosition - (xOffset / 2);
                var loadedY = (newY * yOffset) + enemy.YPosition - (yOffset / 2);
                var obj = Object.Instantiate(enemy.Entity, new Vector3(loadedX, loadedY, Constants.EntityZValue), Quaternion.identity);
                var mob = obj.GetComponent<IEnemy>();
                mob.OnDeath += () => activeEnemies.Remove(obj);
                activeEnemies.Add(obj);
            }

            ActiveRoom = roomConfig;
        }

        public void SetRoomComplete(int roomX, int roomY)
        {
            _generatedRooms[(roomX, roomY)].Completed = true;
        }
    }
}
