using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Confirm : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        StartCoroutine(Conf());

    }


    public IEnumerator Conf()
    {
        yield return new WaitForSeconds(1.5f);
        if (Input.GetButton("A1"))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }


  

}
