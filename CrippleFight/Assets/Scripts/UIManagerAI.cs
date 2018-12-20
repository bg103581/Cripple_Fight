using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerAI : MonoBehaviour {
    public GameObject GameOver;
    public GameObject Player1;
    public GameObject Enemy;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(HealthBarP1.healthBar.fillAmount == 0|| HealthBarP2.healthBar.fillAmount == 0)
        {
            Time.timeScale = 0;
            Player1.GetComponent<PlayerControl>().enabled = false;
            Enemy.GetComponent<EnemyControler>().enabled = false;
            GameOver.SetActive(true);
            
        }
        else
        {
            Time.timeScale = 1;
            Player1.GetComponent<PlayerControl>().enabled = true;
            Enemy.GetComponent<EnemyControler>().enabled = true;


        }
	}
   public void Replay()
    {
        SceneManager.LoadScene("IA");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
}
