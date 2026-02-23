using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public enum BgmClips
{
    StartScene, PlayScene,
}

public class SoundManager : MonoBehaviour
{
    string currentScene;
    BgmClips currentBgm;

    [SerializeField] Sound[] bgmClips;
    [SerializeField] AudioSource bgmSource;

    [SerializeField] Sound[] sfxClips;
    [SerializeField] AudioSource[] sfxSources;


    void Awake()
    {
        bgmSource = GetComponentInChildren<AudioSource>();
        Transform sfxPlayer = transform.GetChild(1);
        sfxSources = sfxPlayer.GetComponents<AudioSource>();

        for (int i = 0; i < sfxSources.Length; i++)
        {
            sfxSources[i].playOnAwake = false;
            sfxSources[i].loop = false;
        }
    }

    void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;

        if (System.Enum.TryParse(currentScene, out BgmClips bgmType))
        {
            if (currentBgm != bgmType)
            {
                PlayBGM(bgmType);
                currentBgm = bgmType;
            }
        }
    }

    void PlayBGM(BgmClips _clip)
    {
        int index = (int)_clip;

        if (index < bgmClips.Length)
        {
            bgmSource.clip = bgmClips[index].clip;
            bgmSource.Play();
        }
    }


}
