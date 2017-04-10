using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    //Ref for the transform properties of a 'Target'.
        //A Transform stores the pos data, scale data, and rotation data of an object.
    public Transform target;
    //Referance to the 'Camera' object within Unity.
    Camera mainCam;
    //The variable that will be assigned the 'lag' speed when the player stops and the camera follows.
    public float moveSpeed = 0.1f;


	// Use this for initialization
	void Start () {
        //Gives a ref to the object that this script is attached to. 
        mainCam = GetComponent<Camera>();


	}
	
	// Update is called once per frame
	void Update () {
        //Math that allows for the camera to scale its 'Size' prop as opposed to the size of the camera and keeps the ratio of scale you are looking to achieve. 
        mainCam.orthographicSize = (Screen.height / 100f) / 0.08f; //The end unit is a ref to scale. E.g.: With a scale of '4f' 32px becomes 64px on the screen. 0>SCALE>1

        //Statement that defines if the camera can see the target... do this.
        if(target)
        {
            //Locats the players pos and follows accordingly.
                //Lerp (From some number, Too some number, and How fast)
                //Added the 'new Vector3' on the to counteract the camera shooting towards the player in 3D space. Thus the z-axis should match the pos in the Unity Editor. 
            transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed) + new Vector3 (0, 0, -10);
        }

	}
}
