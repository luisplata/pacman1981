using System;
using UnityEngine;

public class InputStragety : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    public IInputAdapter GetInput()
    {
#if UNITY_ANDROID
        return new InputJoistyckAdapter(joystick);
#else
        joystick.gameObject.SetActive(false);
        return new InputUnityAdapter();
#endif
    }
}