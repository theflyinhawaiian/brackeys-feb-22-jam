using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float maxVelocity = 0.5f;

    public GameObject GameManager;
    protected Rigidbody2D body;
    public Camera cam;

    private Vector2 move;
    private Vector2 mousePos;

    public float currentTime = -1000f;
    public float iframes = .5f;
    private float spawnBlockingRadius = 15;

    private bool movementEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        // Things we'll re-implement as the need arises
        /*
        manager = GameManager.GetComponent<GameManager>();
        torch = GetComponentInChildren<TorchBehavior>();

        gun = GetComponentInChildren<GunBehavior>();
        torchPlacer = GetComponentInChildren<TorchPlacementBehavior>();

        activeItem = PlayerItem.Torch;
        */
    }

    // Update is called once per frame
    void Update()
    {
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButton("Fire1"))
        {
            // Activating selected item
            //ProcessAction(activeItem);
        }

        // Arming inventory items
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //activeItem = PlayerItem.Weapon;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //activeItem = PlayerItem.Torch;
        }
    }

    // Activating items
    /*
    void ProcessAction(PlayerItem item)
    {
        
        switch (item)
        {
            case PlayerItem.Weapon:
                gun.FireBullet();
                break;
            case PlayerItem.Torch:
                torchPlacer.PlaceTorch();
                break;
        }
    }*/

    void FixedUpdate()
    {
        if (!movementEnabled)
            return;

        body.position = body.position + (move * maxVelocity);

        Vector2 lookDir = mousePos - body.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        body.rotation = angle;
    }

    public bool EnableMovement() => movementEnabled = true;

    public bool DisableMovement() => movementEnabled = false;
}
