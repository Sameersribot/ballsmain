using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class uiandcolors : MonoBehaviour
{
    public Color[] colors;
    public HardLight2D harder;
    public ParticleSystem explosionParticle, randomParticle;
    public SpriteRenderer circleSprite;
    public Gradient gradient;
    public Image[] btns;
    
    private int colorIndex, levelIndex, currentSceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        colorIndex = PlayerPrefs.GetInt("color");
        levelIndex = PlayerPrefs.GetInt("level");
        circleSprite.color = colors[colorIndex];
        explosionParticle.startColor = colors[colorIndex];
        randomParticle.startColor = colors[colorIndex];
        harder.Color = colors[colorIndex];
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        for (int i = 0; i<btns.Length; i++)
        {
            btns[i].color = colors[colorIndex];
        }
    }
    public void prevLevel()
    {
        SceneManager.LoadSceneAsync(--currentSceneIndex);
    }
    public void nextLevel()
    {
        if (levelIndex > currentSceneIndex)
        {
            SceneManager.LoadSceneAsync(++currentSceneIndex);
        }
    }
}
