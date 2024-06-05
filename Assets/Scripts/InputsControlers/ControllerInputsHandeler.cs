using Oculus.Interaction;
using Oculus.Interaction.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputsHandeler : MonoBehaviour
{
    //Requiremnets
    // 1. get hold of the Grab Interactor
    // 2. get hold of the Controller ref (this one)
    // 3. get hold of the grab interactable
    // 4. Get in controller inputs
    // 5. Send inputs to the grabbed interactable

    private OVRInput.Button IndexTriggerAsButton => _controllerRef.Handedness == Handedness.Left ? OVRInput.Button.PrimaryIndexTrigger : OVRInput.Button.SecondaryIndexTrigger;
    private OVRInput.Axis1D IndexTriggerAsAxis1D => _controllerRef.Handedness == Handedness.Left ? OVRInput.Axis1D.PrimaryIndexTrigger : OVRInput.Axis1D.SecondaryIndexTrigger;
    private OVRInput.Axis2D ThumbstickAsAxis2D => _controllerRef.Handedness == Handedness.Left ? OVRInput.Axis2D.PrimaryThumbstick : OVRInput.Axis2D.SecondaryThumbstick;
    //private OVRInput.Button ThumbstickAsAxis2D => 

    [SerializeField] private ControllerRef _controllerRef;
    [SerializeField] private GrabInteractor _grabInteractor;

    private GrabInteractable _grabInteractable;
    private IControllerButtonInputReciver _controllerbuttonInputReciver;
   // private IControllerAxis1DInputReciver _controllerAxis1DInputReciver;
   // private IControllerAxis2DInputReciver _controllerAxis2DInputReciver;

    private void Update()
    {
       if (!_grabInteractor.HasSelectedInteractable)
        {
            _grabInteractable = null;
            _controllerbuttonInputReciver = null;
            return;
        }
           

        if (_grabInteractable == null)
        {
            _grabInteractable = _grabInteractor.SelectedInteractable;
         
        }


        ReadInputs();

    }

    private void ReadInputs()
    {
        
        HandleButton(OVRInput.Get(IndexTriggerAsButton), IndexTriggerAsButton);
        HandleAxis1D(OVRInput.Get(IndexTriggerAsAxis1D), IndexTriggerAsAxis1D);
        HandleAxis2D(OVRInput.Get(ThumbstickAsAxis2D), ThumbstickAsAxis2D);
    

        
    }

    private void HandleButton(bool isPressed,OVRInput.Button button)
    {
        if(_controllerbuttonInputReciver == null)
        {
            if (_grabInteractable.TryGetComponent(out IControllerButtonInputReciver buttonInputReciver))
            {
                _controllerbuttonInputReciver = buttonInputReciver;
            }
        }
        if (_controllerbuttonInputReciver != null)
        {
            _controllerbuttonInputReciver.RecieveInput(isPressed, button);
        }



    }
    private void HandleAxis1D(float value, OVRInput.Axis1D axis1D )
    {

    }
    private void HandleAxis2D(Vector2 value, OVRInput.Axis2D axis2D)
    {

    }
}
