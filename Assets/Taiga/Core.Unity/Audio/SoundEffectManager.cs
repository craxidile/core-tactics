using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Taiga.Core.Unity.Audio
{
    public enum SoundEffect : byte
    {
        Click,
        CursorActiveOnFloor,
        Rollover,
    }

    public class SoundEffectManager : SerializedMonoBehaviour
    {

        internal static SoundEffectManager Instance { get; private set; }

        [SerializeField] private Dictionary<SoundEffect, AudioSource> audioDictionary;

        void Awake()
        {
            Instance = this;
        }

        internal void PlaySoundEffect(in SoundEffect soundEffect, in bool forcePlay = false)
        {
            if (!forcePlay && audioDictionary[soundEffect].isPlaying)
                return;

            audioDictionary[soundEffect].Play();
        }
    }
}
