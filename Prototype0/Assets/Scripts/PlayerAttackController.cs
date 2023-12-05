using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
   /*
    * uses weapon_Active_Check to track which wepaon is equipped then swaps them and animator
    * 
    */

    public Animator animator;
    public GameObject[] weapon_Loadout;
    public GameObject active_Weapon;
    private int[] weapon_Active_Check = new int [2];

    private Gernade nade;
    void Start()
    {
        Weapons_Setup();
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Attack_Handler();
        }

        Select_Weapon ();
    }

    private void Select_Weapon()
    {
        if(Input.mouseScrollDelta.y > 0 || Input.mouseScrollDelta.y < 0)
            Swap_Weapons();
        
        else if(Input.GetKeyDown(KeyCode.Alpha1))
            Swap_First_Weapon();
        
        else if(Input.GetKeyUp(KeyCode.Alpha2))
            Swap_Second_Weapon();

       
    }

    private void Swap_Weapons()
    {

        if (weapon_Active_Check[0] == 1)
        {
            Swap_First_Weapon();
        }

        else if (weapon_Active_Check[0] == 0)
        {
            Swap_Second_Weapon();
        }

    }

    private void Swap_First_Weapon()
    {
        weapon_Loadout[0].SetActive(false);
        weapon_Loadout[1].SetActive(true);
        animator = weapon_Loadout[1].GetComponent<Animator>();

        weapon_Active_Check[0] = 0;
        weapon_Active_Check[1] = 1;

        active_Weapon = weapon_Loadout[1];
    }

    private void Swap_Second_Weapon()
    {
        weapon_Loadout[0].SetActive(true);
        weapon_Loadout[1].SetActive(false);

        weapon_Active_Check[0] = 1;
        weapon_Active_Check[1] = 0;

        animator = weapon_Loadout[0].GetComponent<Animator>();
        active_Weapon = weapon_Loadout[0];
    }

    private void Weapons_Setup()
    {
        weapon_Loadout = GameObject.FindGameObjectsWithTag("Weapon");
        weapon_Loadout[0].SetActive(false);
        weapon_Active_Check[1] = 1; weapon_Active_Check[0] = 0;
        active_Weapon = weapon_Loadout[1];
        animator = weapon_Loadout[1].GetComponent<Animator>();
    }

    private void Attack_Handler()
    {
        if(animator != null) 
        {

            animator.SetTrigger("OnLeftClick");
        }

        if (active_Weapon.name == "Gernade")
        {
            nade = active_Weapon.GetComponent<Gernade>();
            nade.Throw();
        }
    }

 
}
