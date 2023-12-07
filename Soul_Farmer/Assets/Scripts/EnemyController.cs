using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/*
 *  This Controls the Text and Slider for the Animal's health and the death parictle effect
 */
public class EnemyController : MonoBehaviour
{
    public float max_HP;
    public float HP;
    public bool is_Active;
    public float soul_Stones;
    public ParticleSystem death_Particle = new ParticleSystem();
    private PlayerController player_Controller;
    private Slider HP_Bar;
    private TextMeshProUGUI HP_Text;
    
    
    void Start()
    {
        HP_Text = GameObject.Find("Current Enemy Health").GetComponentInChildren<TextMeshProUGUI>();
        HP_Bar = FindAnyObjectByType<Slider>();
        player_Controller = GameObject.Find("Player").GetComponent<PlayerController>();
        HP = max_HP;  
    }

    void Update()
    {   //is_Active referes to the animal up front being harvested / check SpawnManager
        if(is_Active)
        {
            HP_Bar.maxValue = max_HP;
            HP_Bar.value = HP;
            HP_Text.text = "HP:" + HP + "/" + max_HP;
            string.Format("{0:0.00}", HP_Text.text);
            is_Active = false;
        }
    }
    public void update_HP()
    {
        HP_Bar.maxValue = max_HP;
        HP -= player_Controller.player_Damage;
        
        HP_Bar.value = HP;
        
        HP_Text.text = "HP:" + HP.ToString("F1") + "/" + max_HP;
        
        if (HP <= 0)
        {
            Instantiate(death_Particle, transform.position, transform.rotation);
            Destroy(gameObject);
        }  
    }
}
