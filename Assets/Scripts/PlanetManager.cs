using System;
using System.Collections;
using System.Collections.Generic;
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

    #region unity
    void Awake()
    {
        LevelData.planetManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
}
