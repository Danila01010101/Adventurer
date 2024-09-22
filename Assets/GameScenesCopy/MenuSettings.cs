using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.IO;

namespace Adventurer
{
    [System.Serializable]
    public struct Setting 
    {
        public float VolumeMusic;
        public float VolumeSound;
        public float VolumeEffect;
        public int GraphicValue;
        public int MSAAValue;
        public bool Bloom;
    }

    public class MenuSettings : MonoBehaviour
    {
        public TMP_Dropdown Graphicdropdown;
        public TextMeshProUGUI MusicPercent;
        public TextMeshProUGUI SoundPercent;
        public TextMeshProUGUI EffectPercent;
        public TMP_Dropdown MSAADropdown;
        public Slider VolumeMusicSlider;
        public Slider VolumeSoundSlider;
        public Slider VolumeEffectSlider;
        public Toggle BloomToggle;
        public Button SaveSettingsButton;
        public Setting setting;

        private string settingFilePath;

        private void Start()
        {
            settingFilePath = Path.Combine(Application.persistentDataPath, "settings.json");

            LoadSettings();

            // Подписываемся на изменение значений UI, но без сохранения настроек
            VolumeMusicSlider.onValueChanged.AddListener(SetVolumeMusic);
            VolumeSoundSlider.onValueChanged.AddListener(SetVolumeSound);
            VolumeEffectSlider.onValueChanged.AddListener(SetVolumeEffect);
            Graphicdropdown.onValueChanged.AddListener(GraphicChange);
            MSAADropdown.onValueChanged.AddListener(SetMSAA);
            BloomToggle.onValueChanged.AddListener(SetBloom);
        }

        public void SaveSettings() 
        {
            string json = JsonUtility.ToJson(setting, true);
            File.WriteAllText(settingFilePath, json);
            SaveSettingsButton.interactable = false;
        }

        public void LoadSettings() 
        {
            if (File.Exists(settingFilePath))
            {
                string json = File.ReadAllText(settingFilePath);
                setting = JsonUtility.FromJson<Setting>(json);

                VolumeEffectSlider.value = setting.VolumeEffect;
                VolumeMusicSlider.value = setting.VolumeMusic;
                VolumeSoundSlider.value = setting.VolumeSound;
                Graphicdropdown.value = setting.GraphicValue;
                MSAADropdown.value = setting.MSAAValue;
                BloomToggle.isOn = setting.Bloom;
                MusicPercent.text = ((int)(setting.VolumeMusic * 100)).ToString() + "%";
                SoundPercent.text = ((int)(setting.VolumeSound * 100)).ToString() + "%";
                EffectPercent.text = ((int)(setting.VolumeEffect * 100)).ToString() + "%";

                SaveSettingsButton.interactable = false;
            }
        }
        public void GraphicChange(int value)
        {
            setting.GraphicValue = value;
            SaveSettingsButton.interactable = true;
        }

        public void SetMSAA(int value) 
        {
            setting.MSAAValue = value;
            SaveSettingsButton.interactable = true;
        }

        public void SetVolumeMusic(float value) 
        {
            setting.VolumeMusic = value;
            SaveSettingsButton.interactable = true;
            MusicPercent.text = ((int)(value*100)).ToString() + "%";
        }

        public void SetVolumeSound(float value) 
        {
            setting.VolumeSound = value;
            SaveSettingsButton.interactable = true;
            SoundPercent.text = ((int)(value * 100)).ToString() + "%";
        }

        public void SetVolumeEffect(float value) 
        {
            setting.VolumeEffect = value;
            SaveSettingsButton.interactable = true;
            EffectPercent.text = ((int)(value * 100)).ToString() + "%";
        }

        public void SetBloom(bool value) 
        {
            setting.Bloom=value;
            SaveSettingsButton.interactable = true;
        }
    }
}
