
using UnityEngine.Audio;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    

    GameObject soundGameObject;
    public static AudioManager instance;


    [System.NonSerialized]  public AudioSource soundAudioSource;
    [System.NonSerialized] public AudioSource trackAudioSource;
    public enum Sound
    { 
        Eagle,
        Opossum,
        Frog,
        EnemyDeath,
        Cherry,
        Gem,
        PlayerHurt,
        Clock
    }

    public enum Track
    {
       MainMenu,
       Play,
       GameOver
    }


    [System.Serializable]
    public struct SoundAudioClip
    {
        public Sound sound;
        public AudioClip audioClip;
    }

    [System.Serializable]
    public struct TrackAudioClip
    {
        public Track track;
        public AudioClip audioClip;
    }


    [SerializeField] SoundAudioClip[] soundAudioClips = null;
    [SerializeField] TrackAudioClip[] trackAudioClips = null; 
    void Awake()
    {
        //Singleton
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            soundGameObject = new GameObject("Sound");
            DontDestroyOnLoad(soundGameObject);
            soundAudioSource = soundGameObject.AddComponent<AudioSource>();
            trackAudioSource = soundGameObject.AddComponent<AudioSource>();
            trackAudioSource.loop = true;


        }
    }


 




    public void PlaySound(Sound sound)
    {
        soundAudioSource.PlayOneShot(GetSound(sound));
        
    }

    AudioClip GetSound(Sound sound)
    {
        foreach (SoundAudioClip soundAudioClip in soundAudioClips)
        {
            if (soundAudioClip.sound == sound)
                return soundAudioClip.audioClip;

        }

        return null;
    }

    public void PlayTrack(Track track)
    {

        trackAudioSource.Stop();
        trackAudioSource.clip = GetTrack(track);
        trackAudioSource.Play();

    }



    AudioClip GetTrack(Track track)
    {
        foreach (TrackAudioClip trackAudioClip in trackAudioClips)
        {
            if (trackAudioClip.track == track)
                return trackAudioClip.audioClip;

        }

        return null;
    }
}
