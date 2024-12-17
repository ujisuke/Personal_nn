using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.Sounds
{
    public class SEPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip _attackSE;
        [SerializeField] private AudioClip _takeDamageSE;
        [SerializeField] private AudioClip _deadSE;
        [SerializeField] private AudioClip _instantiateDamageObjectSE;
        [SerializeField] private AudioClip _selectSE;
        [SerializeField] private AudioMixer _audioMixer;
        private static SEPlayer singletonInstance;
        public static SEPlayer SingletonInstance => singletonInstance;

        private void Awake()
        {
            singletonInstance = this;
        }

        public void PlayAttack(AudioSource audioSource)
        {
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(_attackSE);
        }

        public void PlayTakeDamage(AudioSource audioSource)
        {
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(_takeDamageSE);
        }

        public void PlayDead(AudioSource audioSource)
        {
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(_deadSE);
        }

        public void PlayInstantiateEnemy1DamageObject(AudioSource audioSource)
        {
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(_instantiateDamageObjectSE);
        }

        public void PlayInstantiateEnemy2DamageObject(AudioSource audioSource)
        {
            audioSource.pitch = 3f;
            audioSource.PlayOneShot(_instantiateDamageObjectSE);
        }

        public void PlayInstantiateEnemy3DamageObject(AudioSource audioSource)
        {
            audioSource.pitch = 2f;
            audioSource.PlayOneShot(_instantiateDamageObjectSE);
        }

        public void PlayInstantiateEnemy4DamageObject(AudioSource audioSource)
        {
            audioSource.pitch = 0.5f;
            audioSource.PlayOneShot(_instantiateDamageObjectSE);
        }

        public void PlaySelect(AudioSource audioSource)
        {
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(_selectSE);
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