using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//spins the doughnut/power-up
public class Rotate : MonoBehaviour
{
    private Vector3 speed = new Vector3(0,0,100);
    PlayerController controller;
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(speed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
        controller.power_Up();
    }

    

}
