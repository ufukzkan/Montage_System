using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMerger : MonoBehaviour
{
    public List<Transform> objectPieces;
    public BoxCollider piecesSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        objectPieces = new List<Transform>();
        int objectChildPieceCount = transform.childCount;
        for (int i = 0; i < objectChildPieceCount; i++)
        {
            objectPieces.Add(transform.GetChild(i));
            objectPieces[i].name = i.ToString() + "copy";
            // objectPieces[i].gameObject.SetActive(false);
            Color alphaColor = Color.gray;
            alphaColor.a = 0.2f;
            objectPieces[i].GetComponent<MeshRenderer>().material.color = alphaColor;
            objectPieces[i].GetComponent<Rigidbody>().useGravity = false;
            objectPieces[i].GetComponent<BoxCollider>().enabled = false;
        }
        /* objectPieces[0].gameObject.SetActive(true);
         for (int i = 1; i < objectChildPieceCount; i++)
         {
             GameObject piece = Instantiate(objectPieces[i].gameObject, new Vector3(Random.Range(piecesSpawnPoint.bounds.min.x, piecesSpawnPoint.bounds.max.x), 0, Random.Range(piecesSpawnPoint.bounds.min.z, piecesSpawnPoint.bounds.max.z)), Quaternion.identity);
             //piece.AddComponent<PieceController>();
             piece.name = i.ToString();
             piece.GetComponent<Rigidbody>().useGravity = true;
             piece.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
             piece.SetActive(true);

         }

        GUIManager.gUIManager.totalPieceNumber = objectPieces.Count;
     }
        */

    }
}
