using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Toggle _natureAnimationsToggle, _grassLooksInCameraToggle;

    [SerializeField] private Slider _graphicsQuality, _sensitivity, _shadowResolution;
    [SerializeField] private Text _graphicsQualityText, _sensitivityText, _shadowResolutionText;

    private void Start()
    {
        LoadSettings();
    }

    public void ChangeGraphicsQuality(Single value)
    {
        QualitySettings.SetQualityLevel(Convert.ToInt32(value));
        _graphicsQualityText.text = "" + value;

        ChangeShadowResolution(_shadowResolution.value);
    }

    public void ChangeShadowResolution(Single value)
    {
        ShadowResolution[] resolutions = new ShadowResolution[4] { ShadowResolution.Low, ShadowResolution.Medium, ShadowResolution.High, ShadowResolution.VeryHigh };
        if (value == 0) QualitySettings.shadows = ShadowQuality.Disable;
        else
        {
            QualitySettings.shadows = ShadowQuality.All;
            QualitySettings.shadowResolution = resolutions[Convert.ToInt32(value) - 1];
        }
         _shadowResolutionText.text = "" + value;
    }

    public void ChangeSensitivity(Single value)
    {
        _sensitivityText.text = "" + value;
    }

    public void LoadSettings()
    {
        int graphicsQuality = PlayerPrefs.GetInt("graphics quality", QualitySettings.GetQualityLevel());
        _graphicsQuality.value = graphicsQuality;
        QualitySettings.SetQualityLevel(graphicsQuality);

        ShadowResolution[] resolutions = new ShadowResolution[4] { ShadowResolution.Low, ShadowResolution.Medium, ShadowResolution.High, ShadowResolution.VeryHigh };
        int shadowResolution = PlayerPrefs.GetInt("shadow resolution", Array.IndexOf(resolutions, QualitySettings.shadowResolution) + 1);
        _shadowResolution.value = shadowResolution;
        if (shadowResolution == 0) QualitySettings.shadows = ShadowQuality.Disable;
        else
        {
            QualitySettings.shadows = ShadowQuality.All;
            QualitySettings.shadowResolution = resolutions[shadowResolution - 1];
        }

        _sensitivity.value = PlayerPrefs.GetFloat("sensitivity", 5);

        _natureAnimationsToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("nature animations", 1));
        _grassLooksInCameraToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("grass looks in camera", 1));
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("graphics quality", Convert.ToInt32(_graphicsQuality.value));
        PlayerPrefs.SetInt("shadow resolution", Convert.ToInt32(_shadowResolution.value));
        PlayerPrefs.SetFloat("sensitivity", _sensitivity.value);

        PlayerPrefs.SetInt("nature animations", Convert.ToInt32(_natureAnimationsToggle.isOn));
        PlayerPrefs.SetInt("grass looks in camera", Convert.ToInt32(_grassLooksInCameraToggle.isOn));
    }
}
