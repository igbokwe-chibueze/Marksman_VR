using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDifficulty : MonoBehaviour
{
    private Button button;

    [Tooltip("The script controlling the game.")]
    public GameController gameController;

    [Tooltip("The games difficulty level.")]
    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        //gameController.StartGame(1);

        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }

    /* When a button is clicked, call the StartGame() method
     * and pass it the difficulty value (1, 2, 3) from the button 
    */
    void SetDifficulty()
    {
        gameController.StartGame(difficulty);
    }
}
