using System.Collections.Generic;
using UnityEngine;

public class SOCelestial : ScriptableObject
{
    [Header("Facts")]
    public List<string> facts;
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
    [Tooltip("Temperature in �Celsius")]
    public float meanTemp;
    public int numberOfSatellites;
    public bool isRingSystem;
}
