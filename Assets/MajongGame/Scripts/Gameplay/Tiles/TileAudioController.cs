using MajongGame.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MajongGame.Gameplay.Tiles
{
    public class TileAudioController
    {
        private readonly TileDTO _tile;
        private readonly AudioSource _audioSource;
        private Dictionary<string, AudioClip> _clips;
        private const string CLIPS_PATH = "Audio/Gameplay/TileClips/";

        public TileAudioController(TileDTO tile, AudioSource audioSource)
        {
            _tile = tile;
            _audioSource = audioSource;

            InitializeClips();
            Subscribe();
        }

        private void InitializeClips()
        {
            _clips = new Dictionary<string, AudioClip>()
            {
                { "TileTaked",  Resources.Load<AudioClip>(CLIPS_PATH + "TileTaked") },
            };
        }

        private void Subscribe()
        {
            _tile.Taked += OnTileTaked;
            _tile.Dead += Unsubscribe;
        }

        private void Unsubscribe()
        {
            _tile.Taked -= OnTileTaked;
            _tile.Dead -= Unsubscribe;
        }

        private void OnTileTaked()
        {
            Play("TileTaked", Random.Range(0.8f, 1f));
        }

        public void Play(string clipName, float pitch = 1f)
        {
            _audioSource.clip = _clips[clipName];
            _audioSource.pitch = pitch;
            _audioSource.Play();
        }
    }
}
