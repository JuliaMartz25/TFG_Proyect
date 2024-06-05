using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerButtonInputReciver : MonoBehaviour , IControllerButtonInputReciver
{
    public event Action<bool,OVRInput.Button> ButtonInputReceived;
    public void RecieveInput(bool isPressed, OVRInput.Button button)
    {
        if(ButtonInputReceived != null)
        {
            ButtonInputReceived?.Invoke(isPressed,button);
        }
    }
}
