using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    private bool isColliding;
    private CameraRot camInstance;

    private delegate void OverrideCam(Vector3 camRot, Vector3 camPos, DialogueChoice scriptObj);
    private event OverrideCam OnOverrideCam;

    [SerializeField] private Transform camTransform;
    void Start()
    {
        isColliding = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        camInstance = other.gameObject.GetComponentInParent<CameraRot>();
        OnOverrideCam += camInstance.OverrideCam;
    }
    private void OnTriggerStay(Collider other)
    {
        isColliding = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isColliding = false;
    }

    public void OnKeyPress(InputAction.CallbackContext context)
    {
        if (context.performed & isColliding)
        {
            OnOverrideCam?.Invoke(camTransform.eulerAngles, camTransform.position, null);
            OnOverrideCam = null;
        }

    }
}
