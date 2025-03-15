using UnityEngine;

[System.Serializable]
public struct SoundEffect
{
    public string GroupID;
    public AudioClip[] clips;
}
public class SoundLibrary : MonoBehaviour
{
    public SoundEffect[] soundEffects;

    public AudioClip GetClipsFromName (string name)
    {
        foreach (var soundEffect in soundEffects)
        {
            if (soundEffect.GroupID == name)
            {
                return soundEffect.clips[Random.Range(0, soundEffect.clips.Length)];
            }
        }
        return null;
    }
    

}
