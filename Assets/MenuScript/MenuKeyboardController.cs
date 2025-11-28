using UnityEngine;
using UnityEngine.UI;

public class MenuKeyboardController : MonoBehaviour
{
    public Button[] buttons;
    int index = 0;

    void Start()
    {
        SelectButton();
    }

    void Update()
    {
        float vertical = Input.GetAxis("Vertical");

        if (vertical > 0.5f)
        {
            index--;
            if (index < 0) index = buttons.Length - 1;
            SelectButton();
            Invoke("ResetInput", 0.2f);
            enabled = false;
        }

        if (vertical < -0.5f)
        {
            index++;
            if (index >= buttons.Length) index = 0;
            SelectButton();
            Invoke("ResetInput", 0.2f);
            enabled = false;
        }

        if (Input.GetButtonDown("Submit"))
        {
            buttons[index].onClick.Invoke();
        }
    }

    void SelectButton()
    {
        buttons[index].Select();
    }

    void ResetInput()
    {
        enabled = true;
    }
}

