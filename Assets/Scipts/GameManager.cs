using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[FormerlySerializedAs("guidObj")] [HideInInspector]
	public GameObject guideObj; //引导物体

	public Transform guideTrs; //存储下一步生成道路坐标以及位置信息
	public GameObject roadTemplate; //道路模板

	public int maxBuildRoundNum = 10; //一次最大生成数
	public int turnSeedRange = 10;
	public List<GameObject> roads; //保存现在能够进行还原的道路

	//	[FormerlySerializedAs("isBuidDirRoad")]
//	public bool isBuildDirRoad; //是否生成方向道路

//	[HideInInspector]
//	public int goldNumber; //吃到的金币数

	int buildRoundNum = 0; //确定转向后的回合数
//	int dirRoadType; //方向道路的类型
//
//	int dirRoadNumber; //方向道路的数量

	private void Start()
	{
		guideObj = Instantiate(roadTemplate);
		guideObj.transform.position = Vector3.zero;
		guideObj.transform.rotation = Quaternion.identity;
		guideObj.name = "Guide";

		guideTrs = guideObj.transform; //上面实例化生成引导物体并存储下一步的道路信息

		for (int i = 0; i < 20; i++) //预先生成20格道路作为跑道
		{
			var tmpRoad = Instantiate(roadTemplate, guideTrs.position, guideTrs.rotation);
			guideTrs.position += guideTrs.forward; //每生成一个道路，引导物体的位置改变
		}

		for (int i = 0; i < 30; i++)
		{
			BuildRoad(); //先生成30个动画效果
		}
	}

	// 回合数更新
	private void resetBuildRoundNum()
	{
		buildRoundNum = maxBuildRoundNum; 
	}

	// 创建转向
	private void BuildTurn()
	{
		int dictSeed = Random.Range(1, 2);
		for (int i = 0; i < 3; i++) //先生成3个格子的道路，作为转向区
		{
			BuildStraight();

		}

		int rotationAngle = (dictSeed == 1) ? 90 : -90;
		guideTrs.position -= guideTrs.forward * 2; //转向区域生成完成后，引导物体回退2格
		guideTrs.Rotate(Vector3.up, rotationAngle); //转向
		guideTrs.position += guideTrs.forward * 2; //转向后引导物体的forward轴改变，改变pos值，到达下一个道路的生成地点
	}
	
	// 创建直路
	private void BuildStraight()
	{
		var tmpRoad = Instantiate(roadTemplate, guideTrs.position, guideTrs.rotation);
		PlayRoadAnimator(tmpRoad);
		guideTrs.position += guideTrs.forward; //每生成一个道路，引导物体的位置改变
		roads.Add(tmpRoad);
	}
	
	/// <summary>
	/// 生成道路的方法
	/// </summary>
	public void BuildRoad()
	{
		int turnSeed = Random.Range(1, turnSeedRange); //用于确定是否转向
		if (turnSeed == 1 && buildRoundNum <= 0)
		{
			resetBuildRoundNum();
			BuildTurn();
		}
		else
		{
			BuildStraight();
		}

		buildRoundNum--;
	}

	/// <summary>
	/// 播放道路动画
	/// </summary>
	/// <param name="road"></param>
	public void PlayRoadAnimator(GameObject road)
	{
		var tmpRoadController = road.GetComponent<RoadController>();
		if (tmpRoadController == null)
		{
			return;
		}

		tmpRoadController.ChangeChildrens();
	}
}