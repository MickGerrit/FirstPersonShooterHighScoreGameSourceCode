using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public Transform Player;
    public float mouseSensitivity;

    public Vector3 targetRotCam;
    public Vector3 targetRotPlayer;

    public float mouseX;
    public float mouseY;

    public float rotAmountX;
    public float rotAmountY;


    float xAxisClamp = 0;

	private void Awake()
	{
        
	}

	// Update is called once per frame
	void Update () {
        Cursor.lockState = CursorLockMode.Locked;

        if (!GameObject.Find("Player").GetComponent<PlayerController>().enabled)
        {
            mouseX = Input.GetAxis("Mouse X");
            transform.rotation = Quaternion.Euler(new Vector3(0, targetRotCam.y, targetRotCam.z));

        }
        else if (GameObject.Find("Player").GetComponent<PlayerController>().enabled)
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            RotateCamera();
        }

	}
    void RotateCamera(){
        rotAmountX = mouseX * mouseSensitivity;
        rotAmountY = mouseY * mouseSensitivity;
        targetRotCam = transform.rotation.eulerAngles;
        targetRotPlayer = Player.rotation.eulerAngles;

        xAxisClamp -= rotAmountY;

        targetRotCam.x -= rotAmountY;
        targetRotCam.z = 0f;
        targetRotPlayer.y += rotAmountX;
        //hier zit de afterclimb bug !
        if (GameObject.Find("Player").GetComponent<ClimbUp>().camReset){
            xAxisClamp = 0;
        }
        if (xAxisClamp > 90 ){
            xAxisClamp = 90;
            targetRotCam.x = 90;
        }else if (xAxisClamp < -90){
            xAxisClamp = -90;
            targetRotCam.x = 270;
        }


            transform.rotation = Quaternion.Euler(targetRotCam);
       
        Player.rotation = Quaternion.Euler(targetRotPlayer);
    }
}
