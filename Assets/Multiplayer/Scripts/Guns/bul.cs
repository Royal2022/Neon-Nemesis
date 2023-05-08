using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bul : NetworkBehaviour
{
    private uint _owner;
    private bool _inited;
    private Vector3 _target;

    [Server]
    public void Init(uint owner, Vector3 target)
    {
        //who did the shot
        //��� ������ �������
        _owner = owner;

        //where the bullet should go
        //���� ������ ������ ����
        _target = target;
        _inited = true;
    }

    void Update()
    {
        if (_inited && isServer)
        {
            transform.Translate((_target - transform.position).normalized * 0.04f);

            

            //bullet has reached the final destination
            //���� �������� �������� �����
            if (Vector3.Distance(transform.position, _target) < 0.1f)
            {
                //then we should destroy it
                //������ �� ����� ����������
                NetworkServer.Destroy(gameObject);
            }
        }
    }
}
