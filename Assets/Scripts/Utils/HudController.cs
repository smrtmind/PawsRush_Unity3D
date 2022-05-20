﻿using Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.Utils
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private Text _coinsValue;
        [SerializeField] private Text _distanceValue;
        [SerializeField] private GameObject _timerToStart;

        private GameSession _gameSession;
        private PlayerController _playerController;
        private Text _timer;
        private FollowCamera _followCamera;
        private Vector3 _defaultCameraVector;

        private void Awake()
        {
            _gameSession = FindObjectOfType<GameSession>();
            _playerController = FindObjectOfType<PlayerController>();
            _timer = _timerToStart.GetComponent<Text>();
            _followCamera = FindObjectOfType<FollowCamera>();

            _defaultCameraVector = _followCamera.Offset;
        }

        private void Update()
        {
            _coinsValue.text = $"{_gameSession.Coins}";

            if ((int)_gameSession.OnStartDelay <= 0)
            {
                _timer.color = Color.green;
                _timer.text = "Go";
            }
            else
            {
                _timer.text = $"{(int)_gameSession.OnStartDelay}";
            }

            if (_playerController.IsRunning)
            {
                _timerToStart.SetActive(false);
                _distanceValue.text = $"{_gameSession.Distance}";
            }
                
        }

        public void OnPause()
        {
            _playerController.SetRunningState(false);
            SetRotation(0f, 180f, 0f);

            _followCamera.Offset = new Vector3(0f, 2f, -3.5f);
        }

        public void OnPlay()
        {
            _playerController.SetRunningState(true);
            SetRotation(0f, 0f, 0f);

            _followCamera.Offset = _defaultCameraVector;
        }

        public void OnRestart()
        {
            var sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }

        public void OnExit()
        {
            Application.Quit();
        }

        private void SetRotation(float x, float y, float z)
        {
            var rotation = new Vector3(x, y, z);
            _playerController.gameObject.transform.rotation = Quaternion.Euler(rotation);
        }
    }
}