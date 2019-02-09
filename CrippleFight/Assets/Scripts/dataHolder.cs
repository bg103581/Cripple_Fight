using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataHolder : MonoBehaviour {

    private static bool fromMenuButton;

    public static bool FromMenuButton {
        get {
            return fromMenuButton;
        }
        set {
            fromMenuButton = value;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
}
