using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuIA : MonoBehaviour {

    public GameObject Menu3, Menu4, Menu5;

    public GameObject FedorP1, FedorP2, NatalyaP1, NatalyaP2;
    public GameObject FedorP1Skin, FedorP2Skin, NatalyaP1Skin, NatalyaP2Skin;
    public GameObject colorFedor1, colorFedor2, colorNatalya1, colorNatalya2;
    public Button Fedor1Pink, Fedor1Green, Fedor2Pink, Fedor2Green, Natalya1Yellow, Natalya1Orange, Natalya2Yellow, Natalya2Orange;

    public static int NumPlayer1, NumPlayer2;
    public static bool checkPlayer1 = false;
    public static bool checkPlayer2 = false;

    public GameObject ringNom, laboNom, panelABArena, confirmButtonArena;
    public GameObject confirmButton, panelAB;

    public GameObject EventP1, EventP2;

    void Update() {
        CancelPlayer1();
        CancelPlayer2();
        Confirm();
        clickConfirm();
        cancelArenaIA();
        confirmArenaIA();

        if (dataHolder.FromMenuButton) {
            Menu4.SetActive(false);
            Menu5.SetActive(false);
            Menu3.SetActive(true);
            dataHolder.FromMenuButton = false;
        }
    }

    // PLAYER SELECTION

    // FEDOR
    public void SelectFedor1() {
        NumPlayer1 = 0;
        checkPlayer1 = true;

    }

    public void SelectFedor2() {
        NumPlayer1 = 3;
        checkPlayer1 = true;

    }

    public void SelectFFedor1() {
        NumPlayer2 = 0;
        checkPlayer2 = true;

    }

    public void SelectFFedor2() {
        NumPlayer2 = 3;
        checkPlayer2 = true;

    }

    
    // NATALYA
    public void SelectNatalya1() {

        NumPlayer1 = 1;
        checkPlayer1 = true;

    }

    public void SelectNatalya2() {
        NumPlayer1 = 4;
        checkPlayer1 = true;
    }

    public void SelectNNatalya1() {
        NumPlayer2 = 1;
        checkPlayer2 = true;
    }

    public void SelectNNatalya2() {
        NumPlayer2 = 4;
        checkPlayer2 = true;
    }

    // ENABLE COLOR SELECTION
    public void Fedor1Skin() {
        colorFedor1.SetActive(true);
        colorNatalya1.SetActive(false);

        NatalyaP1.SetActive(false);
        NatalyaP1Skin.SetActive(false);

        EventP1.SetActive(false);
    }

    public void Fedor2Skin() {
        colorFedor2.SetActive(true);
        colorNatalya2.SetActive(false);

        NatalyaP2.SetActive(false);
        NatalyaP2Skin.SetActive(false);

        EventP2.SetActive(false);
    }

    public void Natalya1Skin() {
        colorFedor1.SetActive(false);
        colorNatalya1.SetActive(true);

        FedorP1.SetActive(false);
        FedorP1Skin.SetActive(false);

        EventP1.SetActive(false);
    }

    public void Natalya2Skin() {
        colorFedor2.SetActive(false);
        colorNatalya2.SetActive(true);

        FedorP2.SetActive(false);
        FedorP2Skin.SetActive(false);

        EventP2.SetActive(false);
    }

    // ENABLE IMAGES ON PLAYER SELECT
    public void FedorSelect1() {
        FedorP1.SetActive(true);
        NatalyaP1.SetActive(false);

        FedorP1Skin.SetActive(false);

        Fedor2Pink.interactable = false;
        Fedor2Green.interactable = true;

        Natalya2Yellow.interactable = true;
        Natalya2Orange.interactable = true;
    }

    public void FedorSelect2() {
        FedorP2.SetActive(true);
        NatalyaP2.SetActive(false);

        FedorP2Skin.SetActive(false);

        Fedor1Pink.interactable = false;
        Fedor1Green.interactable = true;

        Natalya1Yellow.interactable = true;
        Natalya1Orange.interactable = true;
    }

    public void FedorSkinSelect1() {
        FedorP1Skin.SetActive(true);
        NatalyaP1Skin.SetActive(false);

        FedorP1.SetActive(false);

        Fedor2Pink.interactable = true;
        Fedor2Green.interactable = false;

        Natalya2Yellow.interactable = true;
        Natalya2Orange.interactable = true;
    }

    public void FedorSkinSelect2() {
        FedorP2Skin.SetActive(true);
        NatalyaP2Skin.SetActive(false);

        FedorP2.SetActive(false);

        Fedor1Pink.interactable = true;
        Fedor1Green.interactable = false;

        Natalya1Yellow.interactable = true;
        Natalya1Orange.interactable = true;
    }



    public void NatalyaSelect1() {
        FedorP1.SetActive(false);
        NatalyaP1.SetActive(true);

        NatalyaP1Skin.SetActive(false);

        Natalya2Yellow.interactable = false;
        Natalya2Orange.interactable = true;

        Fedor2Pink.interactable = true;
        Fedor2Green.interactable = true;
    }

    public void NatalyaSelect2() {
        FedorP2.SetActive(false);
        NatalyaP2.SetActive(true);

        NatalyaP2Skin.SetActive(false);

        Natalya1Yellow.interactable = false;
        Natalya1Orange.interactable = true;

        Fedor1Pink.interactable = true;
        Fedor1Green.interactable = true;
    }

    public void NatalyaSkinSelect1() {
        FedorP1Skin.SetActive(false);
        NatalyaP1Skin.SetActive(true);

        NatalyaP1.SetActive(false);

        Natalya2Yellow.interactable = true;
        Natalya2Orange.interactable = false;

        Fedor2Pink.interactable = true;
        Fedor2Green.interactable = true;
    }

    public void NatalyaSkinSelect2() {
        FedorP2Skin.SetActive(false);
        NatalyaP2Skin.SetActive(true);

        NatalyaP2.SetActive(false);

        Natalya1Yellow.interactable = true;
        Natalya1Orange.interactable = false;

        Fedor1Pink.interactable = true;
        Fedor1Green.interactable = true;
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

    public void CancelPlayer1() {
        if (Input.GetButton("B1") || Input.GetKeyDown(KeyCode.Backspace)) {
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

    public void CancelPlayer2() {
        if (Input.GetButton("B2") || Input.GetKeyDown(KeyCode.Backspace)) {
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


    // confirm player
    public void clickConfirm() {
        if (Menu4.activeInHierarchy) {
            if (confirmButton.activeInHierarchy) {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Start")) {
                    Menu3.SetActive(false);
                    Menu4.SetActive(false);
                    Menu5.SetActive(true);

                }
            }
        }
    }



    // select arena
    public void selectRingIA() {
        ringNom.SetActive(true);
        laboNom.SetActive(false);
        confirmButtonArena.SetActive(true);
        panelABArena.SetActive(false);
    }

    public void selectLaboIA() {
        laboNom.SetActive(true);
        ringNom.SetActive(false);
        confirmButtonArena.SetActive(true);
        panelABArena.SetActive(false);
    }




    // confirm arena
    public void confirmArenaIA() {
        if (confirmButtonArena.activeInHierarchy) {
            if (ringNom.activeInHierarchy && (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Start"))) {
                SceneManager.LoadScene("IAring");
            } else if (laboNom.activeInHierarchy && (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Start"))) {
                SceneManager.LoadScene("IAlabo");
            }
        }
    }

    // cancel arena
    public void cancelArenaIA() {
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
