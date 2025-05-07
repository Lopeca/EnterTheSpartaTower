using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractHandler : MonoBehaviour
{
    public List<GameObject> interactableList;
    [SerializeField] IInteractable interactTarget;

    private void Update()
    {
        UpdateInteractTarget();
    }
    public void Interact()
    {
        interactTarget?.Interact();
    }
    public void UpdateInteractTarget()
    {
        if (interactableList.Count == 0)
        {
            interactTarget = null;
            return;
        }
        interactableList = interactableList.OrderBy(obj => Vector2.Distance(transform.position, obj.transform.position)).ToList();
        
        IInteractable target = interactableList[0].GetComponent<IInteractable>();
        target.IsInteracting = true;
        interactTarget = target;

        for (int i = 1; i < interactableList.Count; i++)
        {
            interactableList[i].GetComponent<IInteractable>().IsInteracting = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactableObj = collision.GetComponent<IInteractable>();
        
        if (interactableObj != null)
        {
            interactableList.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable interactableObj = collision.GetComponent<IInteractable>();
        if (interactableObj != null)
        {
            interactableObj.IsInteracting = false;
            interactableList.Remove(collision.gameObject);
        }
    }
}
