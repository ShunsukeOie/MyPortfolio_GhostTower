using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadButton : MonoBehaviour
{
    public void RoadScene(string _sSceneName = null)
    {
        // SEManeger���擾
        SEManager seManeger = SEManager.Instance;
        // SEManeger���特�𗬂�
        seManeger.SettingPlaySE();


        // ���O�������ĂȂ��Ȃ�X�L�b�v
        if (_sSceneName == null) { return; }

        // �w��̃V�[�������[�h����
        SceneManager.LoadScene(_sSceneName);
    }
}
