using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
   /*
    * handles most of the GUI as well as multiple buttons
    */

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timer_Text;
    public TextMeshProUGUI souls_Text;
    public TextMeshProUGUI damage_Cost_Text;
    public TextMeshProUGUI time_Cost_Text;
    public GameObject end_State;
    private PlayerController playerController;
    public float souls; 
    public int time = 5;
    private int max_Time;
    public float time_Upgrade_Cost = 50;
    

    void Start()
    {
        max_Time = time;
        playerController= GameObject.Find("Player").GetComponent<PlayerController>();
    }


    void Update()
    {
        
    }
    public IEnumerator countdown()
    {
        do
        {
            yield return new WaitForSeconds(1);
            time -= 1;
            timer_Text.text = time.ToString();
        }
        while (time > 0);
        check_Endstate();
    }

    public void update_Souls(float f)
    {
        souls += f;
        souls_Text.text = "Soul Stones:\n"+ string.Format("{0:0.00}", souls);
    }

    public void check_Endstate()
    {//Times up began the feast
        if (time ==0)
        {
            end_State.SetActive(true);
            update_Endstate();
        }
    }

    public void restart_Game()
    {
        end_State.SetActive(false);
        time = max_Time;
        StartCoroutine(countdown());
    }
    
    public void increase_Time()
    {//used by increase time button
        if (souls > time_Upgrade_Cost)
        {
            max_Time += 2;
            souls -= time_Upgrade_Cost;
            update_Souls(0);
            time_Upgrade_Cost = max_Time * 2 + 110;
            update_Endstate();  
        }      
    }

    public void update_Endstate()
    {
        time_Cost_Text.text = "Ingest: " + string.Format("{0:0.00}", time_Upgrade_Cost.ToString()) + " Souls";
        damage_Cost_Text.text = "Ingest: " + string.Format("{0:0.00}", playerController.damage_Upgrade_Cost.ToString()) + " Souls";
    }
}
