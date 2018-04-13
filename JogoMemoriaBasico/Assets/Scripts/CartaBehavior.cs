using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaBehavior : MonoBehaviour {

    // Identificao da carta
    private int id_carta;

    // Sprites usados
    private Sprite sprite_frente;
    private Sprite sprite_costas;

    // Estados
    private bool selecionado;
    private bool pareado;

    // Renderer
    private SpriteRenderer sr;

    //Animacao
    private int girar;
    private float timer;
    private const float timer_virar = 0.5f;


    void Awake()
    {
        // Inicia estados e Renderer
        sr = GetComponent<SpriteRenderer>();
        selecionado = false;
        pareado = false;
        girar = 0;
        timer = timer_virar;
    }

    // Ajusta os sprites e a id
    public void setCarta(Sprite sf, Sprite sc, int id)
    {
        sprite_frente = sf;
        sprite_costas = sc;
        id_carta = id;
        sr.sprite = sc;
    }

    // Retorna se esta selecionado
    // Usado no testeSelecionados para pegar quem esta selecionado
    public bool getSelecao()
    {
        return selecionado;
    }

    // Retorna ID
    // Usado no testeSelecionados para ver se os selecionados tem a mesma ID
    public int getID()
    {
        return id_carta;
    }


    // Quando encontrado o par ajusta o estado
    public void setPareado()
    {
        pareado = true;
        selecionado  = false;
    }

    // Quando nao encontrado o par ajusta o estado e o sprite
    public void setNonPareado()
    {
        selecionado = false;
        mudaSprite();
    }

    // Quando clickado
    void clickado()
    {
        // Se ja tem dois selecionados ou ja esta pareado nao faz nada
        if((CartaController.controller.getSelecionados() == 2) || pareado)
            return;

        // Se nao esta selecionado atualiza estado, muda sprite e aumenta o valor dos selecionados
        if(!selecionado)
        {
            selecionado = true;
            mudaSprite();
            CartaController.controller.addSelecionados(1);
        }
        else // Se ja estava selecionado desvira (?) talvez deva tirar
        {
            selecionado = false;
            mudaSprite();
            CartaController.controller.addSelecionados(-1);
        }
    }

    // faz a animacao de mudanca de sprite
    private void mudaSprite()
    {
        // Muda Girar
        girar = 1;
    }

    void OnMouseDown()
    {
        clickado();
    }

    void Update()
    {
        // Se é pra girar
        if (girar != 0)
        {

            // gira
            transform.Rotate(new Vector3(0, 180 * girar, 0) * Time.deltaTime);

            // Se ja foi ate a metade do caminho
            if ((transform.eulerAngles.y > 90) && (transform.eulerAngles.y < 270))
            {
                // muda direcao do giro
                girar = -1;
                transform.eulerAngles = new Vector3(0, 90, 0);

                // muda sprite
                if (selecionado)
                    sr.sprite = sprite_frente;
                else
                    sr.sprite = sprite_costas;
            }

            // se terminou caminho
            if (transform.eulerAngles.y > 270)
            {
                // para
                girar = 0;
                transform.eulerAngles = new Vector3(0, 0, 0);

                // caso tenha 2 manda testar
                if (CartaController.controller.getSelecionados() == 2)
                    timer = 0f;
                else
                    timer = timer_virar;

            }
                
        }
        else
        {
            // quando der o timer_virar testa
            if (timer < timer_virar)
                timer += Time.deltaTime;
            else if (timer > timer_virar)
            {
                timer = timer_virar;
                CartaController.controller.testeSelecionados(); // Se o valor dos selecionados agora é 2 faz o teste
            }
        }

    }

}
