using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    public Light luzLinterna;
    // Start is called before the first frame update
    [SerializeField] private ControllerButtonInputReciver controllerButtonInputReciver;
    [SerializeField] private OVRInput.Button _buttonFilter;

    private void OnEnable()
    {
        controllerButtonInputReciver.ButtonInputReceived += OnButtonInputReceived;
    }
    private void OnDisable()
    {
        controllerButtonInputReciver.ButtonInputReceived -= OnButtonInputReceived;
    }

    private void OnButtonInputReceived(bool ispressed, OVRInput.Button button)
    {
        if (! ispressed)
        {
            
            return;
        }

        if(!_buttonFilter.HasFlag(button))
        {
           
            return;
        }
       

        EncenderLinterna();
    }
    

    
    public void EncenderLinterna()
    {
        
            if (luzLinterna.enabled == true)
            {
                luzLinterna.enabled = false;
            }
            else if (luzLinterna.enabled == false)
            {
                luzLinterna.enabled = true;
            }
        
    }
}
