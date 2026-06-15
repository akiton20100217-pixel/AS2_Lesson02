using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class UIMainGame : MonoBehaviour
{
    //UIMainGameを参照する
    [Header("UI Document")]
    public UIDocument uid;

    private VisualElement _Root;
    private Label _ScoreText;
    private Button _GameQuitButton;

    void Start()
    {
        //ルートの取得
        _Root = uid.rootVisualElement;

        //スコアテキストの取得
        _ScoreText = _Root.Q<Label>("ScoreText");
        //スコアテキストの更新
        _ScoreText.text = "ココにスコアの表示";

        //ゲーム終了ボタンの取得
        _GameQuitButton = _Root.Q<Button>("GameQuitButton");
        //押下された時の設定
        _GameQuitButton.clicked += () =>
        {
            Debug.Log("ゲーム終了");
            EditorApplication.isPlaying = false;
            Application.Quit();
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
