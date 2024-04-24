using UnityEngine;

namespace Global
{
    public class GameModeChanger : MonoBehaviour
    {
        [field: SerializeField] public GameModes GameMode { get; set; }
        [SerializeField] private GameObject _pausePanel;

        private void Update()
        {
            if (UnityEngine.Input.GetKeyUp(KeyCode.Escape)) ChangeMode();

            if (GameMode == GameModes.Pause) Pause();
            else Playing();
        }

        public void ChangeMode() => GameMode = GameMode == GameModes.Pause ? GameModes.Playing : GameModes.Pause;

        private void Playing()
        {
            Time.timeScale = 1;
            _pausePanel.SetActive(false);
        }

        private void Pause()
        {
            Time.timeScale = 0;
            _pausePanel.SetActive(true);
        }
    }

    public enum GameModes
    {
        Playing,
        Pause
    }
}