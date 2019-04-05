using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float airTimeCur = 0f;
    public float airTime;
    public Vector3 moveDirection = Vector3.zero;
    public float wallJumpDir;
    public AudioClip footstepClip;
    public AudioSource footstepSound;
    public AudioClip jumpClip;
    public AudioSource jumpSound;
    public float standingStill;
    bool jumpSoundPlayed = false;
	private void Start()
	{
        footstepSound.clip = footstepClip;  
        jumpSound.clip = jumpClip;  
    }

	void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (((controller.isGrounded || GameObject.Find("Player").GetComponent<WallRun>().wallrunning)&& !footstepSound.isPlaying && (standingStill < -0.1 || standingStill > 0.1))){
            
            footstepSound.Play();
            footstepSound.pitch = Random.Range(0.9f, 1.1f);
        } else {
            
        }

        if (Input.GetKey(KeyCode.Q)){
            Time.timeScale = 0.2f;
        }
        else{
            Time.timeScale = 1f;
        }
        if ((GameObject.Find("Player").GetComponent<WallRun>().wallJumpR) || (GameObject.Find("Player").GetComponent<WallRun>().wallJumpL) && !controller.isGrounded){
            Debug.Log("footsteps on wall");
            if (GameObject.Find("Player").GetComponent<WallRun>().wallJumpR){
                wallJumpDir = -Input.GetAxis("Vertical");
            } else if (GameObject.Find("Player").GetComponent<WallRun>().wallJumpL){
                wallJumpDir = Input.GetAxis("Vertical");
            }
            if ((standingStill < -0.1 || standingStill > 0.1)&&!footstepSound.isPlaying){
                footstepSound.Play();
                footstepSound.pitch = Random.Range(0.9f, 1.1f);

            }
            if (!jumpSound.isPlaying && !jumpSoundPlayed){
                jumpSound.Play();
                jumpSound.pitch = Random.Range(0.9f, 1.1f);
                jumpSoundPlayed = true;
                StartCoroutine(JumpSoundReady());
            }
            moveDirection = new Vector3(wallJumpDir, Input.GetAxis("Vertical"), Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= 10;
        }
        else if (controller.isGrounded)
        {
            standingStill = Input.GetAxis("Horizontal") + Input.GetAxis("Vertical");
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            airTimeCur = 0;

        }
        Jump();
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void Jump(){
        if (Input.GetButton("Jump") && (airTimeCur < airTime))
        {
            CharacterController controller = GetComponent<CharacterController>();
            if (!jumpSound.isPlaying && controller.isGrounded){
                jumpSound.Play();
                jumpSound.pitch = Random.Range(0.95f, 1.05f);
            }

            moveDirection.y = jumpSpeed;
            airTimeCur += Time.deltaTime * 20;
        }
        if (!Input.GetButton("Jump"))
        {
            airTimeCur = airTime;
        }
    }

    IEnumerator JumpSoundReady()
    {
        yield return new WaitForSeconds(0.2f);
        jumpSoundPlayed = false;
    }
}


