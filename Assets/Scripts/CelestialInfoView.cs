using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CelestialInfoView : MonoBehaviour
{
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text facts;
    [SerializeField] TMP_Text mass;
    [SerializeField] TMP_Text volume;
    [SerializeField] TMP_Text radius;
    [SerializeField] TMP_Text gravity;
    [SerializeField] TMP_Text temp;
    [SerializeField] TMP_Text nbSatellites;
    [SerializeField] TMP_Text navText;

    // Start is called before the first frame update
    void Start()
    {
        SetVisible(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlanetInfo(SOCelestial celestial)
    {
        title.text = celestial.name;
        string factsText = "";
        foreach (string fact in celestial.facts)
        {
            factsText += "- " + fact + "\n";
        }
        facts.text = factsText;
        mass.text = "Mass : " + celestial.mass + " kg";
        volume.text = "Volume : " + celestial.volume + " km³";
        radius.text = "Radius : " + celestial.radius + " km";
        gravity.text = "Mean Gravity : " + celestial.meanGravity + " m/s²";
        temp.text = "Mean Temperature : " + celestial.meanTemp + " °C";
        nbSatellites.text = "Number of satellites : " + celestial.numberOfSatellites;
        SetVisible(true);

        // Coroutine to delay text fitting for a frame
        // Otherwise UI elements are not updated yet when fetching their preferred sizes
        StartCoroutine(ResizeToFitText());
    }

    public void SetIndex(int index)
    {
        navText.text = index + " of " + LevelData.planetManager.InteractablesCount();
    }

    public void SetVisible(bool visible)
    {
        gameObject.SetActive(visible);
    }

    // Linked to cross in top right corner of this panel
    public void ClosePlanetInfo()
    {
        SetVisible(false);
        LevelData.cameraController.FocusOn(null);
    }

    // Linked to previous celestial button
    public void PrevCelestial()
    {
        LevelData.cameraController.FocusOnPrev();
    }
    
    // Linked to next celestial button
    public void NextCelestial()
    {
        LevelData.cameraController.FocusOnNext();
    }

    IEnumerator ResizeToFitText()
    {
        yield return new WaitForEndOfFrame();

        foreach (TMP_Text textBox in GetComponentsInChildren<TMP_Text>())
        {
            textBox.ForceMeshUpdate();
            Vector2 preferredSize = textBox.GetPreferredValues();
            RectTransform rectTransform = textBox.rectTransform;
            rectTransform.sizeDelta = preferredSize;
            textBox.ForceMeshUpdate();
        }
    }
}
