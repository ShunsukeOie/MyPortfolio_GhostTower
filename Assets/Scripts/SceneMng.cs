using UnityEngine;
using UnityEngine.SceneManagement;

// （ポイント）
// staticクラス（静的クラス）に変更すること
// メソッドも全てstatic（静的メソッド）にすること
// MonoBehaviourクラスは削除すること
public static class SceneController
{
    public static string sceneName;

    // 「このメソッドが実行された時に開いているシーンの名前」を取得する。
    // 今回の場合は、ゲームオーバーの条件が揃った時に、このメソッドを呼び出す。
    public static void CurrentSceneName()
    {
        sceneName = SceneManager.GetActiveScene().name;
        Debug.Log(sceneName);
    }

    // 上記のメソッドで取得されたシーンに戻る。
    // 今回の場合は、コンティニューボタンを押した時にこのメソッドを実行する。
    public static void BackToBeforeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}