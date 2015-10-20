using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour {

    public enum ButtonModes
    { 
        None,
        Up,
        Down,
        Left,
        Right
    }

    public enum ButtonColors
    { 
        White,
        Black
    }

    public enum ButtonWalls
    { 
        None,
        Top,
        Left,
        Bottom,
        Right
    }

    public ButtonModes mode = ButtonModes.None;
    public ButtonColors color = ButtonColors.White;
    public ButtonWalls walls = ButtonWalls.None;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// 
    /// </summary>
    public void ButtonClickHandler()
    { 
        //Toggle button mode
    }
}
