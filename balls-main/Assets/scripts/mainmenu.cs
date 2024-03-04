using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    private AudioSource audioSource;
    public float rotateSpeed; // Duration of the pressing effect
    public GameObject background;


    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

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
        Invoke("playy", 0.3f);
    }

}
