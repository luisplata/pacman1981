using UnityEngine;

public class InputJoistyckAdapter : IInputAdapter
{
    private Joystick joystick;

    public InputJoistyckAdapter(Joystick joystick)
    {
        this.joystick = joystick;
        joystick.SnapX = true;
        joystick.SnapY = true;
    }

    public Vector2 GetDirection()
    {
        return new Vector2(joystick.Horizontal, joystick.Vertical);
    }
}