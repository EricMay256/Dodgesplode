using UnityEngine;

namespace BearFalls
{
  public class AudioManager : MonoBehaviour
  {
    #region Declarations
    public static AudioManager Instance;
    AudioSource _musicSource;
    AudioSource[] _sfxSources;
    int _nextSfxSourceIndex = 0;
    #endregion
    #region Public Methods
    public void PlayMusic(AudioClip clip)
    {
      if (_musicSource.clip != clip)
      {
        _musicSource.clip = clip;
        if (_musicSource.clip != null)
        {
          _musicSource.Play();
        }
        else
        {
          _musicSource.Stop();
        }
      }
    }

    public void PlaySFX(AudioClip clip)
    {
      if (clip == null) return;
      _sfxSources[_nextSfxSourceIndex].clip = clip;
      _sfxSources[_nextSfxSourceIndex].Play();
      _nextSfxSourceIndex = (_nextSfxSourceIndex + 1) % _sfxSources.Length;
    }
    #endregion
    #region Monobehaviours
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
      if (Instance == null)
      {
        Instance = this;
        //DontDestroyOnLoad(gameObject);
      }
      else
      {
        Destroy(gameObject);
      }
      _musicSource = transform.GetChild(0).GetComponent<AudioSource>();
      _sfxSources = transform.GetChild(1).GetComponents<AudioSource>();
    }
    #endregion
  }
}