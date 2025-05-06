using UnityEngine;

namespace MiniBreakout
{
    public class ResetController : MonoBehaviour
    {
        [SerializeField] private Ball ballObj;
        [SerializeField] private Paddle paddleObj;

        private Vector2 ballStartPosition;
        private Vector2 paddleStartPosition;

        private void Start()
        {
            ballStartPosition = ballObj.transform.position;
            paddleStartPosition = paddleObj.transform.position;
            
            // Registra este método no evento OnPlayerLoseLife
            GameManager.Instance.OnPlayerLoseLife.AddListener(ResetBallAndPaddlePosition);
        }

        private void ResetBallAndPaddlePosition()
        {
            ballObj.transform.position = ballStartPosition;
            paddleObj.transform.position = paddleStartPosition;

            ballObj.ResetBall(false);
        }
    }
}