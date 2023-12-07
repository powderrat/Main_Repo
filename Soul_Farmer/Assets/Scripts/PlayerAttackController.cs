using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//  Attack hits enemy then lowers its HP
public class PlayerAttackController : MonoBehaviour
{
    private EnemyController enemy_controller;
    
    void Start()
    {
        enemy_controller = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
    }
    private void OnParticleCollision(GameObject other)
    {
        if(enemy_controller.HP > 0)
        {
            enemy_controller.update_HP();
        }
    }
}
