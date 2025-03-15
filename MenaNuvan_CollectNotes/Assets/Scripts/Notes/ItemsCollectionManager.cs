using BehaviourMachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ItemsCollectionManager : MonoBehaviour
{
    // Singleton instance
    public static ItemsCollectionManager Instance { get; private set; }

    public List<GameObject> Notes;
    public int CurrentNote = 0;
    public GameObject Monster;
    public Animator anim;
   
    //public GameObject ChangeSceneWin;
    //public GameObject RUN;
    //public GameObject Enemy;
    //public GameObject PosPlayer;


    [Header("-------- Counter --------")]
    //public int NoteText = 0;
    public TextMeshProUGUI NoteCounter;
    public GameObject Win;
   

    [Header("-------- Instructions --------")]
    public GameObject Instructions1;
    

    private void Awake()
    {
        // Implement Singleton pattern
        // if (Instance != null && Instance != this)
        // {
        //    Destroy(gameObject);
        // }
        //  else
        //{
        // Instance = this;
        // DontDestroyOnLoad(gameObject); // Keep this object across scenes
        //   }

    }

    private void Start()
    {
        Win.SetActive(false);
        HideAllNotes();
        ShowNote(CurrentNote);
       StartCoroutine (Instructions());
        Monster.SetActive(false );
        MusicManager.instance.PlayMusic("Forest");
       
    }

   
    public IEnumerator Instructions()
    {
        yield return new WaitForSeconds(10);
        Instructions1.SetActive(false);
    }
    public IEnumerator End()
    {
        yield return new WaitForSeconds(5);
        Win.SetActive(true);
    }

    public IEnumerator Screamer()
    {
        yield return new WaitForSeconds(1);
        Monster.SetActive(false);
    }
    public void CollectNote(GameObject Note)
    {
        if (Notes[CurrentNote] == Note)
        { 
            HideNote(CurrentNote);
            CurrentNote++;
            //NoteText++;
            NoteCounter.text = CurrentNote.ToString();

            if (CurrentNote < Notes.Count)
            {
                ShowNote(CurrentNote);
            }
            if (CurrentNote == 3)
            {
                StartCoroutine(Screamer());
                anim.Play("Scream");
                Monster.SetActive(true);
            }
            if (CurrentNote == 4)
            {
                StartCoroutine(End());
                Debug.Log("All notes collected!");
                MusicManager.instance.PlayMusic("End");
                Debug.Log("Nosong");
                
            }
        }
        else
        {
            Debug.Log("Incorrect Note!");
        }
    }

   

    private void HideAllNotes()
    {
        foreach (var note in Notes)
        {
            note.SetActive(false);
        }
    }

    private void ShowNote(int index)
    {
        if (index >= 0 && index < Notes.Count)
        {
            Notes[index].SetActive(true);
        }
    }

    private void HideNote(int index)
    {
        if (index >= 0 && index < Notes.Count)
        {
            Notes[index].SetActive(false);
        }
    }

   
}
