using UnityEngine;

public class RetryButton : MonoBehaviour
{
    public void OnRetryButton()
    {
        // 直前のシーンに遷移する。
        SceneController.BackToBeforeScene();
    }
}