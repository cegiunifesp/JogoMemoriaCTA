using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaController : MonoBehaviour{

    // Controller Atual
    static public CartaController controller = null;

    // Lista de sprites usados nas cartas
    public List<Sprite> Cartas;
    public Sprite CartaCostas;

    // Sample do objeto
    public GameObject Carta;

    // Vetor de posicoes das cartas
    private List<Vector3> matriz;

    // Tamanho do lugar onde serao distribuidas as cartas
    public float largura;
    public float altura;

    // Quantidade de cartas
    public int num_cartas_h, num_cartas_v;

    // Quantidade de cartas selecionadas
    private int selecionados;

    // Cartas esperando
    private CartaBehavior c1, c2;

    // Botoes
    public GameObject BotaoTry;
    public GameObject BotaoIniciar;



    void Start()
    {
        
        selecionados = 0; // Inicia com 0 selecionadas

        // Faz com que exista apenas um controller
        if(controller == null)
            controller = this;
        else
            return;


        // Cria a matriz de posicoes
        if (createMatriz() == 0)
        {
            Debug.Log("Erro na quantidade das cartas");
            return;
        }

        // Pra cada sprite cria dois objetos cartas
        foreach (Sprite s in Cartas)
        {
            GameObject c1 = Instantiate(Carta,transform);
            GameObject c2 = Instantiate(Carta,transform);

            int id = Cartas.IndexOf(s);

            // Faz elas serem iguais
            c1.GetComponent<CartaBehavior>().setCarta(s, CartaCostas, id);
            c2.GetComponent<CartaBehavior>().setCarta(s, CartaCostas, id);

            // Coloca elas em posicoes aleatorias
            id = Random.Range(0, matriz.Count);
            c1.transform.position = matriz[id];
            matriz.RemoveAt(id);

            id = Random.Range(0, matriz.Count);
            c2.transform.position = matriz[id];
            matriz.RemoveAt(id);

        }

        //Esconde botaotry
        BotaoTry.SetActive(false);
    }

    // cria a matriz
    private int createMatriz()
    {
        // Se o numero de cartas nao bate com o de sprites da erro
        if (num_cartas_h * num_cartas_v != Cartas.Count * 2)
            return 0;

        matriz = new List<Vector3>();

        // Espaçamentos entre as cartas
        float delta_x = largura / (num_cartas_h - 1);
        float delta_y = altura / (num_cartas_v - 1);

        // Cria pontos
        for (int i = 0; i < num_cartas_h; i++)
            for (int j = 0; j < num_cartas_v; j++)
                matriz.Add(new Vector3(i * delta_x - largura/2 , j * delta_y - altura/2, -1) + transform.position);

        return 1;

    }

    // Sempre que a selecao de uma carta muda é chamada essa funcao pra atualizar o numero de selecionados
    public void addSelecionados(int i)
    {
        selecionados += i;
    }

    // retorna o numero de selecionados
    public int getSelecionados()
    {
        return selecionados;
    }

    // teste se todos os selecionados sao iguais
    public void testeSelecionados()
    {
        // Faz teste

        // pega o primeiro selecionado
        CartaBehavior selecionado = null;

        // pra cada um faz o teste
        foreach (CartaBehavior c in transform.GetComponentsInChildren<CartaBehavior>())
            if (c.getSelecao())
            if (selecionado == null)
                selecionado = c;
            else if (selecionado.getID() == c.getID())
                Acertou(selecionado, c);
            else
                Errou(selecionado, c);
    }

    private void Acertou(CartaBehavior c1, CartaBehavior c2)
    {
        print("Faz par");
        c1.setPareado();
        c2.setPareado();
        addSelecionados(-2);
        MoedaController.controller.Abrir(c1.getName());
    }

    private void Errou(CartaBehavior c1, CartaBehavior c2)
    {
        print("nao Faz par");
        this.c1 = c1;
        this.c2 = c2;
        BotaoTry.SetActive(true);
    }

    public void TentarNovamente()
    {
        c1.setNonPareado();
        c2.setNonPareado();
        addSelecionados(-2);
        BotaoTry.SetActive(false);
    }

    public void IniciarJogo()
    {
        BotaoIniciar.SetActive(false);
    }

    private void FimdeJogo()
    {
        BotaoIniciar.SetActive(true);
    }
        
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + new Vector3(-largura / 2, altura / 2, 0), transform.position + new Vector3(largura / 2, altura / 2, 0));
        Gizmos.DrawLine(transform.position + new Vector3(-largura / 2, -altura / 2, 0), transform.position + new Vector3(largura / 2, -altura / 2, 0));
        Gizmos.DrawLine(transform.position + new Vector3(-largura / 2, -altura / 2, 0), transform.position + new Vector3(-largura / 2, altura / 2, 0));
        Gizmos.DrawLine(transform.position + new Vector3(largura / 2, altura / 2, 0), transform.position + new Vector3(largura / 2, -altura / 2, 0));
    }
}

