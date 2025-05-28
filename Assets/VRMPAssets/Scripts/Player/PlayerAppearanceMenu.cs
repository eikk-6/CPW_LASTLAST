using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XRMultiplayer
{
    /// <summary>
    /// 예시: 플레이어 캐릭터(프리팹) 선택 메뉴
    /// </summary>
    public class PlayerAppearanceMenu : MonoBehaviour
    {
        [SerializeField] GameObject[] m_PlayerPrefabs;     // 선택 가능한 캐릭터 프리팹 배열
        [SerializeField] Transform m_PreviewParent;        // 미리보기(프리팹을 여기에 Instantiate)
        [SerializeField] TMP_InputField m_PlayerNameInputField;

        private GameObject m_CurrentPreviewInstance;        // 현재 미리보기 인스턴스

        void Awake()
        {
            XRINetworkGameManager.LocalPlayerName.Subscribe(SetPlayerName);
            XRINetworkGameManager.LocalPlayerPrefabIndex.Subscribe(SetPlayerPrefab); // 프리팹 인덱스 구독
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

        // 닉네임 입력값 반영
        public void SubmitNewPlayerName(string text)
        {
            XRINetworkGameManager.LocalPlayerName.Value = text;
        }

        // 캐릭터 프리팹을 랜덤으로 선택
        public void SetRandomPrefab()
        {
            int prefabCount = m_PlayerPrefabs.Length;
            int currentIdx = XRINetworkGameManager.LocalPlayerPrefabIndex.Value;

            // 자기 자신을 제외한 무작위 인덱스 선택
            List<int> availableIndices = new();
            for (int i = 0; i < prefabCount; ++i)
                if (i != currentIdx) availableIndices.Add(i);

            int randomIdx = (availableIndices.Count > 0) ?
                availableIndices[Random.Range(0, availableIndices.Count)] :
                currentIdx;

            XRINetworkGameManager.LocalPlayerPrefabIndex.Value = randomIdx;
        }

        // 프리팹 미리보기/변경 함수 (옵저버 콜백)
        void SetPlayerPrefab(int prefabIndex)
        {
            // 기존 미리보기 파괴
            if (m_CurrentPreviewInstance != null)
                Destroy(m_CurrentPreviewInstance);

            // 새 프리팹 미리보기(혹은 UI에 Sprite 등으로 보여줘도 됨)
            GameObject prefab = m_PlayerPrefabs[Mathf.Clamp(prefabIndex, 0, m_PlayerPrefabs.Length - 1)];
            m_CurrentPreviewInstance = Instantiate(prefab, m_PreviewParent);
            // 위치/스케일/애니메이션 등 추가 커스텀 가능
        }

        // 닉네임 콜백(동일)
        void SetPlayerName(string newName)
        {
            m_PlayerNameInputField.text = newName;
        }
    }
}
