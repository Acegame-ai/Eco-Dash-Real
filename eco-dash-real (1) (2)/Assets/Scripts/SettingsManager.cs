using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("Toggle for Master Audio. On = audio playing, Off = audio paused.")]
    public Toggle masterAudioToggle;
    
    [Tooltip("Toggle for SFX Audio. On = SFX playing, Off = SFX muted.")]
    public Toggle sfxAudioToggle;
    
    [Header("Audio Mixer (Optional)")]
    [Tooltip("Audio Mixer that contains an exposed parameter 'SFXVolume' for controlling SFX volume.")]
    public AudioMixer audioMixer;

    private const string MASTER_AUDIO_KEY = "MasterAudio";
    private const string SFX_AUDIO_KEY = "SFXAudio";

    private void Start()
    {
        // Load saved settings (default is 'on' for both)
        bool masterAudioOn = PlayerPrefs.GetInt(MASTER_AUDIO_KEY, 1) == 1;
        bool sfxAudioOn = PlayerPrefs.GetInt(SFX_AUDIO_KEY, 1) == 1;
        
        // Set the toggles to reflect the saved state
        if (masterAudioToggle != null)
        {
            masterAudioToggle.isOn = masterAudioOn;
        }
        if (sfxAudioToggle != null)
        {
            sfxAudioToggle.isOn = sfxAudioOn;
        }
        
        // Apply the settings
        SetMasterAudio(masterAudioOn);
        SetSFXAudio(sfxAudioOn);
    }

    /// <summary>
    /// Called when the Master Audio toggle is changed.
    /// If true, resumes audio; if false, pauses audio.
    /// </summary>
    /// <param name="isOn">True if audio is enabled; false if disabled.</param>
    public void OnMasterAudioToggleChanged(bool isOn)
    {
        SetMasterAudio(isOn);
        PlayerPrefs.SetInt(MASTER_AUDIO_KEY, isOn ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log("Master Audio Toggle set to " + isOn);
    }

    /// <summary>
    /// Called when the SFX Audio toggle is changed.
    /// If true, SFX volume is full; if false, SFX volume is muted.
    /// </summary>
    /// <param name="isOn">True if SFX is enabled; false if disabled.</param>
    public void OnSFXAudioToggleChanged(bool isOn)
    {
        SetSFXAudio(isOn);
        PlayerPrefs.SetInt(SFX_AUDIO_KEY, isOn ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log("SFX Audio Toggle set to " + isOn);
    }

    /// <summary>
    /// Sets the master audio. Instead of adjusting volume, we pause/resume the AudioListener.
    /// </summary>
    /// <param name="isOn">If true, audio plays; if false, audio is paused.</param>
    private void SetMasterAudio(bool isOn)
    {
        // When isOn is false, pause the audio; when true, resume it.
        AudioListener.pause = !isOn;
    }

    /// <summary>
    /// Sets the SFX audio using an AudioMixer.
    /// If isOn is true, SFX volume is 0 dB; if false, SFX is muted (-80 dB).
    /// </summary>
    /// <param name="isOn">True to enable SFX; false to mute SFX.</param>
    private void SetSFXAudio(bool isOn)
    {
        if (audioMixer != null)
        {
            // Convert the toggle state to decibels.
            float dB = isOn ? 0f : -80f;
            audioMixer.SetFloat("SFXVolume", dB);
        }
        else
        {
            Debug.LogWarning("AudioMixer is not assigned. SFX volume changes will not be applied.");
        }
    }
}
