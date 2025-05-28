using UnityEngine;

public class CharacterVisualSwitcher : MonoBehaviour
{
    public GameObject[] visualPrefabs;   // ĳ���� ���� �����յ�
    public Transform visualParent;       // ���� ���� ��ġ (CharacterVisual)
    public GameObject selectionUI;       // UI ��ü (���� �Ϸ� �� ���� �뵵)

    private GameObject currentVisual;    // ���� ǥ�� ���� ���� �ν��Ͻ�
    private int currentIndex = 0;        // ���� ���õ� index

    void Start()
    {
        // ���� �ڽ� ���־� ���� (������ �⺻���� �پ� �ִ� ��� ���)
        foreach (Transform child in visualParent)
        {
            Destroy(child.gameObject);
        }

        // PlayerPrefs���� ����� index �ҷ����� (������)
        currentIndex = PlayerPrefs.GetInt("SelectedCharacterIndex", 0);
        SwitchTo(currentIndex);
    }

    public void SwitchTo(int index)
    {
        // index�� ��ȿ���� ������ ����
        if (index < 0 || index >= visualPrefabs.Length) return;

        // ���� ���־� ����
        if (currentVisual != null)
        {
            Destroy(currentVisual);
        }

        // ���ο� ���־� Instantiate
        currentVisual = Instantiate(visualPrefabs[index], visualParent);
        currentVisual.transform.localPosition = Vector3.zero;
        currentVisual.transform.localRotation = Quaternion.identity;
        currentVisual.transform.localScale = Vector3.one;

        currentIndex = index; // ���� index ������Ʈ
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

        Debug.Log($"���� �Ϸ�: ĳ���� {currentIndex}");

        if (selectionUI != null)
        {
            selectionUI.SetActive(false); // UI �����
        }

        // ���� �÷��� ���� ���� �߰� ����
    }
}
