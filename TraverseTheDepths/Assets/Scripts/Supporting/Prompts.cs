using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Prompts : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI prompt = null;
    [SerializeField] Animator promptAnim = null;

    int index = 0;
    string[] prompts = new string[]
    {
        "Move MOUSE to look around",
        "Use WASD to swim",
        "Press SPACE to ascend",
        "Press SHIFT to descend",
        "Breath deeply and calmly",
        "Let the water overtake you",
        "Theres no need to rush",
        "Let your mind clear of stress",
        "Relax the tension in your body",
        "Breath In",
        "Breath Out",
        "Remember everything is temporary",
        "Water is fluid and so is life"
    };

    private void Start()
    {
        prompt.text = prompts[index];
    }

    void nextPrompt()
    {
        index++;
        if (index == prompts.Length) index = 0;
        prompt.text = prompts[index];
    }
}
