using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public GameObject[] Players1, Players2;
    public  GameObject PosPlayer1, PosPlayer2;
    public Menu myMenu;
    public GameObject fed;
    // Use this for initialization
    void Start () {

        //Instantiates();

        Menu.checkPlayer1 = false;
        Menu.checkPlayer2 = false;

        myMenu = GetComponent<Menu>();
    }
	
	// Update is called once per frame
	void Update () {
	}

   public void Instantiates()
    {
        Instantiate(Players1[Menu.NumPlayer1], PosPlayer1.transform.position, PosPlayer1.transform.rotation);
        Instantiate(Players2[Menu.NumPlayer2], PosPlayer2.transform.position, PosPlayer2.transform.rotation);
    }
    public void InstantiatesAi()
    {
        Instantiate(Players1[Menu.NumPlayer1], PosPlayer1.transform.position, PosPlayer1.transform.rotation);
        Instantiate(fed, PosPlayer2.transform.position, PosPlayer2.transform.rotation);
    }
    public void again() {
        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.name);
    }

    public void menu() {
        dataHolder.FromMenuButton = true;
        SceneManager.LoadScene("OfficialMenu");
    }

    public void quitter() {
        Application.Quit();
    }
}
