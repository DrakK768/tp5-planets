using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera mainVCam;
    [SerializeField] CinemachineVirtualCamera planetVCam1;
    [SerializeField] InputActionReference mouseClick;
    [SerializeField] InputActionReference mouseDelta;
    [SerializeField] InputActionReference mouseScrollWheel;
    float rotationSpeed = 10f;
    float scrollSpeed = 1.0f;
    float slowDownFactor = 50f;
    bool isMouseClicked;
    bool isMoving;

    void Awake()
    {
        LevelData.cameraController = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mouseClick.action.started += (ctx) => isMouseClicked = true;
        // no need to set it back to false on release
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        if (isMouseClicked)
        {
            isMoving = !EventSystem.current.IsPointerOverGameObject();
            isMouseClicked = false;
        }
        if (mouseClick.action.IsPressed() && isMoving)
        {
            planetVCam1.Priority = 1;
            mainVCam.Priority = 10;
            Vector2 delta = mouseDelta.action.ReadValue<Vector2>();
            Vector3 angles = new Vector3(-delta.y, delta.x, 0) * rotationSpeed * Time.deltaTime;
            mainVCam.transform.RotateAround(Vector3.zero, mainVCam.transform.up, angles.y);
            mainVCam.transform.RotateAround(Vector3.zero, mainVCam.transform.right, angles.x);
        }

        Vector2 scroll = mouseScrollWheel.action.ReadValue<Vector2>();
        if (scroll != Vector2.zero)
        {
            float distanceToZero = mainVCam.transform.position.magnitude;
            if (distanceToZero > 0.01f || scroll.y < 0)
            {
                float ajustedSpeed = scrollSpeed * (distanceToZero / slowDownFactor);
                mainVCam.transform.Translate(new Vector3(0, 0, scroll.y * Time.deltaTime * ajustedSpeed), mainVCam.transform);
            }
        }
    }

    void OnDestroy()
    {
        LevelData.cameraController = null;
    }

    public void FocusOn(Planet planet)
    {
        planetVCam1.transform.SetParent(planet.transform);
        planetVCam1.transform.localRotation = mainVCam.transform.rotation;
        planetVCam1.transform.localPosition = new Vector3(0, 0, planet.GetComponent<SphereCollider>().radius);
        planetVCam1.Priority = 10;
        mainVCam.Priority = 1;
    }
}
