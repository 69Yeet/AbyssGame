using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 movementDir;
    [SerializeField] private GameObject player;
    [SerializeField] private float movSpeed;
    [SerializeField] private float easeSpeed;
    void Update()
    {
        Vector3 futurPos = movementDir * Time.deltaTime * movSpeed + player.transform.position;
        player.transform.position = Vector3.Lerp(player.transform.position, futurPos, easeSpeed);
    }

    public void GetMovement(InputAction.CallbackContext context) 
    { 
        movementDir.x = context.ReadValue<Vector2>().x;
        movementDir.z = context.ReadValue<Vector2>().y;
    }
}
