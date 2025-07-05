using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoTentarNovamenteBehavior : MonoBehaviour {

    public void Click()
    {
        CartaController.controller.TentarNovamente();
    }
}
