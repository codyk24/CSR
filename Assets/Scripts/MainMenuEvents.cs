using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument m_document;

    private Button m_startButton;

    private List<Button> m_menuButtons;

    // Start is called before the first frame update
    void Awake()
    {
        m_document = GetComponent<UIDocument>();

        // Q is short for query
        m_startButton = m_document.rootVisualElement.Q("Start") as Button;
        m_startButton.RegisterCallback<ClickEvent>(OnClickStartButton);

        m_menuButtons = m_document.rootVisualElement.Query<Button>().ToList();
        foreach (var button in m_menuButtons)
        {
            button.RegisterCallback<ClickEvent>(OnMenuButtonClicked);
        }
    }

    private void OnDisable()
    {
        m_startButton.UnregisterCallback<ClickEvent>(OnClickStartButton);

        foreach (var button in m_menuButtons)
        {
            button.UnregisterCallback<ClickEvent>(OnMenuButtonClicked);
        }
    }

    // Update is called once per frame
    private void OnClickStartButton(ClickEvent click)
    {
        Debug.LogFormat("DEBUG... Start button got clicked!");        
    }

    private void OnMenuButtonClicked(ClickEvent click)
    {
        Debug.LogFormat("DEBUG... Do stuff for all buttons on the menu");
    }
}
