using System.Collections;
using UnityEngine;

public enum PieceType
{
    Normal,
    Rotational
}

public class PieceController : MonoBehaviour
{
    private bool dragging = false;
    private float distance;
    private Vector3 startDist;
    private float originalZPosition; 
    private Transform originalPiece;
    private bool correctPlace;
    private bool takeBack;
    private Vector3 originalPos;
    private Quaternion originalRot;
    public PieceType pieceType;
    private bool placed;
    private bool startedRotating;
    private bool detached = false;
    private bool scoreIncreased = false;

    // Rotation speed for the Rotational piece
    public float rotationSpeed = 100.0f;


    private Vector3 initialMousePosition;

    private void Start()
    {
        originalPos = transform.position;
        originalRot = transform.rotation;
    }

    void OnMouseDown()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);
        startDist = transform.position - rayPoint;
        originalPiece = GameObject.Find(name + "copy").transform;
        originalPiece.GetComponent<MeshRenderer>().enabled = true;
        transform.rotation = originalPiece.transform.rotation;

      
        originalZPosition = originalPiece.transform.position.z;
        takeBack = false;
    }

    void OnMouseUp()
    {
        dragging = false;
        GameObject originalPiece = GameObject.Find(name + "copy");
        originalPiece.GetComponent<MeshRenderer>().enabled = false;
        if (!correctPlace)
        {
            takeBack = true;
        }
    }

    void Update()
    {
        if (dragging && !startedRotating)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);

          
            Vector3 newPosition = new Vector3(rayPoint.x + startDist.x, rayPoint.y + startDist.y, originalZPosition);
            transform.position = newPosition;


            if (placed)
            {
                correctPlace = false;

                if (Vector3.Distance(transform.position, originalPiece.position) <= 0.5f)
                {
                    originalPiece = null;
                    DetachPiece();
                }

            }
        }

        if (originalPiece)
        {
            if (Vector3.Distance(transform.position, originalPiece.transform.position) <= 0.05f && !correctPlace)
            {
                correctPlace = true;
                dragging = false;
            }
            if (correctPlace && pieceType == PieceType.Normal)
            {
                transform.position = Vector3.Lerp(transform.position, originalPiece.position, 10f * Time.deltaTime);
                if (Vector3.Distance(transform.position, originalPiece.transform.position) <= 0.3f)
                {
                    originalPiece.GetComponent<MeshRenderer>().enabled = true;
                    Color color = Color.gray;
                    color.a = 1f;
                    originalPiece.GetComponent<MeshRenderer>().material.color = color;
                    placed = true;
                    if (!scoreIncreased)
                    {
                        GUIManager.gUIManager.IncreasePlacedPieceNum();
                        scoreIncreased = true;
                    }

                }
            }
        }
        if (detached)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            originalPiece.GetComponent<MeshRenderer>().enabled = false;
            if (Input.GetMouseButtonDown(1)) // Right-click to detach
            {
                DetachPiece();
            }
        }
        if (correctPlace && pieceType == PieceType.Rotational)
        {
            if (Input.GetMouseButton(1))
            {
                transform.position = Vector3.Lerp(transform.position, originalPiece.position, 1f * Time.deltaTime);
                if (Vector3.Distance(transform.position, originalPiece.transform.position) <= 0.01f)
                {
                    transform.Rotate(Vector3.up, 30);
                    initialMousePosition = Input.mousePosition;
                    originalPiece.GetComponent<MeshRenderer>().enabled = true;
                    Color color = Color.gray;
                    color.a = 1f;
                    originalPiece.GetComponent<MeshRenderer>().material.color = color;
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    placed = true;
                    if (!scoreIncreased)
                    {
                        GUIManager.gUIManager.IncreasePlacedPieceNum();
                        scoreIncreased = true;
                    }
                }
            }
            else
            {
                if (!placed)
                {
                    transform.position = originalPiece.transform.position - new Vector3(0, 0.05f, 0);
                }

            }
        }

        if (takeBack)
        {
            transform.position = Vector3.Lerp(transform.position, originalPos, 5f * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRot, 5f * Time.deltaTime);
        }
    }
    private void DetachPiece()
    {
        transform.position = Vector3.Lerp(transform.position, originalPos, 5f * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, originalRot, 5f * Time.deltaTime);
        detached = false;
        correctPlace = false;
        placed = false;
        startedRotating = false;
        dragging = false;
        GameObject originalPiece = GameObject.Find(name + "copy");
        originalPiece.GetComponent<MeshRenderer>().enabled = false;
        Color color = Color.gray;
        color.a = 1f;
        originalPiece.GetComponent<MeshRenderer>().material.color = color;
        if (scoreIncreased)
        {
            GUIManager.gUIManager.DecreasePlacedPieceNum();
            scoreIncreased = false;
        }
    }
}
