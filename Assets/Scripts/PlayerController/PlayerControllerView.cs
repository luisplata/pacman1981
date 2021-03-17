using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerView : MonoBehaviour
{
    public Vector3 direction;

    [SerializeField]
    float stepSize = 1;
    [SerializeField]
    float walkSpeed = 2;
    [SerializeField]
    bool canMove = true;
    Vector3 nextPosition;

    private Vector2 vector2Direction;
    private Vector2 input;
    private enum Direction
    {
        North,//0
        South,//1
        East,//2
        West,//3
        None//4
    }

    private Vector3[] directionList = new Vector3[]{
        Vector3.up,
        Vector3.down,
        Vector3.right,
        Vector3.left,
        Vector3.zero,
    };

    private string[] directionName = new string[]{
        "North",
        "South",
        "East",
        "West",
        "None",
    };
    private void Start()
    {
        nextPosition = transform.position;
        direction = Vector2.zero;
    }
    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, walkSpeed * Time.deltaTime);
    }
    private void Update()
    {
        if (IsMoving())
        {
            return;
        }
        nextPosition += stepSize * direction;
    }

    private void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
        if (CheckMovement(input))
        {
            SetDirection(directionList[(int)Vector2ToDirection(input)]);
        }
    }
    public bool IsMoving()
    {
        return !canMove || nextPosition != transform.position;
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Colision con {collision.name}");
        if (collision.CompareTag("comida") || collision.CompareTag("powerup"))
        {
            Destroy(collision.gameObject);
        }
    }

    private Direction Vector2ToDirection(Vector2 input)
    {
        Direction direction;
        if (input != Vector2.zero)
        {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                if (input.x > 0)
                {
                    direction = Direction.East;
                }
                else
                {
                    direction = Direction.West;
                }
            }
            else
            {
                if (input.y > 0)
                {
                    direction = Direction.North;
                }
                else
                {
                    direction = Direction.South;
                }
            }
        }
        else
        {
            direction = Direction.None;
        }
        return direction;
    }

    bool CheckMovement(Vector3 direction)
    {
        return true;
    }
}
