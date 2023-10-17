using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour, IPointerClickHandler
{
    public RectTransform LevelNum;
    public RectTransform LevelPersent;
    bool LevelNumAppear = true;
    public void OnStart()
    {
        SceneManager.LoadScene("The Darkest");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("»Æ¿Œ");
            LevelNumAppear = !LevelNumAppear;
            LevelPersent.gameObject.SetActive(!LevelNumAppear);
            LevelNum.gameObject.SetActive(LevelNumAppear);
        }
    }
}
