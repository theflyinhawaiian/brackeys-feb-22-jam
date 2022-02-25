using Assets.Scripts;
using UnityEngine;

public class GameManager : MonoBehaviour, ICameraMoveListener
{
    private enum Direction
    {
        Up, Down, Left, Right, Invalid
    }

    public PlayerManager player;
    public CameraManager cameraManager;
    public Transform[] rooms;

    private RoomManager roomGenerator;

    private float xOffset, yOffset;
    private int roomX, roomY;

    static Transform playerTransform;

    void Start()
    {
        ResourceManager.Load();

        cameraManager.AddCameraMoveListener(this);

        xOffset = rooms[0].transform.localScale.x;
        yOffset = rooms[0].transform.localScale.y;

        roomGenerator = new RoomManager(player.transform, xOffset, yOffset, 5);

        roomX = 0;
        roomY = 0;

        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraManager.IsObjectOffscreen(player.GetComponent<SpriteRenderer>()))
        {
            var direction = GetPositionalRelationship(player.transform.position, cameraManager.transform.position);

            var prevX = roomX;
            var prevY = roomY;

            switch (direction)
            {
                case Direction.Up:
                    roomY++;
                    break;
                case Direction.Down:
                    roomY--;
                    break;
                case Direction.Right:
                    roomX++;
                    break;
                case Direction.Left:
                    roomX--;
                    break;
                default:
                    return;
            }

            roomGenerator.ActivateRoom(prevX, prevY, roomX, roomY);
            cameraManager.MoveToRoom(roomX * xOffset, roomY * yOffset);
        }
    }

    private Direction GetPositionalRelationship(Vector3 obj1, Vector3 obj2)
    {
        var distance = obj1 - obj2;

        var overXThreshold = Mathf.Abs(distance.x) > xOffset / 2;
        var overYThreshold = Mathf.Abs(distance.y) > yOffset / 2;

        if (overXThreshold && distance.x > 0)
            return Direction.Right;
        if (overXThreshold && distance.x < 0)
            return Direction.Left;
        if (overYThreshold && distance.y > 0)
            return Direction.Up;
        if (overYThreshold && distance.y < 0)
            return Direction.Down;

        return Direction.Invalid;
    }

    public void OnCameraStartMoving() {}

    public void OnCameraStopMoving() {}

    public static Transform GetPlayerTransform() => playerTransform;
}
