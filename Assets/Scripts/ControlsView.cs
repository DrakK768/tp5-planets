using System.Collections;
using System.Collections.Generic;
using UI.Dates;
using UnityEngine;
using UnityEngine.UI;

public class ControlsView : MonoBehaviour
{
    [SerializeField] Toggle realScaleCheckBox;
    [SerializeField] DatePicker datePicker;
    [SerializeField] Image playPauseBtnIcon;
    [SerializeField] Sprite playSprite;
    [SerializeField] Sprite pauseSprite;
    bool isPaused = false;

    void Awake()
    {
        LevelData.controlsView = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelData.planetManager.OnTimeChange += (ctx) =>
        {
            datePicker.SelectedDate = LevelData.planetManager.Date;
        };
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

    public void SetSpeed(int speed)
    {
        isPaused = (speed == 0);
        playPauseBtnIcon.sprite = isPaused ? playSprite : pauseSprite;
        LevelData.solarSystemManager.DaysPerFrame = speed;
    }

    public void PlayPauseBtnHandler()
    {
        SetSpeed(isPaused ? 1 : 0);
    }
}
