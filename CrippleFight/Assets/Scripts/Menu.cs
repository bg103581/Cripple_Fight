using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class Menu : MonoBehaviour {
    
    public GameObject Menu1, Menu2, Menu3, Menu4, Menu5, MenuSettings, Menu4IA, Menu5IA;
    public static int NumPlayer1, NumPlayer2;
    public static bool checkPlayer1 = false;
    public static bool checkPlayer2 = false;
    public static Animator LogoAnim, ClinicAnim;
    public GameObject EventSystem1, EventP1, EventP2;
    public GameObject FedorP1, FedorP2, NatalyaP1, NatalyaP2, MarcusP1, MarcusP2;
    public GameObject Button1, confirmButton, panelAB;
    public bool Checkconfirm=false;
    public GameObject ringNom, laboNom, panelABArena, confirmButtonArena;
    public GameObject colorFedor1, colorFedor2, colorNatalya1, colorNatalya2, colorMarcus1, colorMarcus2;
    public GameObject FedorP1Skin, FedorP2Skin, NatalyaP1Skin, NatalyaP2Skin, MarcusP1Skin, MarcusP2Skin;
    public Button Fedor1Pink, Fedor1Green, Fedor2Pink, Fedor2Green, Natalya1Yellow, Natalya1Orange, Natalya2Yellow, Natalya2Orange, Marcus2Yellow, Marcus2Orange, Marcus1Yellow, Marcus1Orange; 

    void Start () {
        LogoAnim = GameObject.Find("Logo").GetComponent<Animator>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	void Update () {
        GotItPress();
        GoToMenu3();
        CancelPlayer1();
        CancelPlayer2();
        Confirm();
        clickConfirm();
        cancelArena();
        confirmArena();
        
        if (dataHolder.FromMenuButton) {
            Menu4.SetActive(false);
            Menu1.SetActive(false);
            Menu2.SetActive(false);
            Menu3.SetActive(true);
            MenuSettings.SetActive(false);
            dataHolder.FromMenuButton = false;
        }
    }

    
    // Menu 1, 2 and 3 navigation
    public void GotItPress()
    {
        if (Menu1.activeInHierarchy) {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("A1") || Input.GetButtonDown("StickCross1")) {
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
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Start")) {
                Invoke("press", 0.2f);
            }
        }
    }
    
    public void press()
    {
        Menu2.SetActive(false);
        Menu3.SetActive(true);
        Menu4.SetActive(false);
        MenuSettings.SetActive(false);
        Menu1.SetActive(false);
        Menu5.SetActive(false);
    }
    

    // game choice (solo or vs)
    public void PlayvsCom() {
        Menu1.SetActive(false);
        Menu2.SetActive(false);
        Menu3.SetActive(false);
        Menu4.SetActive(false);
        Menu5.SetActive(false);
        MenuSettings.SetActive(false);

        Menu4IA.SetActive(true);
        Menu5IA.SetActive(false);
    }

    public void PlyvsPly()
    {
        Menu1.SetActive(false);
        Menu2.SetActive(false);
        Menu3.SetActive(false);
        Menu4.SetActive(true);
        Menu5.SetActive(false);
        MenuSettings.SetActive(false);

        Menu4IA.SetActive(false);
        Menu5IA.SetActive(false);
    }


    // settings
    public void Settings() {

        Menu1.SetActive(false);
        Menu2.SetActive(false);
        Menu3.SetActive(false);
        Menu4.SetActive(false);
        Menu5.SetActive(false);
        MenuSettings.SetActive(true);

        Menu4IA.SetActive(false);
        Menu5IA.SetActive(false);

    }


    // PLAYER SELECTION

    // FEDOR
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

    public void SelectPlayer3() {
        NumPlayer1 = 3;
        checkPlayer1 = true;

    }

    public void SelectPPlayer3() {
        NumPlayer2 = 3;
        checkPlayer2 = true;

    }

    // NATALYA
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

    public void SelectPlayer4() {
        NumPlayer1 = 4;
        checkPlayer1 = true;

    }

    public void SelectPPlayer4() {
        NumPlayer2 = 4;
        checkPlayer2 = true;

    }


    // MARCUS
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

    public void SelectPlayer5() {
        NumPlayer1 = 5;
        checkPlayer1 = true;

    }

    public void SelectPPlayer5() {
        NumPlayer2 = 5;
        checkPlayer2 = true;

    }


    // ENABLE COLOR SELECTION
    public void Fedor1Skin () {
        colorFedor1.SetActive(true);
        colorNatalya1.SetActive(false);
        colorMarcus1.SetActive(false);

        NatalyaP1.SetActive(false);
        NatalyaP1Skin.SetActive(false);
        MarcusP1.SetActive(false);
        MarcusP1Skin.SetActive(false);

        EventP1.SetActive(false);
    }

    public void Fedor2Skin() {
        colorFedor2.SetActive(true);
        colorNatalya2.SetActive(false);
        colorMarcus2.SetActive(false);

        NatalyaP2.SetActive(false);
        NatalyaP2Skin.SetActive(false);
        MarcusP2.SetActive(false);
        MarcusP2Skin.SetActive(false);

        EventP2.SetActive(false);
    }

    public void Natalya1Skin() {
        colorFedor1.SetActive(false);
        colorNatalya1.SetActive(true);
        colorMarcus1.SetActive(false);

        FedorP1.SetActive(false);
        FedorP1Skin.SetActive(false);
        MarcusP1.SetActive(false);
        MarcusP1Skin.SetActive(false);

        EventP1.SetActive(false);
    }

    public void Natalya2Skin() {
        colorFedor2.SetActive(false);
        colorNatalya2.SetActive(true);
        colorMarcus2.SetActive(false);

        FedorP2.SetActive(false);
        FedorP2Skin.SetActive(false);
        MarcusP2.SetActive(false);
        MarcusP2Skin.SetActive(false);

        EventP2.SetActive(false);
    }

    public void Marcus1Skin() {
        colorFedor1.SetActive(false);
        colorNatalya1.SetActive(false);
        colorMarcus1.SetActive(true);

        FedorP1.SetActive(false);
        FedorP1Skin.SetActive(false);
        NatalyaP1.SetActive(false);
        NatalyaP1Skin.SetActive(false);
    }

    public void Marcus2Skin() {
        colorFedor2.SetActive(false);
        colorNatalya2.SetActive(false);
        colorMarcus2.SetActive(true);

        FedorP2.SetActive(false);
        FedorP2Skin.SetActive(false);
        NatalyaP2.SetActive(false);
        NatalyaP2Skin.SetActive(false);
    }

    // ENABLE IMAGES ON PLAYER SELECT
    public void FedorSelect1 () {
        FedorP1.SetActive(true);
        NatalyaP1.SetActive(false);
        MarcusP1.SetActive(false);

        FedorP1Skin.SetActive(false);

        Fedor2Pink.interactable = false;
        Fedor2Green.interactable = true;
        
        Natalya2Yellow.interactable = true;
        Natalya2Orange.interactable = true;
        
        Marcus2Yellow.interactable = true;
        Marcus2Orange.interactable = true;
    }

    public void FedorSelect2() {
        FedorP2.SetActive(true);
        NatalyaP2.SetActive(false);
        MarcusP2.SetActive(false);

        FedorP2Skin.SetActive(false);

        Fedor1Pink.interactable = false;
        Fedor1Green.interactable = true;

        Natalya1Yellow.interactable = true;
        Natalya1Orange.interactable = true;

        Marcus1Yellow.interactable = true;
        Marcus1Orange.interactable = true;
    }

    public void FedorSkinSelect1() {
        FedorP1Skin.SetActive(true);
        NatalyaP1Skin.SetActive(false);
        MarcusP1Skin.SetActive(false);

        FedorP1.SetActive(false);

        Fedor2Pink.interactable = true;
        Fedor2Green.interactable = false;
        
        Natalya2Yellow.interactable = true;
        Natalya2Orange.interactable = true;
        
        Marcus2Yellow.interactable = true;
        Marcus2Orange.interactable = true;
    }

    public void FedorSkinSelect2() {
        FedorP2Skin.SetActive(true);
        NatalyaP2Skin.SetActive(false);
        MarcusP2Skin.SetActive(false);

        FedorP2.SetActive(false);

        Fedor1Pink.interactable = true;
        Fedor1Green.interactable = false;

        Natalya1Yellow.interactable = true;
        Natalya1Orange.interactable = true;

        Marcus1Yellow.interactable = true;
        Marcus1Orange.interactable = true;
    }



    public void NatalyaSelect1() {
        FedorP1.SetActive(false);
        NatalyaP1.SetActive(true);
        MarcusP1.SetActive(false);

        NatalyaP1Skin.SetActive(false);

        Natalya2Yellow.interactable = false;
        Natalya2Orange.interactable = true;
        
        Fedor2Pink.interactable = true;
        Fedor2Green.interactable = true;
        
        Marcus2Yellow.interactable = true;
        Marcus2Orange.interactable = true;
    }

    public void NatalyaSelect2() {
        FedorP2.SetActive(false);
        NatalyaP2.SetActive(true);
        MarcusP2.SetActive(false);

        NatalyaP2Skin.SetActive(false);

        Natalya1Yellow.interactable = false;
        Natalya1Orange.interactable = true;

        Fedor1Pink.interactable = true;
        Fedor1Green.interactable = true;

        Marcus1Yellow.interactable = true;
        Marcus1Orange.interactable = true;
    }

    public void NatalyaSkinSelect1() {
        FedorP1Skin.SetActive(false);
        NatalyaP1Skin.SetActive(true);
        MarcusP1Skin.SetActive(false);

        NatalyaP1.SetActive(false);

        Natalya2Yellow.interactable = true;
        Natalya2Orange.interactable = false;
        
        Fedor2Pink.interactable = true;
        Fedor2Green.interactable = true;
        
        Marcus2Yellow.interactable = true;
        Marcus2Orange.interactable = true;
    }

    public void NatalyaSkinSelect2() {
        FedorP2Skin.SetActive(false);
        NatalyaP2Skin.SetActive(true);
        MarcusP2Skin.SetActive(false);

        NatalyaP2.SetActive(false);

        Natalya1Yellow.interactable = true;
        Natalya1Orange.interactable = false;

        Fedor1Pink.interactable = true;
        Fedor1Green.interactable = true;

        Marcus1Yellow.interactable = true;
        Marcus1Orange.interactable = true;
    }



    public void MarcusSelect1() {
        FedorP1.SetActive(false);
        NatalyaP1.SetActive(false);
        MarcusP1.SetActive(true);

        MarcusP1Skin.SetActive(false);

        Marcus2Yellow.interactable = false;
        Marcus2Orange.interactable = true;

        Fedor1Pink.interactable = true;
        Fedor1Green.interactable = true;

        Natalya1Yellow.interactable = true;
        Natalya1Orange.interactable = true;
    }

    public void MarcusSelect2() {
        FedorP2.SetActive(false);
        NatalyaP2.SetActive(false);
        MarcusP2.SetActive(true);

        MarcusP2Skin.SetActive(false);

        Marcus1Yellow.interactable = false;
        Marcus1Orange.interactable = true;
        
        Fedor2Pink.interactable = true;
        Fedor2Green.interactable = true;
        
        Natalya2Yellow.interactable = true;
        Natalya2Orange.interactable = true;
    }

    public void MarcusSkinSelect1() {
        FedorP1Skin.SetActive(false);
        NatalyaP1Skin.SetActive(false);
        MarcusP1Skin.SetActive(true);

        MarcusP1.SetActive(false);

        Marcus2Yellow.interactable = true;
        Marcus2Orange.interactable = false;
        
        Fedor2Pink.interactable = true;
        Fedor2Green.interactable = true;
        
        Natalya2Yellow.interactable = true;
        Natalya2Orange.interactable = true;
    }

    public void MarcusSkinSelect2() {
        FedorP2Skin.SetActive(false);
        NatalyaP2Skin.SetActive(false);
        MarcusP2Skin.SetActive(true);

        MarcusP2.SetActive(false);

        Marcus1Yellow.interactable = true;
        Marcus1Orange.interactable = false;

        Fedor1Pink.interactable = true;
        Fedor1Green.interactable = true;

        Natalya1Yellow.interactable = true;
        Natalya1Orange.interactable = true;
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
    
    public void CancelPlayer1()
    {
        if (Input.GetButton("B1") || Input.GetKeyDown(KeyCode.Backspace))
        {
            checkPlayer1 = false;
            confirmButton.SetActive(false);
            FedorP1.SetActive(false);
            NatalyaP1.SetActive(false);

            EventP1.SetActive(true);

            colorFedor1.SetActive(false);
            colorNatalya1.SetActive(false);

            FedorP1.SetActive(false);
            NatalyaP1.SetActive(false);

            FedorP1Skin.SetActive(false);
            NatalyaP1Skin.SetActive(false);

            if (NumPlayer1 == 0 || NumPlayer1 == 3) {
                Fedor2Pink.interactable = true;
                Fedor2Green.interactable = true;
            } else {
                Natalya2Yellow.interactable = true;
                Natalya2Orange.interactable = true;
            }
        }
    }

    public void CancelPlayer2()
    {
        if (Input.GetButton("B2") || Input.GetKeyDown(KeyCode.Backspace))
        {
            checkPlayer2 = false;
            confirmButton.SetActive(false);
            FedorP2.SetActive(false);
            NatalyaP2.SetActive(false);

            EventP2.SetActive(true);

            colorFedor2.SetActive(false);
            colorNatalya2.SetActive(false);

            FedorP2.SetActive(false);
            NatalyaP2.SetActive(false);
            
            FedorP2Skin.SetActive(false);
            NatalyaP2Skin.SetActive(false);

            if (NumPlayer2 == 0 || NumPlayer2 == 3) {
                Fedor1Pink.interactable = true;
                Fedor1Green.interactable = true;
            } else {
                Natalya1Yellow.interactable = true;
                Natalya1Orange.interactable = true;
            }

            
        }
    }

    /*public IEnumerator Versus()
    {
        yield return new WaitForSeconds(0.1f);
        Menu4.SetActive(true);

    }*/


    // confirm player
    public void clickConfirm() {
        if (Menu4.activeInHierarchy) {
            if (confirmButton.activeInHierarchy) {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Start")) {
                    Menu1.SetActive(false);
                    Menu2.SetActive(false);
                    Menu3.SetActive(false);
                    Menu4.SetActive(false);
                    Menu5.SetActive(true);
                    MenuSettings.SetActive(false);

                    
                }
            }
        }
    }


    // select arena
    public void selectRing() {
        ringNom.SetActive(true);
        laboNom.SetActive(false);
        confirmButtonArena.SetActive(true);
        panelABArena.SetActive(false);
    }

    public void selectLabo() {
        laboNom.SetActive(true);
        ringNom.SetActive(false);
        confirmButtonArena.SetActive(true);
        panelABArena.SetActive(false);
    }

    // confirm arena
    public void confirmArena() {
        if (confirmButtonArena.activeInHierarchy) {
            if (ringNom.activeInHierarchy && (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Start"))) {
                SceneManager.LoadScene("RocheScene");
            }

            else if (laboNom.activeInHierarchy && (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Start"))) {
                SceneManager.LoadScene("SceneLabo");
            }
        }
    }

    // cancel arena
    public void cancelArena() {
        if (Menu5.activeInHierarchy) {
            if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetButtonDown("B1")) {
                ringNom.SetActive(false);
                laboNom.SetActive(false);
                confirmButtonArena.SetActive(false);
                panelABArena.SetActive(true);
            }
        }
    }

    // quit game
    public void quitMenu() {
        Application.Quit();
    }

    
}
