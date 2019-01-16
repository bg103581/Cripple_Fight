using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public GameObject GameOver;
    public GameObject Player1;
    public GameObject Player2;
    public float Count=10f;
    int CountInt,NumPartie;
    public static GameManager Gamemanager;
      public Text Timer;
    public GameObject [] Players;
        


    // Use this for initialization
    void Start () {
        NumPartie = 1;
        Gamemanager = GameObject.FindObjectOfType<GameManager>();
        Count = 10f;
        
    }
	
	// Update is called once per frame
	void Update () {
        Players = GameObject.FindGameObjectsWithTag("Player");
        CountTime();
      
        if (HealthBarP1.healthBar.fillAmount == 0|| HealthBarP2.healthBar.fillAmount == 0)
        {
            Time.timeScale = 0;
            Player1.GetComponent<PlayerControl>().enabled = false;
            Player2.GetComponent<PlayerControl>().enabled = false;
            GameOver.SetActive(true);
            
        }
        else
        {
            Time.timeScale = 1;
            Player1.GetComponent<PlayerControl>().enabled = true;
            Player2.GetComponent<PlayerControl>().enabled = true;


        }
	}
   public void Replay()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void CountTime()
    {
      
        Count -= Time.deltaTime;
        CountInt = (int)Count;
        Timer.text = CountInt.ToString();
        if (CountInt == 0)
        {
            
            if (NumPartie == 1)
            {
                Destroy(Players[0]);
                Destroy(Players[1]);

                Debug.Log("round2");
                Gamemanager.Instantiates();
                NumPartie = 2;



            }







        }
    }


}
