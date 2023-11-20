using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class GUIManager : MonoBehaviour
{
    public static GUIManager gUIManager;
    public int totalPieceNumber;
    public int currentPlacedPieceNumber = 0;

    public Canvas uiCanvas;

    void Start()
    {
        gUIManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("current placed piece number" + currentPlacedPieceNumber);
        Debug.Log("total piece number" + totalPieceNumber);
    }

    public void IncreasePlacedPieceNum()
    {
        currentPlacedPieceNumber++;


        if (totalPieceNumber - 1 == currentPlacedPieceNumber)
        {

            uiCanvas.enabled = true;

        }

    }
    public void DecreasePlacedPieceNum()
    {
        currentPlacedPieceNumber--;
    }
}
