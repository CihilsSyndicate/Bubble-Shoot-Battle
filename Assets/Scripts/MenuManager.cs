using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private float defaultVolume = 0.8f;
    [SerializeField] private AudioClip uiClick;
    [SerializeField] private AudioClip uiHover;
    [SerializeField] private AudioClip uiSpecial;
    [SerializeField] private AudioSource audioSource;

    [Header("Scene Management")]
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Animator fadeAnimator;

    [Header("UI Components")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI playText;
    [SerializeField] private TextMeshProUGUI settingsText;
    [SerializeField] private TextMeshProUGUI quitText;

    [Header("Panels")]
    [SerializeField] private GameObject homePanel;
    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        SetupInitialUI();
        SetStartVolume();
    }

    private void SetupInitialUI()
    {
        homePanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    #region Button Functions
    public void LoadLevel()
    {
        fadeAnimator.SetTrigger("FadeOut");
        StartCoroutine(WaitToLoadLevel());
    }

    private IEnumerator WaitToLoadLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneToLoad);
    }

    public void OpenSettingsPanel()
    {
        UIClick();
        homePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettingsPanel()
    {
        UIClick();
        settingsPanel.SetActive(false);
        homePanel.SetActive(true);
    }

    public void Quit()
    {
        UIClick();
        Application.Quit();
    }
    #endregion

    #region Audio Management
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

    private void SetStartVolume()
    {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", defaultVolume);
        }

        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        AudioListener.volume = volumeSlider.value;
    }

    public void UIClick()
    {
        audioSource.PlayOneShot(uiClick);
    }

    public void UIHover()
    {
        audioSource.PlayOneShot(uiHover);
    }

    public void UISpecial()
    {
        audioSource.PlayOneShot(uiSpecial);
    }
    #endregion
}