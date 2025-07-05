using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoedaBehavior : MonoBehaviour {

    // Components dele
    private UnityEngine.UI.Text texto;
    private SpriteRenderer sr;

    // Estado
    private bool descoberto;

    // Sprites usados
    private Sprite sCostas, sFrente;

	// Use this for initialization
	void Start () {

        texto = GetComponentInChildren<UnityEngine.UI.Text>();

        sr = GetComponent<SpriteRenderer>();

        setDescoberto(false, " ");
		
	}
	
    // Usado quande ele inicia para salvar os sprites;
    public void setSprites( Sprite sc, Sprite sf)
    {
        sCostas = sc;
        sFrente = sf;
    }

    // Usando quando ele deve mudar de descoberto
    public void setDescoberto(bool d, string s)
    {
        descoberto = d;

        texto.text = s;

        if (descoberto)
            Acertou();
        else
            Errou();

    }

    // Se acertou
    private void Acertou()
    {
        sr.sprite = sFrente;

    }

    // Se errou
    private void Errou()
    {
        sr.sprite = sCostas;
    }


	// Update is called once per frame
	void Update () {
		
	}
}
