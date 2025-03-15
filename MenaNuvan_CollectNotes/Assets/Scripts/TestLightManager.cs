using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestLightManager : MonoBehaviour
{
   // public KeyCode flashlightInput; //key
   // public KeyCode flashlightRech; //key
    public GameObject flashlight; //call the object 
    public float ActualEnergy = 100; //energy
    public float MaxEnergy = 100; //set the max energy that you can get
    public float Velocity = 8f; // the speed that you lose energy
    public float VelocityRech; //speed that you get energy
    public Light LigthFlash; //Call the light 

    public Image Battery;
  
    // Start is called before the first frame update
    void Start()
    {

    }
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X)) //active the light of the flashlight
        {
            LightSource();
            if (LigthFlash.enabled == false)
            { 
                LigthFlash.enabled = true;
            }
        }
        else if (LigthFlash.enabled == true) //turn off the flashlight
        {
            flashlight.SetActive(false);
            LigthFlash.enabled = false; //desactive object
        }

        if (LigthFlash.enabled == true) //each time that you active the flash light something happen, in this case the flashlight is goin to reduce for X Velocity that you set as float public float Velocity = 20f;
        {
            ActualEnergy -= Time.deltaTime * Velocity; // the actual energy is going to reduce (-=) per time.deltaTime *  public float Velocity = 20f;

            if (ActualEnergy <= 0) //if you Energy reach 0 What is going to happen? in this case just turn off, you can do scene manager.
            {
                ActualEnergy = 0; //set the actual energy
                LigthFlash.enabled = false; //off the ligth
                flashlight.SetActive(false); //off the game object
            }
        }

        else if (Input.GetKey(KeyCode.C)) //recover stament, each time that you press a key, you energy is going to recover.
        {
            ActualEnergy += Time.deltaTime * VelocityRech; // the actual energy is going to increase (+=) per time.deltaTime *  public float VelocityRech;

            if (ActualEnergy >= MaxEnergy)  //set the max energy that you can get
            { 
            ActualEnergy= MaxEnergy; //set the max energy that you can get
            }
        }

        Battery.fillAmount = ActualEnergy / MaxEnergy; // Fill the UI Manager

    }

    private void LightSource()
    {
        flashlight.SetActive(true); // call the flashligh object = active
    }

}
