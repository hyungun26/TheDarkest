using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class JustButton : MonoBehaviour
{
    //������ ui�� ���� ��ȣ�ۿ��� �־���ϱ� ������ public�� �θ� ��ġ ��Ŵ
    //UIControll�� list�ȿ� �־� esc�� ���� �ְԲ� ��������� �Ƹ� ������ Ŭ������ list��ġ ������
    //playerstatus�� inventory�� ������������ �ٸ������� �ʿ��� ������ ����� �غ��ô�
    //�Ѱ��� �������� ������ yes no â�� �߰� u,i�� ���� UI�� ���� �̻��ϴٰ� ������
    public UIControll UIControll;
    public RectTransform UI;
    public abstract void OnClick();
}
