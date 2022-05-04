using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColor : MonoBehaviour
{
    [SerializeField] Button _button = null;

    private void Start()
    {
        ResetButtonColor();
    }

    public void DisableButtonColor()
    {
        _button.GetComponent<Image>().color = Color.gray;
    }

    public void ResetButtonColor()
    {
        _button.GetComponent<Image>().color = Color.white;
    }
}
