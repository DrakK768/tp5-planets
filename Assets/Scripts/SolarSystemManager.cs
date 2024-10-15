using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemManager : MonoBehaviour
{
    [SerializeField] List<Planet> planets = new List<Planet>();

    #region unity
    void Awake()
    {
        LevelData.solarSystemManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelData.planetManager.OnTimeChange += UpdatePosition;
        UpdatePosition(new DateTime(2024, 10, 15));
    }

    // Update is called once per frame
    void Update()
    {
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
