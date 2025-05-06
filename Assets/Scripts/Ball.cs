using UnityEngine;

namespace MiniBreakout
{
    [RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float initialSpeed = 5f;
        
        public float CurrentSpeed { get; set; }

        private Rigidbody2D _rigidbody;

        private Vector2 _currentDirection;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            
            ResetBall(true);
        }

        private void Update()
        {
            // Tecla R para reset
            if (Input.GetKeyDown(KeyCode.R)) ResetBall(false);
        }

        private void OnTriggerEnter2D(Collider2D other) // área de detecção, não necessariamente uma colisão física
        {
            // Debuggar a colisão da bolinha
            // Debug.Log($"{name} colidiu com  {other.gameObject.name}", this);

            if (other.CompareTag("LoseZone"))
            {
                GameManager.Instance.ReduceLives();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Debug.Log($"{name} colidiu com {collision.gameObject.name}", this); // "this" vincula o gameObject (ball) ao {name}

            if (collision.gameObject.TryGetComponent(out Block block))
            {
                GameManager.Instance.AddScore(block.Points);
                block.DisableBlock();
            }

            ReflectBallDirection(collision);
        }

        private void ReflectBallDirection(Collision2D collision)
        {
            // Pegar o primeiro ponto de contato da colisão, para utilizar no Debug
            ContactPoint2D contact = collision.GetContact(0);
            // Obter a força normal que o gameObject que colidiu exerce na bola
            Vector2 normal = contact.normal;

            // Calcula nova direção utilizando a direção que ocasiona a colisão e a normal obtida
            _currentDirection = Vector2.Reflect(_currentDirection, normal).normalized;
            // Aplica a nova direção, multiplicando-a pela velocidade, ao linearVelocity (que representa a velocidade de translação do objeto de um ponto ao outro, sem considerar sua rotação). Em um jogo 2D a "linearVelocity" é um Vector2. 
            _rigidbody.linearVelocity = _currentDirection * CurrentSpeed;

            // Debug visual
            Debug.DrawRay(contact.point, _currentDirection * 2f, Color.red, 2f);
        }

        public void ResetBall(bool isFullReset)
        {
            if (isFullReset)
            {
                CurrentSpeed = initialSpeed;
            }
            
            _currentDirection = new Vector2(1f, 1f).normalized;
            _rigidbody.linearVelocity = _currentDirection * initialSpeed;
        }
    }
}