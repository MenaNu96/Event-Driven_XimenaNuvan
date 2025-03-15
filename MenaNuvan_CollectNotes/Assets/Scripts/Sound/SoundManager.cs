using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField]
    private SoundLibrary sfxlibrary;
    [SerializeField]
    public AudioSource Sfx2Dsource;
    public void awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    //If you want to add a sound effect like shoot or destroy
    //SoundManager.Instance.PlaySound3D("Name", transform.position);
    public void Playsound3D(AudioClip clip,Vector3 pos)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos);
        }
    }

    public void PlaySound3D(string soundName, Vector3 pos)
    {
        Playsound3D(sfxlibrary.GetClipsFromName(soundName), pos);
    }
    public void PlaySound2D(string soundName)
    {
        Sfx2Dsource.PlayOneShot(sfxlibrary.GetClipsFromName(soundName));
    }
   
}
