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

    #region unity
    void Awake()
    {
        LevelData.solarSystemManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelData.planetManager.Date = new DateTime(2024, 10, 15);
        LevelData.planetManager.OnTimeChange += UpdatePosition;
        DisplayRealSizes(false); // to correctly set planets' scale relative to each other
    }

    // Update is called once per frame
    void Update()
    {
        LevelData.planetManager.Date = LevelData.planetManager.Date.AddDays(1);
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
            planet.transform.position = PlanetData.GetPlanetPosition(planet.GetPlanetData().planet, t);
        }
    }

    public void DisplayTrajectories(bool visible)
    {
        foreach (Planet planet in planets)
            planet.DisplayTrajectory(visible);
    }

    public void DisplayRealSizes(bool visible)
    {
        float targetScale = visible ? sun.GetSOSun().radius / PlanetData.kmToAu : 0.2f;
        sun.transform.localScale = new Vector3(targetScale, targetScale, targetScale);
        foreach (Planet planet in planets)
        {
            targetScale = PlanetData.GetPlanetRealSize(planet.GetPlanetData()) * (visible ? 1f : scaleFactor);  
            planet.transform.localScale = new Vector3(targetScale, targetScale, targetScale);
        }
    }

    public void SetScale(float scale)
    {
        scaleFactor = scale;
        LevelData.controlsView.CheckRealScale(false);
        DisplayRealSizes(false);
    }
}
