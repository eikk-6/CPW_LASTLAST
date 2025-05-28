using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectManager : MonoBehaviour
{
    public GameObject[] playerPrefabs;      // 선택 가능한 캐릭터 프리팹들
    public Transform previewParent;         // 프리뷰를 보여줄 위치

    private GameObject currentPreview;      // 현재 보여지고 있는 미리보기 캐릭터
    private int currentIndex = 0;           // 현재 인덱스

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
        // 기존 프리뷰 삭제
        if (currentPreview != null)
        {
            Destroy(currentPreview);
        }

        // 새로운 캐릭터 프리뷰 생성
        currentPreview = Instantiate(playerPrefabs[currentIndex], previewParent.position, Quaternion.identity, previewParent);

        // 입력/조작 방지용 컴포넌트 제거
        var input = currentPreview.GetComponent<UnityEngine.InputSystem.PlayerInput>();
        if (input != null)
            Destroy(input);

        var characterController = currentPreview.GetComponent<CharacterController>();
        if (characterController != null)
            Destroy(characterController);
    }
}
