using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    Dictionary<string, SoundData> soundDict = new Dictionary<string, SoundData>();

    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource[] sfxSources;
    [SerializeField] SoundData[] soundData;
    int channelIndex;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);


        bgmSource = GetComponentInChildren<AudioSource>();
        Transform sfxPlayer = transform.GetChild(1);
        sfxSources = sfxPlayer.GetComponents<AudioSource>();

        for (int i = 0; i < sfxSources.Length; i++)
        {
            sfxSources[i].playOnAwake = false;
            sfxSources[i].loop = false;
        }

        foreach (SoundData data in soundData)
        {
            soundDict[data.soundName] = data;

            // 씬 이름으로도 등록 여러 씬들에서 같은 배경음악을 실행시킬때 필요
            foreach (string sceneName in data.sceneNames)
                soundDict[sceneName] = data;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneLoadedBGM(scene);
    }

    void SceneLoadedBGM(Scene scene)
    {
        PlayBGM(scene.name);
    }

    void PlayBGM(string clipName)
    {
        if (!soundDict.TryGetValue(clipName, out SoundData data)) return;
        if (bgmSource.clip == data.clip) return;

        bgmSource.clip = data.clip;
        bgmSource.volume = data.volume;
        bgmSource.pitch = data.pitch;
        bgmSource.Play();
    }

    public void PlaySFX(string clipName)
    {
        if (!soundDict.TryGetValue(clipName, out SoundData data)) return;
        for (int i = 0; i < sfxSources.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxSources.Length;
            if (sfxSources[loopIndex].isPlaying)
                continue;

            sfxSources[loopIndex].clip = data.clip;
            sfxSources[loopIndex].volume = data.volume;
            sfxSources[loopIndex].pitch = data.pitch;
            sfxSources[loopIndex].Play();
            channelIndex = (loopIndex + 1) % sfxSources.Length;
            return;
        }
    }


}
