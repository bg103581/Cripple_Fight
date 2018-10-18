using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    public GameObject GameOver;
    public GameObject Player1;
    public GameObject Player2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(HealthBarP1.healthBar.fillAmount == 0|| HealthBarP2.healthBar.fillAmount == 0)
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
    
}
