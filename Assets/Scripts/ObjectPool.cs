using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab; // ������Ʈ Ǯ�� ������ ������
    public int size; // �ʱ� ������ ������Ʈ ����

    public List<GameObject> objectPool = new List<GameObject>(); // ������Ʈ Ǯ ����Ʈ

    private void Start()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>(); // �ڽ� ������Ʈ���� ������
        int a = transform.childCount; // �ڽ� ������Ʈ ������ ����

        // �ʱ� ������ �ڽ� ������Ʈ���� ������Ʈ Ǯ�� �߰��ϰ� ��Ȱ��ȭ
        for (int i = 0; i < transform.childCount; i++)
        {
            objectPool.Add(transform.GetChild(i).gameObject); // ������Ʈ Ǯ�� �ڽ� ������Ʈ �߰�
            objectPool[i].gameObject.SetActive(false); // �ڽ� ������Ʈ�� ��Ȱ��ȭ
        }
    }

    public GameObject GetObject(Vector3 startPos, Quaternion startRotation)
    {
        // ������Ʈ Ǯ���� ��Ȱ��ȭ�� ������Ʈ�� ã�� Ȱ��ȭ�ϰ� ��ȯ
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = startPos; // ���� ��ġ�� �̵�
                obj.transform.rotation = startRotation;
                obj.SetActive(true); // ������Ʈ Ȱ��ȭ
                return obj; // ������Ʈ ��ȯ
            }
        }

        // ������Ʈ Ǯ�� ��� ������ ������Ʈ�� ���� ��� ���ο� ������Ʈ ����
        GameObject newObj = Instantiate(prefab, transform); // �������� �����Ͽ� ���ο� ������Ʈ ����
        newObj.transform.position = startPos;
        newObj.transform.rotation = startRotation;
        newObj.SetActive(true); // ���ο� ������Ʈ Ȱ��ȭ
        objectPool.Add(newObj); // ������Ʈ Ǯ�� ���ο� ������Ʈ �߰�
        return newObj; // ���ο� ������Ʈ ��ȯ
    }
}
