public interface ILoadingScreen
{
    /// <summary>
    /// 更新載入進度，數值範圍 0 ~ 1
    /// </summary>
    void SetProgress(float value);
}
