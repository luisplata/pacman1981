using UnityEngine;

public class PlayerControllerView : MonoBehaviour, IPlayerControllerView
{
    [SerializeField]
    float stepSize = 1;
    [SerializeField]
    float walkSpeed = 2;
    private LogicPlayerController logic;
    private IInputAdapter _input;

    private void Start()
    {
        logic = new LogicPlayerController(this, transform.position, stepSize, walkSpeed);
    }

    public void Configure()
    {
        _input = GetComponent<PacmanOfMap>().InputStragety.GetInput();
    }

    private void Update()
    {
        var input = _input.GetDirection();
        logic.SetDirection(input.normalized);
        logic.AddDirection(transform.position);
    }
    void LateUpdate()
    {
        var vector = Vector3.MoveTowards(transform.position, logic.NextPosition, logic.WalkSpeed());
        transform.position = vector;
    }

    public float GetDeltaTime()
    {
        return Time.deltaTime;
    }
}