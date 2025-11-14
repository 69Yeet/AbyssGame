using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private Vector3 camRotBuf;
    private Vector3 camPosBuf;
    private bool isOverriden;
    private bool isConfessing;
    void Awake()
    {
        cam = Camera.main;
        isOverriden = false;
        isConfessing = false;
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
        if (isOverriden)
        {
            return;
        }
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
        if (isOverriden)
        {
            return;
        }
        camRot = new Vector3(camAngle, Mathf.Rad2Deg * (((Mathf.PI * 2 * 3) / 4) - currentRot), 0);
    }

    private void Update()
    {
        Position();
        cam.transform.position = camPos;
        Rotation();
        cam.transform.rotation = Quaternion.Euler(camRot);
    }

    public void OverrideCam(Vector3 overRot, Vector3 overPos)
    {
        StartCoroutine(TimedOverride(0.1f, overRot, overPos));
    }

    private IEnumerator TimedOverride(float timeSec, Vector3 overRot, Vector3 overPos)
    {
        isOverriden = true;

        yield return new WaitForSeconds(timeSec);

        camRotBuf = camRot;
        camPosBuf = camPos;

        camPos = overPos;
        camRot = overRot;
    }

    public IEnumerator ReturnCamControl(float timeSec)
    {
        yield return new WaitForSeconds(timeSec);
        camPos = camPosBuf;
        camRot = camRotBuf;
        isOverriden = false;
    }

    public void GoBack(InputAction.CallbackContext context)
    {
        if (isOverriden && !isConfessing && context.performed)
        {
            StartCoroutine(ReturnCamControl(0.01f));
        }
    }

    public void StartConfession(Vector3 rot, Vector3 pos)
    {
        StartCoroutine(StartConTime(0.1f));
    }

    public void StopConfession(int num, priestEvent param)
    {
        StartCoroutine(StopConTime(0.1f));
    }

    private IEnumerator StopConTime(float timeSec)
    {
        yield return new WaitForSeconds(timeSec);
        isConfessing = false;
        isOverriden = false;
    }
    private IEnumerator StartConTime(float timeSec)
    {
        yield return new WaitForSeconds(timeSec);
        isConfessing = true;
    }
}
