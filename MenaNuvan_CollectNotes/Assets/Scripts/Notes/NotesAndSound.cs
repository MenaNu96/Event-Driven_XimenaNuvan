using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class NotesAndSound : MonoBehaviour
{
    public static NotesAndSound Instance { get; private set; }
    public ItemsCollectionManager collectionManager;
    public InventoryObject inventory;
   

    [Header("-------- Sound --------")]
    public AudioSource VoiceSound;

    [Header("-------- Read Note --------")]

    public GameObject MessagePanel;
    public bool inReach = false;
    public GameObject PickUpText;
    public GameObject NoteUi;
    
   // [SerializeField] private GameObject[] Note3d;

    private void Awake()
    {
        //Instance = this;
        //DontDestroyOnLoad(gameObject); // Keep this object across scenes
        //                               //   }

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
            NoteUi.SetActive(true);
            MessagePanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
           var item = GetComponent<Item>();
            if (item)
            {
                inventory.AddItem(item.item, 1);
            }
            Debug.Log("NoItem");
        }


        else if (Input.GetKeyUp(KeyCode.Escape) && inReach)
        {
            VoiceSound.Play();
            collectionManager.CollectNote(gameObject);
            NoteUi.SetActive(false);
            MessagePanel.SetActive(false);
            Destroy(gameObject);

            Debug.Log("NoSound");

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


