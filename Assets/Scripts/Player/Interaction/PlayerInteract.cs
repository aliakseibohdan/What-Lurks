using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    private PlayerUI playerUI;
    private InputManager inputManager;

    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private LayerMask interactableMask;

    private void Start()
    {
        cam = Camera.main;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    private void Update()
    {
        playerUI.UpdateText(string.Empty);

        var ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * interactionDistance);
        RaycastHit hitInfo; 
        if (Physics.Raycast(ray, out hitInfo, interactionDistance, interactableMask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                var interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.PromptMessage);
                if (inputManager.playerActions.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
