using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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


    private int j;
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        mainCanvas.SetActive(true);
        PlayerPrefs.SetInt("audioSettings", 0);
        PlayerPrefs.SetInt("joystickAlignment", 0);
        //colors[0] = new Color(16, 31, 231);
        //colors[1] = new Color(255, 0, 194);
        //colors[2] = new Color(217, 222, 0);
        //colors[3] = new Color(87, 193, 7);
        Debug.Log(PlayerPrefs.GetInt("color"));
        
        PlayerPrefs.SetInt("color", 0);
        
        j = PlayerPrefs.GetInt("color");
        Debug.Log(j);
        initialColorchange();
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
        audioSource.Play();
        switch (PlayerPrefs.GetInt("level"))
        {
            case 1:
                SceneManager.LoadScene("SampleScene");
                break;
            case 2:
                SceneManager.LoadScene("levl2");
                break;
            case 3:
                SceneManager.LoadScene("level3");
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
        Invoke("playy", 0.3f);
    }
    public void settings()
    {
        audioSource.Play();
        mainCanvas.SetActive(false);
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
        if (PlayerPrefs.GetInt("joystickAlignment") == 0)
        {
            PlayerPrefs.SetInt("joystickAlignment", 1);
            leftJoystick.SetActive(false);
            rightJosytick.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("joystickAlignment", 0);
            leftJoystick.SetActive(true);
            rightJosytick.SetActive(false);
        }
    }
    public void audioSettings()
    {
        if (PlayerPrefs.GetInt("audioSettings") == 0)
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
    public void changeSkinRight()
    {
        j = ++j % 5;
        particleSystem.startColor = colors[j];
        hardLight.Color = colors[j];

        for (int i = 0; i < spritesColorChange.Length; i++)
        {
            spritesColorChange[i].color = colors[Mathf.Abs(j)];
        }
        for (int i = 0; i < img.Length; i++)
        {
            img[i].color = colors[j];
        }
    }
    public void changeSkinleft()
    {
        j = Mathf.Abs(--j % 5);
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
        
    }
    private void initialColorchange()
    {
        particleSystem.startColor = colors[j];
        hardLight.Color = colors[j];
        for (int i = 0; i < spritesColorChange.Length; i++)
        {
            spritesColorChange[i].color = colors[Mathf.Abs(j)];
        }
        for (int i = 0; i < img.Length; i++)
        {
            img[i].color = colors[j];
        }
        
    }
    public void credits()
    {
        settingsCanvas.SetActive(false);
        spritesColorChange[0].color = clrTransparent;
        creditsCanvas.SetActive(true);
    }
    public void creditsBack()
    {
        settingsCanvas.SetActive(true);
        spritesColorChange[0].color = colors[j];
        creditsCanvas.SetActive(false);
    }
}
