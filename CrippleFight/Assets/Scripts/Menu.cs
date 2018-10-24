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
    public static Animator LogoAnim;
    public GameObject EventSystem1;

    // Use this for initialization
    void Start () {
        LogoAnim = GameObject.Find("Logo").GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
        AnimPress();
        CancelPlayer1();
        CancelPlayer2();


    }
    public void AnimPress()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("A1"))
        {
            LogoAnim.enabled = true;

            Invoke("press", 1f);
        }
    }
    public void press()
    {
      
               
           Menu1.SetActive(false);
            Menu2.SetActive(true);
        
    }
    public void PlyvsPly()
    {
        Menu2.SetActive(false);
        Menu3.SetActive(true);
        EventSystem1.SetActive(false);

    }
    public void SelectPlayer0()
    {
         NumPlayer1 = 0;
            checkPlayer1 = true;
           
        }
        public void SelectPPlayer0()
        {
            NumPlayer2 = 0;
            checkPlayer2 = true;
           
        }
             

    
    public void SelectPlayer1()
    {
      
            NumPlayer1 = 1;
            checkPlayer1 = true;

    }
    public void SelectPPlayer1()
    { 
            NumPlayer2 = 1;
            checkPlayer2 = true;
          }
    public void SelectPlayer2()
    {
       
            NumPlayer1 = 2;
            checkPlayer1 = true;
          
        }
    public void SelectPPlayer2()
    {
            NumPlayer2 = 2;
            checkPlayer2 = true;
          
        }
    
    public void SelectPlayer3()
    {
        NumPlayer1 = 3;
            checkPlayer1 = true;
         
        }
    public void SelectPPlayer3()
    {
            NumPlayer2 = 3;
            checkPlayer2 = true;
          
        }
           
    
    public void Ready()
    {
        if (checkPlayer1 == true && checkPlayer2 == true)
        SceneManager.LoadScene("SampleScene");
    }
    public void CancelPlayer1()
    { if(Input.GetButton("B1"))
        checkPlayer1 = false;
   
    }
    public void CancelPlayer2()
    {
        if (Input.GetButton("B2"))
            checkPlayer2 = false;
    }
}
