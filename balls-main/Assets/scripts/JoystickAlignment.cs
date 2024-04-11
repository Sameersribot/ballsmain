using UnityEngine;

public class JoystickAlignment : MonoBehaviour
{
    public RectTransform joystick; // Reference to the RectTransform of the joystick
    public bool isLeftAligned; // Initial alignment is left

    // Start is called before the first frame update
    void Start()
    {
        isLeftAligned = PlayerPrefs.GetInt("joystickAlignment") == 0;
        // Set initial alignment
        SetAlignment(isLeftAligned);
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Function to set alignment
    void SetAlignment(bool isLeft)
    {
        // Get the anchorMin and anchorMax of the joystick RectTransform
        Vector2 anchorMin = joystick.anchorMin;
        Vector2 anchorMax = joystick.anchorMax;
        if (isLeft)
        {
            // Set alignment for left
            anchorMin.x = 0f;
            anchorMax.x = 0.5f;
        }
        else
        {
            // Set alignment for right
            anchorMin.x = 0.66f;
            anchorMax.x = 1.16f;
        }

        // Apply the new anchor values
        joystick.anchorMin = anchorMin;
        joystick.anchorMax = anchorMax;
    }
}
