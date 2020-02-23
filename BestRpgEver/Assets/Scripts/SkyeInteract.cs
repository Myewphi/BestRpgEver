using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkyeInteract : InteractBase {

    [SerializeField]
    private GameObject npcText;

    [SerializeField]
    private Image portrait;
    [SerializeField]
    private Sprite newPortrait;
    [SerializeField]
    private Text text;
    [SerializeField]
    private string[] newText;
    private int line;

    public override void Interact()
    {
        npcText.SetActive(true);
        portrait.sprite = newPortrait;
        line = 0;
        text.text = newText[0];
        player.GetComponent<BasicMovement>().cannotMoveBool = true;
    }

    public void NextLine()
    {
        line += 1;
        if (newText.Length > line)
        {
            text.text = newText[line];
        } else
        {
            EndInteract();
        }
    }

    public void EndInteract()
    {
        npcText.SetActive(false);
        portrait.sprite = null;
        text.text = null;
        player.GetComponent<BasicMovement>().cannotMoveBool = false;
    }

    public void Update()
    {
        if(Input.GetButtonDown("Submit"))
        {
            NextLine();
        }
        if(Input.GetButtonDown("Cancel"))
        {
            EndInteract();
        }
    }
}
