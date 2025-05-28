using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XRMultiplayer
{
    /// <summary>
    /// ����: �÷��̾� ĳ����(������) ���� �޴�
    /// </summary>
    public class PlayerAppearanceMenu : MonoBehaviour
    {
        [SerializeField] GameObject[] m_PlayerPrefabs;     // ���� ������ ĳ���� ������ �迭
        [SerializeField] Transform m_PreviewParent;        // �̸�����(�������� ���⿡ Instantiate)
        [SerializeField] TMP_InputField m_PlayerNameInputField;

        private GameObject m_CurrentPreviewInstance;        // ���� �̸����� �ν��Ͻ�

        void Awake()
        {
            XRINetworkGameManager.LocalPlayerName.Subscribe(SetPlayerName);
            XRINetworkGameManager.LocalPlayerPrefabIndex.Subscribe(SetPlayerPrefab); // ������ �ε��� ����
        }

        void Start()
        {
            SetPlayerPrefab(XRINetworkGameManager.LocalPlayerPrefabIndex.Value);
            SetPlayerName(XRINetworkGameManager.LocalPlayerName.Value);
        }

        void OnDestroy()
        {
            XRINetworkGameManager.LocalPlayerName.Unsubscribe(SetPlayerName);
            XRINetworkGameManager.LocalPlayerPrefabIndex.Unsubscribe(SetPlayerPrefab);
        }

        // �г��� �Է°� �ݿ�
        public void SubmitNewPlayerName(string text)
        {
            XRINetworkGameManager.LocalPlayerName.Value = text;
        }

        // ĳ���� �������� �������� ����
        public void SetRandomPrefab()
        {
            int prefabCount = m_PlayerPrefabs.Length;
            int currentIdx = XRINetworkGameManager.LocalPlayerPrefabIndex.Value;

            // �ڱ� �ڽ��� ������ ������ �ε��� ����
            List<int> availableIndices = new();
            for (int i = 0; i < prefabCount; ++i)
                if (i != currentIdx) availableIndices.Add(i);

            int randomIdx = (availableIndices.Count > 0) ?
                availableIndices[Random.Range(0, availableIndices.Count)] :
                currentIdx;

            XRINetworkGameManager.LocalPlayerPrefabIndex.Value = randomIdx;
        }

        // ������ �̸�����/���� �Լ� (������ �ݹ�)
        void SetPlayerPrefab(int prefabIndex)
        {
            // ���� �̸����� �ı�
            if (m_CurrentPreviewInstance != null)
                Destroy(m_CurrentPreviewInstance);

            // �� ������ �̸�����(Ȥ�� UI�� Sprite ������ �����൵ ��)
            GameObject prefab = m_PlayerPrefabs[Mathf.Clamp(prefabIndex, 0, m_PlayerPrefabs.Length - 1)];
            m_CurrentPreviewInstance = Instantiate(prefab, m_PreviewParent);
            // ��ġ/������/�ִϸ��̼� �� �߰� Ŀ���� ����
        }

        // �г��� �ݹ�(����)
        void SetPlayerName(string newName)
        {
            m_PlayerNameInputField.text = newName;
        }
    }
}
