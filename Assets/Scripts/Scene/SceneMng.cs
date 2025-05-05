using UnityEngine;
using UnityEngine.SceneManagement;

// �i�|�C���g�j
// static�N���X�i�ÓI�N���X�j�ɕύX���邱��
// ���\�b�h���S��static�i�ÓI���\�b�h�j�ɂ��邱��
// MonoBehaviour�N���X�͍폜���邱��
public static class SceneController
{
    // �X�e�[�W���N���A���ꂽ���𔻒肷��
    public static bool stage1Clear = false;
    public static bool stage2Clear = false;
    public static bool stage3Clear = false;

    public static string sceneName;

    // �u���̃��\�b�h�����s���ꂽ���ɊJ���Ă���V�[���̖��O�v���擾����B
    // ����̏ꍇ�́A�Q�[���I�[�o�[�̏��������������ɁA���̃��\�b�h�����s����B
    public static void CurrentSceneName()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    // ��L�̃��\�b�h�Ŏ擾���ꂽ�V�[���ɖ߂�B
    // ����̏ꍇ�́A�R���e�B�j���[�{�^�������������ɂ��̃��\�b�h�����s����B
    public static void BackToBeforeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void BackToSelectScene()
    {
        SceneManager.LoadScene("StageSelect");
    }

    // �ǂ̃X�e�[�W���N���A�������𔻒肵�A�w��̃V�[�������[�h����B
    // �S�[���I�u�W�F�N�g�ɐG�ꂽ���ɁA���̃��\�b�h�����s����B
    public static void StageClaer()
    {
        sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            // �N���A�����X�e�[�W��Stage1��������
            case "Game_Stage1":
                {
                    // �܂��t���O���オ���Ă��Ȃ��Ƃ��̂�
                    if (!stage1Clear)
                    {
                        // �t���O���グ��
                        stage1Clear = true;
                    }
                    // �X�e�[�W�Z���N�g�̉�ʂɖ߂�
                    SceneManager.LoadScene("StageSelect");
                }
                break;

            // �N���A�����X�e�[�W��Stage2��������
            case "Game_Stage2":
                {
                    // �܂��t���O���オ���Ă��Ȃ��Ƃ��̂�
                    if (!stage2Clear)
                    {
                        // �t���O���グ��
                        stage2Clear = true;
                    }
                    // �X�e�[�W�Z���N�g�̉�ʂɖ߂�
                    SceneManager.LoadScene("StageSelect");
                }
                break;

            // �N���A�����X�e�[�W��Stage3��������
            case "Game_Stage3":
                {
                    // �܂��t���O���オ���Ă��Ȃ��Ƃ��̂�
                    if (!stage3Clear)
                    {
                        // �t���O���グ��
                        stage3Clear = true;
                    }
                    // �Q�[���N���A�̉�ʂɈڂ�
                    SceneManager.LoadScene("GameClear");
                }
                break;
        }
    }
}