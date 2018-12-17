using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperBarP2 : MonoBehaviour {

    public static Image superBar;
    float MaxSuper = 100f;
    public static float Super;
    public static float superBarfill;



    // Use this for initialization
    void Start() {
        superBar = GetComponent<Image>();
        Super = 0;

    }

    // Update is called once per frame
    void Update() {
        superBar.fillAmount = Super / MaxSuper;
        superBarfill = superBar.fillAmount;
    }
}
