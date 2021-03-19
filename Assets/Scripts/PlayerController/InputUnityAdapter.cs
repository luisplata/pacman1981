using UnityEngine;

public class InputUnityAdapter : IInputAdapter
{
    public Vector2 GetDirection()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}