using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadButton : MonoBehaviour
{
    public void RoadScene(string _sSceneName = null)
    {
        // 名前が入ってないならスキップ
        if (_sSceneName == null) { return; }

        // 指定のシーンをロードする
        SceneManager.LoadScene(_sSceneName);
    }
}
