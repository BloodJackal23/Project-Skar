using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour {

    public int damage = 10;
    Collider collider;
    TeamInfo parentTeamInfo;

    public float pre_ActivationTimer = .5f;
    public float activeTimer = .5f;

    float attackTime;
    [SerializeField] bool attacking;

    // Use this for initialization
    void Start () {
        parentTeamInfo = GetComponentInParent<TeamInfo>();
        collider = GetComponent<Collider>();
        collider.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Fire1"))
        {
            if(!attacking)
            {
                attacking = true;
                attackTime = Time.time + pre_ActivationTimer + activeTimer;
            }
        }

        if(Time.time > attackTime - activeTimer)
        {
            if(Time.time > attackTime)
            {
                collider.enabled = false;
                attacking = false;
            }
            else
            {
                collider.enabled = true;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        Vitals targetVitals;
        if (other.gameObject.layer == LayerMask.NameToLayer("Hitbox"))
        {
            if(other.gameObject.tag == "Character")
            {
                if(other.GetComponentInParent<TeamInfo>() != null && other.GetComponentInParent<TeamInfo>().teamIndex != parentTeamInfo.teamIndex)
                {
                    if (other.GetComponentInParent<Vitals>() != null)
                    {
                        targetVitals = other.GetComponentInParent<Vitals>();
                        targetVitals.currentHP -= damage;
                    }
                }
            }
        }
    }
}
