using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Power
{
	/// <summary>
	/// パワーアップ種類列挙
	/// </summary>
	public enum PowerType
	{
		POWER_SHIELD,
		POWER_BULLET_UPGRADE,
	}

	public abstract class AbstractPower
	{
		/// <summary>
		/// パワー取得時イベント
		/// </summary>
		public event OnPickCallBack onPickCallBack;

		/// <summary>
		/// パワー更新時イベント
		/// </summary>
		public event OnUpdateCallBack onUpdateCallBack;

		/// <summary>
		/// パワー再取得時イベント
		/// </summary>
		public event OnPickAgainCallBack onPickAgainCallBack;

		/// <summary>
		/// パワー無効化時イベント
		/// </summary>
		public event OnLostCallBack onLostCallBack;

		/// <summary>
		/// パワーアップ種類
		/// 内部保存用データ
		/// </summary>
		private PowerType type;
		/// <summary>
		/// パワーアップ種類
		/// 取得用データ
		/// </summary>
		public PowerType Type
		{
			get
			{
				return type;
			}
		}

		/// <summary>
		/// パワーアップ消滅フラグ
		/// Trueの場合は自動的に削除する
		/// オーバーライド可能
		/// </summary>
		public abstract bool IsLost { get; }

		public bool AlreadyLost { get; set; }

		/// <summary>
		/// パワーアップ持続時間（例：倍速モード、スローモーション使う可能性あり）
		/// デフォルトは-1(使わない)
		/// パワー取得時（コンストラクタで格納する）
		/// </summary>
		protected float duration = -1;

		/// <summary>
		/// パワーアップ持続時間
		/// 取得用
		/// </summary>
		public float Duration
		{
			get
			{
				return duration;
			}
		}

		/// <summary>
		/// パワーアップ値（例：シールド）
		/// デフォルトは-1(使わない)
		/// パワー取得時（コンストラクタで格納する）
		/// </summary>
		public int value = -1;

		/// <summary>
		/// 持続時間用タイマー
		/// パワーアップ開始時点カウントする
		/// 内部保存用データ
		/// </summary>
		protected float timer = .0f;

		/// <summary>
		/// 持続時間用タイマー
		/// 取得用データ
		/// </summary>
		public float Timer
		{
			get
			{
				return timer;
			}
		}

		/// <summary>
		/// コンストラクタ①
		/// 構造方法
		/// デフォルト設定（持続時間なし、設定値なし）時このコンストラクタが起動する
		/// </summary>
		/// <param name="type">パワーアップ種類</param>
		/// <param name="manager">パワーアップマネージャー</param>
		protected AbstractPower(PowerType type)
		{
			this.type = type;
		}

		/// <summary>
		/// コンストラクタ②
		/// 構造方法
		/// 持続時間を設定時このコンストラクタが起動する
		/// </summary>
		/// <param name="type">パワーアップ種類</param>
		/// <param name="duration">パワーアップ持続時間</param>
		/// <param name="manager">パワーアップマネージャー</param>
		protected AbstractPower(PowerType type, float duration)
		{
			this.type = type;
			this.duration = duration;
		}

		/// <summary>
		/// コンストラクタ③
		/// 構造方法
		/// 設定値ある時このコンストラクタが起動する
		/// </summary>
		/// <param name="type">パワーアップ種類</param>
		/// <param name="value">パワーアップ設定値</param>
		/// <param name="manager">パワーアップマネージャー</param>
		protected AbstractPower(PowerType type, int value)
		{
			this.type = type;
			this.value = value;
		}

		/// <summary>
		/// コンストラクタ④
		/// 構造方法
		/// 持続時間と設定値ある時このコンストラクタが起動する
		/// </summary>
		/// <param name="type">パワーアップ種類</param>
		/// <param name="duration">パワーアップ持続時間</param>
		/// <param name="value">パワーアップ設定値</param>
		/// <param name="manager">パワーアップマネージャー</param>
		protected AbstractPower(PowerType type, float duration, int value)
		{
			this.type = type;
			this.duration = duration;
			this.value = value;
		}

		/// <summary>
		/// パワーアップ初めて所得時の処理
		/// オーバーライド可能
		/// </summary>
		public virtual void OnPick()
		{
			onPickCallBack?.Invoke();
			AlreadyLost = false;
		}

		/// <summary>
		/// パワーアップ更新時の処理
		/// オーバーライド可能
		/// </summary>
		/// <param name="deltaTime">デルタタイム</param>
		public virtual void OnUpdate(float deltaTime) { onUpdateCallBack?.Invoke(deltaTime); }

		/// <summary>
		/// パワーアップ消滅時の処理
		/// オーバーライド可能
		/// </summary>
		public virtual void OnLost()
		{
			onLostCallBack?.Invoke();
			AlreadyLost = true;
		}

		/// <summary>
		/// パワーアップ再所得時の処理
		/// オーバーライド可能
		/// </summary>
		public virtual void OnPickAgain() { onPickAgainCallBack?.Invoke(); }

		/// <summary>
		/// DEBUG用プリントメソッド
		/// </summary>
		/// <returns>パワーアップ情報</returns>
		public string Print()
		{
			return string.Format("パワー型:" + Type.ToString() + "\n" +
								 "パワー時間" + timer + "(" + duration + ")" + "\n" +
								 "パワー値" + value);
		}
	}
}
