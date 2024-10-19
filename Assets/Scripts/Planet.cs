using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static PlanetData;

public class Planet : MonoBehaviour
{
    [SerializeField] SOPlanet soPlanet;
    public SOPlanet SoPlanet => soPlanet;
    private LineRenderer lineRenderer;
    private int segments = 200;

    #region unity
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        DrawEllipse();
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    void OnMouseUpAsButton()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            LevelData.cameraController.FocusOn(this);
        }
    }

    void DrawEllipse()
    {
        float angle = 0f;
        for (int i = 0; i <= segments; i++)
        {
            Vector3 pos = PlanetData.GetPlanetPosition(soPlanet.planet, angle);

            lineRenderer.SetPosition(i, pos);

            // Increment the angle for the next point
            angle += (2 * Mathf.PI) / segments;
        }
    }

    public void DisplayTrajectory(bool visible)
    {
        lineRenderer.enabled = visible;
    }
}
