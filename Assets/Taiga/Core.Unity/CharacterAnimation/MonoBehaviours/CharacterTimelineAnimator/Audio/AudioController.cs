using System.Collections.Generic;
using Taiga.Core.Unity.CharacterAnimation.Effect;
using Taiga.Core.Unity.UltimateAttack.Providers;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.Base.Audio
{
    public class AudioController
    {

        private BaseCharacterTimelineController _characterTimelineController;
        private Dictionary<string, AudioClip> _audioClipMap;

        public AudioController(BaseCharacterTimelineController characterTimelineController)
        {
            _characterTimelineController = characterTimelineController;
            _audioClipMap = new Dictionary<string, AudioClip>();
        }
        
        internal AudioSource CreateAudioSource()
        {
            var ultimateAttackController = _characterTimelineController.CharacterAnimator.ultimateAttackController.gameObject;
            return ultimateAttackController == null ? null : ultimateAttackController.AddComponent<AudioSource>();
        }

        internal void AddAudioClip(string key, string audioName)
        {
            if (key == null || audioName == null) return;

            var audioPreset = Contexts.sharedInstance.GetProvider<IGameAudioPreset>();
            var audioClip = audioPreset.GetAudioSourceByName(audioName);

            if (audioClip == null) return;
            if (!_audioClipMap.ContainsKey(key))
            {
                _audioClipMap.Add(key, audioClip);
            }
            else
            {
                _audioClipMap[key] = audioClip;
            }
        }

        internal void PlayAudioClip(string clipName)
        {
            if (!_audioClipMap.ContainsKey(clipName) || _characterTimelineController.AudioSource == null) return;
            var audioClip = _audioClipMap[clipName];
            _characterTimelineController.AudioSource.clip = audioClip;
            _characterTimelineController.AudioSource.Play();
        }
    }
}
