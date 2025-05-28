using UnityEngine;

public class CharacterVisualSwitcher : MonoBehaviour
{
    public GameObject[] visualPrefabs;   // 캐릭터 외형 프리팹들
    public Transform visualParent;       // 외형 생성 위치 (CharacterVisual)
    public GameObject selectionUI;       // UI 전체 (선택 완료 후 숨길 용도)

    private GameObject currentVisual;    // 현재 표시 중인 외형 인스턴스
    private int currentIndex = 0;        // 현재 선택된 index

    void Start()
    {
        // 기존 자식 비주얼 제거 (씬에서 기본으로 붙어 있던 경우 대비)
        foreach (Transform child in visualParent)
        {
            Destroy(child.gameObject);
        }

        // PlayerPrefs에서 저장된 index 불러오기 (있으면)
        currentIndex = PlayerPrefs.GetInt("SelectedCharacterIndex", 0);
        SwitchTo(currentIndex);
    }

    public void SwitchTo(int index)
    {
        // index가 유효하지 않으면 무시
        if (index < 0 || index >= visualPrefabs.Length) return;

        // 기존 비주얼 제거
        if (currentVisual != null)
        {
            Destroy(currentVisual);
        }

        // 새로운 비주얼 Instantiate
        currentVisual = Instantiate(visualPrefabs[index], visualParent);
        currentVisual.transform.localPosition = Vector3.zero;
        currentVisual.transform.localRotation = Quaternion.identity;
        currentVisual.transform.localScale = Vector3.one;

        currentIndex = index; // 현재 index 업데이트
    }

    public void Next()
    {
        int newIndex = (currentIndex + 1) % visualPrefabs.Length;
        SwitchTo(newIndex);
    }

    public void Prev()
    {
        int newIndex = (currentIndex - 1 + visualPrefabs.Length) % visualPrefabs.Length;
        SwitchTo(newIndex);
    }

    public void ConfirmSelection()
    {
        PlayerPrefs.SetInt("SelectedCharacterIndex", currentIndex);
        PlayerPrefs.Save();

        Debug.Log($"선택 완료: 캐릭터 {currentIndex}");

        if (selectionUI != null)
        {
            selectionUI.SetActive(false); // UI 숨기기
        }

        // 이후 플레이 시작 로직 추가 가능
    }
}
