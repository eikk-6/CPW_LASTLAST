using UnityEngine;

namespace XRMultiplayer
{
    public class CharacterResetter : MonoBehaviour
    {
        [SerializeField] Vector2 m_MinMaxHeight = new Vector2(-2.5f, 25.0f);
        [SerializeField] float m_ResetDistance = 75.0f;
        [SerializeField] Vector3 offlinePosition = new Vector3(0, .5f, -12.0f);
        [SerializeField] Vector3 onlinePosition = new Vector3(0, .15f, 0);
        Vector3 m_ResetPosition;
        private void Start()
        {
            XRINetworkGameManager.Connected.Subscribe(UpdateResetPosition);
            m_ResetPosition = offlinePosition;
            ResetPlayer();
        }

        void UpdateResetPosition(bool connected)
        {
            if (connected)
            {
                m_ResetPosition = onlinePosition;
            }
            else
            {
                m_ResetPosition = offlinePosition;
                ResetPlayer();
            }
        }

        void Update()
        {
            if (transform.position.y < m_MinMaxHeight.x)
            {
                ResetPlayer();
            }
            else if (transform.position.y > m_MinMaxHeight.y)
            {
                ResetPlayer();
            }
            if (Mathf.Abs(transform.position.x) > m_ResetDistance || Mathf.Abs(transform.position.z) > m_ResetDistance)
            {
                ResetPlayer();
            }
        }

        public void ResetPlayer()
        {
            ResetPlayer(m_ResetPosition);
        }

        void ResetPlayer(Vector3 destination)
        {
            // 텔레포트 기능 제거 → 위치 강제 이동
            transform.position = destination;
            transform.rotation = Quaternion.identity;
        }

        [ContextMenu("Set Player To Online Position")]
        void SetPlayerToOnlinePosition()
        {
            ResetPlayer(onlinePosition);
        }

        [ContextMenu("Set Player To Offline Position")]
        void SetPlayerToOfflinePosition()
        {
            ResetPlayer(offlinePosition);
        }
    }
}
