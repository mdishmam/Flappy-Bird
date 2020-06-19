using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class GameOverWindow : MonoBehaviour
{
    private Text scoreText;

    private void Awake()
    {
        scoreText = transform.Find("scoreText").GetComponent<Text>();

        transform.Find("RetryButton").GetComponent<Button_UI>().ClickFunc = () => { UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene"); };
        //FunctionTimer.Create(() =>
        //{
        //    UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        //}, 1f);
        transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
    private void Start()
    {
        Bird.GetInstance().OnDied += Bird_OnDied;
        Hide();
    }

    private void Update()
    {
        transform.Find("ExitButton").GetComponent<Button_UI>().ClickFunc = () => { Application.Quit(); };
    }

    private void Bird_OnDied(object sender, System.EventArgs e)
    {
        //throw new System.NotImplementedException();
        scoreText.text = Level.GetInstance().GetPipePassed().ToString();
        Show();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
