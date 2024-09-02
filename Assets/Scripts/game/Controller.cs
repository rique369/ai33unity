// General controller script for character.
using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
[RequireComponent (typeof (AnimationManager))]
public class Controller : MonoBehaviour {

	public static float speed;
	public static float Distance;
	[HideInInspector]
	public CharacterController controller;
	private AnimationManager animationManager;

	[Header("Powers game objects in character rig:")]
	public GameObject jpuckObject;
	public GameObject magnetObject;
	public GameObject lTap, rTap;

	[Header("Basic character settings:")]
	public static float jumpHeight = 43F;
	public float swipeDistance = 2f;
	public float speedAddDistance = 2.5f;
	public Transform raycast; // raycast start position for die function
    public static int kd;
	// character state variables
	public bool iRoll = false; 
	public static bool iFly = false;
	public static bool iDie = false;
	public static bool iJump = false;
	public static bool isSwipe = false;
	public static bool iMagnet = false; 
	public static bool doubleJump = false;
	public static bool ForJumpStart = false;
	public static bool coinIsVisible = false;

    // accessory variables
    private bool setGrav = false;
	private bool canControl = false;
	private bool flyAnimation = false;

	private float gravity = 130.0F; 
	private float timeJumping = 0.3f;
	private float curPosition = 0f;

	private int gPower = 1; // using for fast move down when character in air
	private int jumpUsed = 0; // count jumps for double jump
	private int speedBonus;

	private Vector3 moveDirection = Vector3.zero; 

	public static float cF, cJ, cM = 0f;
	public static float doubleCoinTime = 0;
	public static float flyTime, doubleJumpTime, magnetTime = 0f;
    private bool animPlaySaving;
    private float savingTime;

    // controll variables
    public enum SwipeDirection 
	{
		Null = 0,	//no swipe 
		Duck = 1,	// down 
		Jump = 2,	// up 
		Right = 3,	// right 
		Left = 4	// left 
	}
			
	private SwipeDirection direction;
	private SwipeDirection sSwipeDirection;	
	private float fSensitivity = 15f;
	private float fInitialX;
	private float fInitialY;
	private float fFinalX;
	private float fFinalY;
	private float inputX;			//x-coordinate
	private float inputY;			//y-coordinate
	private float slope;
	private float fDistance;						
	private int iTouchStateFlag;							

	private Transform ShadowPlane;
    private int mgn, kvr, bts, x2t;
    //Effects
    public GameObject dieEffect;
    public GameObject startEffect;
    public GameObject shieldEffect;

    void Start(){
        dieEffect.SetActive(false);
        shieldEffect.SetActive(false);
        Distance = 0;
        speed = 10f;
        // controll
        fInitialX = 0.0f;
		fInitialY = 0.0f;
		fFinalX = 0.0f;
		fFinalY = 0.0f;
		inputX = 0.0f;
		inputY = 0.0f;
		iTouchStateFlag = 0;
		sSwipeDirection = SwipeDirection.Null;
        //end controll
        animationManager = this.GetComponent<AnimationManager>();
		controller = this.GetComponent<CharacterController>();
		ShadowPlane = transform.Find("ShadowPlane");
		iFly = iDie = iMagnet = doubleJump = false;
		canControl = true;
        cF = cJ = cM = 0f;
        mgn = ProtectedPrefs.GetInt("m_LvL");
        kvr = ProtectedPrefs.GetInt("k_LvL");
        x2t = ProtectedPrefs.GetInt("d_LvL");
        bts = ProtectedPrefs.GetInt("b_LvL");

        if (mgn == 1){magnetTime = 15f;}
        else if (mgn == 2) {magnetTime = 20f;}
        else if (mgn == 3){magnetTime = 25f;}
        else if (mgn == 4){magnetTime = 30f;}
        else if (mgn == 5){magnetTime = 35f;}
        else {magnetTime = 10f;}

        if (kvr == 1){flyTime = 15f;}
        else if (kvr == 2){flyTime = 20f;}
        else if (kvr == 3){flyTime = 25f;}
        else if (kvr == 4){flyTime = 30f;}
        else if (kvr == 5){flyTime = 35f;}
        else {flyTime = 10f;}

        if (x2t == 1){doubleCoinTime = 15f;}
        else if (x2t == 2){doubleCoinTime = 20f;}
        else if (x2t == 3){doubleCoinTime = 25f;}
        else if (x2t == 4){doubleCoinTime = 30f;}
        else if (x2t == 5){doubleCoinTime = 35f;}
        else {doubleCoinTime = 10f;}

        if (bts == 1){doubleJumpTime = 15f;}
        else if (bts == 2){doubleJumpTime = 20f;}
        else if (bts == 3){doubleJumpTime = 25f;}
        else if (bts == 4){doubleJumpTime = 30f;}
        else if (bts == 5){doubleJumpTime = 35f;}
        else {doubleJumpTime = 10f;}

        Invoke("StartEffect", 1f);
    }

