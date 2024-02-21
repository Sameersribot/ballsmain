using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class followobstacle : MonoBehaviour
{
    public GameObject player, obstaclesforjump;
    public GameObject[] obstacleslvl8;
    public GameObject[] obstaclesToScale;
    public float initialPos, finalPos, initialPoshort, finalPoshort;
    private float maxScale = 13.1f, minScale = 7.94f;
    private bool scaling = true;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(true);
        initialPos = obstacleslvl8[0].transform.position.x;
        initialPoshort = obstacleslvl8[2].transform.position.x;
        finalPoshort = obstacleslvl8[3].transform.position.x;
        finalPos = obstacleslvl8[1].transform.position.x;
        //InvokeRepeating("scaleObstacles", 3f, 2);
        //InvokeRepeating("descaleObjects", 3f, 2);
    }

    // Update is called once per frame
    void Update()
    {
        transform.DOMove(player.transform.position, 4.2f, false);

        for (int i = 0; i <= 1; i++)
        {
            if (obstacleslvl8[i].transform.position.x - initialPos < 0.1f)
            {
                obstacleslvl8[i].transform.DOMoveX(finalPos, 3f, false);
            }
            else if (finalPos - obstacleslvl8[i].transform.position.x < 0.1f)
            {
                obstacleslvl8[i].transform.DOMoveX(initialPos, 3f, false);
            }
        }
        for (int i = 2; i <= 3; i++)
        {
            if (obstacleslvl8[i].transform.position.x - initialPoshort < 0.1f)
            {
                obstacleslvl8[i].transform.DOMoveX(finalPoshort, 3f, false);
            }
            else if (finalPos - obstacleslvl8[i].transform.position.x < 0.1f)
            {
                obstacleslvl8[i].transform.DOMoveX(initialPoshort, 3f, false);
            }
        }
    }
}
