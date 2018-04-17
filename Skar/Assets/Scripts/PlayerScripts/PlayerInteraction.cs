using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkarLib;

public class PlayerInteraction : MonoBehaviour {

    // public List<GameObject> interactables = new List<GameObject>();
    public float interactionRadius = 1.5f;
    public Collider[] interactables;
    public LayerMask interctableLayers;
    public Interactable currentInteractable;
    int index;
	
	// Update is called once per frame
	void Update () {
        interactables = Physics.OverlapSphere(transform.position, interactionRadius, interctableLayers);
        AssignInteractable();
    }

    void AssignInteractable()
    {
        if(interactables.Length > 0)
        {
            if(index >= interactables.Length)
            {
                index = 0;
            }
            CycleThroughInteractables();
            currentInteractable = interactables[index].GetComponentInParent<Interactable>();
            Interact();
        }
        else
        {
            index = 0;
            currentInteractable = null;
        }
    }

    void CycleThroughInteractables()
    {
        if(Input.GetButtonDown("Change action"))
        {
            if(index >= interactables.Length - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }
    }

    void Interact()
    {
        if(Input.GetButtonDown("Interact"))
        {
            switch (currentInteractable.type)
            {
                case Interactions.interactableTypes.Container:
                    Interactions.ContainerAction(gameObject, currentInteractable);
                    break;

                case Interactions.interactableTypes.Dialogue:
                    break;

                case Interactions.interactableTypes.Door:
                    break;

                case Interactions.interactableTypes.Pickup:
                    break;

                case Interactions.interactableTypes.Switch:
                    Interactions.SwitchAction(currentInteractable);
                    break;

                case Interactions.interactableTypes.Light:
                    break;
            }
        }
    }
}
