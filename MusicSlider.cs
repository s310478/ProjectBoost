using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }

        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolumeAndDeselect(); });
    }

    public void ChangeVolumeAndDeselect()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
        StartCoroutine(DeselectSlider());
    }

    private IEnumerator DeselectSlider()
    {
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(null);
    }

    void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
