using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitals : MonoBehaviour {

    public int currentHP;
    public int maxHP = 100;
    public float timer = 1;
    public bool dead;
	// Use this for initialization
	void Start () {
        currentHP = maxHP;
	}
	
	// Update is called once per frame
	void Update () {
        DeathCheck();
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }

    void DeathCheck()
    {
        if(currentHP <= 0)
        {
            currentHP = 0;
            dead = true;
            StartCoroutine("deathTimer");
        }
    }

    IEnumerator deathTimer()
    {
        yield return new WaitForSeconds(timer);
       // if(GetComponentInChildren<AI_MovementPattern>() != null)
        {
        //    GetComponentInChildren<AI_MovementPattern>().DeleteCustomWaypoint();
        }
        Destroy(gameObject);
    }
}
