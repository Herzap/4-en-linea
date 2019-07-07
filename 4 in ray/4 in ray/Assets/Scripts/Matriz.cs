using UnityEngine;

public class Matriz : MonoBehaviour
{
    // variables de tipo entero: estas guardan los valores de ancho y alto de la matriz
    public int width;
    public int height;

    // variables de tipo game object: una representa el objeto actual o nuevo que se crea en la matriz y luego se reemplaza por el siguiente  
    // y la otra el flujo de pantalla
    public GameObject puzzlePiece;
    GameObject goAct;
    // variable que guarda cada objeto de la matriz en su posicion respectiva
    private GameObject[,] grid;
   
    // variables de tipo material que dan texturas y color a los ojetos
    public Material Lost;
    public Material Hitler;
    
    // variables de tipo boleano: la primera mantiene el juego activo mientras esté en true
    // la segunda alterna los turnos
    bool game = true;
    public bool player = true;

    // variables de típo color que asignan un color a un objeto en la situacion corespondiente
    public Color goColor;
    public Color colorPlayer1;
    public Color colorPlayer2;

    // en el start se ejecuta el codigo que crea la matriz con los parametros que el usuario asigne en el inspector
    // tambien al momento de crear los objetos de la matriz les asigna un color establecido
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

    // en el update se ejecuta el codigo que hace que mientras el bool game esté positivo se lea la posicion del mouse
    // y se instancie la funcion que veremos acontinuacion
    void Update()
    {
        if (game == true)
        {
            Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Juego(mPosition);
        }
    }

    // en esta funcion, se lee el click y se alternan los turnos y los colores de los jugadores mediante el bool player
    // tambien se instancian las variables de comprobación pr cada dirección
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

    // funcion que accede a los parametros de la matriz y a los componentes de color de cada objeto 
    // y pregunta si hay 4 objetos del mismo color en el eje Y para igualar la variable bool a false y bloquear el juego
    // también crea un Quad que tapa la matriz
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

    // funcion que accede a los parametros de la matriz y a los componentes de color de cada objeto 
    // y pregunta si hay 4 objetos del mismo color en el eje X para igualar la variable bool a false y bloquear el juego
    // también crea un Quad que tapa la matriz
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

    // funcion que recorre la matriz completa (por ambos ejes) y añade el primer objeto que encuentre pintado a un contador
    // y resta una posición al eje Y y suma uno al eje X a partir de la posición del objeto añadido y si el objeto que esta en la nueva posición
    // esta pintado lo añade y así sucesivamente hasta que hayan 4 objetos en el contador o el que siga no sea del mismo color que los añadidos e iguale el contador a 0 para futuras comprobaciones
    // (tambien iguala el bool game a false si se cumple la condición de contador = 4)
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

    // funcion que recorre la matriz completa (por ambos ejes) y añade el primer objeto que encuentre pintado a un contador
    // y resta una posición al eje X y suma uno al eje Y a partir de la posición del objeto añadido y si el objeto que esta en la nueva posición
    // esta pintado lo añade y así sucesivamente hasta que hayan 4 objetos en el contador o el que siga no sea del mismo color que los añadidos e iguale el contador a 0 para futuras comprobaciones
    // (tambien iguala el bool game a false si se cumple la condición de contador = 4)
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