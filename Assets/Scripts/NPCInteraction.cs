using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    GameManager gm;
    InteractiveCode code;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        code = this.GetComponent<InteractiveCode>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.hadTalked && gm.unicornHorns == 0)
            code.codeNumber = 1001;
        else if (gm.hadTalked && gm.unicornHorns == 1)
            code.codeNumber = 1002;
        else if (gm.hadTalked && gm.unicornHorns > 1)
            code.codeNumber = 1003;

        if (gm.hadTalked && gm.Doll > 0)
            code.codeNumber = 1004;
    }

    public void PressKeyText()
    {
    }
}
