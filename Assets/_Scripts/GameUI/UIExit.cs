using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class UIExit : MonoBehaviour
    {
        [SerializeField] private Button button;

        private void Start()
        {
            button.onClick.AddListener(Exit);
        }

        private void Exit()
        {
            Application.Quit();
        }

        private void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}
