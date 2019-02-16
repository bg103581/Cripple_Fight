using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarP2 : MonoBehaviour {
	public static Image healthBar;
	float MaxHealth=120f;
	public static float Health;
	public static float healthBarfill ;



	// Use this for initialization
	void Start () {
		healthBar = GetComponent<Image>();
		Health = MaxHealth;
		
	}
	
	// Update is called once per frame
	void Update () {
		healthBar.fillAmount = Health / MaxHealth;
		healthBarfill = healthBar.fillAmount;
	}

}
