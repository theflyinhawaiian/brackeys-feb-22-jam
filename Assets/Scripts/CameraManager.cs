using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float maxVelocity = 1f;

    private Camera mainCamera;

    private Vector3 target;
    private bool isMoving;

    private List<ICameraMoveListener> cameraMoveListeners = new List<ICameraMoveListener>();

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (!isMoving)
            return;

        var move = Vector3.Lerp(mainCamera.transform.position, target, maxVelocity * Time.deltaTime);

        if (move != target)
        {
            mainCamera.transform.position = move;
            return;
        }

        isMoving = false;
        NotifyMovingStopped();
    }

    public bool IsObjectOffscreen(Renderer renderer) => !GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(mainCamera), renderer.bounds);

    public void MoveToRoom(float xCoord, float yCoord)
    {
        target = new Vector3(xCoord, yCoord, mainCamera.transform.position.z);
        isMoving = true;
        NotifyMovingStarted();
    }

    public void AddCameraMoveListener(ICameraMoveListener listener) => cameraMoveListeners.Add(listener);

    public void RemoveCameraMoveListener(ICameraMoveListener listener) => cameraMoveListeners.Remove(listener);

    public void NotifyMovingStarted()
    {
        foreach(var listener in cameraMoveListeners)
        {
            listener.OnCameraStartMoving();
        }
    }

    public void NotifyMovingStopped()
    {
        foreach (var listener in cameraMoveListeners)
        {
            listener.OnCameraStopMoving();
        }
    }
}