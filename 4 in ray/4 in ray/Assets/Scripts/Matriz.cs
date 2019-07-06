using UnityEngine;

public class Matriz : MonoBehaviour
{
    public int width;
    public int height;

    public GameObject puzzlePiece;
    private GameObject[,] grid;
    public Material Lost;
    public Material Hitler;
    GameObject goAct;

    bool game = true;
    public bool player = true;

    public Color goColor;
    public Color colorPlayer1;
    public Color colorPlayer2;

    void Start()
    {


        grid = new GameObject[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject go = GameObject.Instantiate(puzzlePiece) as GameObject;

                Vector3 position = new Vector3(x, y, 0);
                go.transform.position = position;
                grid[x, y] = go;

                go.GetComponent<Renderer>().material.color = goColor;
            }
        }
    }

    void Update()
    {
        if (game == true)
        {
            Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Juego(mPosition);
        }
    }

    void Juego(Vector3 position)
    {
        int x = (int)(position.x + 0.5f);
        int y = (int)(position.y + 0.5f);

        if (Input.GetButtonDown("Fire1"))
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                GameObject go = grid[x, y];
                if (go.GetComponent<Renderer>().material.color == goColor)
                {
                    Color colorTurno = Color.clear;
                    if (player)
                    {
                        colorTurno = colorPlayer1;
                    }
                    else
                        colorTurno = colorPlayer2;
                    go.GetComponent<Renderer>().material.color = colorTurno;
                    player = !player;
                    VerificarY(x, y, colorTurno);
                    VerificarX(x, y, colorTurno);
                    VerificarXY(x, y, colorTurno);
                    VerificarYX(x, y, colorTurno);
                }
            }
        }
    }

    void VerificarY(int x, int y, Color chekcolor)
    {
        int cont = 0;

        for (int j = y - 3; j <= y + 3; j++)
        {
            if (j < 0 || j >= width)
                continue;

            GameObject sphere = grid[x, j];

            if (sphere.GetComponent<Renderer>().material.color == chekcolor)
            {
                cont++;
                if (cont == 4)
                {
                    goAct = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    goAct.transform.localScale = new Vector3(30.7528f, 19.62602f, 1.0f);
                    goAct.transform.position = new Vector3(6.54f, 4.62f, -2);

                    goAct.GetComponent<Renderer>().material = Hitler;



                    game = false;
                }
            }
            else
                cont = 0;
        }
    }

    void VerificarX(int x, int y, Color chekcolor)
    {
        int cont = 0;

        for (int i = x - 3; i <= x + 3; i++)
        {
            if (i < 0 || i >= width)
                continue;

            GameObject sphere = grid[i, y];

            if (sphere.GetComponent<Renderer>().material.color == chekcolor)
            {
                cont++;
                Debug.Log(cont);
                if (cont == 4)
                {
                    goAct = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    goAct.transform.localScale = new Vector3(30.7528f, 19.62602f, 1.0f);
                    goAct.transform.position = new Vector3(6.54f, 4.62f, -2);

                    goAct.GetComponent<Renderer>().material = Hitler;

                    game = false;
                }
            }
            else
                cont = 0;
        }
    }

    void VerificarXY(int x, int y, Color chekcolor)
    {
        int cont = 0;
        int j = y - 3;
        for (int i = x - 3; i <= x + 3; i++)
        {
            if (i >= 0 && i < width && j >= 0 && j < height)
            {
                GameObject sphere = grid[i, j];


                if (sphere.GetComponent<Renderer>().material.color == chekcolor)
                {
                    cont++;
                    Debug.Log(cont);
                    if (cont == 4)
                    {
                        goAct = GameObject.CreatePrimitive(PrimitiveType.Quad);
                        goAct.transform.localScale = new Vector3(23.329f, 19.551f, 1.0f);
                        goAct.transform.position = new Vector3(5.43f, 4.65f, -2);

                        goAct.GetComponent<Renderer>().material = Lost;

                        game = false;
                    }
                }
                else
                    cont = 0;
            }
            j++;
        }
    }

    void VerificarYX(int x, int y, Color chekcolor)
    {
        int cont = 0;
        int j = y + 3;

        for (int i = x - 3; i <= x + 3; i++)
        {
            if (i >= 0 && i < width && j >= 0 && j < height)
            {
                GameObject sphere = grid[i, j];


                if (sphere.GetComponent<Renderer>().material.color == chekcolor)
                {
                    cont++;
                    Debug.Log(cont);
                    if (cont == 4)
                    {
                        goAct = GameObject.CreatePrimitive(PrimitiveType.Quad);
                        goAct.transform.localScale = new Vector3(23.329f, 19.551f, 1.0f);
                        goAct.transform.position = new Vector3(5.43f, 4.65f, -2);

                        goAct.GetComponent<Renderer>().material = Lost;

                        game = false;
                    }
                }
                else
                    cont = 0;
            }
            j--;
        }
    }
}