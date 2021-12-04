using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDeffence
{
    public class MainMenuComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject _startGamePanel;
        [SerializeField]
        private GameObject _optionsPanel;
        private float _sound;
        private float _music;
        [SerializeField]
        private Slider _soundSlider;
        [SerializeField]
        private Slider _musicSlider;
        [SerializeField]
        private Text _soundValueText;
        [SerializeField]
        private Text _musicValueText;
        [SerializeField]
        private AudioSource _audioSource;
        private bool _soundValueModified;
        private bool _musicValueModified;

        public void Awake()
        {
            Helper.Difficulty = Difficulty.Easy;
            Helper.MapType = MapType.Map_Grassfields;
            _sound = PlayerPrefs.GetFloat("Sound", 1);
            _soundSlider.value = _sound;
            _soundValueText.text = Mathf.RoundToInt(_sound * 100).ToString();
            _music = PlayerPrefs.GetFloat("Music", 1);
            _musicSlider.value = _music;
            _musicValueText.text = Mathf.RoundToInt(_music * 100).ToString();
            _audioSource.volume = _music;
        }
        public void OnSoundChange_UnityEditror(float value)
        {
            _soundValueModified = true;
            _sound = value;
            _soundValueText.text = Mathf.RoundToInt(_sound * 100).ToString();
        }
        public void OnMusicChange_UnityEditror(float value)
        {
            _musicValueModified = true;
            _music = value;
            _musicValueText.text = Mathf.RoundToInt(_music * 100).ToString();
            _audioSource.volume = _music;
        }

        public void OnDifficultyChange_UnityEditror(int difficultyNumber)
        {
            Helper.Difficulty = (Difficulty)difficultyNumber;
        }

        public void OnStartGame_UnityEditor()
        {
            _optionsPanel.SetActive(false);
            _startGamePanel.SetActive(true);
        }
        
        public void OnStartGameAccept_UnityEditor()
        {
            if (_soundValueModified) PlayerPrefs.SetFloat("Sound", _sound);
            if (_musicValueModified) PlayerPrefs.SetFloat("Music", _music);
            SceneManager.LoadScene("GameScene");
        }

        public void OnOptions_UnityEditror()
        {
            _optionsPanel.SetActive(true);
            _startGamePanel.SetActive(false);
        }
        public void OnExitGame_UnityEditror()
        {
            if (_soundValueModified) PlayerPrefs.SetFloat("Sound", _sound);
            if (_musicValueModified) PlayerPrefs.SetFloat("Music", _music);
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }

}
