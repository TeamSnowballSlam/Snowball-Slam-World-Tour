/// <summary>
/// This script manages the display settings in the settings menu
/// Took some code from https://www.youtube.com/watch?v=HnvPNoU9Wjw. Thanks to Liam in my studio team for finding that video
/// Code was modified to fix bugs and add new options
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScreenManager : MonoBehaviour
{
    [Header("Dropdowns")]
    [SerializeField] private TMP_Dropdown displayModeDropdown;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown refreshRateDropdown;
    [SerializeField] private TMP_Dropdown vsyncDropdown;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;
    private List<int> refreshRates;

    private int currentRefreshRate;
    private int currentResolutionIndex = -1;
    private int currentRefreshRateIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdownPopulate();
        refreshRateDropdownPopulate();
        displayModeDropdownPopulate();
        vsyncDropdownPopulate();
        SetResolution(currentResolutionIndex);
        SetRefreshRate(currentRefreshRateIndex);
        SetDisplayMode((int)Screen.fullScreenMode);
        SetVSync(QualitySettings.vSyncCount);
    }

    private void resolutionDropdownPopulate()
    {
        filteredResolutions = new List<Resolution>(); //Creating a list of resolutions that match the current refresh rate

        resolutionDropdown.ClearOptions(); //Clearing the dropdown options
        currentRefreshRate = (int)Screen.currentResolution.refreshRateRatio.value; //Getting the current refresh rate

        for (int i = 0; i < resolutions.Length; i++)
        {
            if(resolutions[i].refreshRateRatio.value == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]); //Adding resolutions that match the current refresh rate
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++) //Populating the dropdown with the resolutions that match the current refresh rate
        {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height;
            options.Add(resolutionOption);
            if(filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height) //Setting the current resolution index to the current resolution
            {
                currentResolutionIndex = i;   
            }
        }
        if (currentResolutionIndex == -1)
        {
            currentResolutionIndex = filteredResolutions.Count - 1;
            //Setting to the last resolution in the list if none match
            //This prevents it from instantly going to terrible resolutions
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, (int)resolution.refreshRateRatio.value);
    }
    private void refreshRateDropdownPopulate()
    {
        refreshRates = new List<int>(); // Creating a list of refresh rates

        refreshRateDropdown.ClearOptions();

        for (int i = 0; i < resolutions.Length; i++)
        {
            int refreshRate = (int)resolutions[i].refreshRateRatio.value;
            if (!refreshRates.Contains(refreshRate))
            {
                refreshRates.Add(refreshRate); // Adding unique refresh rates to the list
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < refreshRates.Count; i++)
        {
            string refreshRateOption = refreshRates[i] + " Hz";
            options.Add(refreshRateOption);
            if (refreshRates[i] == (int)Screen.currentResolution.refreshRateRatio.value)
            {
                currentRefreshRateIndex = i;
            }
        }

        if (currentRefreshRateIndex == -1)
        {
            currentRefreshRateIndex = refreshRates.Count - 1;
            // Setting to the last refresh rate in the list if none match
            // This prevents it from instantly going to terrible refresh rates
        }

        refreshRateDropdown.AddOptions(options);
        refreshRateDropdown.value = currentRefreshRateIndex;
        refreshRateDropdown.RefreshShownValue();
    }

    public void SetRefreshRate(int refreshRateIndex)
    {
        int refreshRate = refreshRates[refreshRateIndex];
        Resolution currentResolution = Screen.currentResolution;
        Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreenMode, refreshRate);
    }

    private void displayModeDropdownPopulate()
    {
        displayModeDropdown.ClearOptions();

        List<string> options = new List<string> { "Fullscreen", "FullScreenWindow", "Borderless", "Windowed" };
        displayModeDropdown.AddOptions(options);
        displayModeDropdown.value = (int)Screen.fullScreenMode;
        displayModeDropdown.RefreshShownValue();
    }

    public void SetDisplayMode(int modeIndex)
    {
        Screen.fullScreenMode = (FullScreenMode)modeIndex;
    }

    private void vsyncDropdownPopulate()
    {
        vsyncDropdown.ClearOptions();

        List<string> options = new List<string> { "Off", "On" };
        vsyncDropdown.AddOptions(options);
        vsyncDropdown.value = QualitySettings.vSyncCount;
        vsyncDropdown.RefreshShownValue();
    }

    public void SetVSync(int vsyncIndex)
    {
        QualitySettings.vSyncCount = vsyncIndex;
    }
}
