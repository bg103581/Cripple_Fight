using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour {
    public GameObject Menu1, Menu2, Menu3;
   public static int NumPlayer1,NumPlayer2;
    public static bool checkPlayer1 = false;
    public static bool checkPlayer2 = false;
   
   
    // Use this for initialization
    void Start () {
    

    }
	
	// Update is called once per frame
	void Update () {
        press();
       
	}
    public void press()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("A1"))
        {
           Menu1.SetActive(false);
            Menu2.SetActive(true);
        }
    }
    public void PlyvsPly()
    {
        Menu2.SetActive(false);
        Menu3.SetActive(true);


    }
    public void SelectPlayer0()
    {
        if (checkPlayer1 == false)
        { NumPlayer1 = 0;
            checkPlayer1 = true;
           
        }
        else if (checkPlayer1 == true && checkPlayer2 == false)
        {
            NumPlayer2 = 0;
            checkPlayer2 = true;
           
        }
             

    }
    public void SelectPlayer1()
    {
        if (checkPlayer1 == false)
        {
            NumPlayer1 = 1;
            checkPlayer1 = true;
            
        }
        else if (checkPlayer1 == true && checkPlayer2 == false)
        {
            NumPlayer2 = 1;
            checkPlayer2 = true;
           
        }
    }
    public void SelectPlayer2()
    {
        if (checkPlayer1 == false)

        {
            NumPlayer1 = 2;
            checkPlayer1 = true;
          
        }
        else if (checkPlayer1 == true && checkPlayer2 == false)
        {
            NumPlayer2 = 2;
            checkPlayer2 = true;
          
        }
    }
    public void SelectPlayer3()
    {
        if (checkPlayer1 == false)
        { NumPlayer1 = 3;
            checkPlayer1 = true;
         
        }
         else if (checkPlayer1 == true && checkPlayer2 == false)
        {
            NumPlayer2 = 3;
            checkPlayer2 = true;
          
        }
           
    }
    public void Ready()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void CancelPlayer1()
    {
        checkPlayer1 = false;
    }
    public void CancelPlayer2()
    {
        checkPlayer2 = false;
    }
}
