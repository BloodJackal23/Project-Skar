using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [SerializeField] Transform heldWeapon;

	// Use this for initialization
	void Start () {
        heldWeapon = transform.Find("Held weapon");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
