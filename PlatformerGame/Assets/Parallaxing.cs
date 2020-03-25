using UnityEngine;

public class Parallaxing : MonoBehaviour {

	public Transform[] backgrounds;     // Array (list) of all the backgrounds and foregrounds to be parallaxed
	private float[] parallaxScales;     // Proportion of the camera's movement to move the backgrounds by.
	public float smoothing = 1f;        // How smooth the parallax will be. Make sure to set this above 0

	private Transform cam;              // Reference to the main camera's transform
	private Vector3 previousCamPos;		// The position of the camera in the previous frame

	//Called before Start(). Great for references
	void Awake ()
    {
		//Setup camera reference
		cam = Camera.main.transform;

    }

	// Use this for initialization
	void Start () {
		//Store Previous Frame had the current frames camera position
		previousCamPos = cam.position;

		// assigning corresponding parallaxScales
		parallaxScales = new float[backgrounds.Length];

		for (int i = 0; i < backgrounds.Length; i++)
        {
			parallaxScales[i] = backgrounds[i].position.z * -1;
        }
	}
	
	// Update is called once per frame
	void Update () {
		

		// for each backgrouund
		for (int i = 0; i < backgrounds.Length; i++)
        {
			// the parallax is the opposite of the camera movement because the previous frame multiiplied by the scale
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

			// set a target x position which is the current position plus the parallax multiplied
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			// create a target position which is the background's current position with it's target x position
			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			// fade between current position and the target position using lerp
			backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

		//set previousCamPos to the camera's position at the end of the frame
		previousCamPos = cam.position;
	}
}
