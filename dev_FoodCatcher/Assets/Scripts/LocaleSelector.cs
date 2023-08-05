using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    bool active = false;
    int ID;

    public ToggleGroup toggleGroup;
    void Start()
    {
        ID = PlayerPrefs.GetInt("LocalKey", 0); 
        ChangeLocale(ID);
    }

    public void ChangeLocale(int localID)
    {
        if (active == true)
            return;
        StartCoroutine(SetLocale(localID));
    }

    IEnumerator SetLocale(int localID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale =
            LocalizationSettings.AvailableLocales.Locales[localID];

        PlayerPrefs.SetInt("LocalKey", localID);
        active = false;
    }

    public void ToggleLanguage()
    {
        ID = PlayerPrefs.GetInt("LocalKey", 0);

        foreach (Toggle toggle in toggleGroup.ActiveToggles())
        {
            int toggleID = toggle.transform.GetSiblingIndex();
            if (toggleID == ID)
            {
                Debug.Log("The function is working");
                return; // The selected language is already active, no need to change.
            }

            else
            {
                ChangeLocale(toggleID);
            }
        }
    }
}
