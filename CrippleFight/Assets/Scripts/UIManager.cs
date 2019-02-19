using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public GameObject GameOver,Round;
    public GameObject Player1;
    public GameObject Player2;
    public float Count=10f;
    public int i, P1W,P2W,PN, CountInt,NumPartie;
    public static GameManager Gamemanager;
    public Text Timer;
    public GameObject [] players;
    public Text RoundT;
    public bool checknumber, EndAnim, p1Lose, p2Lose;
    public GameObject Fedor, Natalya, Marcus, doubleKO, winner;
    public GameObject FedorSkin, NatalyaSkin, MarcusSkin;

    public GameObject HUD1, HUD2, triangle, counter;


    // Use this for initialization
    void Start () {
        i = 1;
        checknumber = true;
        NumPartie = 1;
        Gamemanager = GameObject.FindObjectOfType<GameManager>();
        Count = 100f;
        EndAnim = false;
        p1Lose = p2Lose = false;
        HUD1.SetActive(true);
        HUD2.SetActive(true);
        triangle.SetActive(true);
        counter.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {

        players = GameObject.FindGameObjectsWithTag("Player");
        Player1 = players[0];
        Player2 = players[1];
        p1Lose = (HealthBarP1.healthBar.fillAmount == 0);
        p2Lose = (HealthBarP2.healthBar.fillAmount == 0);


        if (p1Lose || p2Lose)
        {
            Time.timeScale = 0;
            Player1.GetComponent<PlayerControl>().enabled = false;
            Player2.GetComponent<PlayerControl>().enabled = false;
            GameOver.SetActive(true);

            if (p1Lose) {
                if (Player2.name == "FedorP2(Clone)") {
                    Fedor.SetActive(true);
                    Natalya.SetActive(false);
                    Marcus.SetActive(false);

                    FedorSkin.SetActive(false);
                    NatalyaSkin.SetActive(false);
                    MarcusSkin.SetActive(false);
                }

                else if (Player2.name == "NataliaP2(Clone)") {
                    Fedor.SetActive(false);
                    Natalya.SetActive(true);
                    Marcus.SetActive(false);

                    FedorSkin.SetActive(false);
                    NatalyaSkin.SetActive(false);
                    MarcusSkin.SetActive(false);
                }

                else if (Player2.name == "MarcusP2(Clone)") {
                    Fedor.SetActive(false);
                    Natalya.SetActive(false);
                    Marcus.SetActive(true);

                    FedorSkin.SetActive(false);
                    NatalyaSkin.SetActive(false);
                    MarcusSkin.SetActive(false);
                }

                else if (Player2.name == "FedorP2skin(Clone)") {
                    Fedor.SetActive(false);
                    Natalya.SetActive(false);
                    Marcus.SetActive(false);

                    FedorSkin.SetActive(true);
                    NatalyaSkin.SetActive(false);
                    MarcusSkin.SetActive(false);
                } else if (Player2.name == "NataliaP2skin(Clone)") {
                    Fedor.SetActive(false);
                    Natalya.SetActive(false);
                    Marcus.SetActive(false);

                    FedorSkin.SetActive(false);
                    NatalyaSkin.SetActive(true);
                    MarcusSkin.SetActive(false);
                } else if (Player2.name == "MarcusP2skin(Clone)") {
                    Fedor.SetActive(false);
                    Natalya.SetActive(false);
                    Marcus.SetActive(false);

                    FedorSkin.SetActive(false);
                    NatalyaSkin.SetActive(false);
                    MarcusSkin.SetActive(true);
                }

                winner.SetActive(true);
                doubleKO.SetActive(false);
            }

            else if (p2Lose) {
                Debug.Log(Player1.name);
                if (Player1.name == "FedorP1(Clone)") {
                    Fedor.SetActive(true);
                    Natalya.SetActive(false);
                    Marcus.SetActive(false);

                    FedorSkin.SetActive(false);
                    NatalyaSkin.SetActive(false);
                    MarcusSkin.SetActive(false);
                } else if (Player1.name == "NataliaP1(Clone)") {
                    Fedor.SetActive(false);
                    Natalya.SetActive(true);
                    Marcus.SetActive(false);

                    FedorSkin.SetActive(false);
                    NatalyaSkin.SetActive(false);
                    MarcusSkin.SetActive(false);
                } else if (Player1.name == "MarcusP1(Clone)") {
                    Fedor.SetActive(false);
                    Natalya.SetActive(false);
                    Marcus.SetActive(true);

                    FedorSkin.SetActive(false);
                    NatalyaSkin.SetActive(false);
                    MarcusSkin.SetActive(false);
                } else if (Player1.name == "FedorP1skin(Clone)") {
                    Fedor.SetActive(false);
                    Natalya.SetActive(false);
                    Marcus.SetActive(false);

                    FedorSkin.SetActive(true);
                    NatalyaSkin.SetActive(false);
                    MarcusSkin.SetActive(false);
                } else if (Player1.name == "NataliaP1skin(Clone)") {
                    Fedor.SetActive(false);
                    Natalya.SetActive(false);
                    Marcus.SetActive(false);

                    FedorSkin.SetActive(false);
                    NatalyaSkin.SetActive(true);
                    MarcusSkin.SetActive(false);
                } else if (Player1.name == "MarcusP1skin(Clone)") {
                    Fedor.SetActive(false);
                    Natalya.SetActive(false);
                    Marcus.SetActive(false);

                    FedorSkin.SetActive(false);
                    NatalyaSkin.SetActive(false);
                    MarcusSkin.SetActive(true);
                }

                winner.SetActive(true);
                doubleKO.SetActive(false);
            }

            else if (p1Lose && p2Lose) {
                Fedor.SetActive(false);
                Natalya.SetActive(false);
                Marcus.SetActive(false);
                FedorSkin.SetActive(false);
                NatalyaSkin.SetActive(false);
                MarcusSkin.SetActive(false);
                doubleKO.SetActive(true);
                winner.SetActive(false);
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
        }
	}


   public void Replay()
    {
        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.name);
    }
    public void menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    /*
      public void CountTime()
     {

         Count -= Time.deltaTime;
         CountInt = (int)Count;
         Timer.text = CountInt.ToString();

         if (CountInt == 0)
         {
             i++;
             winnercheck();


             if (NumPartie == 1 && checknumber)
             {
                 Destroy(Player2);
                 Destroy(Player1);

                 //  StartCoroutine(Rounds());
                 NumPartie = 2;
                 RoundT.text = "Round"+i;
                 Round.SetActive(true);


             }


                 if (NumPartie == 2)
                 {

                 Count = 10;
                 StartCoroutine(Rounds());
                 NumPartie = 1;



             }






         }
             }
    public IEnumerator Rounds()
     {

         yield return new WaitForSeconds(5f);
         Round.SetActive(false);

         Count = 10;
         Count -= Time.deltaTime;
         Timer.text = CountInt.ToString();
         Gamemanager.Instantiates();
         EndAnim = true;





     }

     public void winnercheck()
     {
         if (HealthBarP1.healthBarfill > HealthBarP2.healthBarfill)
         {
             P1W += 1;
         }
         else if (HealthBarP1.healthBarfill < HealthBarP2.healthBarfill)
         {
             P2W += 1;
         }
         else
             PN += 1;

         if(i==3)
         { 
             if( P1W==2 && P1W>P2W || P1W > P2W && PN>1)
         {
            Debug.Log ("Player1win");
             checknumber = false;
         }
         else if ( P1W < P2W && P2W==2 || P1W < P2W && PN > 1)
         {
             Debug.Log("Player2win");
             checknumber = false;
         }
             else if ( PN > 1)
             {
                 Debug.Log("Round3");

             }
             else
         {
             Debug.Log("Round3");

         }
         }



         if (PN >= 1 && P1W > P2W)
         {
             Debug.Log("Player1win");
             checknumber = false;

         }
         else if (PN >= 1 && P1W < P2W)
         {
             Debug.Log("Player2win");
             checknumber = false;

         }
         if (PN >= 2 && P1W > P2W)
         {
             Debug.Log("Player1win");
             checknumber = false;

         }
         else if (PN >= 2 && P1W < P2W)
         {
             Debug.Log("Player2win");
             checknumber = false;

         }




         if (i == 4)
         {


             if (P1W > P2W)
             {
                 Debug.Log("Player1win");
                 checknumber = false;
             }
             else if (P1W < P2W)
             {
                 Debug.Log("Player2win");
                 checknumber = false;
             }
             else 
                  Debug.Log("egalité");
             checknumber = false;
         }

     }

    */








}
