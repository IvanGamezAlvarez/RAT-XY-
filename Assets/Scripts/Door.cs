using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public GameObject ProximityPromt;
    Vector2 posOffset = new Vector2();
    Vector2 tempPos = new Vector2();
    public float amplitude = 0.5f;
    public float frequency = 1f;
    public GameObject FadeOut;
    public AudioSource WinSound;
    public AudioSource Music;
    public int NextLevel;
    public bool PlayerInside;
    private void Start()
    {
        posOffset = ProximityPromt.transform.position;
    }

    private void Update()
    {
        Floating();
        if (PlayerInside)
        {
            if (Input.GetKey(KeyCode.E))
            {
                Music.Stop();
                WinSound.Play();
                FadeOut.SetActive(true);
                
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("player inside");
            PlayerInside = true;
          
            if (ProximityPromt.activeInHierarchy == false)
            {
                ProximityPromt.SetActive(true);
            }
           
           
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInside = false;
            ProximityPromt.SetActive(false);
        }
    }

    public void Floating()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        ProximityPromt.transform.position = tempPos;
    }
    public void SceneLoad()
    {
        SceneManager.LoadScene(NextLevel);
    }


}
