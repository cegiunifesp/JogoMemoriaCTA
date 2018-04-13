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
                matriz.Add(new Vector3(i * delta_x - largura/2 , j * delta_y - altura/2, -1));

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
                {
                    print("Faz par");
                    selecionado.setPareado();
                    c.setPareado();
                    addSelecionados(-2);
                }
                else
                {
                print("nao Faz par");
                    selecionado.setNonPareado();
                    c.setNonPareado();
                    addSelecionados(-2);

                }
    }

}

