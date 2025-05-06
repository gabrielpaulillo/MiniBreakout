using TMPro;
using UnityEngine;

namespace MiniBreakout
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI livesText;
        [SerializeField] private TextMeshProUGUI scoreText;
        
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject winPanel;

        private void Start()
        {
            GameManager.Instance.OnGameOver.AddListener(ShowGameOverUI);
            GameManager.Instance.OnGameReset.AddListener(HideGameOverUI);
            GameManager.Instance.OnGameWin.AddListener(ShowWinPanelUI);
            GameManager.Instance.OnGameReset.AddListener(HideWinPanelUI);
            gameOverPanel.SetActive(false);
            winPanel.SetActive(false);
        }
        
        public void ShowGameOverUI() => gameOverPanel.SetActive(true);
        public void HideGameOverUI() => gameOverPanel.SetActive(false);
        
        public void ShowWinPanelUI() => winPanel.SetActive(true);
        public void HideWinPanelUI() => winPanel.SetActive(false);

        public void UpdateScoreText(int score)
        {
            if (scoreText != null)
            {
                scoreText.text = $"Score: {score}";
            }
        }

        public void UpdateLivesText(int lives)
        {
            if (livesText != null)
            {
                livesText.text = $"Lives: {lives}";
            }
        }
    }
}