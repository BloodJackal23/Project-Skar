using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitesManager : MonoBehaviour {
    PlayerMovement movl;
	StatesManager st;
    Rigidbody rb;
    [Header("Dash")]
	public bool canDash = true;
	public float dashtime = .5f;
	public float dashspeed = 10;
	public bool isInDash = false;
	// Use this for initialization
	void Start () {
		movl = GetComponent<PlayerMovement>();
		st = GetComponent<StatesManager>();
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Dash"))
        {
			StartCoroutine(Dash());
		}
		if(isInDash)
        {
          rb.useGravity = false;
          rb.drag = movl.movementSettings.groundedDrag;
		}
		else
		{
            rb.useGravity = true;
		}
	}
	IEnumerator Dash(){
		Vector3 dir;
		if(movl.inputSettings.GetMoveDirection(movl.inputSettings.GetInput(), Camera.main.transform) != Vector3.zero)
        {
		    dir = movl.inputSettings.GetMoveDirection(movl.inputSettings.GetInput(), Camera.main.transform);
			movl.RotateToMoveDirection(dir,8.1f);
		}else
        {
			dir = transform.forward;
		}
        
		st.CanMove = false;
		st.moveState = StatesManager.MoveState.isInDash;
		isInDash = true;
        rb.AddForce(dir * dashspeed,ForceMode.VelocityChange);
		yield return new WaitForSeconds(dashtime);
		isInDash = false;
		st.CanMove = true;
	}
}
