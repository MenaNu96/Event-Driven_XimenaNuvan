using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory; //call  inventory
    public int X_Space; //hold X space
    public int x_start;
    public int y_start;
    public int Number_column;
    public int Y_Space; //hold Y space
    Dictionary<inventorySlot, GameObject> itemsDisplayer = new Dictionary<inventorySlot, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
       UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (itemsDisplayer.ContainsKey(inventory.Container[i]))
            {
                itemsDisplayer[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].Amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i); //Set the actual position
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].Amount.ToString("n0"); //Call quantity
                itemsDisplayer.Add(inventory.Container[i], obj);
            }
        
        }
    }

    //Loop items in the inventory
    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i); //Set the actual position
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].Amount.ToString("n0"); //Call quantity
            itemsDisplayer.Add(inventory.Container[i], obj);
        }
    }

    public Vector3 GetPosition(int i) //Position in the inventory
    {
        //Set objects in a grid
        return new Vector3(x_start + ( X_Space * (i % Number_column)), y_start + (-Y_Space * (i/ Number_column)), 0f);
    }

}
