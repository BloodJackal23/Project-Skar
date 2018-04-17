using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateBehaviour : StateMachineBehaviour {
    public doAtTime[] doatTime;
    public UnityEvent stateEnter;
	public UnityEvent stateExit;
	public UnityEvent stateStay;

    // Use this for initialization
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		stateEnter.Invoke();
	}
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{   
		foreach(doAtTime action in doatTime){
			if(action.time > (stateInfo.normalizedTime)){
				Debug.Log("Done");
				action.DoThis.Invoke();
			}else{
				continue;
			}
		}
        stateStay.Invoke();
	}
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        stateExit.Invoke();
	}
	

}
[System.Serializable]
public class doAtTime{
	[SerializeField]
	public float time;
	[SerializeField]
	public UnityEvent DoThis;
}
