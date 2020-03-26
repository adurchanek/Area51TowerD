

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{



	public float panSpeed = 30f;
	public float panBoarderThickness = Screen.height/10f;

	private bool doMovement = true;

	public float scrollSpeed = 5;
	
	public float minY = 10;
	public float maxY = 80;

	//public float circleRadius;
	public Vector3 position;
	public float positionTimeCounter;

	public Transform center;
	
	public float mouseChange;
	public float cameraRotation;

	private float tiltAngle;
	private float smooth;


	public bool mouseIsReset;

	public bool xPosRotation;
	public bool yPosRotation;

	public float xVel;
	
	public float yVel;
	public float lastX;
	public float lastY;
	private bool inMotion;

	public float touchDeltaY1;
	public float touchDeltaY0;

	//public Text rotationText;
	

	private float localRotationZ;
	private float localRotationY;
	
	//private float deltaRotation;
	
	float lastPressY0;
	float lastPressY1;
	float currentPressY0;
	float currentPressY1;
	private bool deltaY01NotAvailable;

	
	
	public float deltaPositionY0 ;
	public float deltaPositionX0 ;
		
	public float deltaPositionY1 ;
	public float deltaPositionX1 ;
	
	public Text Y0Text;
	public Text Y1Text;
	
	public Text X0Text;
	public Text X1Text;

	public float angleRotation;
	public float lastAngleRotation;

	public float deltaRotation;
	private bool deltaRotationAvailable;

	public bool scopeTest;


	private float inputPositionChangeX;
	private float inputPositionChangeY;
	
	private float inputPositionChange2X;
	private float inputPositionChange2Y;




	public float rotationSpeed;
	public float positionSpeed = 10f;
	private bool twoFingerResetNeeded;

	public GameObject directionalPoints;

	public bool calculatedTarget;

	public Vector3 nearestPoint;

	public float nearestPointY;
	public float nearestPointX;

	public Boolean momentum;
	private bool twoFingersDown;

	public double fingerLiftStartTime;
	private bool oneFingerLifted;
	private bool allFingersLifted;


	// Use this for initialization
	void Start ()
	{
		//circleRadius = Vector3.Distance(transform.position, center.position);
		positionTimeCounter = 0f;

		position = new Vector3(0,0,0);
		transform.LookAt(center);

		mouseChange = 0;
		cameraRotation = 0;

		tiltAngle = 60f;

		smooth = 5f;

		mouseIsReset = false;

		xPosRotation = false;
		yPosRotation = false;
		lastX = 0;
		lastY = 0;
		inMotion = false;
		touchDeltaY0 = 0f;
		touchDeltaY1 = 0f;
		lastPressY0 = 0;
		lastPressY1 = 0;

		currentPressY0 = 0f;
		currentPressY1 = 0f;
		//rotationText.text = "Rotation: " + localRotationZ;
		//Y0Text.text = "0Y: " + deltaPositionY0;
		//X0Text.text = "0X: " + deltaPositionX0;
		//Y1Text.text = "1Y: " + deltaPositionY1;
		//X1Text.text = "1X: " + deltaPositionX1;
		localRotationZ = 0f;
		deltaY01NotAvailable = true;
		deltaPositionY0 = 0f;
		deltaPositionX0 = 0f;
		deltaPositionY1 = 0f;
		deltaPositionX1 = 0f;
		angleRotation = 0f;
		deltaRotation = 0f;
		deltaRotationAvailable = false;
		lastAngleRotation = 0f;
		scopeTest = false;
		inputPositionChangeX = 0f;
		inputPositionChangeY = 0f;
		inputPositionChange2X = 0f;
		inputPositionChange2Y = 0f;
		twoFingerResetNeeded = false;

		localRotationY = 0f;

		calculatedTarget = false;
		
		nearestPoint = new Vector3();

		momentum = false;

		rotationSpeed = 0f;
		twoFingersDown = false;

		fingerLiftStartTime = -3f;

		oneFingerLifted = false;

		allFingersLifted = true;


		if (center == null)

		{

			center = transform;
		}



	}
	
	// Update is called once per frame
	void Update()
	{
		//float dist = Vector3.Distance(center.position, transform.position);
		//Debug.Log(dist);


//		if (GameController.gameEnded)
//		{
//			enabled = false;
//			return;
//		}


		//Debug.Log(CalculateClosestPoint());
		
//		var vec1 = transform.eulerAngles;
//		vec1.x = transform.eulerAngles.x;
//		vec1.y = Mathf.Round(vec1.y / 90) * 90;
//		vec1.z = Mathf.Round(vec1.z / 90) * 90;
//		Quaternion rotation1 = Quaternion.Euler(vec1);
//		transform.rotation = Quaternion.Lerp(transform.rotation, rotation1, .5f);
//				
//		transform.LookAt(center, transform.up);
		
		
		//if(!Input.GetMouseButton(1))
		if(false)

		{
			if (Math.Abs(deltaRotation) >= .1f)
			{
				if (deltaRotation < 0)
				{
					deltaRotation = deltaRotation - deltaRotation / 15f;
					if (deltaRotation >= 0)
					{
						deltaRotation = 0;
					}
					else
					{

						localRotationZ = transform.rotation.eulerAngles.z + 4 * deltaRotation;
						transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
							transform.rotation.eulerAngles.y, localRotationZ);
					}
				}
				else if (deltaRotation > 0)
				{
					deltaRotation = deltaRotation - deltaRotation / 15f;
					if (deltaRotation <= 0)
					{
						deltaRotation = 0;
					}
					else
					{

						localRotationZ = transform.rotation.eulerAngles.z + 4 * deltaRotation;
						transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
							transform.rotation.eulerAngles.y, localRotationZ);
					}
				}
			}
			else
			{
				deltaRotation = 0f;
				deltaRotationAvailable = false;
				lastAngleRotation = 0f;
				angleRotation = 0f;



			}
		}
		

		if (Input.GetMouseButton(0))
		{
			calculatedTarget = false;

			momentum = true;
			


			if (Input.GetMouseButton(1))
			{
				allFingersLifted = false;
				
				if (oneFingerLifted)
				{
				
					deltaRotation = 0f;
					deltaRotationAvailable = false;
					lastAngleRotation = 0f;
					angleRotation = 0f;

					oneFingerLifted = false;
				}

				twoFingersDown = true;
				fingerLiftStartTime = Time.time;
				
				angleRotation = -1*calculateAngle(Input.GetTouch(1).position.x, Input.GetTouch(1).position.y, Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
				
				if (!deltaRotationAvailable)
				{
					deltaRotationAvailable = true;
					lastAngleRotation = angleRotation;
				}
				else
				{
					deltaRotation = angleRotation - lastAngleRotation;
					lastAngleRotation = angleRotation;
				}

				localRotationZ = transform.rotation.eulerAngles.z + 4*deltaRotation;
				transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y,localRotationZ);
				
				inputPositionChangeX = (float)((double)-1*((double)Input.GetTouch(0).deltaPosition.y+(double)Input.GetTouch(1).deltaPosition.y));
				inputPositionChangeY = (float)(((double)Input.GetTouch(0).deltaPosition.x+(double)Input.GetTouch(1).deltaPosition.x));
				
				transform.RotateAround(center.position, transform.right, inputPositionChangeX*Time.deltaTime*positionSpeed);
				transform.RotateAround(center.position, transform.up, inputPositionChangeY*Time.deltaTime*positionSpeed);

			}
			else
			{

				if (twoFingersDown)

				{
					fingerLiftStartTime = Time.time;
					twoFingersDown = false;
					oneFingerLifted = true;
				}
				





				//good
//				deltaRotation = 0f;
//				deltaRotationAvailable = false;
//				lastAngleRotation = 0f;
//				angleRotation = 0f;
//				//good
				
				
				
				
//				var vec = transform.eulerAngles;
//				vec.x = transform.eulerAngles.x;
//				vec.y = Mathf.Round(vec.y / 90) * 90;
//				vec.z = Mathf.Round(vec.z / 90) * 90;
//				Quaternion rotation = Quaternion.Euler(vec);
//				transform.rotation = Quaternion.Lerp(transform.rotation, rotation, .5f);
//				
//				transform.LookAt(center, transform.up);





				if((Time.time - fingerLiftStartTime > .5f ||allFingersLifted))// && !oneFingerLifted))//||allFingersLifted)

				{
					try
					{
						inputPositionChangeY = (float) Input.GetTouch(0).deltaPosition.y;
						inputPositionChangeX = (float) Input.GetTouch(0).deltaPosition.x;


						//if(inputPositionChangeX<inputPositionChangeY)
						
//						inputPositionChangeY = (float) -1*Input.GetTouch(0).deltaPosition.y;
//						inputPositionChangeX = (float) Input.GetTouch(0).deltaPosition.x;
//						transform.RotateAround(center.position, transform.right, inputPositionChangeX*Time.deltaTime*positionSpeed);
//						transform.RotateAround(center.position, transform.up, inputPositionChangeY*Time.deltaTime*positionSpeed);


						transform.RotateAround(center.position, transform.right,-Input.GetTouch(0).deltaPosition.y * Time.deltaTime * positionSpeed);
						transform.RotateAround(center.position, transform.up,Input.GetTouch(0).deltaPosition.x * Time.deltaTime * positionSpeed);

					}
					catch (Exception e)
					{
						transform.RotateAround(center.position, transform.right, -Input.GetAxis("Mouse Y") * 2);
						transform.RotateAround(center.position, transform.up, Input.GetAxis("Mouse X") * 2);
					}

					oneFingerLifted = false;
				}

				else
				{

					oneFingerLifted = true;
					
					if(Math.Abs(inputPositionChangeX) >= 10f || Math.Abs(inputPositionChangeY) >= 10f)
						{
			

			
			
							if (Math.Abs(inputPositionChangeX) >= 10f)
							{
								if (inputPositionChangeX < 0)
								{
									inputPositionChangeX = inputPositionChangeX - inputPositionChangeX / 5f;
									if (inputPositionChangeX >= 0)
									{
										inputPositionChangeX = 0;
									}
									else
									{
										transform.RotateAround(center.position, transform.right, inputPositionChangeX*Time.deltaTime*positionSpeed);
										
									}
								}
								else if (inputPositionChangeX > 0)
								{
									inputPositionChangeX = inputPositionChangeX - inputPositionChangeX / 5f;
									if (inputPositionChangeX <= 0)
									{
										inputPositionChangeX = 0;
									}
									else
									{
										transform.RotateAround(center.position, transform.right, inputPositionChangeX*Time.deltaTime*positionSpeed);
										
									}
								}
							}
							else
							{
								inputPositionChangeX = 0;
								momentum = false;
							}
			
							if (Math.Abs(inputPositionChangeY) >= 10f)
							{
			
			
								if (inputPositionChangeY < 0)
								{
									inputPositionChangeY = inputPositionChangeY - inputPositionChangeY / 5f;
									if (inputPositionChangeY >= 0)
									{
										inputPositionChangeY = 0;
									}
									else
									{
										
										transform.RotateAround(center.position, transform.up, inputPositionChangeY*Time.deltaTime*positionSpeed);
									}
								}
								else if (inputPositionChangeY > 0)
								{
									//inputPositionChangeY = inputPositionChangeY - inputPositionChangeY / 10f;
			
									inputPositionChangeY = inputPositionChangeY - inputPositionChangeY / 5f;
									if (inputPositionChangeY <= 0)
									{
										inputPositionChangeY = 0;
									}
									else
									{
										
										transform.RotateAround(center.position, transform.up, inputPositionChangeY*Time.deltaTime*positionSpeed);
									}
								}
							}
							else
							{
								inputPositionChangeY = 0;
			
			
							}
						}
			
						else
						{
							inputPositionChangeY = 0;
							inputPositionChangeX = 0;
							

						}
					}
				}
			//allFingersLifted = false;
			}

			
		else
		{
			
			
			deltaRotation = 0f;
			deltaRotationAvailable = false;
			lastAngleRotation = 0f;
			angleRotation = 0f;
			allFingersLifted = true;
			
			if (twoFingersDown)

			{
				//fingerLiftStartTime = Time.time;
				twoFingersDown = false;
				oneFingerLifted = true;
			}
			
			//good
//			var vec1 = transform.eulerAngles;
//			vec1.x = transform.eulerAngles.x;
//			//vec1.x = Mathf.Round(vec1.x / 90) * 90;
//			vec1.y = Mathf.Round(vec1.y / 90) * 90 ;
//			//vec1.y = transform.eulerAngles.y;
//			vec1.z = Mathf.Round(vec1.z / 90) * 90 ; 
//			
//			
//			
//			float y = vec1.y;
//			if (y > 180.0f)
//			{
//				y -= 360.0f;
//				vec1.y = y;
//			}
//			
//			
//			float z = vec1.z;
//			if (z > 180.0f)
//			{
//				z -= 360.0f;
//				vec1.z = z;
//			}
//			Quaternion rotation1 = Quaternion.Euler(vec1);
//			
//			transform.rotation = Quaternion.Lerp(transform.rotation, rotation1, .5f);
//				
//			transform.LookAt(center, transform.up);
//			
			
			if(Math.Abs(inputPositionChangeX) >= 10f || Math.Abs(inputPositionChangeY) >= 10f)
			{
				if(!oneFingerLifted)
				{
					if (Math.Abs(inputPositionChangeX) > 11f || Math.Abs(inputPositionChangeY) > 11f)

					{

						var vec1 = transform.eulerAngles;
						vec1.x = transform.eulerAngles.x;
						//vec1.x = Mathf.Round(vec1.x / 90) * 90;
						vec1.y = Mathf.Round(vec1.y / 90) * 90;
						//vec1.y = transform.eulerAngles.y;
						vec1.z = Mathf.Round(vec1.z / 90) * 90;



						float y = vec1.y;
						if (y > 180.0f)
						{
							y -= 360.0f;
							vec1.y = y;
						}


						float z = vec1.z;
						if (z > 180.0f)
						{
							z -= 360.0f;
							vec1.z = z;
						}

						Quaternion rotation1 = Quaternion.Euler(vec1);

						transform.rotation = Quaternion.Lerp(transform.rotation, rotation1, .5f);

						transform.LookAt(center, transform.up);
					}
				}


				if (Math.Abs(inputPositionChangeX) >= 10f)
				{
					if (inputPositionChangeX < 0)
					{
						inputPositionChangeX = inputPositionChangeX - inputPositionChangeX / 5f;
						if (inputPositionChangeX >= 0)
						{
							inputPositionChangeX = 0;
						}
						else
						{

							if (oneFingerLifted)
							{
								transform.RotateAround(center.position, transform.right, inputPositionChangeX*Time.deltaTime*positionSpeed);
							}
							else
							{
								transform.RotateAround(center.position, transform.up,inputPositionChangeX * Time.deltaTime * positionSpeed);
								
							}

						}
					}
					else if (inputPositionChangeX > 0)
					{
						inputPositionChangeX = inputPositionChangeX - inputPositionChangeX / 5f;
						if (inputPositionChangeX <= 0)
						{
							inputPositionChangeX = 0;
						}
						else
						{
							if (oneFingerLifted)
							{
								transform.RotateAround(center.position, transform.right, inputPositionChangeX*Time.deltaTime*positionSpeed);
							}
							else
							{
								transform.RotateAround(center.position, transform.up,inputPositionChangeX * Time.deltaTime * positionSpeed);
								
							}
						}
					}
				}
				else
				{
					inputPositionChangeX = 0;
					momentum = false;
				}

				if (Math.Abs(inputPositionChangeY) >= 10f)
				{


					if (inputPositionChangeY < 0)
					{
						inputPositionChangeY = inputPositionChangeY - inputPositionChangeY / 5f;
						if (inputPositionChangeY >= 0)
						{
							inputPositionChangeY = 0;
						}
						else
						{
							
							if (oneFingerLifted)
							{
								transform.RotateAround(center.position, transform.up, inputPositionChangeY*Time.deltaTime*positionSpeed);
							}
							else
							{
							
								transform.RotateAround(center.position, transform.right,-1f * inputPositionChangeY * Time.deltaTime * positionSpeed);
								
							}
						}
					}
					else if (inputPositionChangeY > 0)
					{
						//inputPositionChangeY = inputPositionChangeY - inputPositionChangeY / 10f;

						inputPositionChangeY = inputPositionChangeY - inputPositionChangeY / 5f;
						if (inputPositionChangeY <= 0)
						{
							inputPositionChangeY = 0;
						}
						else
						{
							if (oneFingerLifted)
							{
								transform.RotateAround(center.position, transform.up, inputPositionChangeY*Time.deltaTime*positionSpeed);
							}
							else
							{
							
								transform.RotateAround(center.position, transform.right,-1f * inputPositionChangeY * Time.deltaTime * positionSpeed);
								
							}
						}
					}
				}
				else
				{
					inputPositionChangeY = 0;


				}
			}

			else
			{
				inputPositionChangeY = 0;
				inputPositionChangeX = 0;
				
				var vec1 = transform.eulerAngles;
				vec1.x = transform.eulerAngles.x;
				//vec1.x = Mathf.Round(vec1.x / 90) * 90;
				vec1.y = Mathf.Round(vec1.y / 90) * 90 ;
				//vec1.y = transform.eulerAngles.y;
				vec1.z = Mathf.Round(vec1.z / 90) * 90 ; 
			
			
			
				float y = vec1.y;
				if (y > 180.0f)
				{
					y -= 360.0f;
					vec1.y = y;
				}
			
			
				float z = vec1.z;
				if (z > 180.0f)
				{
					z -= 360.0f;
					vec1.z = z;
				}
				Quaternion rotation1 = Quaternion.Euler(vec1);
			
				transform.rotation = Quaternion.Lerp(transform.rotation, rotation1, .5f);
				
				transform.LookAt(center, transform.up);

				oneFingerLifted = false;

			}
			

			
			
			

//
//			
			
//			if((Math.Abs(inputPositionChangeY) <= 5f) && (Math.Abs(inputPositionChangeX) <=5f))
//			{
//				float averageInputPositionChange = (inputPositionChangeY + inputPositionChangeX)/2;
//				
////				var vec = transform.eulerAngles;
////				vec.x = transform.eulerAngles.x;
////				vec.y = Mathf.Round(vec.y / 90) * 90 + 1;
////				vec.z = Mathf.Round(vec.z / 90) * 90 + 1;
////				Quaternion rotation = Quaternion.Euler(vec);
////				transform.rotation = Quaternion.Lerp(transform.rotation, rotation, .5f);
////				
//
//				
//				
//
//				//Debug.Log(vec);
//				
//				
//				if (!calculatedTarget)
//				{
//					
//					nearestPoint = CalculateClosestPoint();
//
//					nearestPointY = (nearestPoint - transform.position).normalized.y;
//					nearestPointX = (nearestPoint - transform.position).normalized.x;
//					////Debug.Log(nearestPoint);
//				}
//
//				if (Vector3.Distance(nearestPoint, transform.position) > .5 )
//
//				{
//
//
//					//transform.RotateAround(center.position, transform.right, Vector3.SignedAngle((CalculateClosestPoint() - transform.position).normalized, transform.forward, Vector3.up) * Time.deltaTime);
//					/////transform.RotateAround(center.position, (CalculateClosestPoint() - transform.position).normalized, 2 );
//
//					//transform.Translate();
//
//					//transform.RotateAround(center.position, transform.right, -1*nearestPointY );
//					//transform.RotateAround(center.position, transform.up, nearestPointX) ;
//
//					////////////////////////////////////transform.position = Vector3.MoveTowards(transform.position, nearestPoint, 5.25f);
//					
//					transform.position = Vector3.MoveTowards(transform.position, nearestPoint, averageInputPositionChange);
//					
//					
//					////////transform.LookAt(center);
//					transform.LookAt(center, transform.up);
//
//
//
//					//var relativeUp = center.TransformDirection (Vector3.forward);
//					//var relativePos = center.position - transform.position;
//					//transform.rotation = Quaternion.LookRotation(relativePos,relativeUp);
//
//
//					//transform.RotateAround(center.position, nearestPoint, 1);
//
//					//var distance = heading.magnitude;
//					//var direction = heading / distance; // This is now the normalized direction.
//				}
//				else
//
//				{
//
//					//Debug.Log(Vector3.Distance(nearestPoint, transform.position) > 5);
//
//
//
//					calculatedTarget = false;
//					
//					inputPositionChangeY = 0;
//					
//					inputPositionChangeX = 0;
//					
////					var vec = transform.eulerAngles;
////					vec.x = transform.eulerAngles.x;
////
////				
////					//vec.y = transform.eulerAngles.y;
////					vec.y = Mathf.Round(vec.y / 90) * 90;
////					/////vec.z = Mathf.Round(vec.z / 180) * 180;
////					vec.z = Mathf.Round(vec.z / 90) * 90;
////					//transform.eulerAngles = vec;
////					//45 degrees for x value on directional GO
////			
////					//Vector3 rotationVector = new Vector3(0, 30, 0);
////					Quaternion rotation = Quaternion.Euler(vec);
////			
////					transform.rotation = Quaternion.Lerp(transform.rotation, rotation, .5f);
//					
//				}
//
//			}
//
//			else
//			{
////				var vec = transform.eulerAngles;
////				vec.x = transform.eulerAngles.x;
////				vec.y = Mathf.Round(vec.y / 90) * 90;
////				vec.z = Mathf.Round(vec.z / 90) * 90;
////				Quaternion rotation = Quaternion.Euler(vec);
////				transform.rotation = Quaternion.Lerp(transform.rotation, rotation, .5f);
////				
////				transform.LookAt(center, transform.up);
//
//			}
			
			
			//Vector3 nearestPoint = CalculateClosestPoint() - transform.position;




			//Debug.Log(Vector3.Distance(nearestPoint, transform.position));
			//Debug.Log(nearestPoint);
			
			//var vec = transform.eulerAngles;
			//vec.x = transform.eulerAngles.x;
			//vec.y = transform.eulerAngles.y;
			//vec.z = Mathf.Round(vec.z / 90) * 90;
			//transform.eulerAngles = vec;
			
			//g
//			var vec =  transform.eulerAngles;
//			vec.x = Mathf.Round(vec.x / 90) * 90;
//			vec.y = Mathf.Round(vec.y / 90) * 90;
//			vec.z = Mathf.Round(vec.z / 90) * 90;
//			
//			
//			transform.eulerAngles = vec;
			//g
			
			/*//goood down
			if (!calculatedTarget)
			{
				nearestPoint = CalculateClosestPoint();

				nearestPointY = (nearestPoint - transform.position).normalized.y;
				nearestPointX = (nearestPoint - transform.position).normalized.x;
				Debug.Log(nearestPoint);
			}
			if(Vector3.Distance(nearestPoint, transform.position) > .1)

			{
				
				
				//transform.RotateAround(center.position, transform.right, Vector3.SignedAngle((CalculateClosestPoint() - transform.position).normalized, transform.forward, Vector3.up) * Time.deltaTime);
				/////transform.RotateAround(center.position, (CalculateClosestPoint() - transform.position).normalized, 2 );

				//transform.Translate();
				
				//transform.RotateAround(center.position, transform.right, -1*nearestPointY );
				//transform.RotateAround(center.position, transform.up, nearestPointX) ;
				
				transform.position = Vector3.MoveTowards(transform.position, nearestPoint, 3.25f);
				////////transform.LookAt(center);
				transform.LookAt(center, transform.up);
				

				
				//var relativeUp = center.TransformDirection (Vector3.forward);
				//var relativePos = center.position - transform.position;
				//transform.rotation = Quaternion.LookRotation(relativePos,relativeUp);
				
				
				//transform.RotateAround(center.position, nearestPoint, 1);
				
				//var distance = heading.magnitude;
				//var direction = heading / distance; // This is now the normalized direction.
			}
			else

			{
				
				//Debug.Log(Vector3.Distance(nearestPoint, transform.position) > 5);
				
				

				calculatedTarget = false;
			}
			*///good up
			
			
			
			/*
if (Math.Abs(inputPositionChangeX) >= .01f)
{
	if (inputPositionChangeX < 0)
	{
		inputPositionChangeX = inputPositionChangeX - inputPositionChangeX / 10f;
		if (inputPositionChangeX >= 0)
		{
			inputPositionChangeX = 0;
		}
		else
		{
			transform.RotateAround(center.position, transform.up,
				inputPositionChangeX * Time.deltaTime * positionSpeed);
		}
	}
	else if (inputPositionChangeX > 0)
	{
		inputPositionChangeX = inputPositionChangeX - inputPositionChangeX / 10f;
		if (inputPositionChangeX <= 0)
		{
			inputPositionChangeX = 0;
		}
		else
		{
			transform.RotateAround(center.position, transform.up,
				inputPositionChangeX * Time.deltaTime * positionSpeed);
			scopeTest = true;
		}
	}
}
else
{
	inputPositionChangeX = 0;
}

if (Math.Abs(inputPositionChangeY) >= .01f)
{
	if (inputPositionChangeY < 0)
	{
		inputPositionChangeY = inputPositionChangeY - inputPositionChangeY / 10f;
		if (inputPositionChangeY >= 0)
		{
			inputPositionChangeY = 0;
		}
		else
		{
			transform.RotateAround(center.position, transform.right,
				-1f * inputPositionChangeY * Time.deltaTime * positionSpeed);
		}
	}
	else if (inputPositionChangeY > 0)
	{
		inputPositionChangeY = inputPositionChangeY - inputPositionChangeY / 10f;
		if (inputPositionChangeY <= 0)
		{
			inputPositionChangeY = 0;
		}
		else
		{
			transform.RotateAround(center.position, transform.right,
				-1f * inputPositionChangeY * Time.deltaTime * positionSpeed);
			scopeTest = true;
		}
	}
}
else
{
	inputPositionChangeY = 0;
}

*/ 
			//UNCOMMENT ABOVE FOR MOMENTUM CODE UP TO DATE

			/*
			if (Math.Abs(inputPositionChangeX) >= .01f)
			{
				if (inputPositionChangeX < 0)
				{
					inputPositionChangeX = inputPositionChangeX - inputPositionChangeX / 10f;
					if (inputPositionChangeX >= 0)
					{
						inputPositionChangeX = 0;
					}
					else
					{
						transform.RotateAround(center.position, transform.up,
							inputPositionChangeX * Time.deltaTime * positionSpeed);
					}
				}
				else if (inputPositionChangeX > 0)
				{
					inputPositionChangeX = inputPositionChangeX - inputPositionChangeX / 10f;
					if (inputPositionChangeX <= 0)
					{
						inputPositionChangeX = 0;
					}
					else
					{
						transform.RotateAround(center.position, transform.up,
							inputPositionChangeX * Time.deltaTime * positionSpeed);
						scopeTest = true;
					}
				}
			}
			else
			{
				inputPositionChangeX = 0;
			}

			if (Math.Abs(inputPositionChangeY) >= .01f)
			{
				if (inputPositionChangeY < 0)
				{
					inputPositionChangeY = inputPositionChangeY - inputPositionChangeY / 10f;
					if (inputPositionChangeY >= 0)
					{
						inputPositionChangeY = 0;
					}
					else
					{
						transform.RotateAround(center.position, transform.right,
							-1f * inputPositionChangeY * Time.deltaTime * positionSpeed);
					}
				}
				else if (inputPositionChangeY > 0)
				{
					inputPositionChangeY = inputPositionChangeY - inputPositionChangeY / 10f;
					if (inputPositionChangeY <= 0)
					{
						inputPositionChangeY = 0;
					}
					else
					{
						transform.RotateAround(center.position, transform.right,
							-1f * inputPositionChangeY * Time.deltaTime * positionSpeed);
						scopeTest = true;
					}
				}
			}
			else
			{
				inputPositionChangeY = 0;
			}
			
			*/ 
			//UNCOMMENT ABOVE FOR MOMENTUM CODE UP TO DATE



			//Vector3 v = transform.position - center.position;
			
			//float angle = Vector3.Angle(v, transform.forward);
			
			
			//Quaternion qq = new Quaternion();
			//qq.eulerAngles = v;
			

			//transform.eulerAngles = vec;
			
			//transform.localRotation = Quaternion.Euler(vec);
			
			//localRotationZ = transform.rotation.eulerAngles.z + 4*deltaRotation;
			//localRotationY = transform.rotation.eulerAngles.y;
			//transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles.x,localRotationY,localRotationZ);

			
			
			//rotationText.text = "Rotation: " + deltaRotation;
			//Y0Text.text = "ScopeTest: " + scopeTest;
			
			//localRotationZ = transform.rotation.eulerAngles.z + 4*deltaRotation;
			
			/*
			var vec = transform.rotation.eulerAngles;
			vec.y = Mathf.Round(vec.y / 90) * 90;
			vec.z = Mathf.Round(vec.z / 90) * 90;


			Vector3 transformPrevious = transform.rotation.eulerAngles;
			var lastRotation = transform.rotation.eulerAngles;
			transform.LookAt(center);
			Vector3 transformAfter = transform.rotation.eulerAngles;
			transform.eulerAngles = lastRotation;
			
			
			Quaternion qCurrent = Quaternion.Euler(transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z);
			Quaternion qNew = Quaternion.Euler(transformAfter.x,vec.y,vec.z);
			transform.rotation = Quaternion.Lerp(transform.rotation, qNew, Time.time*.01f);

			*/
			//X0Text.text = "Y1: " + transformAfter;

			//lookPos.y = vec.y;
			//lookPos.z = vec.z;
			

			//transform.LookAt(center);
			
			
			
			//transform.rotation.SetLookRotation();
			
			/*
			
			Y1Text.text = "Y2: " + transform.rotation.eulerAngles.y;
			X1Text.text = "Z2: " + transform.rotation.eulerAngles.z;
			
			
			var vec = transform.rotation.eulerAngles;
			vec.y = Mathf.Round(vec.y / 90) * 90;
			vec.z = Mathf.Round(vec.z / 90) * 90;


			
			var lookPos = center.position - transform.position;
			lookPos.x = 0;

			var rotation = Quaternion.LookRotation(lookPos);
			
			Quaternion qNew = Quaternion.Euler(Quaternion.LookRotation(lookPos).x,vec.y,vec.z);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.time * .01f);


			//https://docs.unity3d.com/ScriptReference/Mathf.LerpAngle.html
			*/



		}


	return;


	if (Input.GetKeyDown(KeyCode.Escape))
			doMovement = !doMovement;

		if (!doMovement)
			return;
	

		if (Input.GetKey("w")||Input.mousePosition.y >= Screen.height - panBoarderThickness)

		{
			transform.Translate(Vector3.forward*panSpeed*Time.deltaTime,Space.World);
			
		}
		if (Input.GetKey("s")||Input.mousePosition.y <  panBoarderThickness)

		{
			transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);

		}
		
		if (Input.GetKey("a")||Input.mousePosition.x <  panBoarderThickness)

		{
			transform.Translate(Vector3.left*panSpeed*Time.deltaTime,Space.World);
			
		}
		
		if (Input.GetKey("d")||Input.mousePosition.x >= Screen.width - panBoarderThickness)

		{
			transform.Translate(Vector3.right*panSpeed*Time.deltaTime,Space.World);
			
		}

		float scroll = Input.GetAxis("Mouse ScrollWheel");
		

		Vector3 pos = transform.position;

		pos.y -= scroll * scrollSpeed * Time.deltaTime*1000;
		pos.y = Mathf.Clamp(pos.y, minY, maxY); //TODO clamp left right up down

		transform.position = pos;

	}

//	private Vector3 CalculateClosestPoint()
//	{
//		GameObject[] points = directionalPoints.GetComponent<Center>().directionalPoints;
//		Transform nearest;
//		float shortestDistance = Mathf.Infinity;
//		GameObject nearestEnemy = null;
//		
//		foreach (GameObject point in points)
//		{
//
//			float distanceToEnemy = Vector3.Distance(transform.position, point.transform.position);
//			
//			//Debug.Log(point.transform.position);
//			if (distanceToEnemy < shortestDistance)
//			{
//				shortestDistance = distanceToEnemy;
//				nearestEnemy = point;
//
//				
//			}
//		}
//		
//		//nearestEnemy.transform.localScale = new Vector3(10,10,10);
//
//		calculatedTarget = true;
//		
//		return nearestEnemy.transform.position;
//	}

	public float calculateAngle(float startX, float startY, float endX, float endY ) {

		float radAngle;
		float degAngle;

		float deltaY;
		float deltaX;

		deltaX = endX - startX;
		deltaY = (endY - startY);
		
		radAngle = (float) Mathf.Atan2(deltaY, deltaX);
		degAngle = ((57.2957795f * radAngle));

		return degAngle;
	}
}




