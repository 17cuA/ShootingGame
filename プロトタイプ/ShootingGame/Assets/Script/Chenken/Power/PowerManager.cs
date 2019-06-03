using System;
using System.Collections.Generic;
using UnityEngine;

namespace Power
{
	public class PowerManager : Singleton<PowerManager>
	{
		/// <summary>
		/// パワーアップリスト
		/// 全部のパワーアップはここで保存する
		/// </summary>
		private List<AbstractPower> powers = new List<AbstractPower>();

		/// <summary>
		/// パワーアップ数
		/// </summary>
		public int PowerCount
		{
			get
			{
				return powers.Count;
			}
		}

		/// <summary>
		/// 新規パワー
		/// </summary>
		/// <param name="power">パワークラス</param>
		public void AddPower(AbstractPower power)
		{
			//Null　パラメータ
			if(power == null)
			{
				Debug.LogError("Null Power");
				return;
			}

			//パワー種類検索
			for(int i = 0; i < powers.Count; ++i)
			{
				if(powers[i].Type == power.Type)
				{
					Debug.Log("既に存在するパワー");
					return;
				}
			}
			//検索し終わり、持っていないパワー種類を確認した
			//入れとく
			powers.Add(power);
		}

		/// <summary>
		/// パワー削除
		/// </summary>
		/// <param name="type">パワー種類</param>
		public void RemovePower(PowerType type)
		{
			//パワー種類検索
			for (int i = 0; i < powers.Count; ++i)
			{
				//存在するパワー種類
				if (powers[i].Type == type)
				{
					//削除
					powers.Remove(powers[i]);
					return;
				}
			}
			Debug.Log("存在しないパワー、何も削除してなかった");
		}

		/// <summary>
		/// パワーピック
		/// </summary>
		/// <param name="power">パワーアップクラス</param>
		public void Pick(PowerType type)
		{
			//パワー検索
			for (int i = 0; i < powers.Count; ++i)
			{
				//検索種類と同じ
				if (powers[i].Type == type)
				{
					//既に無効
					if(powers[i].IsLost)
					{
						//有効にする
						//初めて取得として認識する
						Debug.Log("取得");
						powers[i].OnPick();
						return;
					}
					//有効
					else
					{
						//再取得として認識する
						Debug.Log("再取得");
						powers[i].OnPickAgain();
						return;
					}				
				}
			}

			Debug.Log("存在しないパワーアップ");
		}


		/// <summary>
		/// ワーアップ消す（無効化）
		/// オーバーロードメソッド
		/// </summary>
		/// <param name="type">パワーアップ種類</param>
		public void Lost(PowerType type)
		{
			AbstractPower lostPower = null;
			//リスト検索
			for (int i = 0; i < powers.Count; ++i)
			{
				//検索パワー種類が存在する
				if (powers[i].Type == type)
				{
					//削除するパワーを取得
					lostPower = powers[i];
					break;
				}
			}
			if (lostPower == null)
			{
				Debug.LogError("存在しないパワータイプ：" + type + "、削除失敗");
				return;
			}
			//無効化
			lostPower.OnLost();
		}

		/// <summary>
		/// パワーアップリストからパワーアップを取得
		/// </summary>
		/// <param name="type">パワーアップタイプ</param>
		/// <returns></returns>
		public AbstractPower GetPower(PowerType type)
		{
			AbstractPower ret = null;
			//リスト検索
			for (int i = 0; i < powers.Count; ++i)
			{
				//検索パワーアップ種類がある場合
				if (powers[i].Type == type)
				{
					//パワーアップクラス取得、ループ中断
					ret = powers[i];
					break;
				}
			}
			if (ret == null)
			{
				Debug.LogError("存在しないパワータイプ：" + type + "、取得失敗");
				return null;
			}
			return ret;
		}

		/// <summary>
		/// 指定パワーを持っているかどうかを検索する
		/// </summary>
		/// <param name="type">パワーアップ種類</param>
		/// <returns>True: 持っている</returns>
		/// <returns>False: 持っていない</returns>
		public bool HasPower(PowerType type)
		{
			for (int i = 0; i < powers.Count; ++i)
			{
				//検索パワーアップ種類がある場合
				if (powers[i].Type == type)
				{
					//パワーは持っている、Trueを返す
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 持っているパワーアップを更新
		/// </summary>
		/// <param name="deltaTime">デルタタイム</param>
		public void OnUpdate(float deltaTime)
		{
			//全部のパワーアップ更新
			if (powers != null && powers.Count > 0)
			{
				for (int i = 0; i < powers.Count; ++i)
				{
					//無効ではない限り更新する
					if (!powers[i].IsLost)
					{
						powers[i].OnUpdate(deltaTime);
					}
					//無効で、まだLost（PowerType）を実行していない
					if(powers[i].IsLost && !powers[i].AlreadyLost)
					{
						Lost(powers[i].Type);
					}
				}
			}
		}

		/// <summary>
		/// DEBUG用
		/// 持っているすべてのパワーアップ情報を印刷する
		/// </summary>
		/// <returns></returns>
		public string Print()
		{
			var ret = string.Empty;
			if (powers != null && powers.Count > 0)
			{
				for (int i = 0; i < powers.Count; ++i)
				{
					ret += powers[i].Print();
				}
			}
			return ret;
		}
	}
}

