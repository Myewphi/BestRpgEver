using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BasicMovement : MonoBehaviour {

    public bool cannotMoveBool;

    private int facing = 3; //1 = north, 2 = east, 3 = south, 4 = west
    [SerializeField]
    private Sprite[] spriteState;
    [SerializeField]
    private SpriteRenderer spriteRend;

    void Awake()
    {
        //Keeps player from being deleted during scene changes
        DontDestroyOnLoad(gameObject);
    }

	void Update () {
        //Move Up
        if (Input.GetAxisRaw("Up") == 1 && !cannotMoveBool && !Physics2D.Raycast(transform.position, Vector2.up, 1))
        {
            cannotMoveBool = true;
            facing = 1; //north
            spriteRend.sprite = ChangeDirection(1);
            StartCoroutine("MoveTo", new Vector3(0f, 0.1f));
            Debug.Log("north");
        } 
        else if (Input.GetAxisRaw("Up") == 1 && !cannotMoveBool && Physics2D.Raycast(transform.position, Vector2.up, 1).collider.tag == "Obstacle")
        {
            facing = 1; //north
            spriteRend.sprite = ChangeDirection(1);
        }
        else if(Input.GetAxisRaw("Up") == 1 && !cannotMoveBool && Physics2D.Raycast(transform.position, Vector2.up, 1).collider.tag == "Door")
        {
            facing = 1; //north
            spriteRend.sprite = ChangeDirection(1);
            SceneManager.LoadScene(Physics2D.Raycast(transform.position, Vector2.up, 1).collider.gameObject.GetComponent<DoorTeleport>().targetScene);
            transform.position = Physics2D.Raycast(transform.position, Vector2.up, 1).collider.gameObject.GetComponent<DoorTeleport>().telPos;
        }
        else if (Input.GetAxisRaw("Up") == 1 && !cannotMoveBool)
        {
            facing = 1; //north
            spriteRend.sprite = ChangeDirection(1);
        }

        //Move Down
        if (Input.GetAxisRaw("Down") == 1 && !cannotMoveBool && !Physics2D.Raycast(transform.position, Vector2.down, 1))
        {
            cannotMoveBool = true;
            facing = 3; //south
            spriteRend.sprite = ChangeDirection(3);
            StartCoroutine("MoveTo", new Vector3(0f, -0.1f));
        }
        else if (Input.GetAxisRaw("Down") == 1 && !cannotMoveBool && Physics2D.Raycast(transform.position, Vector2.down, 1).collider.tag == "Obstacle")
        {
            facing = 3; //south
            spriteRend.sprite = ChangeDirection(3);
        }
        else if (Input.GetAxisRaw("Down") == 1 && !cannotMoveBool && Physics2D.Raycast(transform.position, Vector2.down, 1).collider.tag == "Door")
        {
            facing = 3; //south
            spriteRend.sprite = ChangeDirection(3);
            SceneManager.LoadScene(Physics2D.Raycast(transform.position, Vector2.down, 1).collider.gameObject.GetComponent<DoorTeleport>().targetScene);
            transform.position = Physics2D.Raycast(transform.position, Vector2.down, 1).collider.gameObject.GetComponent<DoorTeleport>().telPos;
        }
        else if(Input.GetAxisRaw("Down") == 1 && !cannotMoveBool)
        {
            facing = 3; //south
            spriteRend.sprite = ChangeDirection(3);
        }

        //Move Left
        if (Input.GetAxisRaw("Left") == 1 && !cannotMoveBool && !Physics2D.Raycast(transform.position, Vector2.left, 1))
        {
            cannotMoveBool = true;
            facing = 4; //west
            spriteRend.sprite = ChangeDirection(4);
            StartCoroutine("MoveTo", new Vector3(-0.1f, 0f));
        }
        else if (Input.GetAxisRaw("Left") == 1 && !cannotMoveBool && Physics2D.Raycast(transform.position, Vector2.left, 1).collider.tag == "Obstacle")
        {
            facing = 4; //west
            spriteRend.sprite = ChangeDirection(4);
        }
        else if (Input.GetAxisRaw("Left") == 1 && !cannotMoveBool && Physics2D.Raycast(transform.position, Vector2.left, 1).collider.tag == "Door")
        {
            facing = 4; //west
            spriteRend.sprite = ChangeDirection(4);
            SceneManager.LoadScene(Physics2D.Raycast(transform.position, Vector2.left, 1).collider.gameObject.GetComponent<DoorTeleport>().targetScene);
            transform.position = Physics2D.Raycast(transform.position, Vector2.left, 1).collider.gameObject.GetComponent<DoorTeleport>().telPos;
        }
        else if(Input.GetAxisRaw("Left") == 1 && !cannotMoveBool)
        {
            facing = 4; //west
            spriteRend.sprite = ChangeDirection(4);
        }

        //Move Right
        if (Input.GetAxisRaw("Right") == 1 && !cannotMoveBool && !Physics2D.Raycast(transform.position, Vector2.right, 1))
        {
            cannotMoveBool = true;
            facing = 2; //east
            spriteRend.sprite = ChangeDirection(2);
            StartCoroutine("MoveTo", new Vector3(0.1f, 0f));
        }
        else if (Input.GetAxisRaw("Right") == 1 && !cannotMoveBool && Physics2D.Raycast(transform.position, Vector2.right, 1).collider.tag == "Obstacle")
        {
            facing = 2; //east
            spriteRend.sprite = ChangeDirection(2);
        }
        else if (Input.GetAxisRaw("Right") == 1 && !cannotMoveBool && Physics2D.Raycast(transform.position, Vector2.right, 1).collider.tag == "Door")
        {
            facing = 2; //east
            spriteRend.sprite = ChangeDirection(2);
            SceneManager.LoadScene(Physics2D.Raycast(transform.position, Vector2.right, 1).collider.gameObject.GetComponent<DoorTeleport>().targetScene);
            transform.position = Physics2D.Raycast(transform.position, Vector2.right, 1).collider.gameObject.GetComponent<DoorTeleport>().telPos;
        }
        else if(Input.GetAxisRaw("Right") == 2)
        {
            facing = 2; //east
            spriteRend.sprite = ChangeDirection(1);
        }

        //Interact
        if(Input.GetButtonDown("Interact") && !cannotMoveBool)
        {
            if(facing == 1 && Physics2D.Raycast(transform.position, Vector2.up, 1).collider.tag == "Interactable")
            {
                Physics2D.Raycast(transform.position, Vector2.up, 1).collider.GetComponent<InteractBase>().player = gameObject;
                Physics2D.Raycast(transform.position, Vector2.up, 1).collider.GetComponent<InteractBase>().Interact();
            }
            if (facing == 2 && Physics2D.Raycast(transform.position, Vector2.right, 1).collider.tag == "Interactable")
            {
                Physics2D.Raycast(transform.position, Vector2.right, 1).collider.GetComponent<InteractBase>().player = gameObject;
                Physics2D.Raycast(transform.position, Vector2.right, 1).collider.GetComponent<InteractBase>().Interact();
            }
            if (facing == 3 && Physics2D.Raycast(transform.position, Vector2.down, 1).collider.tag == "Interactable")
            {
                Physics2D.Raycast(transform.position, Vector2.down, 1).collider.GetComponent<InteractBase>().player = gameObject;
                Physics2D.Raycast(transform.position, Vector2.down, 1).collider.GetComponent<InteractBase>().Interact();
            }
            if (facing == 4 && Physics2D.Raycast(transform.position, Vector2.left, 1).collider.tag == "Interactable")
            {
                Physics2D.Raycast(transform.position, Vector2.left, 1).collider.GetComponent<InteractBase>().player = gameObject;
                Physics2D.Raycast(transform.position, Vector2.left, 1).collider.GetComponent<InteractBase>().Interact();
            }
        }
    }

    Sprite ChangeDirection(int i)
    {
        return spriteState[i - 1];
    }

    IEnumerator MoveTo (Vector3 direction)
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
