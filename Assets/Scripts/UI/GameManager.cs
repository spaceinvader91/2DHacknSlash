using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    //Used for singleton
    public static GameManager GM;

    //Create Keycodes that will be associated with each of our commands.
    //These can be accessed by any other script in our game
    public KeyCode forward { get; set; }
    public KeyCode backward { get; set; }
    public KeyCode left { get; set; }
    public KeyCode right { get; set; }


    public KeyCode aButton { get; set; }
    public KeyCode bButton { get; set; }
    public KeyCode xButton { get; set; }
    public KeyCode yButton { get; set; }

    public KeyCode lbButton { get; set; }
    public KeyCode rbButton { get; set; }

    public KeyCode rsClick { get; set; }
    public KeyCode lsClick { get; set; }


    public KeyCode startButton { get; set; }
    public KeyCode selectButton { get; set; }
    



    

   



    void Awake()
    {
        //Singleton pattern
        if (GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else if (GM != this)
        {
            Destroy(gameObject);
        }
        /*Assign each keycode when the game starts.
         * Loads data from PlayerPrefs so if a user quits the game,
         * their bindings are loaded next time. Default values
         * are assigned to each Keycode via the second parameter
         * of the GetString() function
         */
         //Xbox 360 Controls
        
        aButton = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("aButton", "JoystickButton0"));
        bButton = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("bButton", "JoystickButton1"));
        xButton = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("xButton", "JoystickButton2"));
        yButton = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ybutton", "JoystickButton3"));
      


        lbButton = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("lbButton", "JoystickButton4"));
        rbButton = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rbButton", "JoystickButton5"));

        rsClick = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rsClick", "JoystickButton9"));
        lsClick = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("lsClick", "JoystickButton8"));

        startButton = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("startButton", "JoystickButton7"));
        selectButton = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("selectButton", "JoystickButton6"));



        //////
        ///

        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "W"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));


    }

    void Start()
    {

    }

    void Update()
    {



    }
}