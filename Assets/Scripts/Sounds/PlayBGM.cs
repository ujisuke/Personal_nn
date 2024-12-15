using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.Sounds
{
    public class PlayBGM : MonoBehaviour
    {
        [SerializeField] private AudioClip _lobbyBGM;
        [SerializeField] private AudioClip _battleRoomBGM;
        [SerializeField] private AudioMixer _audioMixer;
        private static AudioSource audioSource;
        public static PlayBGM SingletonInstance;

        private void Awake()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("BGM")[0];
            SingletonInstance = this;
        }

        public void PlayLobby()
        {
            if (audioSource.clip == _lobbyBGM) return;
            ChangeBGM(_lobbyBGM).Forget();
        }

        public void PlayBattleRoom()
        {
            if (audioSource.clip == _battleRoomBGM) return;
            ChangeBGM(_battleRoomBGM).Forget();
        }

        private static async UniTask ChangeBGM(AudioClip audioClip)
        {
            audioSource.outputAudioMixerGroup.audioMixer.GetFloat("BGMVolume", out float maxVolume);
            for (int i = 1; i < 10; i++)
            {
                audioSource.outputAudioMixerGroup.audioMixer.SetFloat("BGMVolume", (80f + maxVolume) * (1f - i / 10f) - 80f);
                await UniTask.Delay(TimeSpan.FromSeconds(0.01f));
            }
            audioSource.Stop();
            audioSource.clip = audioClip;
            audioSource.Play();
            for (int i = 1; i < 10; i++)
            {
                audioSource.outputAudioMixerGroup.audioMixer.SetFloat("BGMVolume", (80f + maxVolume) * (i / 10f) - 80f);
                await UniTask.Delay(TimeSpan.FromSeconds(0.01f));
            }
            audioSource.outputAudioMixerGroup.audioMixer.SetFloat("BGMVolume", maxVolume);
        }

        public float GetBGMVolume()
        {
            _audioMixer.GetFloat("BGMVolume", out float volume);
            return volume + 80f;
        }

        public void SetBGMVolume(float volume)
        {
            _audioMixer.SetFloat("BGMVolume", volume - 80f);
        }
    }
}