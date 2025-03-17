using UnityEngine;
using UnityEngine.UI;

public class Settingsandoptions : MonoBehaviour
{
    [Header("Panels")]
    [Tooltip("The panel that contains the settings UI.")]
    public GameObject settingsPanel;
    
    [Tooltip("The panel that contains the options UI (with 5 buttons).")]
    public GameObject optionsPanel;

    [Header("Option Buttons (Optional)")]
    [Tooltip("An array of the 5 option buttons (if you need to reference them individually).")]
    public Button[] optionButtons;

    /// <summary>
    /// Call this method to toggle the Settings panel active/inactive.
    /// Attach this to your Settings button's OnClick event.
    /// </summary>
    public void ToggleSettingsPanel()
    {
        if (settingsPanel != null)
        {
            bool isActive = settingsPanel.activeSelf;
            settingsPanel.SetActive(!isActive);
            Debug.Log("UIManager: Settings Panel set to " + (!isActive));
        }
        else
        {
            Debug.LogWarning("UIManager: Settings Panel reference is missing!");
        }
    }

    /// <summary>
    /// Call this method to toggle the Options panel active/inactive.
    /// Attach this to your Options button's OnClick event.
    /// </summary>
    public void ToggleOptionsPanel()
    {
        if (optionsPanel != null)
        {
            bool isActive = optionsPanel.activeSelf;
            optionsPanel.SetActive(!isActive);
            Debug.Log("UIManager: Options Panel set to " + (!isActive));
        }
        else
        {
            Debug.LogWarning("UIManager: Options Panel reference is missing!");
        }
    }
}
