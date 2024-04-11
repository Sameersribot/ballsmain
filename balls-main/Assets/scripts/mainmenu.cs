using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.InteropServices;


public class mainmenu : MonoBehaviour
{
    private AudioSource audioSource;
    public float rotateSpeed; // Duration of the pressing effect
    public GameObject background, mainCanvas, settingsCanvas, leftJoystick, rightJosytick;
    public GameObject audiOff,audiOn, creditsCanvas;
    public Color[] colors;
    public Color clrTransparent;
    public SpriteRenderer[] spritesColorChange;
    public Image[] img;
    public ParticleSystem particleSystem;
    public HardLight2D hardLight;
    string packageName = "com.gaskit.glowduct";

    public void OpenRateUsPage()
    {
#if UNITY_ANDROID
        // Construct the URI to open the Play Store page
        string url = "market://details?id=" + packageName;

        // Open the Play Store page
        Application.OpenURL(url);
#else
        Debug.Log("Rate Us feature is only available on Android devices.");
#endif
    }

    private int j;

    string subject = "Hey I am playing this awesome new game called glowduct,do give it try and enjoy \n";
    string body = "https://play.google.com/store/apps/details?id=com.gaskit.glowduct";

    public void OnAndroidTextSharingClick()
    {
        //FindObjectOfType<AudioManager>().Play("Enter");
        StartCoroutine(ShareAndroidText());
    }

    IEnumerator ShareAndroidText()
    {
        yield return new WaitForEndOfFrame();
        //execute the below lines if being run on a Android device
        //Reference of AndroidJavaClass class for intent
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        //Reference of AndroidJavaObject class for intent
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        //call setAction method of the Intent object created
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        //set the type of sharing that is happening
        intentObject.Call<AndroidJavaObject>("setType", "text/plain");
        //add data to be passed to the other activity i.e., the data to be sent
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "TITLE");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body);
        //get the current activity
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        //start the activity by sending the intent data
        AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via");
        currentActivity.Call("startActivity", jChooser);
    }


    private void Awake()
    {
        changeColorStart();
    }
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        mainCanvas.SetActive(true);
        //colors[0] = new Color(16, 31, 231);
        //colors[1] = new Color(255, 0, 194);
        //colors[2] = new Color(217, 222, 0);
        //colors[3] = new Color(87, 193, 7);
        Debug.Log(PlayerPrefs.GetInt("color"));
        
        
        j = PlayerPrefs.GetInt("color");
        Debug.Log(j);
        
        // Attach the OnClick method to each button
        //foreach (Button button in buttonsToScale)
        //{
        //  button.onClick.AddListener(() => OnButtonClick(button));
        //}

    }

    // Update is called once per frame
    void Update()
    {
        background.transform.Rotate(0, 0, rotateSpeed);        // Check if we are currently scaling
    }
    public void playy()
    {
        //audioSource.Play();
        Debug.Log("clicked");
        switch (PlayerPrefs.GetInt("level"))
        {
            case 1:
                SceneManager.LoadScene("SampleScene");
                break;
            case 2:
                SceneManager.LoadScene("levl2");
                break;
            case 3:
                SceneManager.LoadScene("SampleScene");
                break;
            case 4:
                SceneManager.LoadScene("level4");
                break;
            default:
                SceneManager.LoadScene("SampleScene");
                break;
        }
    }
    public void clickPlay()
    {
        audioSource.Play();
        PlayerPrefs.SetInt("color", j);
        playy();
    }
    public void settings()
    {
        audioSource.Play();
        mainCanvas.SetActive(false);
        if (PlayerPrefs.GetInt("audioSettings") == 1)
        {
            audiOff.SetActive(false);
            audiOn.SetActive(true);
        }
        else
        {
            audiOff.SetActive(true);
            audiOn.SetActive(false);
        }
        hardLight.gameObject.SetActive(false);
        settingsCanvas.SetActive(true);
    }

    public void exitSettings()
    {
        audioSource.Play();
        hardLight.gameObject.SetActive(true);
        mainCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
    }
    public void joystick()
    {
        audioSource.Play();
        joystickSettr();
        
    }
    public void audioSettings()
    {
        audioSource.Play();
        checkAudioPref();
        
    }
    public void changeSkinRight()
    {
        audioSource.Play();
        j = ++j % 5;
        skinChanger(j);
    }
    public void changeSkinleft()
    {
        audioSource.Play();
        j = (j - 1 + 5) % 5;

        particleSystem.startColor = colors[j];
        hardLight.Color = colors[j];

        for (int i = 0; i < spritesColorChange.Length; i++)
        {
            spritesColorChange[i].color = colors[j];
        }
        for (int i = 0; i < img.Length; i++)
        {
            img[i].color = colors[j];
        }
        /*Debug.Log(j);
        Debug.Log(Mathf.Abs(j));*/
    }
    
    public void credits()
    {
        audioSource.Play();

        settingsCanvas.SetActive(false);
        spritesColorChange[0].color = clrTransparent;
        creditsCanvas.SetActive(true);
    }
    public void creditsBack()
    {
        audioSource.Play();

        settingsCanvas.SetActive(true);
        spritesColorChange[0].color = colors[j];
        creditsCanvas.SetActive(false);
    }
    public void checkAudioPref()
    {
        if (PlayerPrefs.GetInt("audioSettings") != 1)
        {
            PlayerPrefs.SetInt("audioSettings", 1);
            audiOff.SetActive(false);
            audiOn.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("audioSettings", 0);
            audiOff.SetActive(true);
            audiOn.SetActive(false);
        }
    }
    void changeColorStart()
    {
        if (PlayerPrefs.GetInt("audioSettings") != 1)
        {
            audiOff.SetActive(false);
            audiOn.SetActive(true);
        }
        else
        {
            audiOff.SetActive(true);
            audiOn.SetActive(false);
        }
        skinChanger(PlayerPrefs.GetInt("color"));
        if (PlayerPrefs.GetInt("joystickAlignment") != 1)
        {
            leftJoystick.SetActive(false);
            rightJosytick.SetActive(true);
        }
        else
        {
            leftJoystick.SetActive(true);
            rightJosytick.SetActive(false);
        }
    }
    void skinChanger(int indx)
    {
        particleSystem.startColor = colors[indx];
        hardLight.Color = colors[indx];

        for (int i = 0; i < spritesColorChange.Length; i++)
        {
            spritesColorChange[i].color = colors[Mathf.Abs(indx)];
        }
        for (int i = 0; i < img.Length; i++)
        {
            img[i].color = colors[indx];
        }
    }
    void joystickSettr()
    {
        if (PlayerPrefs.GetInt("joystickAlignment") != 1)
        {
            PlayerPrefs.SetInt("joystickAlignment", 1);
            leftJoystick.SetActive(true);
            rightJosytick.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("joystickAlignment", 0);
            leftJoystick.SetActive(false);
            rightJosytick.SetActive(true);
        }
    }
}
