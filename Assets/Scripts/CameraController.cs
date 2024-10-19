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
    [SerializeField] CinemachineVirtualCamera planetVCam2;
    [SerializeField] InputActionReference mouseClick;
    [SerializeField] InputActionReference mouseDelta;
    [SerializeField] InputActionReference mouseScrollWheel;
    [SerializeField] CelestialInfoView celestialInfoView;
    CinemachineVirtualCamera currVCam;
    Interactable currCelestialInFocus = null;
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
        currVCam = mainVCam;
        mouseClick.action.started += (ctx) => isMouseClicked = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        // Mouse Click handler is late to avoid warning (bc UI elements are not yet processed when mouseClick action triggers)
        if (isMouseClicked)
        {
            isMoving = !EventSystem.current.IsPointerOverGameObject(); // Prevents moving when interacting with UI
            isMouseClicked = false;
        }

        Vector3 currVCamCenterPoint = (currCelestialInFocus == null ? Vector3.zero : currCelestialInFocus.transform.position);

        // Cam mouvement around a planet/sun
        if (mouseClick.action.IsPressed() && isMoving)
        {
            Vector2 delta = mouseDelta.action.ReadValue<Vector2>();
            Vector3 angles = new Vector3(-delta.y, delta.x, 0) * rotationSpeed * Time.deltaTime;
            currVCam.transform.RotateAround(currVCamCenterPoint, currVCam.transform.up, angles.y);
            currVCam.transform.RotateAround(currVCamCenterPoint, currVCam.transform.right, angles.x);
        }

        // Zoom handling (non linear, scaling with distance to sun)
        Vector2 scroll = mouseScrollWheel.action.ReadValue<Vector2>();
        if (scroll != Vector2.zero)
        {
            float distanceToCenterPoint = (currVCam.transform.position - currVCamCenterPoint).magnitude;
            if (distanceToCenterPoint > 0.01f || scroll.y < 0) // Doesn't scroll farther if distance < 0.01
            {
                float ajustedSpeed = scrollSpeed * (distanceToCenterPoint / slowDownFactor);
                currVCam.transform.Translate(new Vector3(0, 0, scroll.y * Time.deltaTime * ajustedSpeed), currVCam.transform);
            }
        }
    }

    void OnDestroy()
    {
        LevelData.cameraController = null;
    }

    public void FocusOn(Interactable celestial)
    {
        if (celestial == currCelestialInFocus) return;

        bool isPlanetNull = (celestial == null);
        CinemachineVirtualCamera subVCam = (currVCam == planetVCam1 ? planetVCam2 : planetVCam1);
        
        if (!isPlanetNull)
        {
            // If a planet is selected, set subCam to this planet
            subVCam.transform.SetParent(celestial.transform);
            subVCam.transform.localScale = new Vector3(1, 1, 1);
            subVCam.transform.localPosition = (currVCam.transform.position - celestial.transform.position).normalized * 2;
            subVCam.transform.LookAt(celestial.transform.position);
            celestialInfoView.SetPlanetInfo(celestial.SoCelestial);
            celestialInfoView.SetIndex(LevelData.planetManager.IndexOfInteractable(celestial) + 1);
        }

        // Set which cam is active depending on selection (bigger priority = active cam)
        currVCam.Priority = 1;
        subVCam.Priority = (isPlanetNull ? 1 : 10);
        mainVCam.Priority = (isPlanetNull ? 10 : 1);

        // Set variables to move the right cam correctly
        currCelestialInFocus = celestial;
        currVCam = (isPlanetNull ? mainVCam : subVCam);
    }

    public void FocusOnPrev()
    {
        FocusOn(LevelData.planetManager.PrevInteractable(currCelestialInFocus));
    }

    public void FocusOnNext()
    {
        FocusOn(LevelData.planetManager.NextInteractable(currCelestialInFocus));
    }
}
