using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public AudioSource audioSource1; // First AudioSource for one clip
    public AudioSource audioSource2; // Second AudioSource for the other clip
    public AudioClip defaultClip;
    public AudioClip otherClip1; // Clip 1 for certain levels
    public AudioClip otherClip2; // Clip 2 for certain levels

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Assuming you have added two AudioSources in the Inspector
            PlayMusic(audioSource1, defaultClip); // Play default clip on AudioSource1
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Register event
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unregister event
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check for the specific levels and play two clips
        if (scene.name == "EasyLevel_1" || scene.name == "EasyLevel_2" || scene.name == "EasyLevel_3")
        {
            PlayMusic(audioSource1, otherClip1); // Play first clip on AudioSource1
            PlayMusic(audioSource2, otherClip2); // Play second clip on AudioSource2
        }
        else
        {
            // Play default clip on AudioSource1 and stop AudioSource2
            PlayMusic(audioSource1, defaultClip);
            audioSource2.Stop(); // Stop second AudioSource if it's playing
        }
    }

    public void PlayMusic(AudioSource source, AudioClip clip)
    {
        if (source.clip != clip)
        {
            source.clip = clip;
            source.Play();
        }
    }
}
