using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlanetData;

public class Planet : MonoBehaviour
{
    [SerializeField] SOPlanet soPlanet;
    private LineRenderer lineRenderer;
    private int segments = 100;

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

    void DrawEllipse()
    {
        float angle = 0f;
        for (int i = 0; i <= segments; i++)
        {
            float a = GetKeplerParameter(soPlanet.planet, KeplerParameter.a)[0];
            float e = GetKeplerParameter(soPlanet.planet, KeplerParameter.e)[0];
            float r = (a*(1-Mathf.Pow(e, 2)) / (1 + e*Mathf.Cos(angle)));
            float x = Mathf.Cos(angle) * r;
            float y = Mathf.Sin(angle) * r;

            lineRenderer.SetPosition(i, new Vector3(x, y, 0));

            // Increment the angle for the next point
            angle += (2 * Mathf.PI) / segments;
        }
    }

    public void DisplayTrajectory(bool visible)
    {
        lineRenderer.enabled = visible;
    }

    public SOPlanet GetPlanetData()
    {
        return soPlanet;
    }
}
