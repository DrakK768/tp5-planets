using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] InputActionReference mouseClick;
    [SerializeField] InputActionReference mouseDelta;
    [SerializeField] InputActionReference mouseScrollWheel;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float scrollSpeed = 1.0f;
    Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        if (mouseClick.action.IsPressed())
        {
            Vector2 delta = mouseDelta.action.ReadValue<Vector2>();
            Vector3 angles = new Vector3(-delta.y, delta.x, 0) * rotationSpeed * Time.deltaTime;
            cam.transform.RotateAround(Vector3.zero, cam.transform.up, angles.y);
            cam.transform.RotateAround(Vector3.zero, cam.transform.right, angles.x);
        }

        Vector2 scroll = mouseScrollWheel.action.ReadValue<Vector2>();
        if (scroll != Vector2.zero)
        {
            cam.transform.Translate(new Vector3(0, 0, scroll.y * Time.deltaTime * scrollSpeed), cam.transform);
        }
    }
}
