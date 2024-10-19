using UnityEngine;

[CreateAssetMenu(fileName = "NewPlanetData", menuName = "PlanetData/Planet")]
public class SOPlanet : SOCelestial
{
    [Header("Planet parameters")]
    public PlanetData.Planet planet;
}
