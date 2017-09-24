using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxesTerrainGenerator : MonoBehaviour {

    public int startTerrainCord = 0;
    public int finalTerrainCord = 500;
    private int realTerrainLenght;
    private int qntBoxes = 10;

    public static BoxesTerrainGenerator instance = null;//a instancia comeca vazia
    private Dictionary<int, List<double>> boxes = new Dictionary<int, List<double>>();

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
        realTerrainLenght = finalTerrainCord - startTerrainCord;
    }

    public Dictionary<int, List<double>> generateListBoxes() {
        double tamBox = (double)realTerrainLenght / qntBoxes;


        double aux1 = 0;
        double aux2 = 0;
        for (int i = 0; i < qntBoxes; i++) {
            if (i == 0) {
                aux2 = aux1 + tamBox;
                boxes.Add(i, new List<double>(new double[] { aux1, aux2 }));
            }
            else {
                aux1 = aux2 + 0.0000000001;
                aux2 = aux2 + tamBox;
                boxes.Add(i, new List<double> { aux1, aux2 });
            }
        }

        string s = "";
        foreach (var item in boxes) {
            s = s + " key " + item.Key + " valor1: " + item.Value.ToArray().GetValue(0) + " valor2: " + item.Value.ToArray().GetValue(1);
        }
        Debug.Log(s);
        return boxes;

    }
}
