using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class HDRIBrightnessControl : MonoBehaviour
{
    [Range(0f, 24f)]
    public int value = 1;
    public bool useRealTime = true;
    private Material skyboxMaterial;
    private float minReflection = 0.5f, maxReflection = 1.0f, minExposure = 0.09f, maxExposure = 1f;

    void Start()
    {
        skyboxMaterial = RenderSettings.skybox;
        if (skyboxMaterial == null)
        {
            Debug.LogError("Cannot find Skybox Material in RenderSettings!");
        }
    }

    void Update()
    {
        if (skyboxMaterial != null)
        {
            int currentHour = useRealTime ? DateTime.Now.Hour : value;
            float intensityValue = GetLightIntensity(currentHour);
            skyboxMaterial.SetFloat("_Exposure", Mathf.Lerp(minExposure, maxExposure, intensityValue));
            RenderSettings.reflectionIntensity = Mathf.Lerp(minReflection, maxReflection, intensityValue);
        }
    }

    float GetLightIntensity(float hour)
    {
        if (hour >= 0f && hour <= 12f)
        {
            return hour / 12f;
        }
        else
        {
            return (24f - hour) / 12f;
        }
    }
}
