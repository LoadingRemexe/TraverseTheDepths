using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public Animator animator;
    [SerializeField] Animator CharacterAnimator = null;
    public string sentence;
    public float typingSpeed; //the smaller this is, the faster it will go

    IEnumerator Type()
    {
        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            NextSentence();
            animator.SetBool("IsOpen", true);
            if (CharacterAnimator) CharacterAnimator.SetBool("Talking", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StopCoroutine("Type");
            animator.SetBool("IsOpen", false);
            if (CharacterAnimator) CharacterAnimator.SetBool("Talking", false);
        }
    }

    public void NextSentence()
    {
            textDisplay.text = "";
            StartCoroutine("Type");
    }
}
