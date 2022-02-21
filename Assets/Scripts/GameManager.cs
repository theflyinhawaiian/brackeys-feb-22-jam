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

    private float xOffset, yOffset;
    private int camX, camY;

    void Start()
    {
        cameraManager.AddCameraMoveListener(this);

        xOffset = rooms[0].transform.localScale.x;
        yOffset = rooms[0].transform.localScale.y;

        camX = 0;
        camY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraManager.IsObjectOffscreen(player.GetComponent<SpriteRenderer>()))
        {
            var direction = GetPositionalRelationship(player.transform.position, cameraManager.transform.position);

            switch (direction)
            {
                case Direction.Up:
                    camY++;
                    break;
                case Direction.Down:
                    camY--;
                    break;
                case Direction.Right:
                    camX++;
                    break;
                case Direction.Left:
                    camX--;
                    break;
                default:
                    return;
            }

            cameraManager.MoveToRoom(camX * xOffset, camY * yOffset);
            //AdjustBackgrounds();
            player.DisableMovement();
        }
    }

    void AdjustBackgrounds()
    {
        var zValue = rooms[0].transform.position.z;
        rooms[0].transform.position = new Vector3(camX * xOffset, camY * yOffset, zValue);
        rooms[1].transform.position = new Vector3(camX-1 * xOffset, camY * yOffset, zValue);
        rooms[2].transform.position = new Vector3(camX * xOffset, camY+1 * yOffset, zValue);
        rooms[3].transform.position = new Vector3(camX * xOffset, camY-1 * yOffset, zValue);
        rooms[4].transform.position = new Vector3(camX+1 * xOffset, camY * yOffset, zValue);
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

    public void OnCameraStopMoving()
    {
        player.EnableMovement();
    }
}
