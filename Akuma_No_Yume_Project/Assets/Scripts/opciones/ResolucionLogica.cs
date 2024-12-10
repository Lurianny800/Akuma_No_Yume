using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolucionLogica : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    Resolution[] resolutions;
    public Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }

        ResolutionCheck();

    }

    public void FullscreenOn(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void ResolutionCheck()
    {
        resolutions = Screen.resolutions;
        dropdown.ClearOptions();

        List<string> options = new List<string>();
        int resolucionactual = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                resolucionactual = i;
            }
        }

        dropdown.AddOptions(options);
        dropdown.value = resolucionactual;
        dropdown.RefreshShownValue();

        dropdown.value = PlayerPrefs.GetInt("resNum", 0);
    }

    public void ChooseResolution(int indicadorRes)
    {
        PlayerPrefs.SetInt("resNum", dropdown.value);

        Resolution resolution = resolutions[indicadorRes];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

    }

    // Update is called once per frame
    void Update()
    {

    }
}