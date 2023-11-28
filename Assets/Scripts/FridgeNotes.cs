using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeNotes : MonoBehaviour
{
    public GameObject ProximityPromt;
    Vector2 posOffset = new Vector2();
    Vector2 tempPos = new Vector2();
    public float amplitude = 0.5f;
    public float frequency = 1f;
    public GameObject Note;
    public AudioSource InteractSound;
    public bool PlayerInside;
    public bool OpenNote;
    private void Start()
    {
        posOffset = ProximityPromt.transform.position;
    }
    private void Update()
    {
       

        Floating();
        if (PlayerInside)
        {
            if (OpenNote == false)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    InteractSound.Play();
                    Note.SetActive(true);
                    OpenNote = true;

                }
            }
            else if (OpenNote)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Note.SetActive(false);
                    OpenNote = false;
                }
            }
        }
       

    }

    public void Floating()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        ProximityPromt.transform.position = tempPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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
            Note.SetActive(false);
            OpenNote = false;
        }
    }

   
}
