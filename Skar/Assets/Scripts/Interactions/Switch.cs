using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkarLib;

public class Switch : MonoBehaviour {

    public bool on;
    //public List<Interactable> controlledObjects = new List<Interactable>();
    public Interactable[] controlledObjects;

    private void Start()
    {
        foreach (Interactable controlledObject in controlledObjects)
        {
            SwitchEffect(controlledObject);
        }
    }

    public void SwitchEffect(Interactable controlledObject)
    {
        switch (controlledObject.type)
        {
            case Interactions.interactableTypes.Container:
                controlledObject.GetComponentInChildren<ItemContainer>().locked = on;
                break;

            case Interactions.interactableTypes.Light:
                //Light objectLight = controlledObject.GetComponentInChildren<Light>();
                controlledObject.GetComponentInChildren<Light>().enabled = on;
                break;
        }
    }
}
