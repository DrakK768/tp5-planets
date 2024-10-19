using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        LevelData.cameraController.FocusOn(this);
    }

    void DrawEllipse()
    {
        float angle = 0f;
        for (int i = 0; i <= segments; i++)
        {
            float a = GetKeplerParameter(soPlanet.planet, KeplerParameter.a)[0];
            float e = GetKeplerParameter(soPlanet.planet, KeplerParameter.e)[0];
            float I = GetKeplerParameter(soPlanet.planet, KeplerParameter.I)[0] * Mathf.Deg2Rad;
            float lNode = GetKeplerParameter(soPlanet.planet, KeplerParameter.longNode)[0] * Mathf.Deg2Rad;
            float lPeri = GetKeplerParameter(soPlanet.planet, KeplerParameter.longPeri)[0] * Mathf.Deg2Rad;
            float peri = lPeri - lNode;
            float r = (a * (1 - Mathf.Pow(e, 2)) / (1 + e * Mathf.Cos(angle)));
            float x = Mathf.Cos(angle) * r;
            float y = Mathf.Sin(angle) * r;

            // 1. Rotate by lPeri (argument of periapsis) around the Z-axis
            float x_peri = x * Mathf.Cos(peri) - y * Mathf.Sin(peri);
            float y_peri = x * Mathf.Sin(peri) + y * Mathf.Cos(peri);
            float z_peri = 0;  // No change in z for this rotation

            // 2. Rotate by i (inclination) around the X-axis
            float x_incl = x_peri;
            float y_incl = y_peri * Mathf.Cos(I) - z_peri * Mathf.Sin(I);
            float z_incl = y_peri * Mathf.Sin(I) + z_peri * Mathf.Cos(I);

            // 3. Rotate by lNode (longitude of ascending node) around the Z-axis
            float x_orbit = x_incl * Mathf.Cos(lNode) - y_incl * Mathf.Sin(lNode);
            float y_orbit = x_incl * Mathf.Sin(lNode) + y_incl * Mathf.Cos(lNode);
            float z_orbit = z_incl;

            lineRenderer.SetPosition(i, new Vector3(x_orbit, y_orbit, z_orbit));

            // Increment the angle for the next point
            angle += (2 * Mathf.PI) / segments;
        }
    }

    public void DisplayTrajectory(bool visible)
    {
        lineRenderer.enabled = visible;
    }
}
