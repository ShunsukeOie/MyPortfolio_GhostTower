using UnityEngine;

public class RetryButton : MonoBehaviour
{
    public void OnRetryButton()
    {
        // ���O�̃V�[���ɑJ�ڂ���B
        SceneController.BackToBeforeScene();
    }
}