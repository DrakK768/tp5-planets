using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public event Action<DateTime> OnTimeChange;
    private DateTime date;
    public DateTime Date { 
        get => date; 
        set {
            date = value;
            OnTimeChange?.Invoke(date);
        }
    }
    private List<Interactable> interactables;

    #region unity
    void Awake()
    {
        LevelData.planetManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        interactables = new List<Interactable>();
        Interactable[] allInteractables = FindObjectsOfType<Interactable>();
        Interactable[] allInteractablesSorted = allInteractables.OrderBy(i => i.transform.GetSiblingIndex()).ToArray();
        foreach (Interactable celestial in allInteractablesSorted)
        {
            if (celestial.SoCelestial != null)
            {
                interactables.Add(celestial);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        LevelData.planetManager = null;
    }
    #endregion

    public int InteractablesCount()
    {
        return interactables.Count;
    }

    public int IndexOfInteractable(Interactable celestial)
    {
        return interactables.IndexOf(celestial);
    }

    public Interactable PrevInteractable(Interactable celestial)
    {
        int prevIndex = (IndexOfInteractable(celestial) - 1 + InteractablesCount()) % InteractablesCount();
        return interactables[prevIndex];
    }

    public Interactable NextInteractable(Interactable celestial)
    {
        int nextIndex = (IndexOfInteractable(celestial) + 1) % InteractablesCount();
        return interactables[nextIndex];
    }
}
