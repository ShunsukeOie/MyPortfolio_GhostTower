using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadButton : MonoBehaviour
{
    public void RoadScene(string _sSceneName = null)
    {
        // SEManegerを取得
        SEManager seManeger = SEManager.Instance;
        // SEManegerから音を流す
        seManeger.SettingPlaySE();


        // 名前が入ってないならスキップ
        if (_sSceneName == null) { return; }

        // 指定のシーンをロードする
        SceneManager.LoadScene(_sSceneName);
    }
}
