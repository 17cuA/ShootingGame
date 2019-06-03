namespace Power
{
	/// <summary>
	/// パワー取得時コールバック
	/// </summary>
	public delegate void OnPickCallBack();

	/// <summary>
	/// パワー更新時コールバック
	/// </summary>
	/// <param name="deltaTime"></param>
	public delegate void OnUpdateCallBack(float deltaTime);

	/// <summary>
	/// パワー再取得時コールバック
	/// </summary>
	public delegate void OnPickAgainCallBack();

	/// <summary>
	/// パワー無効化時コールバック
	/// </summary>
	public delegate void OnLostCallBack();
}