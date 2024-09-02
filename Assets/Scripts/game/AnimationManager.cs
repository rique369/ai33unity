/*
	Animation Manager will help you to adjust the playback speed
	of animation and quickly recall animation clip their other game scripts.
 */

using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour {

	[System.Serializable]
	public class AnimationSet{
		public AnimationClip animation;
		public float speedAnimation = 1;
	}

    public bool isMenu;

	public AnimationSet idleAnimation,
                        idleAnimation2,
                        selectAnimation,
                        runAnimation,
						turnLeftAnimation,
						turnRightAnimation,
						rollAnimation,
						jumpStartAnimation,
                        jumpStartAnimation2,
                        jumpLoopAnimation,	
						jumpEndAnimation,
						flyAnimation,
						flyUpAnimation,
						flyLeftAnimation,
						flyRightAnimation,
						die2Animation,
						dieAnimation;
	
	public delegate void AnimationHandle();
	public AnimationHandle animationState;
	
	private CharacterController cc;
	private Animation animationComponent;
	private float 	speed_Run, 
					speedFly;

	void Start()
	{
        if (isMenu)
        {
            animationState = Idle2;
        }
        else {
            animationState = Run;
        }
        cc = this.GetComponent<CharacterController> ();
		animationComponent = this.GetComponent<Animation> ();
	}
	
	void Update () 
	{
		if(animationState != null)
			animationState();
        	
		if (GameControll.pause) {
			animationComponent.enabled = false;
		} else {
            animationComponent.enabled = true;
        }
	}

	public void Idle()
	{
		animationComponent[idleAnimation.animation.name].speed = idleAnimation.speedAnimation;
		animationComponent.Play (idleAnimation.animation.name);
	}

    public void Idle2()
    {
        animationComponent[idleAnimation2.animation.name].speed = idleAnimation2.speedAnimation;
        animationComponent.Play(idleAnimation2.animation.name);
        if (animationComponent[idleAnimation2.animation.name].normalizedTime > 0.95f)
        {
            animationState = Idle;
        }
    }

    public void Run()
	{
		
		if (!Controller.iFly) 
		{
			speed_Run = Controller.speed * runAnimation.speedAnimation;
			animationComponent [runAnimation.animation.name].speed = speed_Run;
            animationComponent.Play(runAnimation.animation.name);
        }
	}

	public void TurnLeft()
	{
		animationComponent[turnLeftAnimation.animation.name].speed = turnLeftAnimation.speedAnimation;
		animationComponent.Play(turnLeftAnimation.animation.name);
		if(animationComponent[turnLeftAnimation.animation.name].normalizedTime > 0.95F){
			animationState = Run;	
		}
	}
	
	public void TurnRight()
	{
		animationComponent[turnRightAnimation.animation.name].speed = turnRightAnimation.speedAnimation;
		animationComponent.Play(turnRightAnimation.animation.name);
		if(animationComponent[turnRightAnimation.animation.name].normalizedTime > 0.95f){
			animationState = Run;	
		}
	}

	public void Roll()
	{
		animationComponent [rollAnimation.animation.name].speed = rollAnimation.speedAnimation;;
		animationComponent.Play(rollAnimation.animation.name);
        if (animationComponent[rollAnimation.animation.name].normalizedTime >= 1f)
        {
            animationState = Run;
        }
    }

	public void JumpStart()
	{
		animationComponent.Play(jumpStartAnimation.animation.name);
		if(animationComponent[jumpStartAnimation.animation.name].normalizedTime > 0.9f && Controller.iJump && Controller.doubleJump){
			animationState = JumpLoop;
		}
		else if(animationComponent[jumpStartAnimation.animation.name].normalizedTime > 0.95f){
			animationState = JumpEnd;
		}
	}

    public void JumpStart2()
    {
        animationComponent[jumpStartAnimation2.animation.name].speed = jumpStartAnimation2.speedAnimation;
        animationComponent.Play(jumpStartAnimation2.animation.name);
        if (animationComponent[jumpStartAnimation2.animation.name].normalizedTime > 0.9f && Controller.iJump)
        {
            animationState = JumpLoop;
        }
        else if (animationComponent[jumpStartAnimation2.animation.name].normalizedTime > 0.95f)
        {
            animationState = JumpEnd;
        }
    }

    public void JumpLoop()
	{
		animationComponent[jumpLoopAnimation.animation.name].speed = jumpLoopAnimation.speedAnimation;
		animationComponent.Play(jumpLoopAnimation.animation.name);
		if(animationComponent[jumpLoopAnimation.animation.name].normalizedTime > 0.95f && Controller.iJump){
			animationState = JumpEnd;
		}
		else if(animationComponent[jumpLoopAnimation.animation.name].normalizedTime > 0.95f){
			animationState = JumpEnd;
		}
	}

	public void JumpEnd()
	{
		animationComponent.Play(jumpEndAnimation.animation.name);
		if(animationComponent[jumpEndAnimation.animation.name].normalizedTime > 0.9f || cc.isGrounded == true){
			animationState = Run;	
		}
	}

	public void Die()
	{
        animationComponent[dieAnimation.animation.name].speed = dieAnimation.speedAnimation;
        animationComponent.Play(dieAnimation.animation.name);
	}

	public void Die2()
	{
        animationComponent[die2Animation.animation.name].speed = die2Animation.speedAnimation;
        animationComponent.Play(die2Animation.animation.name);
	}

	public void FlyUp()
	{
		animationComponent[flyUpAnimation.animation.name].speed = flyUpAnimation.speedAnimation;
		animationComponent.Play(flyUpAnimation.animation.name);
		if(animationComponent[flyUpAnimation.animation.name].normalizedTime > 0.95f){
			animationState = Fly;
		}
	}
	
	public void Fly(){
		speedFly = 1f * flyAnimation.speedAnimation;
		animationComponent.Play(flyAnimation.animation.name);
		animationComponent [flyAnimation.animation.name].speed = speedFly;
	}
	
	public void FlyLeft(){
		animationComponent[flyLeftAnimation.animation.name].speed = flyLeftAnimation.speedAnimation;
		animationComponent.Play(flyLeftAnimation.animation.name);
		if(animationComponent[flyLeftAnimation.animation.name].normalizedTime > 0.95f){
			animationState = Fly;	
		}
	}
	
	public void FlyRight(){
		animationComponent[flyRightAnimation.animation.name].speed = flyRightAnimation.speedAnimation;
		animationComponent.Play(flyRightAnimation.animation.name);
		if(animationComponent[flyRightAnimation.animation.name].normalizedTime > 0.95f){
			animationState = Fly;	
		}
	}

    public void Select()
    {
        animationComponent[selectAnimation.animation.name].speed = selectAnimation.speedAnimation;
        animationComponent.Play(selectAnimation.animation.name);
        if (animationComponent[selectAnimation.animation.name].normalizedTime > 0.99f)
        {
            animationState = Idle;
        }
    }

}
