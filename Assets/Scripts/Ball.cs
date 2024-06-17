using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody rigibody;
    float timer;
    [Range (0,1f)]
    public float bounciness;
    PhysicMaterial physicMat;

    private void Start()
    { 
        physicMat = new PhysicMaterial();
        physicMat.bounciness = bounciness;
        physicMat.frictionCombine = PhysicMaterialCombine.Minimum;
        physicMat.bounceCombine = PhysicMaterialCombine.Maximum;
        GetComponent<SphereCollider>().material = physicMat;
        
    }
    void Update()
    {
        if (this.gameObject == null)
        {
            timer = 0;
        }

        if (this.gameObject != null)
        {
            timer += Time.deltaTime;
            if (timer >= 2f)
            {
               
                rigibody.useGravity = true;
               this.gameObject.transform.SetParent(null);
                timer = 0;
            }
            else
            {
                rigibody.useGravity = false;
            }

        }

    }

    
}
