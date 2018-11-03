using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class BirdScript : MonoBehaviour
{

    public static BirdScript instance;

    [SerializeField]
    private Rigidbody2D myRigidbody;

    [SerializeField]
    private Animator anim;
   
    private float forwardSpeed = 3f;
    
    private float bounceSpeed = 4f;

    private bool didFlap;

    public bool isAlive;

    private Button flapButton;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip flapClip, pointClip, diedClip;

    public int Score;


    void Awake()
    {
       if (instance == null)
        {
            instance = this;
        }

        isAlive = true;
        Score = 0;

        flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button> ();
        flapButton.onClick.AddListener(() => FlapTheBird());
        SetCameraX();
    }

     void FixedUpdate()
    {
        if (isAlive)
        {
            Vector3 temp = transform.position;
            temp.x += forwardSpeed * Time.deltaTime;
            transform.position = temp;


            if (didFlap)
            {
                didFlap = false;
                myRigidbody.velocity = new Vector2(0, bounceSpeed);
                audioSource.PlayOneShot(flapClip);
                anim.SetTrigger("Flap");
            }

            if(myRigidbody.velocity.y >=0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                float angle = 0;
                angle = Mathf.Lerp(0, -90, -myRigidbody.velocity.y /10);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
    void SetCameraX()
    {
        CameraScript.offsetX = (Camera.main.transform.position.x - transform.position.x - 1f);
    }

    public float GetPositionX()
    {
        return transform.position.x;
    }

    public void FlapTheBird()
    {
        didFlap = true;
    }

    void OnCollisionEnter2D(Collision2D target)

    {
        if(target.gameObject.tag == "Ground" || target.gameObject.tag == "Pipe")
        {
            if(isAlive)
            {
                isAlive = false;
                anim.SetTrigger("Bird Died");
                Debug.Log("GAME_OVER");
                audioSource.PlayOneShot(diedClip);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D target)

    {
        if (target.tag == "PipeHolder")
        {
            Score++;
            audioSource.PlayOneShot(pointClip);
            Debug.Log(Score);
        }
    }




}
	
