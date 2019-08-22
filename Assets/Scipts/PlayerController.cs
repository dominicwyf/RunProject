using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

//引入DoTween插件
/* 思考题
 * 1：由于我们的地形有高有底，会导致我们的（playController.isGrounded）检测是否在地面有时失效。怎么解决呢？
 * 2：玩家在进行下滑时，身形会能够通过比较低一点的地形，这一块这个Demo里面没有。那么我们如何实现这个功能呢？
 */

public class PlayerController : MonoBehaviour
{
	public float initPlayerSpeed = 3.0f;
	public float speedUpCoefficient = 3.0f;
	[FormerlySerializedAs("playAnimator")] [FormerlySerializedAs("playAnimtor")] 
	public Animator playerAnimator;
	
	public float moveSpeed = 6.0f;
	public float jumpSpeed = 8.0f;
	public float gravity = 20.0f;
//	public float jumpPower; //玩家的跳跃高度
//	[HideInInspector] public GameObject nowRoad; //现在玩家脚下的道路
//	bool isTurnleftEnd = true; //左转向是否完成
//	bool isTurnRightEnd = true; //右转向是否完成
//	bool isJumpState; //现在是否是转向状态

//	RuntimeAnimatorController nowController; //现在的动画控制器
//	AnimationClip[] cilps;

	CharacterController characterController;
	Vector3 MoveIncrements;
	[SerializeField] float transverseSpeed = 5.0f; //玩家横向的移动速度

	void Start()
	{		
		characterController = GetComponent<CharacterController>();

//		jumpPower = 3.0f;
//		playController = GetComponent<CharacterController>();
//		playAnimator = GetComponent<Animator>();
//		nowController = playAnimator.runtimeAnimatorController;
//		cilps = nowController.animationClips;
//		for (int i = 0; i < cilps.Length; i++)
//		{
//			if (cilps[i].events.Length <= 0)
//			{
//				switch (cilps[i].name)
//				{
//					case "JUMP00":
//						AnimationEvent endEvent = new AnimationEvent();
//						endEvent.functionName = "JumpEnd";
//						endEvent.time = cilps[i].length - (20.0f / 56.0f) * 1.83f;
//						cilps[i].AddEvent(endEvent);
//						AnimationEvent centerEvent = new AnimationEvent();
//						centerEvent.functionName = "JumpCenter";
//						centerEvent.time = cilps[i].length * 0.3f;
//						cilps[i].AddEvent(centerEvent);
//						break;
//					case "SLIDE00":
//						AnimationEvent slideEvent = new AnimationEvent();
//						slideEvent.functionName = "SlideEnd";
//						slideEvent.time = cilps[i].length - (15.0f / 42.0f) * 1.33f;
//						cilps[i].AddEvent(slideEvent);
//						break;
//				}
//			}
//		}
	}

	void handleInput()
	{
//		if (Input.GetKeyDown(KeyCode.J) && isTurnleftEnd)
//		{
//			isTurnleftEnd = false; //更新转向状态
//			transform.Rotate(Vector3.up, -90);
//			Quaternion tmpQuaternion = transform.rotation; //计算转向后的四元数并保存
//			transform.Rotate(Vector3.up, 90); //角度回滚
//			Tween tween = transform.DORotateQuaternion(tmpQuaternion, 0.3f); //使用DoTween插件进行转向的平滑运动
//			tween.OnComplete(() => isTurnleftEnd = true); //动画结束后转向状态更新
//		}
//
//		if (Input.GetKeyDown(KeyCode.L) && isTurnRightEnd)
//		{
//			isTurnRightEnd = false;
//			transform.Rotate(Vector3.up, 90);
//			Quaternion tmpQuaternion = transform.rotation;
//			transform.Rotate(Vector3.up, -90);
//			Tween tween = transform.DORotateQuaternion(tmpQuaternion, 0.3f);
//			tween.OnComplete(() => isTurnRightEnd = true);
//		}
//
//		if (Input.GetKeyDown(KeyCode.Space) && playController.isGrounded)
//		{
//			isJumpState = true; //更新跳跃状态
//			playAnimator.SetBool("IsJump", true); //播放跳跃动画
//		}
		
		if (Input.GetKeyDown(KeyCode.W))
		{
			SetPlayerMove();
		}
		
//		if (Input.GetKeyUp(KeyCode.W))
//		{
//			SetPlayerIdle();
//		}
	}

	void SetPlayerMove()
	{
		playerAnimator.SetBool("isMove", true);

//		if (characterController.isGrounded) {
//			MoveIncrements = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
//			MoveIncrements = transform.TransformDirection(MoveIncrements);
//			MoveIncrements *= moveSpeed;
//			if (Input.GetButton("Jump"))
//				MoveIncrements.y = jumpSpeed;
//		}
//		MoveIncrements.y -= gravity * Time.deltaTime;
		characterController.Move(transform.forward * Time.deltaTime);
//		playerAnimator.SetFloat("moveSpeed", characterController.velocity.magnitude); //动画状态更新
	}

	void SetPlayerIdle()
	{
		playerAnimator.SetBool("isMove", false);
	}
	
	void Update()
	{
//		moveSpeed += Time.deltaTime * 0.3f;
//		float moveDir = Input.GetAxis("Horizontal");
//		MoveIncrements = transform.forward * moveSpeed * Time.deltaTime;
//		MoveIncrements += transform.right * transverseSpeed * Time.deltaTime * moveDir;
//		if (isJumpState) //如果现在正在进行跳跃
//		{
//			MoveIncrements.y += jumpPower * Time.deltaTime;
//		}
//		else
//		{
//			MoveIncrements.y += playController.isGrounded ? 0f : -5.0f * Time.deltaTime * 1f; //更新重力
//		}
//
//		playController.Move(MoveIncrements);
//		playAnimator.SetFloat("MoveSpeed", playController.velocity.magnitude); //动画状态更新

		handleInput();
	}

	/*private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.gameObject != nowRoad) //去重复避免删除错误
		{
			nowRoad = hit.gameObject;
			Destroy(hit.gameObject, 1.0f);
			GameManager.instance.BuidRoad(); //生成道路
			GameManager.instance.CloseRoadAnimator();
		}
	}

	public void JumpEnd()
	{
		playAnimator.SetBool("IsJump", false);
	}

	public void JumpCenter()
	{
		isJumpState = false;
	}

	public void SlideEnd()
	{
		playAnimator.SetBool("IsSlide", false);
	}*/
}