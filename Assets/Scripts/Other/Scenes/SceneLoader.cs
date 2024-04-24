using UnityEngine;
using UnityEngine.SceneManagement;

namespace Other.Scenes
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private string _loadScene;
        public void LoadScene() => SceneManager.LoadScene(_loadScene);
        public void QuitGame() => Application.Quit();
    }
}