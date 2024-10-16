using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsView : MonoBehaviour
{
    [SerializeField] Toggle realScaleCheckBox;

    void Awake()
    {
        LevelData.controlsView = this;
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
        LevelData.controlsView = null;
    }

    public void CheckRealScale(bool isChecked)
    {
        realScaleCheckBox.isOn = isChecked;
    }
}
