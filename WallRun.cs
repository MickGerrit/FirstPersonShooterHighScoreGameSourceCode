using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour {
    private bool isWallR = false;
    private bool isWallL = false;
    private RaycastHit hitR;
    private RaycastHit hitL;
    private int jumpCount = 0;
    private Rigidbody rb;
    public float canRun = 0;
    public float wallRunLimit = 1;
    public bool wallJumpR = false;
    public bool wallJumpL = false;
    public bool wallrunning = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

	private void Awake()
	{
        GameObject.Find("ColRight").GetComponent<ColDetect>().collided = false;
        GameObject.Find("ColLeft").GetComponent<ColDetect>().collided = false;
	}

	// Update is called once per frame
	void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (GameObject.Find("ColRight").GetComponent<ColDetect>().collided && Input.GetKey(KeyCode.Space) && canRun <= wallRunLimit)
        {
            isWallR = true;
            isWallL = false;
            canRun += 1 * Time.deltaTime;
            gameObject.GetComponent<PlayerController>().moveDirection.y = 0f;
            wallrunning = true;
            StartCoroutine(afterRun());
        }
        if (GameObject.Find("ColLeft").GetComponent<ColDetect>().collided && Input.GetKey(KeyCode.Space) && canRun <= wallRunLimit)
        {
            isWallR =   false;
            isWallL = true;
            canRun += 1 * Time.deltaTime;
            gameObject.GetComponent<PlayerController>().moveDirection.y = 0f;
            wallrunning = true;
            StartCoroutine(afterRun());
        }
        if (controller.isGrounded){
            Debug.Log("isGrounded");
            canRun = 0;
        }

        if (Input.GetButtonUp("Jump") && GameObject.Find("ColRight").GetComponent<ColDetect>().collided)
        {
            //jump
            wallJumpR = true;
        }
        if (wallJumpR == true){
            GameObject.Find("ColRight").GetComponent<ColDetect>().collided = false;
            //gameObject.GetComponent<PlayerController>().Jump();
            StartCoroutine(CanWallJumpR());
        }
        if (Input.GetButtonUp("Jump") && GameObject.Find("ColLeft").GetComponent<ColDetect>().collided)
        {
            //jump
            wallJumpL = true;
        }
        if (wallJumpL == true)
        {
            GameObject.Find("ColLeft").GetComponent<ColDetect>().collided = false;
            //gameObject.GetComponent<PlayerController>().Jump();
            StartCoroutine(CanWallJumpL());
        }
    }
        

        IEnumerator afterRun(){
            yield return new WaitForSeconds(0.5f);
            isWallL = false;
            isWallR = false;
        gameObject.GetComponent<PlayerController>().gravity = 40;
        wallrunning = false;

	    }

    IEnumerator CanWallJumpR(){
        yield return new WaitForSeconds(0.2f);
        GameObject.Find("ColRight").GetComponent<ColDetect>().collided = false;
        wallJumpR = false;
    }
    IEnumerator CanWallJumpL()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject.Find("ColLeft").GetComponent<ColDetect>().collided = false;
        wallJumpL = false;
    }
}
