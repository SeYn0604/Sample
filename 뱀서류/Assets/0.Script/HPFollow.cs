using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPFollow : MonoBehaviour
{
    RectTransform rect;
    public GameObject player;  // �÷��̾� ��ü�� ���� �����ϵ��� public ���� �߰�

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        if (player != null)  // �÷��̾ �Ҵ�Ǿ� ���� ���� ����
        {
            Vector3 newPosition = new Vector3(player.transform.position.x, player.transform.position.y - 0.81f, player.transform.position.z);
            rect.position = newPosition;
        }
    }
}
