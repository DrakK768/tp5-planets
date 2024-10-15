using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemManager : MonoBehaviour
{
    [SerializeField] List<Planet> planets = new List<Planet>();
    DateTime date = new DateTime(2024, 10, 15);

    #region unity
    void Awake()
    {
        LevelData.solarSystemManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelData.planetManager.Date = date;
        LevelData.planetManager.OnTimeChange += UpdatePosition;
    }

    // Update is called once per frame
    void Update()
    {
        date = date.AddDays(1);
        LevelData.planetManager.Date = date;
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
            planet.transform.position = PlanetData.GetPlanetPosition(planet.GetPlanet(), t);
        }
    }
}
