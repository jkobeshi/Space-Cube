using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    public ParticleSystem deathPart;
    public ParticleSystem winPart;
    public AudioClip WinAud;
    public AudioClip DieAud;
    public AudioClip JumpAud;
    AudioSource Aud;
    int numRooms;
    float movespeed = 3f;
    bool canJump = true;
    Vector2 curGravity = new Vector3(0, -9.81f, 0);
    float nextWarp = 50f;
    int curRoom = 0;
    bool resMov = false;
    GameObject[] Rooms;
    int SolidLayer = 1 << 3;
    void Start()
    {
        Aud = Camera.main.GetComponent<AudioSource>();
        Rooms = GameObject.FindGameObjectsWithTag("Room"); numRooms = Rooms.Length;
        Physics.gravity = curGravity;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position.y > 75) || (transform.position.y < -75))
            Kill();
        if (Physics.Raycast(transform.position + new Vector3(0.49f, 0, 0), curGravity, 0.51f, SolidLayer) ||
            Physics.Raycast(transform.position + new Vector3(-0.49f, 0, 0), curGravity, 0.51f, SolidLayer))
            canJump = true;
        else
            canJump = false;
        if (!resMov)
            Moving();
    }

    void Moving()
    { 
        Vector3 vel = rb.velocity;
        if (Input.GetKeyDown("z") && canJump)
        { Physics.gravity = -curGravity; curGravity = -curGravity; Aud.clip = JumpAud; Aud.Play(); }
        if (Input.GetKeyDown("x") && canJump)
        {
            Aud.clip = JumpAud; Aud.Play();
            transform.position += new Vector3(nextWarp, 0, 0); nextWarp = 50f; curRoom += 1;
            if(((curRoom + 1) % numRooms) == 0)
                nextWarp = -(numRooms - 1) * nextWarp;
        }

        if ((Input.GetAxisRaw("Horizontal") != 0) && 
            !Physics.Raycast(transform.position + new Vector3(0, 0.495f, 0), new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), 0.51f, SolidLayer) &&
            !Physics.Raycast(transform.position + new Vector3(0, -0.495f, 0), new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), 0.51f, SolidLayer) &&
            !Physics.Raycast(transform.position, new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), 0.51f, SolidLayer))
            vel.x = Input.GetAxisRaw("Horizontal") * movespeed;

        if ((Input.GetKeyDown("up") || Input.GetKeyDown("space")) && canJump)
        {
            canJump = false; vel.y = movespeed * 2f;
            AudioSource.PlayClipAtPoint(JumpAud, Camera.main.transform.position);
            if (Physics.gravity.y > 0)
            {
                vel.y = -vel.y;
            }
        }
        rb.velocity = vel;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal")
        { Destroy(other.gameObject); Win(); }
        if (other.tag == "Spike" && !GameControl.instance.gameOver)
            Kill();
    }
    void Kill()
    {
        resMov = true;
        Aud.clip = DieAud; Aud.Play();
        Instantiate(deathPart, transform.position, transform.rotation);
        GameControl.instance.gameOver = true;
        Destroy(gameObject);
    }
    void Win()
    {
        resMov = true;
        Aud.clip = WinAud; Aud.Play();
        Instantiate(winPart, transform.position, transform.rotation);
        GameControl.instance.Win = true;
    }
}
