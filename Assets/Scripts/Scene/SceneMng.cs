using UnityEngine;
using UnityEngine.SceneManagement;

// （ポイント）
// staticクラス（静的クラス）に変更すること
// メソッドも全てstatic（静的メソッド）にすること
// MonoBehaviourクラスは削除すること
public static class SceneController
{
    // ステージがクリアされたかを判定する
    public static bool stage1Clear = false;
    public static bool stage2Clear = false;
    public static bool stage3Clear = false;

    public static string sceneName;

    // 「このメソッドが実行された時に開いているシーンの名前」を取得する。
    // 今回の場合は、ゲームオーバーの条件が揃った時に、このメソッドを実行する。
    public static void CurrentSceneName()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    // 上記のメソッドで取得されたシーンに戻る。
    // 今回の場合は、コンティニューボタンを押した時にこのメソッドを実行する。
    public static void BackToBeforeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void BackToSelectScene()
    {
        SceneManager.LoadScene("StageSelect");
    }

    // どのステージをクリアしたかを判定し、指定のシーンをロードする。
    // ゴールオブジェクトに触れた時に、このメソッドを実行する。
    public static void StageClaer()
    {
        sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            // クリアしたステージがStage1だったら
            case "Game_Stage1":
                {
                    // まだフラグが上がっていないときのみ
                    if (!stage1Clear)
                    {
                        // フラグを上げる
                        stage1Clear = true;
                    }
                    // ステージセレクトの画面に戻る
                    SceneManager.LoadScene("StageSelect");
                }
                break;

            // クリアしたステージがStage2だったら
            case "Game_Stage2":
                {
                    // まだフラグが上がっていないときのみ
                    if (!stage2Clear)
                    {
                        // フラグを上げる
                        stage2Clear = true;
                    }
                    // ステージセレクトの画面に戻る
                    SceneManager.LoadScene("StageSelect");
                }
                break;

            // クリアしたステージがStage3だったら
            case "Game_Stage3":
                {
                    // まだフラグが上がっていないときのみ
                    if (!stage3Clear)
                    {
                        // フラグを上げる
                        stage3Clear = true;
                    }
                    // ゲームクリアの画面に移る
                    SceneManager.LoadScene("GameClear");
                }
                break;
        }
    }
}