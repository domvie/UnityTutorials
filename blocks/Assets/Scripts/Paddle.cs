using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // configuration paramters
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 16f;
    [SerializeField] float screenWidthInUnits = 16f;

    // cached refs
    GameStatus theGameSession;
    Ball theBall;

    // Start is called before the first frame update;
    void Start()
    {
        theGameSession = FindObjectOfType<GameStatus>();
        theBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = paddlePos;
    }

    private float GetXPos() 
    {
        if (theGameSession.IsAutoPlayEnabled())
        {
            return theBall.transform.position.x;
        }
        return (Input.mousePosition.x / Screen.width * screenWidthInUnits);
    }
}
