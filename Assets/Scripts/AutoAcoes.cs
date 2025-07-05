using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAcoes : MonoBehaviour {


    public bool Rodar;
    public float angperseg;

    public bool Balancar;
    private float velAng;
    private float timerB;
    private float omega;
    public float periodo;
    public float amplitude;


    public bool Escalar;
    public float scaleI;
    public float scaleF;
    public bool voltar; // Vai e volta
    public bool repetir; // Repete o escalonamento infinitamente
    public bool pararnofim; // Faz o escalonamento apenas uma vez
    public float tempoIda;
    public float tempoVolta;
    private float timerE;
    private bool voltarsafe;

    public bool IrVoltar;
    public bool mexerX;
    public bool mexerY;
    public float xRange;
    public float yRange;
    public float tempoTrajx;
    public float tempoTrajy;
    public float acelx;
    public float acely;
    public float inittimex;
    public float inittimey;
    private float timerIx;
    private float timerIy;
    private int indox;
    private int indoy;

    public bool FazCirculo;
    public float xRaio;
    public float yRaio;
    public float tempoRaiox;
    public float tempoRaioy;
    private float timerFx;
    private float timerFy;
    private float lastposx;
    private float lastposy;


    // Use this for initialization
    void Start () {

        if (Balancar)
        {
            timerB = 0;
            omega = Mathf.PI * 2 / periodo;
            transform.Rotate(new Vector3(0, 0, amplitude));
        }

        if (Escalar)
        {
            transform.localScale = new Vector3(scaleI, scaleI);
        }
        timerE = 0;
        voltarsafe = voltar;

        if (IrVoltar)
        {
            timerIx = inittimex;
            timerIy = inittimey;
            indox = 1;
            indoy = 1;
        }

        if (FazCirculo)
        {
            timerFx = 0;
            timerFy = 0;
            lastposx = 0;
            lastposy = 0;

            if (tempoRaiox == 0)
                tempoRaiox++;

            if (tempoRaioy == 0)
                tempoRaioy++;
        }
    }

    // Update is called once per frame
    void Update () {


        // RODAR ***********************

        if (Rodar)
        {
            transform.Rotate (0,0, angperseg*Time.deltaTime);
        }


        // BALANCAR *********************
        if (Balancar)
        {
            if (timerB > periodo)
                timerB = 0;

            velAng = -omega * amplitude * Mathf.Sin(omega * timerB);

            transform.Rotate(new Vector3(0, 0, velAng * Time.deltaTime));

            timerB += Time.deltaTime;
        }


        // ESCALAR **********************
        if (Escalar)
        {
            timerE += Time.deltaTime;

            float scale = scaleI + (scaleF - scaleI) * timerE / tempoIda;

            if (scale < 0)
                scale = 0;


            transform.localScale = new Vector3(scale, scale,1);


            if (timerE > tempoIda)
            {
                timerE = 0;

                float aux = scaleI;
                scaleI = scaleF;
                scaleF = aux;
                aux = tempoIda;
                tempoIda = tempoVolta;
                tempoVolta = aux;

                if (voltar)
                {
                    voltar = false;
                }
                else
                {
                    voltar = voltarsafe;

                    if (!repetir)
                    {   
                        if (pararnofim)
                        {
                            Escalar = false;
                            timerE = 0;
                        }
                        else
                        {
                            gameObject.SetActive(false);
                        }
                    }
                }
            }
        }



        // IR E VOLTAR ***********************************
        if (IrVoltar)
        {

            float posx = 0;
            float posy = 0;

            if (mexerX)
                posx =  indox * (Time.deltaTime *  xRange / tempoTrajx + 500* acelx *Time.deltaTime * Time.deltaTime);

            if (mexerY)
                posy =  indoy * (Time.deltaTime * yRange / tempoTrajy + 500* acely * Time.deltaTime * Time.deltaTime);


            transform.position += new Vector3(posx, posy);


            if (indox == 1)
                timerIx += Time.deltaTime;
            else
                timerIx -= Time.deltaTime;

            if (indoy == 1)
                timerIy += Time.deltaTime;
            else
                timerIy -= Time.deltaTime;


            if (timerIy > tempoTrajy)
            {
                timerIy = tempoTrajy;
                indoy = indoy * -1;
            }

            if (timerIx > tempoTrajx)
            {
                timerIx = tempoTrajx;
                indox = indox * -1;
            }

            if (timerIy < 0)
            {
                timerIy = 0;
                indoy = indoy * -1;
            }

            if (timerIx < 0)
            {
                timerIx = 0;
                indox = indox * -1;
            }

        }




        // FAZER CIRCULO *************************************
        if (FazCirculo)
        {
            timerFx += Time.deltaTime;
            timerFy += Time.deltaTime;

            if (timerFx > tempoRaiox)
                timerFx = 0;

            if (timerFy > tempoRaioy)
                timerFy = 0;

            float posx = Mathf.Sin(2 * Mathf.PI * timerFx / tempoRaiox) * xRaio;
            float posy = Mathf.Sin(2 * Mathf.PI * timerFy / tempoRaioy) * yRaio;

            transform.position += new Vector3(posx - lastposx, posy - lastposy);

            lastposx = posx;
            lastposy = posy;

        }

    }


    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    public void DoBalanco()
    {
        Balancar = true;
    }

    public void DoCirculo()
    {
        FazCirculo = true;
    }

    public void DoIrVoltar()
    {
        IrVoltar = true;
    }

    public void DoEscalar()
    {

        if (Escalar)
            return;
        
        Escalar = true;
        voltar = voltarsafe;
    }
}
