using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "Sound/SoundData")]
public class SoundData : ScriptableObject
{
    public string soundName;
    public AudioClip clip;
    public float volume = 1f;
    public float pitch = 1f;

    public string[] sceneNames;
}
