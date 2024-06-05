using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllerAxis2DInputReciver 
{
    public void RecieveInput(Vector2 valueaxis2d, OVRInput.Axis2D axis2d);
}
