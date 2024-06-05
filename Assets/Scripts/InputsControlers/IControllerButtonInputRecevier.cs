using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllerButtonInputReciver 
{
    public void RecieveInput(bool isPressed, OVRInput.Button button);
}
