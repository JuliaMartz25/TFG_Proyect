using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllerAxis1DInputReciver 
{
    public void RecieveInput(float value, OVRInput.Axis1D axis1d);
}
