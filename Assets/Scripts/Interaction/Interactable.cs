using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool useEvents;

    [field: SerializeField]
    public string PromptMessage { get; set; }

    public void BaseInteract()
    {
        if (useEvents)
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        Interact();
    }

    protected virtual void Interact()
    {
    }
}
