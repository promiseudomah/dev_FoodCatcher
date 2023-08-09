using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    private bool isActive = false;
    private int selectedLocaleID;

    public ToggleGroup toggleGroup;

    private void Start()
    {
        selectedLocaleID = PlayerPrefs.GetInt("SelectedLocaleID", 0);
        ChangeLocale(selectedLocaleID);
    }

    public void ChangeLocale(int localeID)
    {
        if (isActive)
            return;

        foreach (Toggle toggle in toggleGroup.GetComponentsInChildren<Toggle>())
        {
            int toggleIndex = toggle.transform.GetSiblingIndex();
            toggle.isOn = toggleIndex == localeID;
        }

        StartCoroutine(SetLocale(localeID));
    }

    private IEnumerator SetLocale(int localeID)
    {
        isActive = true;
        yield return LocalizationSettings.InitializationOperation;

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        PlayerPrefs.SetInt("SelectedLocaleID", localeID);

        isActive = false;
    }

    public void ToggleLanguage()
    {
        selectedLocaleID = PlayerPrefs.GetInt("SelectedLocaleID", 0);

        foreach (Toggle toggle in toggleGroup.ActiveToggles())
        {
            int toggleIndex = toggle.transform.GetSiblingIndex();
            if (toggleIndex == selectedLocaleID)
            {
                Debug.Log("The function is working");
                return; // The selected language is already active; no need to change.
            }
            else
            {
                ChangeLocale(toggleIndex);
            }
        }
    }
}