	void FixedUpdate(){
		setShadow ();
		if (controller.isGrounded) {
			moveDirection.y = 0;
            if(gPower==5) gPower = 1;
        }

		RaycastHit ray;
		if (Physics.Raycast (raycast.transform.position, Vector3.right, out ray, 0.5F)) {
			if (ray.collider.tag == "Blocks") {
                StartCoroutine("Left");
            }
		}

		if (Physics.Raycast (raycast.transform.position, Vector3.left, out ray, 0.5F)) {
			if (ray.collider.tag == "Blocks") {
                StartCoroutine("Right");
            }
		}

		if (Physics.Raycast (raycast.transform.position, Vector3.forward, out ray, 0.5F)) {
			if (ray.collider.tag == "Blocks" &&  !GameControll.SaveMe) {
				StartCoroutine(Dead());
			}
		}

        if(iFly) moveDirection.y = jumpHeight;
    }

    void StartEffect() {
        GameObject eff = Instantiate(startEffect, new Vector3(transform.position.x, transform.position.y + 10F, transform.position.z + 20F), transform.rotation) as GameObject;
        eff.transform.SetParent(this.transform);
    }

	private void setShadow(){
		if (iFly || iDie || !controller.isGrounded && !iJump) ShadowPlane.gameObject.SetActive (false);
		else ShadowPlane.gameObject.SetActive (true);

		if (jumpUsed>0) ShadowPlane.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y- jumpHeight/7, this.transform.position.z);
		if (controller.isGrounded) ShadowPlane.transform.position = new Vector3 (this.transform.localPosition.x, 0.1f, this.transform.localPosition.z);
	}

	void Update() {
        if (GameControll.SaveMe)
        {
            savingTime += Time.deltaTime;
            shieldEffect.SetActive(true);
            if (!animPlaySaving)
            {
                animationManager.animationState = animationManager.Run;
                animPlaySaving = true;
            }
            if (savingTime > 4f)
            {
                savingTime = 0;
                GameControll.SaveMe = false;
                animPlaySaving = false;
                shieldEffect.SetActive(false);
            }
            iDie = false;
            dieEffect.SetActive(false);
        }

        if (GameControll.pause || iDie) return;
        if (iFly == true) {
			speedBonus = 2;
		} else {
			speedBonus=1;
		}

		Distance += (speed/2 * speedBonus/3 * Time.deltaTime);
		if(Distance>speedAddDistance){
			AddSpeed();
		}

		if (canControl && !iDie) {
			// Touches & Mouse controll
			if (iTouchStateFlag == 0 && Input.GetMouseButtonDown (0)) {		
				fInitialX = Input.mousePosition.x;	
				fInitialY = Input.mousePosition.y;	
				sSwipeDirection = SwipeDirection.Null;
                iTouchStateFlag = 1;
			}		
			if (iTouchStateFlag == 1) {
				fFinalX = Input.mousePosition.x;
				fFinalY = Input.mousePosition.y;
				sSwipeDirection = swipeDirection ();	
				if (sSwipeDirection != SwipeDirection.Null)
					iTouchStateFlag = 2;
			}	
			if (iTouchStateFlag == 2 || Input.GetMouseButtonUp (0)) {
				iTouchStateFlag = 0;
			}
			direction = getSwipeDirection ();
			switch (direction) {
			case SwipeDirection.Right:
				StartCoroutine ("Right");
				break;
			case SwipeDirection.Left:
				StartCoroutine ("Left");
				break;
			case SwipeDirection.Jump:
				StartCoroutine ("Jump");
				break;
			case SwipeDirection.Duck:
				StartCoroutine ("Down");
				break;
			default:
				//print("null");
				break;
			}
			// END controll
		

			moveDirection = transform.TransformDirection (moveDirection);
			moveDirection *= 1f;
			moveDirection.z = speed * 2 * speedBonus;
			moveDirection.y -= gravity * Time.deltaTime * gPower;
			controller.Move (moveDirection * Time.deltaTime);
	
			transform.position = Vector3.Lerp (transform.position, new Vector3 (curPosition, transform.position.y, transform.position.z), Time.deltaTime * 6);
		}

		// Jump timer
		if (iJump) {
			timeJumping -= Time.deltaTime;
			if ( timeJumping < 0 && controller.isGrounded){
				jumpUsed = 0;
				iJump = false;
			}
		} // end jump timer

		if (controller.isGrounded == false && !iJump && !iFly) animationManager.animationState = animationManager.JumpEnd;
				
		if(ForJumpStart && !iFly){
			moveDirection.y = jumpHeight;
			ForJumpStart = false;
		}

		if (iMagnet) {
			magnetObject.SetActive(true);
			cM += Time.deltaTime;
			if(cM>=magnetTime){
				iMagnet = false;
				magnetObject.SetActive(false);
				cM = 0;
			}
		}

        if (doubleJump) {
		    lTap.SetActive(true);
			rTap.SetActive(true);
			cJ += Time.deltaTime;
			if(cJ>=doubleJumpTime){
				doubleJump = false;
				lTap.SetActive(false);
				rTap.SetActive(false);
				cJ = 0;
			}
		}

        if (iFly) {
			jpuckObject.SetActive (true);
			
			if (!flyAnimation)
				FlyAnim ();
			cF += Time.deltaTime;
           
			if (transform.position.y>= 30f) {
				coinIsVisible = true;
                transform.position = new Vector3(transform.position.x, 30f, transform.position.z);
			}
			if (cF >= flyTime + 3f) {
				coinIsVisible = false;
				jpuckObject.SetActive (false);
				flyAnimation = false;
				gravity = 140f;
				moveDirection.y -= gravity * Time.deltaTime * gPower;
			}
			if (cF >= flyTime + 3.1f) {
				iFly = false;
				cF = 0;
				setGrav = true;
			}
		} else {
			if (setGrav && controller.isGrounded) {
				gravity = 130f;
				setGrav = false;
			}
		}
	}

    void FlyAnim(){
		flyAnimation = true;
		animationManager.animationState = animationManager.FlyUp;
	}

	IEnumerator Dead(){
		if (!iDie) {
			AudiosManager.instance.PlayingSound ("Hit");
			iDie = true;
			kd = Random.Range (0, 3);
			if (kd != 2) {
				animationManager.animationState = animationManager.Die;
            } else {
				animationManager.animationState = animationManager.Die2;
                dieEffect.SetActive(true);
            }
			yield return new WaitForSeconds (1f);
		}
	}

	//	    ---------------------------- Controller ----------------------------------------

	IEnumerator Left(){
		isSwipe = true;
		if (curPosition == 0) {
			curPosition = -swipeDistance;
			AudiosManager.instance.PlayingSound("Swipe");
			if (!iFly)
				animationManager.animationState = animationManager.TurnLeft;
			else 
				animationManager.animationState = animationManager.FlyLeft;
		} else if (curPosition == swipeDistance && curPosition != 0) {
			curPosition = 0;
			AudiosManager.instance.PlayingSound("Swipe");
			if (!iFly)
				animationManager.animationState = animationManager.TurnLeft;
			else 
				animationManager.animationState = animationManager.FlyLeft;
		} else {
			if (!iFly && !iJump){
				animationManager.animationState = animationManager.TurnRight;
				AudiosManager.instance.PlayingSound("Hit");
			}
		}
		yield return new WaitForSeconds (1f);
        gPower = 1;
        isSwipe = false;
	}

	IEnumerator Right(){
		isSwipe = true;
		if(curPosition ==0){
			curPosition = swipeDistance;
			AudiosManager.instance.PlayingSound("Swipe");
			if(!iFly)
				animationManager.animationState = animationManager.TurnRight;
			else 
				animationManager.animationState = animationManager.FlyRight;
		}
		else if(curPosition == -swipeDistance && curPosition != 0){
			curPosition = 0;
			AudiosManager.instance.PlayingSound("Swipe");
			if(!iFly)
				animationManager.animationState = animationManager.TurnRight;
			else 
				animationManager.animationState = animationManager.FlyRight;
		} else {
			if (!iFly && !iJump){
				animationManager.animationState = animationManager.TurnLeft;
				AudiosManager.instance.PlayingSound("Hit");
			}
		}
		yield return new WaitForSeconds (1f);
        gPower = 1;
        isSwipe = false;
	}

	IEnumerator Jump(){
		if (!iFly && jumpUsed <2 && !iJump) {
			if(controller.isGrounded && !doubleJump){
                int j = Random.Range(0,3);
                if (j == 1)
                {
                    animationManager.animationState = animationManager.JumpStart2;
                }
                else
                {
                    animationManager.animationState = animationManager.JumpStart;
                }
                moveDirection.y = jumpHeight;
				iJump = true;
				AudiosManager.instance.PlayingSound("Swipe");
				jumpUsed++;
			}
			if(doubleJump){
				animationManager.animationState = animationManager.JumpStart;
				moveDirection.y = jumpHeight;
				jumpUsed++;
				AudiosManager.instance.PlayingSound("Swipe");
				if(jumpUsed>=2)
					iJump = true;
			}

		}
		gPower = 1;
		yield return new WaitForSeconds (.1f);
	}

	IEnumerator Down(){
		if (!iRoll) {
			if (controller.isGrounded && !iFly) {
				controller.height = 1f;
				controller.center = new Vector3 (0, 0.5f, 0);
				iRoll = true;
                gPower = 1;
                ShadowPlane.transform.localScale = new Vector3(2.3f,2.3f,1.2f);
                animationManager.animationState = animationManager.Roll;
            } else if (controller.isGrounded == false && !iFly) {
				gPower = 5;
			}
           
            AudiosManager.instance.PlayingSound("Swipe");
			yield return new WaitForSeconds (0.75f);
			controller.height = 4f;
			controller.center = new Vector3 (0, 2, 0);
			ShadowPlane.transform.localScale = new Vector3(2,2,1.2f);
			iRoll = false;
		}
	}

	//		----------------------- TOUCH & MOUSE CONTROLLER ------------------------

	public SwipeDirection getSwipeDirection (){
		if (sSwipeDirection != SwipeDirection.Null){
			var etempSwipeDirection = sSwipeDirection;
			sSwipeDirection = SwipeDirection.Null;
			return etempSwipeDirection;
		}
		else
			return SwipeDirection.Null;
	}


	private SwipeDirection swipeDirection (){
		inputX = fFinalX - fInitialX;
		inputY = fFinalY - fInitialY;
		slope = inputY / inputX;
		fDistance = Mathf.Sqrt( Mathf.Pow((fFinalY-fInitialY), 2) + Mathf.Pow((fFinalX-fInitialX), 2) );
		if (fDistance <= (Screen.width/fSensitivity))
			return SwipeDirection.Null;

		if (inputX >= 0 && inputY > 0 && slope > 1){		
			return SwipeDirection.Jump;
		}
		else if (inputX <= 0 && inputY > 0 && slope < -1){
			return SwipeDirection.Jump;
		}
		else if (inputX > 0 && inputY >= 0 && slope < 1 && slope >= 0){
			return SwipeDirection.Right;
		}
		else if (inputX > 0 && inputY <= 0 && slope > -1 && slope <= 0){
			return SwipeDirection.Right;
		}
		else if (inputX < 0 && inputY >= 0 && slope > -1 && slope <= 0){
			return SwipeDirection.Left;
		}
		else if (inputX < 0 && inputY <= 0 && slope >= 0 && slope < 1){
			return SwipeDirection.Left;
		}
		else if (inputX >= 0 && inputY < 0 && slope < -1){
			return SwipeDirection.Duck;
		}
		else if (inputX <= 0 && inputY < 0 && slope > 1){
			return SwipeDirection.Duck;
		}
		return SwipeDirection.Null;	
	}

	private void AddSpeed(){
		speed += 1.2f;
		speedAddDistance = speedAddDistance * 2;
		if(speed>38){
			speed = 38;
		}
	}

}
	