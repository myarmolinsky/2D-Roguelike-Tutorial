using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // this allows us to interact with Unity UI elements

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Slider healthSlider;
    public Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
