using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Container {
    public bool isFull;
    public double start, end, qnt;
    public Container(bool isFull, double start, double end, double qnt) {
        this.isFull = isFull;
        this.start = start;
        this.end = end;
        this.qnt = qnt;
    }
}

public class BoxesTerrainGenerator : MonoBehaviour {

    private int startTerrainCord = 0;
    private int finalTerrainCord = 380;
    private int realTerrainLenght;
    public static int qntBoxes = 7;

    public static BoxesTerrainGenerator instance = null;//a instancia comeca vazia
    private static List<Container> boxes = new List<Container>();


    void Awake() {

        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
        realTerrainLenght = finalTerrainCord - startTerrainCord;
    }

    private void OnDestroy() {
        boxes = new List<Container>();
    }

    public List<Container> generateListBoxes() {
        double tamBox = (double)realTerrainLenght / qntBoxes;
        double aux1 = 0;
        double aux2 = 0;
        for (int i = 0; i < qntBoxes; i++) {
            if (i == 0) {
                aux2 = aux1 + tamBox;
                boxes.Add(new Container(false, aux1, aux2, 0));
            }
            else {
                aux1 = aux2 + 0.0001;
                aux2 = aux2 + tamBox;
                boxes.Add(new Container(false, aux1, aux2, 0));
            }
        }
        return boxes;
    }
}
