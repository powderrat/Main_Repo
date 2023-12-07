using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
/*
 * Handles the farmers stats as well as any Input from the use I.E Attck/Pause
 */
public class PlayerController : MonoBehaviour, IAction
{
    public ParticleSystem player_Attack_Ps = new ParticleSystem();
    private GameManager gameManager;
    public float player_Damage;
    public float damage_Upgrade_Cost;
    public GameObject menu;
    public AudioClip[] sound_Effects;
    private AudioSource player_Sound;
    void Start()
    {
        player_Damage = .8f;
        damage_Upgrade_Cost = 15f;
        gameManager= GameObject.Find("Game Manager").GetComponent<GameManager>();
        menu = GameObject.Find("Menu");
        player_Sound = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }
    void Update()
    {
        shoot(false);
        pause();
    }
    public bool shoot(bool power_up)
    {
        if(gameManager.time > 0 && !menu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Instantiate(player_Attack_Ps);
                play_Effects();
                return true;
            }

            else if (Input.GetButtonDown("Fire1"))
            {
                Instantiate(player_Attack_Ps);
                play_Effects();
                return true;
            }

            if (power_up)
            {
                Instantiate(player_Attack_Ps);
                play_Effects();
                return true;
            }
        }
        return false;
    }
    public void upgrade_Damage()
    {//used by increase attack button
        if(gameManager.souls >= damage_Upgrade_Cost)
        {
            player_Damage++;
            gameManager.souls -= damage_Upgrade_Cost;
            damage_Upgrade_Cost = player_Damage * 12 + 25;
            gameManager.update_Souls(0);
            gameManager.update_Endstate();
        }
    }
    public void power_Up()
    {
        StartCoroutine(auto_Click());
    }
    IEnumerator auto_Click()
    {
        int amount_Of_Shots = 15;

        do
        {
            yield return new WaitForSeconds(.1f);
            shoot(true);
            amount_Of_Shots -= 1;

        }
        while (amount_Of_Shots > 0);

        
    }
    public void play_Game()
    {//used by Play button
        menu.SetActive(false);
        if (gameManager.timer_Text.text == "0")
        {
            StartCoroutine(gameManager.countdown());
        }
        Time.timeScale = 1;
    }
    public void pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0; 
            menu.SetActive(true);
        }
            
    }
    private void play_Effects()
    {
        player_Sound.PlayOneShot(sound_Effects[Random.Range(0, 5)]);
    }

}
