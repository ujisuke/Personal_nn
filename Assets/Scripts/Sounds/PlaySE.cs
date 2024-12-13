using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.Sounds
{
    public class PlaySE : MonoBehaviour
    {
        [SerializeField] private AudioClip _takeDamageSE;
        [SerializeField] private AudioClip _deadSE;
        [SerializeField] private AudioClip _instantiateDamageObjectSE;
        [SerializeField] private AudioMixer _audioMixer;
        private static AudioSource _audioSource;
        public static PlaySE _SingletonInstance;

        private void Awake()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("SE")[0];
            _SingletonInstance = this;
        }

        public void PlayTakeDamage()
        {
            _audioSource.pitch = 1f;
            _audioSource.PlayOneShot(_takeDamageSE);
        }

        public void PlayDead()
        {
            _audioSource.pitch = 1f;
            _audioSource.PlayOneShot(_deadSE);
        }

        public void PlayInstantiateEnemyDamageObject()
        {
            _audioSource.PlayOneShot(_instantiateDamageObjectSE);
        }

        public void PlayInstantiateEnemy1DamageObject()
        {
            _audioSource.pitch = 1f;
            _audioSource.PlayOneShot(_instantiateDamageObjectSE);
        }

        public void PlayInstantiateEnemy2DamageObject()
        {
            _audioSource.pitch = 4f;
            _audioSource.PlayOneShot(_instantiateDamageObjectSE);
        }

        public void PlayInstantiateEnemy3DamageObject()
        {
            _audioSource.pitch = 2f;
            _audioSource.PlayOneShot(_instantiateDamageObjectSE);
        }

        public void PlayInstantiateEnemy4DamageObject()
        {
            _audioSource.pitch = 0.5f;
            _audioSource.PlayOneShot(_instantiateDamageObjectSE);
        }
    }
}