using UnityEngine;
using UnityEngine.InputSystem;

//public class CameraRot : MonoBehaviour
//{
//    [SerializeField] private float angleCam;
//    [SerializeField] private float distanceLook;
//    [SerializeField] private GameObject target;


//}

public class CameraRot : MonoBehaviour
{
    private Vector2 cameraValues;
    private float cameraRot;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform charaPos;
    [SerializeField] private float camAngle;
    private float angleRad => Mathf.Deg2Rad * camAngle;
    [SerializeField] private float zoomMin;
    [SerializeField] private float zoomMax;
    [SerializeField] private float zoomSpeed;
    private float zoomCurrent;
    private float circleRad;
    [SerializeField] private float speedRot;
    private static float currentRot;
    public static float getRot { get { return currentRot; } }
    private Vector3 camPos;
    private Vector3 camRot;
    void Awake()
    {
        cam = Camera.main;
    }
    public void CameraActions(InputAction.CallbackContext context)
    {
        cameraValues = context.ReadValue<Vector2>();
        //Debug.Log(cameraValues.y);
        System.Math.Round(cameraValues.x, 2);
        System.Math.Round(cameraValues.y, 1);
    }

    private void Position()
    {
        zoomCurrent += -cameraValues.y * zoomSpeed * Time.deltaTime;
        zoomCurrent = Mathf.Clamp(zoomCurrent, zoomMin, zoomMax);
        circleRad = zoomCurrent * Mathf.Cos(angleRad);
        currentRot += cameraValues.x * speedRot * Time.deltaTime;

        currentRot %= Mathf.PI * 2;

        float camPosX = circleRad * Mathf.Cos(currentRot);
        float camPosZ = circleRad * Mathf.Sin(currentRot);
        camPos = new Vector3(camPosX + charaPos.position.x,
         Mathf.Abs(zoomCurrent * Mathf.Sin(angleRad)), camPosZ + charaPos.position.z);
    }

    private void Rotation()
    {
        camRot = new Vector3(camAngle, Mathf.Rad2Deg * (((Mathf.PI * 2 * 3) / 4) - currentRot), 0);
    }

    private void Update()
    {
        Position();
        cam.transform.position = camPos;
        Rotation();
        cam.transform.rotation = Quaternion.Euler(camRot);
    }
}
