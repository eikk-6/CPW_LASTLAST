using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectManager : MonoBehaviour
{
    public GameObject[] playerPrefabs;      // ���� ������ ĳ���� �����յ�
    public Transform previewParent;         // �����並 ������ ��ġ

    private GameObject currentPreview;      // ���� �������� �ִ� �̸����� ĳ����
    private int currentIndex = 0;           // ���� �ε���

    void Start()
    {
        ShowPreview();
    }

    public void Next()
    {
        currentIndex = (currentIndex + 1) % playerPrefabs.Length;
        ShowPreview();
    }

    public void Prev()
    {
        currentIndex = (currentIndex - 1 + playerPrefabs.Length) % playerPrefabs.Length;
        ShowPreview();
    }

    public void Select()
    {
        PlayerPrefs.SetInt("SelectedCharacterIndex", currentIndex);
        SceneManager.LoadScene("MainGameScene");
    }

    private void ShowPreview()
    {
        // ���� ������ ����
        if (currentPreview != null)
        {
            Destroy(currentPreview);
        }

        // ���ο� ĳ���� ������ ����
        currentPreview = Instantiate(playerPrefabs[currentIndex], previewParent.position, Quaternion.identity, previewParent);

        // �Է�/���� ������ ������Ʈ ����
        var input = currentPreview.GetComponent<UnityEngine.InputSystem.PlayerInput>();
        if (input != null)
            Destroy(input);

        var characterController = currentPreview.GetComponent<CharacterController>();
        if (characterController != null)
            Destroy(characterController);
    }
}
