using UnityEngine;

namespace MiniBreakout
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Paddle : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private float speed = 12f;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            float moveInput = Input.GetAxis("Horizontal");
            _rigidbody.linearVelocity = new Vector2(moveInput * speed, 0);
        }
    }
}