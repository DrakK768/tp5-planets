using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SolarSystemManager : MonoBehaviour
{
    [SerializeField] Sun sun;
    [SerializeField] List<Planet> planets = new List<Planet>();
    float scaleFactor = 1000f;
    int daysPerFrame = 1;
    public int DaysPerFrame
    {
        set => daysPerFrame = value;
    }

    #region unity
    void Awake()
    {
        LevelData.solarSystemManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelData.planetManager.Date = DateTime.Now;
        LevelData.planetManager.OnTimeChange += UpdatePosition;
        DisplayRealSizes(false); // to correctly set planets' scale relative to each other
    }

    // Update is called once per frame
    void Update()
    {
        if (daysPerFrame != 0)
            LevelData.planetManager.Date = LevelData.planetManager.Date.AddDays(daysPerFrame);
    }

    void OnDestroy()
    {
        LevelData.solarSystemManager = null;
    }
    #endregion

    void UpdatePosition(DateTime t)
    {
        foreach (Planet planet in planets)
        {
            planet.transform.position = PlanetData.GetPlanetPosition(planet.SoPlanet.planet, t);
        }
    }

    // Linked to show trajectories's checkbox
    public void DisplayTrajectories(bool visible)
    {
        foreach (Planet planet in planets)
            planet.DisplayTrajectory(visible);
    }

    // Linked to show real sizes's checkbox
    public void DisplayRealSizes(bool visible)
    {
        float targetScale = visible ? sun.GetSOSun().radius / PlanetData.kmToAu : 0.2f;
        sun.transform.localScale = new Vector3(targetScale, targetScale, targetScale);
        foreach (Planet planet in planets)
        {
            targetScale = planet.SoPlanet.radius / PlanetData.kmToAu * (visible ? 1f : scaleFactor);  
            planet.transform.localScale = new Vector3(targetScale, targetScale, targetScale);
        }
    }

    // Linked to scale selector slider
    public void SetScale(float scale)
    {
        scaleFactor = scale;
        LevelData.controlsView.CheckRealScale(false);
        DisplayRealSizes(false);
    }
}
