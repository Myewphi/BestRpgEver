using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BasicMovement : MonoBehaviour {

    public bool cannotMoveBool;

    private int facing = 3; //1 = north, 2 = east, 3 = south, 4 = west

    [SerializeField]
    private Sprite[] directionSprites; //sprites for all directions the player faces
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        //Keeps player from being deleted during scene changes
        DontDestroyOnLoad(gameObject);
    }

	void Update () {
        //Move Up
        if(Input.GetAxisRaw("Up") == 1)
        {
            MovementHandler(Vector2.up, new Vector3(0f, 0.1f), 1);
        }
        //Move Right
        if (Input.GetAxisRaw("Right") == 1)
        {
            MovementHandler(Vector2.right, new Vector3(0.1f, 0f), 2);
        }
        //Move Down
        if (Input.GetAxisRaw("Down") == 1)
        {
            MovementHandler(Vector2.down, new Vector3(0f, -0.1f), 3);
        }
        //Move Left
        if (Input.GetAxisRaw("Left") == 1)
        {
            MovementHandler(Vector2.left, new Vector3(-0.1f, 0f), 4);
        }

        //Interact
        if (Input.GetButtonDown("Interact") && !cannotMoveBool)
        {
            //Interact North
            if(facing == 1)
            {
                InteractionHandler(Vector2.up);
            }
            //Interact East
            if (facing == 2)
            {
                InteractionHandler(Vector2.right);
            }
            //Interact South
            if (facing == 3)
            {
                InteractionHandler(Vector2.down);
            }
            //Interact West
            if (facing == 4)
            {
                InteractionHandler(Vector2.left);
            }
        }
    }

    private void MovementHandler(Vector2 directionVector2, Vector3 directionVector3, int directionFacing)
    {
        //Changes direction before movement
        facing = directionFacing;
        spriteRenderer.sprite = ChangeDirection(directionFacing);

        //Checks where we are going before movement
        //Normal Ground
        if (!cannotMoveBool && !Physics2D.Raycast(transform.position, directionVector2, 1))
        {
            cannotMoveBool = true;
            StartCoroutine("MoveCoroutine", directionVector3);
        }
        //Obstacles
        else if (!cannotMoveBool && Physics2D.Raycast(transform.position, directionVector2, 1).collider.tag == "Obstacle")
        {
            //Nothing is done when an obstacle is hit for now
        }
        //Doors
        else if (!cannotMoveBool && Physics2D.Raycast(transform.position, directionVector2, 1).collider.tag == "Door")
        {
            SceneManager.LoadScene(Physics2D.Raycast(transform.position, directionVector2, 1).collider.gameObject.GetComponent<DoorTeleport>().targetScene);
            transform.position = Physics2D.Raycast(transform.position, directionVector2, 1).collider.gameObject.GetComponent<DoorTeleport>().telPos;
        }
        //Combat Areas
        else if (!cannotMoveBool && Physics2D.Raycast(transform.position, directionVector2, 1).collider.tag == "CombatArea")
        {
            cannotMoveBool = true;
            StartCoroutine("MoveCoroutine", directionVector3);
        }
    }

    private void InteractionHandler(Vector2 directionVector2)
    {
        if(Physics2D.Raycast(transform.position, directionVector2, 1).collider.tag == "Interactable")
        {
            //This is the raycast
            RaycastHit2D raycast = Physics2D.Raycast(transform.position, directionVector2, 1);
            //grabs InteractBase script from object hit by raycast
            InteractBase interactScript = raycast.collider.GetComponent<InteractBase>();
            //Links interact script to the player
            interactScript.player = gameObject;
            //Runs interact scripts Interact() function
            interactScript.Interact();
        }
    }

    Sprite ChangeDirection(int i)
    {
        return directionSprites[i - 1];
    }

    IEnumerator MoveCoroutine (Vector3 direction)
    {
        //for smooth movement between tiles
        for(int i = 0; i != 10; i++)
        {
            transform.position = transform.position + direction;
            yield return new WaitForSeconds(0.01f);
        }
        cannotMoveBool = false;
    }
}
