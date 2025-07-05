using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoedaController : MonoBehaviour {

    // Controller Atual
    static public MoedaController controller = null;

    // Lista de sprites usados nas cartas
    public Sprite MoedaFrente;
    public Sprite MoedaCostas;

    // Sample do objeto
    public GameObject Moeda;

    // Vetor de posicoes das moedas
    private List<Vector3> matriz;
    private MoedaBehavior[] moedas;

    // Tamanho do lugar onde serao distribuidas as cartas
    public float altura;

    // Quantidade de moedas
    public int numMoedas;

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


        // Vetor de moedas
        moedas = new MoedaBehavior[numMoedas];

        // Pra cada sprite cria dois objetos cartas
        for(int i = 0; i<numMoedas;i++)
        {
            
            GameObject m1 = Instantiate(Moeda,transform);

            moedas[i] = m1.GetComponent<MoedaBehavior>();

            // Faz elas serem iguais
            moedas[i].setSprites(MoedaCostas,MoedaFrente);

            // Coloca elas em posicoes aleatorias
            m1.transform.position = matriz[i];


            print(i.ToString());

        }

    }

    // cria a matriz
    private int createMatriz()
    {
        matriz = new List<Vector3>();

        // Espaçamentos entre as cartas
        float delta_y = altura / (numMoedas - 1);

        // Cria pontos
        for (int i = 0; i < numMoedas; i++)
            matriz.Add(new Vector3(0f, i * delta_y - altura/2, -1f) + transform.position);


        return 1;

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + new Vector3(-1 / 2, altura / 2, 0), transform.position + new Vector3(1 / 2, altura / 2, 0));
        Gizmos.DrawLine(transform.position + new Vector3(-1 / 2, -altura / 2, 0), transform.position + new Vector3(1 / 2, -altura / 2, 0));
        Gizmos.DrawLine(transform.position + new Vector3(-1 / 2, -altura / 2, 0), transform.position + new Vector3(-1 / 2, altura / 2, 0));
        Gizmos.DrawLine(transform.position + new Vector3(1 / 2, altura / 2, 0), transform.position + new Vector3(1 / 2, -altura / 2, 0));
    }

    public void Abrir(string nome)
    {
        
        moedas[selecionados].setDescoberto(true, nome);
        selecionados++;
    }

    public void Fechar()
    {

        for (int i = 0; i < numMoedas; i++)
            moedas[i].setDescoberto(false, " ");

        selecionados = 0;
    }
}
