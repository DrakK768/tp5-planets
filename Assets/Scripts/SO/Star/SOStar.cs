using UnityEngine;

[CreateAssetMenu(fileName = "NewStarData", menuName = "PlanetData/Star")]
public class SOStar : ScriptableObject
{
    public string starName;
    [Header("Bulk parameters")]
    [Tooltip("Mass in kg")]
    public float mass;
    [Tooltip("Volume in km^3")]
    public float volume;
    [Tooltip("Radius in km")]
    public float radius;
    public float ellipticity;
    [Tooltip("Density in kg/m^3")]
    public float meanDensity;
    [Tooltip("Gravity in m/s^2")]
    public float meanGravity;
    public int numberOfSatellites;
    public bool isRingSystem;
}
