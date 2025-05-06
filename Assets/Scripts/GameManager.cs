using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace MiniBreakout
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance; // Ponto de acesso global - singleton 

        public UnityEvent OnPlayerLoseLife;
        public UnityEvent OnGameOver;
        public UnityEvent OnGameReset;
        public UnityEvent OnGameWin;

        [Space(10), SerializeField] private UIManager uiManager;

        [Space(10), SerializeField] private int currentLives = 3;
        [SerializeField] private int currentScore = 0;
        [SerializeField] private int totalBlocks;

        [Space(10), SerializeField] private Ball ball;

        private bool firstSpeedIncrement;
        private bool secondSpeedIncrement;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject); // Garante que só exista uma instância
                return;
            }

            Instance = this;

            totalBlocks = FindObjectsByType<Block>(FindObjectsInactive.Include, FindObjectsSortMode.None).Length;
            Debug.Log($"Total blocks: {totalBlocks}");
            UpdateUI();

            firstSpeedIncrement = false;
            secondSpeedIncrement = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 0f)
                ResetGame();
            
            Debug.Log($"Ball Speed: {ball.CurrentSpeed}");
        }

        private void UpdateUI()
        {
            uiManager.UpdateLivesText(currentLives);
            uiManager.UpdateScoreText(currentScore);
        }

        public void ReduceLives()
        {
            currentLives--;
            Debug.Log($"Vidas: {currentLives}");
            UpdateUI();

            if (currentLives <= 0)
                HandleGameOver();
            else
                OnPlayerLoseLife?.Invoke();
        }

        public void AddScore(int points)
        {
            currentScore += points;

            if (currentScore > 11 && firstSpeedIncrement == false)
            {
                ball.CurrentSpeed += 1.0f;
                firstSpeedIncrement = true;
            }
            else if (currentScore > 21 && secondSpeedIncrement == false)
            {
                ball.CurrentSpeed += 1.0f;
                secondSpeedIncrement = true;
            }
            
            UpdateUI();
            
            totalBlocks--;
            
            if (totalBlocks == 0)
                HandleWinCondition();
        }
        
        private void ResetGame()
        {
            Time.timeScale = 1f; // Retomar o jogo
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recarregar a cena atual
            currentScore = 0;
            currentLives = 3;
            UpdateUI();
            OnGameReset?.Invoke();
        }

        private void HandleWinCondition()
        {
            Time.timeScale = 0f;
            OnGameWin?.Invoke();
            Debug.Log("YOU WIN!");
        }
    
        private void HandleGameOver()
        {
            Time.timeScale = 0f; // Pausa o jogo
            OnGameOver?.Invoke();
            Debug.Log("GAME OVER!");
        }
    }
}
