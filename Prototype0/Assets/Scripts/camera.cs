using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    internal static object main;
    public Transform camera_Transform;
    public GameObject weapon;
    private Vector3 weapon_Offset = new Vector3(-.0910f, -.7645f, 1f);
    
    void Update()
    {
        transform.position = camera_Transform.position;
        weapon.transform.position = transform.position + weapon_Offset;
    }

    
}
