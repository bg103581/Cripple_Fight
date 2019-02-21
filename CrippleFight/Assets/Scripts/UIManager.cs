using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject TimerO, GameOver, Round;
    public GameObject Player1;
    public GameObject Player2;
    public float Count = 10f;
    public int i, P1W, P2W, PN, CountInt, NumPartie;
    public static GameManager Gamemanager;
    public Text Timer;
    public GameObject[] players, RoundNumber;
    public Text RoundT;
    public bool ppn, pp1Lose, pp2Lose, RoundN, checknumber, EndAnim, p1Lose, p2Lose;
    public GameObject Fedor, Natalya, Marcus, doubleKO, winner;
    public GameObject FedorSkin, NatalyaSkin, MarcusSkin;

    public GameObject HUD1, HUD2, triangle, counter;
    public GameObject r1w, r2w, r1w1, r2w1;
    
    public GameObject PauseMenuUI;


    // Use this for initialization
    void Start()
    {
        i = 0;
        Count = 0;
        RoundN = false;
        checknumber = true;
        NumPartie = 1;
        Gamemanager = GameObject.FindObjectOfType<GameManager>();
        Count = 0f;
        EndAnim = false;

        p1Lose = p2Lose = false;
        HUD1.SetActive(false);
        HUD2.SetActive(false);
        triangle.SetActive(false);
        counter.SetActive(false);

       ppn= p1Lose = p2Lose = false;

        r1w.SetActive(false);
        r2w.SetActive(false);
        r1w1.SetActive(false);
        r2w1.SetActive(false);

        PauseMenuUI.SetActive(false);
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuUI.activeInHierarchy) {
            CountTime();
        }
        
 
        players = GameObject.FindGameObjectsWithTag("Player");
        Player1 = players[0];
        Player2 = players[1];

        p1Lose = (HealthBarP1.Health <= 0);
        p2Lose = (HealthBarP2.Health <= 0);

        if (PauseMenuUI.activeInHierarchy) {

            Player1.GetComponent<PlayerControl>().enabled = false;
            Player2.GetComponent<PlayerControl>().enabled = false;
            
            if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Pause1") || Input.GetButtonDown("Pause2")) {
                Resume();
            }

        } else {
            Player1.GetComponent<PlayerControl>().enabled = true;
            Player2.GetComponent<PlayerControl>().enabled = true;

            if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Pause1") || Input.GetButtonDown("Pause2")) {
                if (!GameOver.activeInHierarchy) {
                    Pause();
                }
                
            }
        }

        if (pp1Lose || pp2Lose || ppn)
        {
            i = 0;
            Time.timeScale = 0;
            Player1.GetComponent<PlayerControl>().enabled = false;
            Player2.GetComponent<PlayerControl>().enabled = false;
            GameOver.SetActive(true);
            TimerO.SetActive(false);

            if (pp1Lose)
            {
                if (Player2.name == "FedorP2(Clone)")
                {
                    Fedor.SetActive(true);
                    Natalya.SetActive(false);
                    Marcus.SetActive(false);

                    FedorSkin.SetActive(false);
                    NatalyaSkin.SetActive(false);
                    MarcusSkin.SetActive(false);
                    ppn = pp1Lose = pp2Lose = false;
                }

                else if (Player2.name == "NataliaP2(Clone)")
                {
                    Fedor.SetActive(false);
                    Natalya.SetActive(true);
                    Marcus.SetActive(false);

                    FedorSkin.SetActive(false);
                    NatalyaSkin.SetActive(false);
                    MarcusSkin.SetActive(false);
                    ppn = pp1Lose = pp2Lose = false;
                }

                else if (Player2.name == "MarcusP2(Clone)")
                {
                    Fedor.SetActive(false);
                    Natalya.SetActive(false);
                    Marcus.SetActive(true);
                    ppn = pp1Lose = pp2Lose = false;
                    FedorSkin.SetActive(false);
                    NatalyaSkin.SetActive(false);
                    MarcusSkin.SetActive(false);
                }

                else if (Player2.name == "FedorP2skin(Clone)")
                {
                    Fedor.SetActive(false);
                    Natalya.SetActive(false);
                    Marcus.SetActive(false);
                    ppn = pp1Lose = pp2Lose = false;
                    FedorSkin.SetActive(true);
                    NatalyaSkin.SetActive(false);
                    MarcusSkin.SetActive(false);
                }
                else if (Player2.name == "NataliaP2skin(Clone)")
                {
                    Fedor.SetActive(false);
                    Natalya.SetActive(false);
                    Marcus.SetActive(false);
                    ppn = pp1Lose = pp2Lose = false;
                    FedorSkin.SetActive(false);
                    NatalyaSkin.SetActive(true);
                    MarcusSkin.SetActive(false);
                }
                else if (Player2.name == "MarcusP2skin(Clone)")
                {
                    Fedor.SetActive(false);
                    Natalya.SetActive(false);
                    Marcus.SetActive(false);
                    ppn = pp1Lose = pp2Lose = false;
                    FedorSkin.SetActive(false);
                    NatalyaSkin.SetActive(false);
                    MarcusSkin.SetActive(true);
                }

                winner.SetActive(true);
                doubleKO.SetActive(false);
            }

            else if (pp2Lose)
            {
                Debug.Log(Player1.name);
                if (Player1.name == "FedorP1(Clone)")
                {
                    Fedor.SetActive(true);
                    Natalya.SetActive(false);
                    Marcus.SetActive(false);
                    ppn = pp1Lose = pp2Lose = false;
                    FedorSkin.SetActive(false);
                    NatalyaSkin.SetActive(false);
                    MarcusSkin.SetActive(false);
                }
                else if (Player1.name == "NataliaP1(Clone)")
                {
                    Fedor.SetActive(false);
                    Natalya.SetActive(true);
                    Marcus.SetActive(false);
                    ppn = pp1Lose = pp2Lose = false;
                    FedorSkin.SetActive(false);
                    NatalyaSkin.SetActive(false);
                    MarcusSkin.SetActive(false);
                }
                else if (Player1.name == "MarcusP1(Clone)")
                {
                    Fedor.SetActive(false);
                    Natalya.SetActive(false);
                    Marcus.SetActive(true);
                    ppn = pp1Lose = pp2Lose = false;
                    FedorSkin.SetActive(false);
                    NatalyaSkin.SetActive(false);
                    MarcusSkin.SetActive(false);
                }
                else if (Player1.name == "FedorP1skin(Clone)")
                {
                    Fedor.SetActive(false);
                    Natalya.SetActive(false);
                    Marcus.SetActive(false);
                    ppn = pp1Lose = pp2Lose = false;
                    FedorSkin.SetActive(true);
                    NatalyaSkin.SetActive(false);
                    MarcusSkin.SetActive(false);
                }
                else if (Player1.name == "NataliaP1skin(Clone)")
                {
                    Fedor.SetActive(false);
                    Natalya.SetActive(false);
                    Marcus.SetActive(false);
                    ppn = pp1Lose = pp2Lose = false;
                    FedorSkin.SetActive(false);
                    NatalyaSkin.SetActive(true);
                    MarcusSkin.SetActive(false);
                }
                else if (Player1.name == "MarcusP1skin(Clone)")
                {
                    Fedor.SetActive(false);
                    Natalya.SetActive(false);
                    Marcus.SetActive(false);
                    ppn = pp1Lose = pp2Lose = false;
                    FedorSkin.SetActive(false);
                    NatalyaSkin.SetActive(false);
                    MarcusSkin.SetActive(true);
                }
            

                winner.SetActive(true);
                doubleKO.SetActive(false);
            }
            else if(ppn)
            {
                Fedor.SetActive(false);
                Natalya.SetActive(false);
                Marcus.SetActive(false);

                FedorSkin.SetActive(false);
                NatalyaSkin.SetActive(false);
                MarcusSkin.SetActive(false);
                ppn = pp1Lose = pp2Lose = false;
            }

           

            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>().mute = true;

        }

        else
        {
            Time.timeScale = 1;
            Player1.GetComponent<PlayerControl>().enabled = true;
            Player2.GetComponent<PlayerControl>().enabled = true;
        }

        if (GameOver.activeInHierarchy) {
            HUD1.SetActive(false);
            HUD2.SetActive(false);
            triangle.SetActive(false);
            counter.SetActive(false);

            Player1.GetComponent<PlayerControl>().enabled = false;
            Player2.GetComponent<PlayerControl>().enabled = false;
            
        }
	}
    
    public void Replay()
    {
        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.name);
    
    }
    public void menu()
    {
        dataHolder.FromMenuButton = true;
        SceneManager.LoadScene("OfficialMenu");
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

        if (CountInt == 0 || p2Lose || p1Lose)
        {


            winnercheck();


            if (checknumber)
            {
                Count = 20;
                HealthBarP1.Health = 120;
                HealthBarP2.Health = 120;
                RoundNumber[i].SetActive(true);

                StartCoroutine(Rounds());
                TimerO.SetActive(false);

                Destroy(Player2);
                Destroy(Player1);

                Debug.Log("cro");

            }
        }
    }
    public IEnumerator Rounds()
    {

        yield return new WaitForSeconds(4f);
        // Round.SetActive(false);
        RoundNumber[i].SetActive(false);
        i++;
        Count =100;
        TimerO.SetActive(true);

        Count -= Time.deltaTime;
        Timer.text = CountInt.ToString();
        HealthBarP1.Health = 120;
        HealthBarP2.Health = 120;
        Gamemanager.Instantiates();
        HUD1.SetActive(true);
        HUD2.SetActive(true);
        triangle.SetActive(true);
        counter.SetActive(true);

        EndAnim = true;
        RoundN = true;
        

    }

    public void winnercheck()
    {

        if ((Timer.text == "0" || p2Lose) && RoundN == true && (HealthBarP1.healthBarfill > HealthBarP2.healthBarfill))
        {
            P1W += 1;

            if(P1W == 1) {
                r1w.SetActive(true);
            } else if (P1W == 2) {
                r2w.SetActive(true);
            }

        }
        else if ((Timer.text == "0" || p1Lose) && RoundN == true && (HealthBarP1.healthBarfill < HealthBarP2.healthBarfill))
        {
            P2W += 1;

            if (P2W == 1) {
                r1w1.SetActive(true);
            } else if (P2W == 2) {
                r2w1.SetActive(true);
            }

        }
        else if ((Timer.text == "0") && RoundN == true && (HealthBarP1.healthBarfill == HealthBarP2.healthBarfill))
        {
            PN += 1;

        }

        if (i == 2)
        {
            if ((Timer.text == "0" || p1Lose || p2Lose) && RoundN == true && P1W == 2 && P1W > P2W || P1W > P2W && PN > 1)
            {
                Debug.Log("Player1win");
                checknumber = false;
                pp2Lose = true;
            }
            else if ((Timer.text == "0" || p1Lose || p2Lose) && RoundN == true && P1W < P2W && P2W == 2 || P1W < P2W && PN > 1)
            {
                Debug.Log("Player2win");
                checknumber = false;
                pp1Lose = true;
            }
            else if ((Timer.text == "0" || p1Lose || p2Lose) && RoundN == true && PN > 1)
            {
                Debug.Log("Round3");

            }

        }



        if ((Timer.text == "0" || p1Lose || p2Lose) && RoundN == true && PN >= 1 && P1W > P2W)
        {
            Debug.Log("Player1win");
            TimerO.SetActive(false);
            checknumber = false;
            pp2Lose = true;


        }
        else if ((Timer.text == "0" || p1Lose || p2Lose) && RoundN == true && PN >= 1 && P1W < P2W)
        {
            Debug.Log("Player2win");
            TimerO.SetActive(false);
            checknumber = false;
            pp1Lose = true;


        }
        if ((Timer.text == "0" || p1Lose || p2Lose) && RoundN == true && (PN >= 2 && P1W > P2W))
        {
            Debug.Log("Player1win");
            TimerO.SetActive(false);
            checknumber = false;
            pp2Lose = true;


        }
        else if ((Timer.text == "0" || p1Lose || p2Lose) && RoundN == true && PN >= 2 && P1W < P2W)
        {
            Debug.Log("Player2win");
            TimerO.SetActive(false);
            checknumber = false;
            pp1Lose = true;


        }




        if (i == 3)
        {


            if ((Timer.text == "0" || p1Lose || p2Lose) && RoundN == true && P1W > P2W)
            {
                Debug.Log("Player1win");
                TimerO.SetActive(false);
                checknumber = false;
                pp2Lose = true;


            }
            else if ((Timer.text == "0" || p1Lose || p2Lose) && RoundN == true && P1W < P2W)
            {
                Debug.Log("Player2win");
                TimerO.SetActive(false);
                checknumber = false;
                pp1Lose = true;


            }
            else
                Debug.Log("egalité");
            TimerO.SetActive(false);
            checknumber = false;
           ppn = true;
            

        }

    }


    // resume
    public void Resume() {
        PauseMenuUI.SetActive(false);
    }

    // pause
    public void Pause() {
        PauseMenuUI.SetActive(true);
    }
}
