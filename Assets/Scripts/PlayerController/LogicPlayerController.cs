using System;
using UnityEngine;

public class LogicPlayerController
{
    private IPlayerControllerView playerControllerView;
    private Vector3 direction;
    private float stepSize = 1;
    private float walkSpeed = 2;
    private bool canMove = true;
    Cell[,] map;
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

    public float WalkSpeed()
    {
        return walkSpeed * playerControllerView.GetDeltaTime();
    }

    public Vector3 NextPosition { get; private set; }

    public LogicPlayerController(IPlayerControllerView playerControllerView, Vector2 nextPosition, float stepSize, float walkSpeed)
    {
        this.playerControllerView = playerControllerView;
        direction = Vector3.zero;
        NextPosition = nextPosition;
        this.stepSize = stepSize;
        this.walkSpeed = walkSpeed;
        playerControllerView.Configure();
        map = ServiceLocator.Instance.GetService<IMapGenerator>().GetMap();
    }

    public void AddDirection(Vector3 position)
    {
        if (IsMoving(position))
        {
            return;
        }
        Position p = playerControllerView.GetPositionPacmanInTheMap();
        Cell cell = map[(int)p.X, (int)p.Y];
        Cell nextCell = map[(int)(p.X + direction.x) , (int)(p.Y + (direction.y))];
        if (!nextCell.IsTrigger)
        {
            return;
        }
        p.AddX(direction.x);
        p.AddY(direction.y);
        cell = map[(int)p.X, (int)p.Y];
        
        NextPosition = cell.Sprite.transform.position;
    }

    public bool CheckMovement(Vector3 input)
    {
        return true;
    }

    public void SetDirection(Vector3 input)
    {
        if (CheckMovement(input))
        {
            direction = InputToDirectionVector3(input);
        }
    }

    private Vector3 InputToDirectionVector3(Vector2 input)
    {
        
        return directionList[(int)ConvertInputValueToDirectionEnum(input)];
    }

    private Direction ConvertInputValueToDirectionEnum(Vector2 input)
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
                if (input.y < 0)
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

    public bool IsMoving(Vector3 position)
    {
        return !canMove || NextPosition != position;
    }
}