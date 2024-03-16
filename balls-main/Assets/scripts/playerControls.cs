using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class playerControls : MonoBehaviour
{
    public float carSpeed = 10.0f;
    public GameObject particles, mainCanvas ,joystickCanvas, volumePost, speedingEfct;
    public GameObject gameOverCanvas, enemyCircle;
    public Joystick joystick;
    public int lives = 2, levl;
    //public CinemachineCameraOffset cinemachine;
    public GameObject[] heart;
    private float movementx, movementY;
    private Vector2 currentDirection, touchStartPos, movement;
    public Vector2 initialEnemyPosition;
    private float initialY;
    TrailRenderer trail;
    public float sensitivity = 0.02f;
    public float speed, rotationSpeed;
    public float bounceForce = 5f; // Adjust this value to control the bounce force
    private Rigidbody2D rb;
    private Vector2 pose;
    private float finalpos, initialpos;
    public GameObject[] obstacles;
    private int skincolor;
    public float speedofBall = 5f;
    public float maxSpeed = 10f;
    public float acceleration = 2f;
    public Slider slider;
    public float vignetteIncreaseRate = 0.1f;
    private float virgenetteInitial, bloomInitial, chromaticInitial;
    private bool isTouchingScreen = false;
    public PostProcessVolume postProcessVolume;
    private Vignette vignette;
    private Bloom bloom;
    public Camera mainCamera;
    private Renderer[] renderers;

    // Intensity of the shake
    public float shakeIntensity = 1f;

    // Duration of the shake in seconds
    public float shakeDuration = 1f;

    public CinemachineImpulseSource impulseSource_speeding, impulseSource_out;

    void Start()
    {
        renderers = FindObjectsOfType<Renderer>();
        pose = transform.position;
        FindObjectOfType<AudioMnagaer>().Play("baground");
        initialpos = 390f;
        finalpos = 427f;
        skincolor = 4;
        rotationSpeed = 20f;
        levl = 0;
        slider.value = 1;
//        initialEnemyPosition = enemyCircle.transform.position;
        trail = gameObject.GetComponent<TrailRenderer>();
        impulseSource_speeding = GetComponent<CinemachineImpulseSource>();
        mainCanvas.SetActive(true);
        // Save the original position of the camera

        // Get the Rigidbody2D component attached to the GameObject
        rb = GetComponent<Rigidbody2D>();
        postProcessVolume = volumePost.GetComponent<PostProcessVolume>();

        if (postProcessVolume != null)
        {
            postProcessVolume.profile.TryGetSettings(out vignette);
            virgenetteInitial = vignette.intensity.value;
            postProcessVolume.profile.TryGetSettings(out bloom);
            bloomInitial = bloom.intensity.value;
        }
        else
        {
            Debug.LogError("PostProcessVolume or Vignette component not found.");
        }
        //InvokeRepeating("spawner", 2f, 1f);
    }

    void Update()
    {
        movementx = joystick.Horizontal;
        movementY = joystick.Vertical;
        // Iterate through all the active touches
        RenderObjectsInCamera();

        foreach (GameObject obstacle in obstacles)
        {
            obstacle.transform.Rotate(new Vector3(0f, 0f, 0.5f));
        }
        Move(movementx, movementY);
        power();
    }
    private void RenderObjectsInCamera()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        foreach (Renderer renderer in renderers)
        {
            // Check if the renderer's bounds intersect with the camera's view frustum
            if (GeometryUtility.TestPlanesAABB(planes, renderer.bounds))
            {
                // The object is within the camera's view, so render it
                renderer.enabled = true;
            }
            else
            {
                // The object is outside the camera's view, so don't render it
                renderer.enabled = false;
            }
        }

    }


    void Move(float x, float y)
    {
        // Apply movement using the Rigidbody2D component
        rb.velocity = new Vector2(x * speed, y * speed);
        currentDirection = Vector2.Lerp(currentDirection, movement.normalized, rotationSpeed * Time.fixedDeltaTime);
        float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle-90f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            Invoke("spawn", 1.2f);
            heart[--lives].SetActive(false);
            FindObjectOfType<AudioMnagaer>().Play("restart");
            camerashakemanager.instance.cameraShake(impulseSource_out);
            Instantiate(particles, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "finish1")
        {
            FindObjectOfType<AudioMnagaer>().Play("levelcmplt");
            transform.position = new Vector2(75.1f, 153f);
            levl = 1;
            bloom.intensity.value += 10f;
            Invoke("finish", 0.8f);
        }
        else if (collision.gameObject.tag == "finish2")
        {
            FindObjectOfType<AudioMnagaer>().Play("levelcmplt");
            transform.position = new Vector2(76.1f, 290f);
            levl = 2;
            Invoke("finish", 0.8f);
        }
        else if (collision.gameObject.tag == "finish3")
        {
            FindObjectOfType<AudioMnagaer>().Play("levelcmplt");
            transform.position = new Vector2(76.1f, 347f);
            levl = 3;
            Invoke("finish", 0.8f);
        }
        else if (collision.gameObject.tag == "finish4")
        {
            FindObjectOfType<AudioMnagaer>().Play("levelcmplt");
            transform.position = new Vector2(76.1f, 393f);
            levl = 4;
            Invoke("finish", 0.8f);
        }
        else if (collision.gameObject.tag == "finish5")
        {
            FindObjectOfType<AudioMnagaer>().Play("levelcmplt");
            SceneManager.LoadScene("levl2");
            PlayerPrefs.SetInt("level", 2);
            PlayerPrefs.Save();
            
        }
        else if (collision.gameObject.tag == "finish6")
        {
            FindObjectOfType<AudioMnagaer>().Play("levelcmplt");
            SceneManager.LoadScene("level3");
            PlayerPrefs.SetInt("level", 3);
            PlayerPrefs.Save();

        }
        else if (collision.gameObject.tag == "finish7")
        {
            FindObjectOfType<AudioMnagaer>().Play("levelcmplt");
            SceneManager.LoadScene("level4");
            PlayerPrefs.SetInt("level", 4);
            PlayerPrefs.Save();

        }
        else if(collision.gameObject.tag == "cheat_code_4")
        {
            SceneManager.LoadScene("level4");
        }
    }
    private void ReflectBounce(Vector2 normal)
    {
        transform.position = normal;
    }
    
    public void clickPause()
    {
        mainCanvas.SetActive(false);
        enemyCircle.SetActive(false);
        enemyCircle.transform.position = initialEnemyPosition;
        FindObjectOfType<AudioMnagaer>().Play("button_click");
        joystickCanvas.SetActive(true);
    }
    public void clickPlay()
    {
        mainCanvas.SetActive(true);
        enemyCircle.SetActive(true);
        FindObjectOfType<AudioMnagaer>().Play("button_click");
        joystickCanvas.SetActive(false);
    }
    void power()
    {
        if (Input.touchCount > 1 && slider.value > 0)
        {
            Touch touch = Input.GetTouch(1);

            if (touch.phase == TouchPhase.Began)
            {
                isTouchingScreen = true;
                FindObjectOfType<AudioMnagaer>().Play("power");
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isTouchingScreen = false;
            }
        }
        else if (Input.GetButton("Jump") && slider.value > 0)
        {
            isTouchingScreen = true;
        }
        else
        {
            isTouchingScreen = false;
        }

        UpdatePlayerSpeed();
        UpdateVignetteEffect();
    }
    private void UpdatePlayerSpeed()
    {
        if (isTouchingScreen)
        {
            rb.velocity = new Vector2(rb.velocity.x * acceleration + Time.deltaTime, rb.velocity.y*acceleration + Time.deltaTime);
            slider.value -= Time.deltaTime * 0.2f;
            // Increase the vignette intensity 
            // Clamp speed to maxSpeed
        }
        else
        {
            // Reset speed when not touching the screen
            slider.value += Time.deltaTime * 0.1f;
        }

        // Move the player or perform other speed-related actions here
        //transform.Translate(Vector3.forward * speedofBall * Time.deltaTime);
    }

    private void UpdateVignetteEffect()
    {
        if (isTouchingScreen && vignette != null)
        {
            // Clamp vignette intensity to a maximum value if needed
            vignette.intensity.value = Mathf.Min(vignette.intensity.value, 0.55f);
            speedingEfct.SetActive(true);
            camerashakemanager.instance.cameraShake(impulseSource_speeding);          // Increase the vignette intensity
            vignette.intensity.value += 0.05f;

            //chromatic.intensity.value = Mathf.Min(chromatic.intensity.value, 0.65f);
            //chromatic.intensity.value += 0.05f;

        }
        else if(Input.GetButton("Jump") && vignette != null)
        {
            // Clamp vignette intensity to a maximum value if needed
            vignette.intensity.value = Mathf.Min(vignette.intensity.value, 0.35f);
            speedingEfct.SetActive(true);
            camerashakemanager.instance.cameraShake(impulseSource_speeding);          // Increase the vignette intensity
                                                                                      //chromatic.intensity.value = Mathf.Min(chromatic.intensity.value, 0.65f);
                                                                                      //chromatic.intensity.value += 0.05f;
            vignette.intensity.value += 0.05f;
        } 
        else if (vignette != null)
        {
            // Reset vignette intensity when not touching the screen
            vignette.intensity.value = virgenetteInitial;
            //chromatic.intensity.value = chromaticInitial;
            //CinemachineVirtualcamera.m_Lens.FieldOfView = Mathf.Clamp(CinemachineVirtualcamera.m_Lens.FieldOfView - 0.2f,initialCvCamvalue, 84.4f);
            //CinemachineVirtualcamera.m_Lens.FieldOfView -= 0.2f;


            speedingEfct.SetActive(false);
        }
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void spawn()
    {
        if (levl == 0 && lives > 0)
        {
            gameObject.SetActive(true);
            transform.position = pose;
        }
        else if (levl == 1 && lives > 0)
        {
            gameObject.SetActive(true);
            transform.position = new Vector2(75.1f, 153f);
        }
        else if (levl == 2 && lives > 0)
        {
            gameObject.SetActive(true);
            transform.position = new Vector2(76.1f, 290f);
                    }
        else if (levl == 3 && lives > 0)
        {
            gameObject.SetActive(true);
            transform.position = new Vector2(76.1f, 347f);
                    }
        else if (levl == 4 && lives > 0)
        {
            gameObject.SetActive(true);
            transform.position = new Vector2(76.1f, 393f);
        }
        else
        {
            gameObject.SetActive(true);
            
            mainCanvas.SetActive(false);
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                DOTween.Kill(enemyCircle.transform, false);
                enemyCircle.transform.position = initialEnemyPosition;
                enemyCircle.SetActive(false);
            }
            gameOverCanvas.SetActive(true); 
        }
    }
    void finish()
    {
        bloom.intensity.value = bloomInitial;
    }
    public void watchVideo()
    {
        lives=1;
        Debug.Log(lives);
        heart[0].SetActive(true);
        mainCanvas.SetActive(true);
        if(SceneManager.GetActiveScene().buildIndex == 2) enemyCircle.SetActive(true);
        FindObjectOfType<AudioMnagaer>().Play("button_click");
        gameOverCanvas.SetActive(false);
        spawn();
    }
}
