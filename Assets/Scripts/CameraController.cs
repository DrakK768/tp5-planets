using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] InputActionReference mouseClick;
    [SerializeField] InputActionReference mouseDelta;
    [SerializeField] InputActionReference mouseScrollWheel;
    float rotationSpeed = 10f;
    float scrollSpeed = 1.0f;
    float slowDownFactor = 50f;
    Camera cam;
    bool isMouseClicked;
    bool isMoving;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
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
            Vector2 delta = mouseDelta.action.ReadValue<Vector2>();
            Vector3 angles = new Vector3(-delta.y, delta.x, 0) * rotationSpeed * Time.deltaTime;
            cam.transform.RotateAround(Vector3.zero, cam.transform.up, angles.y);
            cam.transform.RotateAround(Vector3.zero, cam.transform.right, angles.x);
        }

        Vector2 scroll = mouseScrollWheel.action.ReadValue<Vector2>();
        if (scroll != Vector2.zero)
        {
            float distanceToZero = cam.transform.position.magnitude;
            if (distanceToZero > 0.01f || scroll.y < 0)
            {
                float ajustedSpeed = scrollSpeed * (distanceToZero / slowDownFactor);
                cam.transform.Translate(new Vector3(0, 0, scroll.y * Time.deltaTime * ajustedSpeed), cam.transform);
            }
        }
    }
}
