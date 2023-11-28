using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleController : MonoBehaviour
{

    public CircleCollider2D circleCollider;

    [Header("Refernce of Scale")]
    GameObject FirstObject, SecondObject;
    public GameObject PivotOne;
    public GameObject PivotTwo;
    [SerializeField] private Vector2 diferenceOne;
    [SerializeField] private Vector2 diferenceTwo;
    private float FirstScale, SecondScale;

    private bool CanTake = true;
    public Transform enviroment;
    private Color colorToSave;
    public Color ColorToSwitch;
    private bool XtrueYfalse;
    private bool XtrueYfalse2;
    private bool horizontal;
    private bool vertical;
    private int Count;
    public AudioSource MusicInSwitching;
    public AudioSource SelectObject;
    public Color VericalColor, HorizontalColor;
    [Header("Canvas")]

    public GameObject imageRef1, imageRef2, viewPort;
    public RectTransform RT1, RT2;
    public float referenceSizeCanvasViewPort, RestartSize, BiggerNumber;
    public GameObject canvas1, canvas2, canvas3;
    public GameObject textAdvise;
    public GameObject Arrow1, Arrow2;

    private void Start()
    {
        referenceSizeCanvasViewPort = viewPort.GetComponent<RectTransform>().rect.width;
        RestartSize = imageRef1.GetComponent<RectTransform>().rect.width;
        RT1 = imageRef1.GetComponent<RectTransform>();
        RT2 = imageRef2.GetComponent<RectTransform>();
    }



    void Update()
    {
        SwitchPositionCollider();
        GetAxis();
    }

    void SwitchPositionCollider()
    {
        if (Input.GetKey(KeyCode.W))
        {
            horizontal = false;
            vertical = true;
            circleCollider.offset = new Vector2(0f, 0.4f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontal = true;
            vertical = false;

            circleCollider.offset = new Vector2(0.5f, -0.15f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            horizontal = true;
            vertical = false;
            circleCollider.offset = new Vector2(0.4f, -0.15f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            horizontal = false;
            vertical = true;
            circleCollider.offset = new Vector2(0, -0.65f);
        }
        else
        {
            horizontal = false;
            vertical = false;
            circleCollider.offset = new Vector2(0, 0);
        }
    }
    IEnumerator ScaleDownAnimation(float time, GameObject referenceObj, Vector2 OriginScale, Vector2 NewScale, bool XtrueYfalse)
    {

        CanTake = false;
        textAdvise.SetActive(true);
        float i = 0;
        float rate = 1 / time;

        Vector2 fromScale = OriginScale;
        Vector2 toScale = NewScale;
        while (i < 1)
        {
            if (XtrueYfalse)
            {
                i += Time.deltaTime * rate;
                referenceObj.transform.localScale = new Vector2(Mathf.Lerp(fromScale.x, toScale.x, i), referenceObj.transform.localScale.y);
          
                yield return 0;
            }
            else
            {
                i += Time.deltaTime * rate;
                referenceObj.transform.localScale = Vector2.Lerp(fromScale, toScale, i);
                referenceObj.transform.localScale = new Vector2(referenceObj.transform.localScale.x , (Mathf.Lerp(fromScale.y, toScale.y, i)));
              
                yield return 0;
            }

           
        }
        Count++;
        if (Count == 2)
        {
            textAdvise.SetActive(false);
            SecondObject.transform.parent = enviroment;
            FirstObject.transform.parent = enviroment;
            FirstObject = null;
            SecondObject = null;
            CleanSavedScales();
            CanTake = true;


        }
    }
    void SwitchScales()
    {
        // --------------------------------------------------first Obj-------------------------------------------------
        if (XtrueYfalse)
        {
            //----------------------this foor "X" -----------------------------------------------------------



            if (diferenceOne.x < 0)
            {
                PivotOne.transform.position = FirstObject.transform.position - new Vector3(FirstObject.transform.localScale.x / 2, 0, 0);
            }
            else
            {
                PivotOne.transform.position = FirstObject.transform.position + new Vector3(FirstObject.transform.localScale.x / 2, 0, 0);
            }
            PivotOne.transform.localScale = FirstObject.transform.localScale;
            FirstObject.transform.parent = PivotOne.transform;
            StartCoroutine(ScaleDownAnimation(1.0f, PivotOne, PivotOne.transform.localScale, new Vector2(SecondScale, PivotOne.transform.localScale.y), true));
            //PivotOne.transform.localScale = new Vector2(SecondScale, PivotOne.transform.localScale.y);
        }
        else
        {
            //----------------------this foor "Y" -----------------------------------------------------------

            if (diferenceOne.y < 0)
            {
                PivotOne.transform.position = FirstObject.transform.position - new Vector3(0, FirstObject.transform.localScale.y / 2, 0);
            }
            else
            {
                PivotOne.transform.position = FirstObject.transform.position + new Vector3(0, FirstObject.transform.localScale.y / 2, 0);
            }
            PivotOne.transform.localScale = FirstObject.transform.localScale;
            FirstObject.transform.parent = PivotOne.transform;
            StartCoroutine(ScaleDownAnimation(1.0f, PivotOne, PivotOne.transform.localScale, new Vector2(PivotOne.transform.localScale.x, SecondScale), false)); 
            //PivotOne.transform.localScale = new Vector2(PivotOne.transform.localScale.x, SecondScale);
        }
        //  -----------------------------------------------second Obj---------------------------------
        if (XtrueYfalse2)
        {
            //----------------------this foor "X" -----------------------------------------------------------


            if (diferenceTwo.x < 0)
            {
                PivotTwo.transform.position = SecondObject.transform.position - new Vector3(SecondObject.transform.localScale.x / 2, 0, 0);
            }
            else
            {
                PivotTwo.transform.position = SecondObject.transform.position + new Vector3(SecondObject.transform.localScale.x / 2, 0, 0);
            }

            PivotTwo.transform.localScale = SecondObject.transform.localScale;
            SecondObject.transform.parent = PivotTwo.transform;
            StartCoroutine(ScaleDownAnimation(1.0f, PivotTwo, PivotTwo.transform.localScale, new Vector2(FirstScale,PivotTwo.transform.localScale.x), true));
           // PivotTwo.transform.localScale = new Vector2(FirstScale, PivotTwo.transform.localScale.y);

        }
        else
        {
            //----------------------this foor "Y" -----------------------------------------------------------
            if (diferenceTwo.y < 0)
            {
                PivotTwo.transform.position = SecondObject.transform.position - new Vector3(0, SecondObject.transform.localScale.y / 2, 0);
            }
            else
            {
                PivotTwo.transform.position = SecondObject.transform.position + new Vector3(0, SecondObject.transform.localScale.y / 2, 0);
            }


            PivotTwo.transform.localScale = SecondObject.transform.localScale;
            SecondObject.transform.parent = PivotTwo.transform;
            StartCoroutine(ScaleDownAnimation(1.0f, PivotTwo, PivotTwo.transform.localScale, new Vector2(PivotTwo.transform.localScale.x, FirstScale), false));
            //PivotTwo.transform.localScale = new Vector2(PivotTwo.transform.localScale.x, FirstScale);
        }
        
      
        //FirstObject = null;
        //SecondObject = null;
        //FirstScale = 0;
        //SecondScale = 0;
        //RT1.sizeDelta = new Vector2(RestartSize, RestartSize);
        //RT2.sizeDelta = new Vector2(RestartSize, RestartSize);
        //canvas1.SetActive(false);
        //canvas2.SetActive(false);
       // canvas3.SetActive(false); 
      
       //PivotOne.transform.localScale = new Vector3(1, 1, 1);
       //PivotTwo.transform.localScale = new Vector3(1, 1, 1);

    }
    void GetAxis()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CleanSavedScales();
        }


        if (Input.GetKeyDown(KeyCode.E) && CanTake)
        {
            if (FirstScale == 0)
            {
                
                if (FirstObject)
                {
                    

                    if (horizontal)
                    {
                        if (colorToSave == HorizontalColor || colorToSave != VericalColor)
                        {
                            SelectObject.Play();
                            CreatingCanvas(FirstObject, FirstScale, XtrueYfalse);
                            FirstScale = FirstObject.transform.localScale.x;
                            XtrueYfalse = true;
                            canvas1.SetActive(true);
                            if (diferenceOne.x < 0)
                            {
                                Arrow1.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                            }
                            else
                            {
                                Arrow1.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                            }

                            return;
                        }
                      
                    }
                    else if (vertical)
                    {
                        if (colorToSave != HorizontalColor || colorToSave == VericalColor)
                        {
                            SelectObject.Play();

                            CreatingCanvas(FirstObject, FirstScale, XtrueYfalse);
                            FirstScale = FirstObject.transform.localScale.y;
                            XtrueYfalse = false;
                            canvas1.SetActive(true);
                            if (diferenceOne.y < 0)
                            {
                                Arrow1.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                            }
                            else
                            {
                                Arrow1.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                            }
                            return;
                        }
                          
                    }
                }
            }
            else if (SecondScale == 0)
            {
                
                if (SecondObject != FirstObject)
                {
                    if (SecondObject)
                    {
                        if (horizontal)
                        {
                            if (colorToSave == HorizontalColor || colorToSave != VericalColor)
                            {
                                SelectObject.Play();

                                CreatingCanvas(SecondObject, SecondScale, XtrueYfalse2);
                                SecondScale = SecondObject.transform.localScale.x;
                                XtrueYfalse2 = true;
                                canvas2.SetActive(true);
                                canvas3.SetActive(true);
                                if (diferenceTwo.x < 0)
                                {
                                    Arrow2.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                                }
                                else
                                {
                                    Arrow2.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                                }
                                return;
                                //  SwitchScales();
                            }

                        }
                        else if (vertical)
                        {
                            if (colorToSave != HorizontalColor || colorToSave == VericalColor)
                            {
                                SelectObject.Play();

                                CreatingCanvas(SecondObject, SecondScale, XtrueYfalse2);
                                SecondScale = SecondObject.transform.localScale.y;
                                XtrueYfalse2 = false;
                                canvas2.SetActive(true);
                                canvas3.SetActive(true);
                                if (diferenceTwo.y < 0)
                                {
                                    Arrow2.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                }
                                else
                                {
                                    Arrow2.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                                }
                                return;
                            }
                                                                           
                        }
                    }
                }
            }
            else
            {
                MusicInSwitching.Play();
                 SwitchScales();
            }
        }
    }

    void CleanSavedScales()
    {
            if(SecondObject)
            {
           // SecondObject.transform.parent = enviroment;

            }
            else if (FirstObject)
            {
           // FirstObject.transform.parent = enviroment;
            }

        // FirstObject = null;
        //SecondObject = null;
        Arrow1.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        Arrow2.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        FirstScale = 0;
            SecondScale = 0;
            RT1.sizeDelta = new Vector2(RestartSize, RestartSize);
            RT2.sizeDelta = new Vector2(RestartSize, RestartSize);
            canvas1.SetActive(false);
            canvas2.SetActive(false);
            canvas3.SetActive(false);
            PivotOne.transform.localScale = new Vector3(1, 1, 1);
            PivotTwo.transform.localScale = new Vector3(1, 1, 1);
              Count = 0;


    }

    void CreatingCanvas(GameObject RefObject, float Scale, bool Orientation)
    {
        float DividerProporcional = referenceSizeCanvasViewPort / BiggerNumber;
        if (FirstScale == 0)
        {
            RT1.sizeDelta = new Vector2(DividerProporcional * RefObject.transform.localScale.x, DividerProporcional * RefObject.transform.localScale.y);
        }
        else
        {
            RT2.sizeDelta = new Vector2(DividerProporcional * RefObject.transform.localScale.x, DividerProporcional * RefObject.transform.localScale.y);
        }



    }
    float GetTheBiggerNumber(GameObject RefObj)
    {
        if (RefObj.transform.localScale.x < RefObj.transform.localScale.y)
        {
            return RefObj.transform.localScale.y;
        }
        else
        {
            return RefObj.transform.localScale.x;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ActivableObj"))
        {
            colorToSave = collision.GetComponent<SpriteRenderer>().color;
            collision.GetComponent<SpriteRenderer>().color = ColorToSwitch;
          
          
            if (FirstScale == 0)
            {
                diferenceOne = collision.transform.position - transform.position;
                FirstObject = collision.transform.gameObject;

            }
            else if (SecondScale == 0)
            {
                diferenceTwo = collision.transform.position - transform.position;
                SecondObject = collision.transform.gameObject;      
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ActivableObj"))
        {
            collision.GetComponent<SpriteRenderer>().color = colorToSave;
            if (FirstScale == 0)
            {
                FirstObject = null;
            }
            else if (SecondScale == 0)
            {
                SecondObject = null;
            }
        }
    }




}
