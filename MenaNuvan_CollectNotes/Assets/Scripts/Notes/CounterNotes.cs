using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CounterNotes : MonoBehaviour
{
 public static CounterNotes Instance { get; private set; }
    public ItemsCollectionManager collectionManager;

    [Header("-------- Sound --------")]
    public AudioSource VoiceSound;

    [Header("-------- Read Note --------")]

    public GameObject MessagePanel;
    public bool inReach = false;
    public GameObject PickUpText;
    public GameObject NoteUi;
    [SerializeField] private GameObject[] Note3d;

    //[Header("-------- Counter --------")]
    //public int Note = 0;
    //public TextMeshProUGUI NoteCounter;
    private void Awake()
    {
        // Implement Singleton pattern
        // if (Instance != null && Instance != this)
        // {
        //    Destroy(gameObject);
        // }
        //  else
        //{
        //Instance = this;
        //DontDestroyOnLoad(gameObject); // Keep this object across scenes
                                       //   }

    }
    void Start()
    {
        //Reference the singleton instance of the ItemsCollectionManager
       if (collectionManager == null)
      {
           collectionManager = ItemsCollectionManager.Instance;
       }

}
    // Start is called before the first frame update

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && inReach)
        {
            PickUpText.SetActive(false);
            NoteUi.SetActive(false);
            MessagePanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            inReach = true;
           
        }
        

        else if (Input.GetKeyUp(KeyCode.C) && inReach)
        {

            //SoundManager.instance.PlaySound3D("Notes", transform.position);
            collectionManager.CollectNote(gameObject);
            NoteUi.SetActive(true);
            MessagePanel.SetActive(false);
            Destroy(gameObject);

        }
    }
    

void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUpText.SetActive(true);
            inReach = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUpText.SetActive(false);
            inReach = false;
        }
    }

}
