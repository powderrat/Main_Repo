using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Gernade : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("References")]
    public GameObject another_Nade;
    public Transform cam;
    public Transform attack_Point;
    public ParticleSystem boomy;
    public GameObject camera0; 
    

    [Header("Settings")]
    public int total_Throws;
    public float throw_Cooldown;
    public bool is_Prefab;
    public float radius = 15f;

    [Header("Throwing")]
    public float throw_Force;
    public float upward_Throw_Force;
    bool ready_To_Throw;
    public float boom_Force = 1500f; 

    void Start()
    {
        ready_To_Throw = true;
        camera0 = GameObject.Find("Camera Holder");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && ready_To_Throw && total_Throws > 0)
        {
            Throw();
        }
    }

    public void Throw()
    {
        ready_To_Throw = false;
        //thrown obj
        GameObject Projectile = Instantiate(another_Nade, attack_Point.position, cam.rotation);

        Rigidbody projectilerb = Projectile.GetComponent<Rigidbody>();

        //add force
        Vector3 force_To_Add = cam.forward * throw_Force + transform.up * upward_Throw_Force;

        projectilerb.AddForce(force_To_Add, ForceMode.Impulse);

        total_Throws--;

        Invoke(nameof(reset_Throw), throw_Cooldown);
    }

    public void reset_Throw()
    {
        ready_To_Throw = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(is_Prefab)
        {
            Instantiate(boomy, transform.position, transform.rotation);

            Collider [] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach(Collider collider in colliders) 
            {
                
                Rigidbody temp = collider.GetComponent<Rigidbody>();

                if(temp != null)
                {
                    temp.AddExplosionForce(boom_Force, transform.position, radius);
                    CameraShaker.Instance.ShakeOnce(4f, 4f, .3f, .2f);
                    
                }
            }

            Destroy(this.gameObject);
        }
         

       
    }


}
