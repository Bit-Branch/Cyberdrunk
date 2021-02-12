using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectAssets : MonoBehaviour
{
       private static ProjectAssets instance;

    public static ProjectAssets GetInstance() {
        return instance;
    }

    private void Awake() {
        instance = this;
    }


    // public Sprite pipeHeadSprite;
    // public Transform pfPipeHead;
    // public Transform pfPipeBody;
    // public Transform pfGround;
    // public Transform pfCloud_1;
    // public Transform pfCloud_2;
    // public Transform pfCloud_3;

    public SoundAudioClip[] soundAudioClipArray;

    [Serializable]
    public class SoundAudioClip {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }

    public BackGroundMusic[] backGroundMusicArray;

    [Serializable]
    public class BackGroundMusic {
        public SoundManager.Track track;
        public AudioClip audioClip;
    }

}
