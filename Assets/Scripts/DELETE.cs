using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DELETE : MonoBehaviour
{
    public TMP_Text frameRateText;
    private void Update()
    {
        // Calculate frame rate
        float currentFrameRate = 1f / Time.deltaTime;

        // Get VSync status
        bool vsyncEnabled = QualitySettings.vSyncCount > 0;

        // Get current resolution
        Resolution currentResolution = Screen.currentResolution;

        // Update text
        frameRateText.text = "Frame Rate: " + Mathf.RoundToInt(currentFrameRate) + "\n" +
                             "VSync: " + (vsyncEnabled ? "Enabled" : "Disabled" + "\n" +
                             "Resolution: " + currentResolution.width + "x" + currentResolution.height);
    }
}
