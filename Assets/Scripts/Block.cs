using UnityEngine;

namespace MiniBreakout
{
    public class Block : MonoBehaviour
    {
        public int Points { get; private set; } = 1;

        public void DisableBlock()
        {
            gameObject.SetActive(false);
        }
    }
}