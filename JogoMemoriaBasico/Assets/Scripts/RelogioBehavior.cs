using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelogioBehavior : MonoBehaviour {

    private bool ligado;

    private float timer;

    private UnityEngine.UI.Text texto;

	// Use this for initialization
	void Start () {
		
        ligado = false;

        texto = GetComponentInChildren<UnityEngine.UI.Text>();
	}
	
	// Update is called once per frame
	void Update () {
	    
        if (ligado)
        {
            timer += Time.deltaTime;
            texto.text = timer.ToString("N0");
        }
	}

    public void Turn(bool onoff)
    {
        ligado = onoff;

        if(ligado)
            timer = 0;
    }
}
