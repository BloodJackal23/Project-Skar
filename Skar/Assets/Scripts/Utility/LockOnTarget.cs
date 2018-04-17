using UnityEngine;

public class LockOnTarget : MonoBehaviour {

    public Transform lockOnTarget;

	// Use this for initialization
	void Start () {
		if(transform.Find("Lock-on target") != null)
        {
            lockOnTarget = transform.Find("Lock-on target");
        }
        else
        {
            lockOnTarget = transform;
        }
	}
}
