using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour
{
    [Tooltip("If left to None, it will take the SOPlanet of the Planet component from the same gameobject. If None is found, this celestial will not be interactable")]
    [SerializeField] SOCelestial soCelestial;
    public SOCelestial SoCelestial => soCelestial;

    // Start is called before the first frame update
    void Start()
    {
        if (soCelestial == null && TryGetComponent<Planet>(out Planet planet))
            soCelestial = planet.SoPlanet;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseUpAsButton()
    {
        if (soCelestial != null && !EventSystem.current.IsPointerOverGameObject())
        {
            LevelData.cameraController.FocusOn(this);
        }
    }
}
