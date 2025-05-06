using UnityEngine;

namespace MiniBreakout
{
    public class FireworkEffect : MonoBehaviour
    {
        private ParticleSystem fireworkParticleSystem;

        private void Start()
        {
            fireworkParticleSystem = GetComponent<ParticleSystem>();
            
            ParticleSystem.MainModule mainModule = fireworkParticleSystem.main;
            mainModule.startLifetime = 1f;
            mainModule.useUnscaledTime = true;
        }

        public void PlayEffect()
        {
            Debug.Log($"Fogos iniciaram!");
            fireworkParticleSystem.Play();
        }
    }
}
