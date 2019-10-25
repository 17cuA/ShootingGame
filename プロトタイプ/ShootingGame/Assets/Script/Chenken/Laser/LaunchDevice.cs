using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// レーザー装置タイプ列挙型
/// </summary>
public enum DeviceType
{
	TYPE_1_STRAIGHT,
	TYPE_2_ROTATE,
}

/// <summary>
/// 抽象基本レーザー装置クラス
/// </summary>
public abstract class LaunchDevice
{
	public abstract DeviceType Type { get; }															//タイプ
	public abstract Instance_Laser_Node_Generator CurrentGenerator { get; }			//現在使用するレーザーノード生成装置
	public abstract float CanLaunchTime { get; }													//発射できる時間
	public abstract void GenerateLine();																	//レーザー線（全体、レンダリング制御）
	public abstract void LaunchNode();																	//レーザーノード生成
	public abstract void ResetGenerator();																//レーザーノード生成装置のリセット
	public abstract void UpdateCanLaunchTime();													//発射時間更新
	public abstract void Reset();																			//リセット
}

/// <summary>
/// 派生レーザー装置（直線型レーザー装置）
/// </summary>
public class StraightLaunchDevive : LaunchDevice
{
	private float overloadDuration;				//オーバーロード持続時間
	private float launchInterval;					//レーザーノードの発射間隔
	private float canLaunchTime;					//発射できる時間点
	private float laserWidth;							//レーザーの太さ
	private Material laserMaterial;					//レーザーのマテリアル
	private int pointMax;								//レーザーノードの最大数
	private GameObject emitterInstance;		//発射装置実体（親オブジェクト）

	private Instance_Laser_Node_Generator currentGenerator;	//現在使用するレーザーノード生成装置
	private List<Instance_Laser_Node_Generator> generators; //管理するレーザーノード生成装置リスト

	/// <summary>
	/// コンストラクタ
	/// 構造方法
	/// </summary>
	/// <param name="overloadDuration">　オーバーロード持続時間	</param>
	/// <param name="launchInterval">		レーザーノードの発射間隔	</param>
	/// <param name="laserWidth">			レーザーの太さ				</param>
	/// <param name="laserMaterial">			レーザーのマテリアル		</param>
	/// <param name="pointMax">				レーザーノードの最大数	</param>
	/// <param name="emitterInstance">		発射装置実体（親オブジェクト）</param>
	public StraightLaunchDevive(float overloadDuration, float launchInterval,float laserWidth,Material laserMaterial, int pointMax, GameObject emitterInstance)
	{
		this.overloadDuration = overloadDuration;
		this.launchInterval = launchInterval;
		this.canLaunchTime = 0f;
		this.laserWidth = laserWidth;
		this.laserMaterial = laserMaterial;
		this.pointMax = pointMax;
		this.emitterInstance = emitterInstance;
		this.currentGenerator = null;
		this.generators = new List<Instance_Laser_Node_Generator>();
	}

	/// <summary>
	/// レーザー装置タイプ、取得用
	/// </summary>
	public override DeviceType Type
	{
		get
		{
			return DeviceType.TYPE_1_STRAIGHT;
		}
	}

	/// <summary>
	/// 現在使用するレーザーノード生成装置（取得用）
	/// </summary>
	public override Instance_Laser_Node_Generator CurrentGenerator
	{
		get
		{
			return currentGenerator;
		}
	}

	/// <summary>
	/// 発射できる時間（取得用）
	/// </summary>
	public override float CanLaunchTime
	{
		get
		{
			return canLaunchTime;
		}
	}

	/// <summary>
	/// レーザー線（全体、レンダリング制御）
	/// </summary>
	public override void GenerateLine()
	{
		//******	ここでは簡易のオブジェクトプールです	*****

		//管理するノード生成装置をチェックする
		for (var i = 0; i < generators.Count; ++i)
		{
			//非アクティブの生成装置があれば
			if(!this.generators[i].gameObject.activeSelf)
			{
				//リセットと使用準備
				currentGenerator = generators[i];
				currentGenerator.ResetLineRenderer();
				currentGenerator.IsFixed = true;
				currentGenerator.gameObject.SetActive(true);

				//処理中断
				return;
			}
		}

		//■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
		//■　ノード生成装置が存在しない、もしくは全部アクティブ状態　の場合　■
		//■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■

		//新しくノード生成装置を作る
		var generatorGo = new GameObject("Generator");
		//スクリプトアタッチメント
		var generator = generatorGo.AddComponent<Instance_Laser_Node_Generator>();
		//装置の初期化
		generator.Setting(laserWidth, laserMaterial, pointMax);
		generator.IsFixed = true;

		//親の下で生成、位置初期化
		generatorGo.transform.SetParent(emitterInstance.transform);
		generatorGo.transform.localPosition = Vector3.zero;

		//現在使用する装置にする
		currentGenerator = generator;
		//管理リストに追加
		generators.Add(currentGenerator);
	}

	/// <summary>
	/// レーザーノード生成
	/// </summary>
	public override void LaunchNode()
	{
		//回転しないので、Falseを渡す
		this.currentGenerator.LaunchNode(false);

		//発射できる時間を更新
		//発射間隔（秒）以降で発射再開
		//ごく短い時間
		canLaunchTime = Time.time + launchInterval;
	}

	/// <summary>
	/// レーザーノード生成装置のリセット
	/// </summary>
	public override void ResetGenerator()
	{
		currentGenerator.IsFixed = false;

		//現在使用するノード生成装置をNULLにする
		currentGenerator = null;
	}

	/// <summary>
	/// 発射時間更新
	/// 外での呼び出し
	/// </summary>
	public override void UpdateCanLaunchTime()
	{
		canLaunchTime = Time.time + overloadDuration;
	}

	/// <summary>
	/// リセット
	/// </summary>
	public override void Reset()
	{
		//発射装置のリセット
		//全体リセット

		canLaunchTime = 0;
		currentGenerator.ResetGenerator();
		currentGenerator.ResetLineRenderer();
		currentGenerator.gameObject.SetActive(false);
		currentGenerator = null;
		for(var i = 0; i < generators.Count; ++i)
		{
			GameObject.Destroy(generators[i].gameObject);
		}

		generators.Clear();
	}
}

