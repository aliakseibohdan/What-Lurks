using SmallHedge.SoundManager;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 1f;
    private bool isGrounded;

    private const float gravity = -9.8f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = controller.isGrounded;
    }

    public void ProcessMove(Vector2 input)
    {
        var moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(speed * Time.deltaTime * transform.TransformDirection(moveDirection));
        if (moveDirection != Vector3.zero && isGrounded)
            SoundManager.PlaySound(SoundType.FOOTSTEP, volume: 0.2f, delayTime: 0.3f);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
        }
    }
}
