using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour {

    public GameObject Menu1, Menu2, Menu3, Menu4, MenuSettings;
    public static int NumPlayer1, NumPlayer2;
    public static bool checkPlayer1 = false;
    public static bool checkPlayer2 = false;
    public static Animator LogoAnim, ClinicAnim;
    public GameObject EventSystem1;
    public GameObject FedorP1, FedorP2, NatalyaP1, NatalyaP2, MarcusP1, MarcusP2;
    public GameObject confirmButton, panelAB;
    

    void Start () {
        LogoAnim = GameObject.Find("Logo").GetComponent<Animator>();
    }
	
	void Update () {
        GotItPress();
        GoToMenu3();
        CancelPlayer1();
        CancelPlayer2();
        Confirm();
    }

    // Animation du logo quand on click sur play
    public void GotItPress()
    {
        if (Menu1.activeInHierarchy) {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("A1")) {
                LogoAnim.enabled = true;
                Invoke("StartPress", 1f);
            }
        }
    }
    
    public void StartPress() {
        Menu1.SetActive(false);
        Menu2.SetActive(true);
    }

    public void GoToMenu3() {
        if (Menu2.activeInHierarchy) {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("A1")) {
                Invoke("press", 0.2f);
            }
        }
    }
    
    public void press()
    {
        Menu2.SetActive(false);
        Menu3.SetActive(true);
    }


    // SCENE NAVIGATION
    public void backMenu() {

        Menu1.SetActive(false);
        Menu2.SetActive(false);
        Menu3.SetActive(true);
        Menu4.SetActive(false);
        MenuSettings.SetActive(false);
        

    }

    public void PlayvsCom() {

        SceneManager.LoadScene("IA");
    }

    public void PlyvsPly()
    {
        Menu1.SetActive(false);
        Menu2.SetActive(false);
        Menu3.SetActive(false);
        Menu4.SetActive(true);
        EventSystem1.SetActive(false);
    }

    public void Settings() {

        Menu1.SetActive(false);
        Menu2.SetActive(false);
        Menu3.SetActive(false);
        Menu4.SetActive(false);
        MenuSettings.SetActive(true);

    }


    // PLAYER SELECTION
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

    // ENABLE IMAGES ON PLAYER SELECT

    public void FedorSelect1 () {
        FedorP1.SetActive(true);
        NatalyaP1.SetActive(false);
        MarcusP1.SetActive(false);
    }

    public void FedorSelect2() {
        FedorP2.SetActive(true);
        NatalyaP2.SetActive(false);
        MarcusP2.SetActive(false);
    }

    public void NatalyaSelect1() {
        FedorP1.SetActive(false);
        NatalyaP1.SetActive(true);
        MarcusP1.SetActive(false);
    }

    public void NatalyaSelect2() {
        FedorP2.SetActive(false);
        NatalyaP2.SetActive(true);
        MarcusP2.SetActive(false);
    }

    public void MarcusSelect1() {
        FedorP1.SetActive(false);
        NatalyaP1.SetActive(false);
        MarcusP1.SetActive(true);
    }

    public void MarcusSelect2() {
        FedorP2.SetActive(false);
        NatalyaP2.SetActive(false);
        MarcusP2.SetActive(true);
    }


    // READY OR CANCEL
    public void Confirm() {
        if (Menu4.activeInHierarchy) {
            if (checkPlayer1 == true && checkPlayer2 == true) {
                panelAB.SetActive(false);
                confirmButton.SetActive(true);
            } else {
                panelAB.SetActive(true);
                confirmButton.SetActive(false);
            }
        }
    }

    public void Ready()
    {
        if (checkPlayer1 == true && checkPlayer2 == true && confirmButton.activeInHierarchy) {
            SceneManager.LoadScene("SampleScene");
        }
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
