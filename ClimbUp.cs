using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbUp : MonoBehaviour {
    private bool canClimb;
    private Rigidbody rb;
    private PlayerController playerController;
    public Animator anim;
    public Camera regularCam;
    public Camera parkourCam;
    public bool camReset = false;

    public AudioClip climbClip;
    public AudioSource climbSound;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        //anim = gameObject.GetComponent<Animator>();
        climbSound.clip = climbClip;
	}
	
	// Update is called once per frame
	void Update () {
        camReset = false;
	}
    IEnumerator afterClimb(){
        yield return new WaitForSeconds(0.883333f);
        regularCam.transform.rotation = parkourCam.transform.rotation;
        transform.position = parkourCam.transform.position;
        playerController.moveDirection = new Vector3(0, 0, 0);
        regularCam.depth = 1;
        parkourCam.depth = 0;
        playerController.enabled = true;
        rb.isKinematic = false;
        anim.SetTrigger("Exit");
        camReset = true;
    }

	 void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Climb" && this.tag == "Player" && Input.GetKey(KeyCode.W))
        {
            canClimb = true;
            BeginClimb();
        }
	}
     void OnTriggerExit(Collider other)
	{
        canClimb = false;
	}

    void BeginClimb(){
        Debug.Log("climb");
        climbSound.Play();
        climbSound.pitch = Random.Range(0.9f, 1f);
        playerController.moveDirection = new Vector3(0, 0, 0);
        playerController.airTimeCur = 100f;
        parkourCam.transform.position = regularCam.transform.position;
        regularCam.depth = 0;
        parkourCam.depth = 1;
        playerController.enabled = false;
        rb.isKinematic = true;
        anim.SetTrigger("Climb");
        camReset = false;
        StartCoroutine(afterClimb());

    }
}
