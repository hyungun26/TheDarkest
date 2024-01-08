using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //Dragon UI
    public RectTransform DragonBattleUI;
    public Slider DecreaseHP;
    public Slider DecreaseHP2;
    public RectTransform[] alpha;
    [SerializeField]
    float alphaControll = 0.0f;
    float startAlpha = 0.0f;
    float endAlpha = 1.0f;
    [SerializeField]
    float lerpTime = 0.5f; // lerp되는 시간 조절가능
    float currentTime = 0.0f;
    [SerializeField]
    public bool dragonState = false;
    [SerializeField]
    public bool dragonDead = false;    

    // Start is called before the first frame update
    void Start()
    {
        alpha = GetComponentsInChildren<RectTransform>();
        foreach (RectTransform child in alpha)
        {
            if (child.name != "Dragon")
                child.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if(dragonState)
        DragonHPUI();
    }

    public void DragonHPUI()
    {
        startAlpha = 0.0f;
        endAlpha = 1.0f;
        for (int i = 0; i < alpha.Length; i++)
        {
            alpha[i].gameObject.SetActive(true);
            if (alpha[i].GetComponent<Image>() != null) // Image component를 가지고있다면
            {
                Color color = alpha[i].GetComponent<Image>().color;
                color.a = alphaControll;
                alpha[i].GetComponent<Image>().color = color;
            }
            else if (alpha[i].GetComponent<TextMeshProUGUI>() != null)
            {
                Color color = alpha[i].GetComponent<TextMeshProUGUI>().color;
                color.a = alphaControll;
                alpha[i].GetComponent<TextMeshProUGUI>().color = color;
            }
        }
        if(dragonDead)
        {
            float val = startAlpha;
            startAlpha = endAlpha;
            endAlpha = val;
        }
        StartCoroutine(UIOnOff(startAlpha, endAlpha));
    }


    IEnumerator UIOnOff(float start, float end) // fade in fade out 효과 코딩인데... 중복코드란 말이지
    {
        while (!Mathf.Approximately(alphaControll, end))
        {
            currentTime += Time.deltaTime;
            if(currentTime >= lerpTime)
            {
                currentTime = lerpTime;
            }
            alphaControll = Mathf.Lerp(start, end, currentTime/lerpTime); //lerp를 사용할때는 시작지점과 끝점을 정해놓고 하는게 좋다.
            yield return alphaControll;
        }
        currentTime = 0.0f;
        dragonState = false;
        if(dragonDead)
        {
            this.transform.gameObject.SetActive(false);
        }
    }
}
