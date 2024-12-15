using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.Sounds
{
    public class PlaySE : MonoBehaviour
    {
        [SerializeField] private AudioClip _attackSE;
        [SerializeField] private AudioClip _takeDamageSE;
        [SerializeField] private AudioClip _deadSE;
        [SerializeField] private AudioClip _instantiateDamageObjectSE;
        [SerializeField] private AudioMixer _audioMixer;
        private static AudioSource audioSource;
        public static PlaySE SingletonInstance;

        private void Awake()
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            SingletonInstance = this;
        }

        public void PlayAttack()
        {
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(_attackSE);
        }

        public void PlayTakeDamage()
        {
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(_takeDamageSE);
        }

        public void PlayDead()
        {
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(_deadSE);
        }

        public void PlayInstantiateEnemyDamageObject()
        {
            audioSource.PlayOneShot(_instantiateDamageObjectSE);
        }

        public void PlayInstantiateEnemy1DamageObject()
        {
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(_instantiateDamageObjectSE);
        }

        public void PlayInstantiateEnemy2DamageObject()
        {
            audioSource.pitch = 4f;
            audioSource.PlayOneShot(_instantiateDamageObjectSE);
        }

        public void PlayInstantiateEnemy3DamageObject()
        {
            audioSource.pitch = 2f;
            audioSource.PlayOneShot(_instantiateDamageObjectSE);
        }

        public void PlayInstantiateEnemy4DamageObject()
        {
            audioSource.pitch = 0.5f;
            audioSource.PlayOneShot(_instantiateDamageObjectSE);
        }

        public float GetSEVolume()
        {
            _audioMixer.GetFloat("SEVolume", out float volume);
            return volume + 80f;
        }

        public void SetSEVolume(float volume)
        {
            _audioMixer.SetFloat("SEVolume", volume - 80f);
        }
    }
}