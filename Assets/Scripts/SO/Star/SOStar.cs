using UnityEngine;

[CreateAssetMenu(fileName = "NewStarData", menuName = "PlanetData/Star")]
public class SOStar : SOCelestial
{
    [Header("Star parameters")]
    public string starName;
}
