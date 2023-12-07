using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
/*
 * Handles the spawning of the animals and power-ups
 */
public class SpawnManager : MonoBehaviour
{
    Vector3 pos_One = new Vector3(8.65f, 0, 0);
    Vector3 pos_Two = new Vector3(20.25f, 0, 0);
    public GameObject[] Enemies;
    GameObject enemy_1; 
    GameObject enemy_2;
    GameManager game_Manager;
    public GameObject power_Up, current_Power_Up;
    
    void Start()
    {
        // two enemies on screen. The one upfront being harvested is active
        game_Manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        enemy_1 = spawn_Enemy_Pos_1();
        enemy_1.GetComponent<EnemyController>().is_Active = true;
        enemy_2 = spawn_Enemy_Pos_2();
        current_Power_Up = spawn_Power_Up();
    }
    void Update()
    {   //on death place next enemy up front and spawn anther 
        if (enemy_1 == null)
        {
            enemy_2.GetComponent<EnemyController>().is_Active = true;
            enemy_2.transform.position = pos_One;
            enemy_1 = spawn_Enemy_Pos_2();
            game_Manager.update_Souls(enemy_1.GetComponent<EnemyController>().soul_Stones);

        }

        if (enemy_2 == null)
        {
            enemy_1.GetComponent<EnemyController>().is_Active = true;
            enemy_1.transform.position = pos_One;
            enemy_2 = spawn_Enemy_Pos_2();
            game_Manager.update_Souls(enemy_2.GetComponent<EnemyController>().soul_Stones);
        }

    }

    GameObject spawn_Enemy_Pos_1()
    {
        return Instantiate(Enemies[UnityEngine.Random.Range(0, Enemies.Length)], pos_One, Enemies[0].transform.rotation);
    }

    GameObject spawn_Enemy_Pos_2()
    {
        return Instantiate(Enemies[UnityEngine.Random.Range(0, Enemies.Length)], pos_Two, Enemies[0].transform.rotation);
    }

    GameObject spawn_Power_Up()
    {
        float x, y, z;
        x = UnityEngine.Random.Range(-8, 6);
        y = UnityEngine.Random.Range(6, .5f);
        z = UnityEngine.Random.Range(-2, 3);
        Vector3 spawn_Pos = new Vector3(x, y, z);
        StartCoroutine(power_Up_Timer());
        return Instantiate(power_Up, spawn_Pos, power_Up.transform.rotation);
    }

    IEnumerator power_Up_Timer() 
    {
        float seconds = UnityEngine.Random.Range(5f, 30f);
        yield return new WaitForSeconds(seconds);
        current_Power_Up = spawn_Power_Up();

    }

}

