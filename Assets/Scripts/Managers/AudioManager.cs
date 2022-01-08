using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Instance
    // Criar uma instância do AudioManager
    private static AudioManager instance;
    // Permite que essa instância seja acessada
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned AudioManager",typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }
            return instance;
        }
        set
        {
            instance = value;
        
        }
        
    }
    #endregion

    #region Fields
    private AudioSource musicSource;
    private AudioSource musicSource2;
    private AudioSource sfxSource;
    // Ajuda a confirmar qual music source está sendo tocada
    private bool firstMusicSourceIsPlaying;
    #endregion



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // Da certeza de que esse objeto não será destruido
        DontDestroyOnLoad(gameObject);

        // Cria os audio sources e os salva
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource2 = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        // Cria a ação de loop nas músicas
        musicSource.loop = true;
        musicSource2.loop = true;

    }

    // Método que faz as músicas serem tocadas
    public void PlayMusic(AudioClip musicClip)
    {
        // Determina qual music source está ativo
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;

        activeSource.clip = musicClip;
        activeSource.volume = 0.5f;
        activeSource.Play();
    }
    
    // Serve para fazer uma transição com fade para a próxima música
    public void PlayMusicWithFade(AudioClip newclip, float transitionTime = 1.0f)
    {
        // Determina qual music source está ativo
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;
        StartCoroutine(UpdateMusicWithFade(activeSource, newclip, transitionTime));

    }

    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1.0f)
    {
        // Determina qual music source está ativo
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;
        AudioSource newSource = (firstMusicSourceIsPlaying) ? musicSource2 : musicSource;

        // Troca o music source
        firstMusicSourceIsPlaying = !firstMusicSourceIsPlaying;


        newSource.clip = musicClip;
        newSource.Play();
        StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTime));

    }

    // Corrotina que controla a transição com fade
    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        // Verifica se o music source está ativo e tocando
        if (!activeSource.isPlaying) activeSource.Play();

        float time = 0.0f;

        // Fade Out
        for (time = 0; time < transitionTime; time+= Time.deltaTime)
        {
            activeSource.volume = (1 - (time / transitionTime));
            yield return null;
        }

        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();


        // Fade in
        for (time = 0; time < transitionTime; time += Time.deltaTime)
        {
            activeSource.volume = (time / transitionTime);
            yield return null;
        }

    }
    private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float transitionTime)
    {
        float time = 0.0f;
        for (time = 0.0f; time <= transitionTime; time += Time.deltaTime )
        {
            original.volume = (1-(time / transitionTime));
            newSource.volume = (time / transitionTime);
            yield return null;
        }

        original.Stop();
    }

    // Método que faz os sound effects serem tocados
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip, volume);
    }


    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        musicSource2.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }






}






