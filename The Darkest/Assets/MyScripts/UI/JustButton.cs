using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class JustButton : MonoBehaviour
{
    //무조건 ui에 대한 상호작용이 있어야하기 때문에 public을 부모에 위치 시킴
    //UIControll는 list안에 넣어 esc로 끌수 있게끔 만들기위함 아마 지금은 클릭으로 list위치 변경은
    //playerstatus와 inventory만 적용을했지만 다른곳에도 필요한 일인지 고민을 해봅시다
    //한가지 문제점이 더있음 yes no 창이 뜨고 u,i를 눌러 UI를 띄우면 이상하다고 생각함
    public UIControll UIControll;
    public RectTransform UI;
    public abstract void OnClick();
}
