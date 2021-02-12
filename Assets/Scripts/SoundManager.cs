
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class SoundManager {

public static bool soundEnabled = true;

public static bool musicEnabled = true;
    public enum Sound {
        Lose,
        ButtonClick
    }

    public enum Track{
        Track1,Track2,Track3,Track4
    }

    public static void PlaySound(Sound sound) {
        GameObject gameObject = new GameObject("Sound", typeof(AudioSource));
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
    }

    private static AudioClip GetAudioClip(Sound sound) {
        foreach (ProjectAssets.SoundAudioClip soundAudioClip in ProjectAssets.GetInstance().soundAudioClipArray) {
            if (soundAudioClip.sound == sound) {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }

    public static AudioClip GetRandomBackgroundMusic() {
        Array values = Enum.GetValues(typeof(Track));
        System.Random random = new System.Random();
        Track track = (Track)values.GetValue(random.Next(values.Length));
        return GetBackgroundMusic(track);
    }

        private static AudioClip GetBackgroundMusic(Track track) {
        foreach (ProjectAssets.BackGroundMusic backgroundMusic in ProjectAssets.GetInstance().backGroundMusicArray) {
            if (backgroundMusic.track == track) {
                return backgroundMusic.audioClip;
            }
        }
        Debug.LogError("Sound " + track + " not found!");
        return null;
    }

    public static void PlayButtonClickSound(){
        if(soundEnabled){
        PlaySound(Sound.ButtonClick);
        }
    }

    public static void EnableMusic(){
//         if (GUI.Button(Rect(Screen.width-55,5,45,20),"Music"))
//    {
//      if (audio.isPlaying)
//        {
//        audio.Pause ();
//        }
//        else
//        {
//        audio.Play ();          
//        }
//     }
    }
   /* public static void AddButtonSounds(this Button button) {
        button.MouseOverOnceFunc += () => PlaySound(Sound.ButtonOver);
        button.ClickFunc += () => PlaySound(Sound.ButtonClick);
    }*/

}
