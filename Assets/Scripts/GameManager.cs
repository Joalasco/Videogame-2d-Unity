using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Vector3 nuevaPosicion;
    public GameState gameState = GameState.Inicio;
    public GameObject panelInicio;
    public GameObject panelGameOver;
    public GameObject panelVictoria;
    public GameObject panelControles;
    public GameObject Instruccioness;
    public bool siguiente = false;

    public enum GameState
    {
        Inicio,
        Jugando,
        GameOver,
        Victoria,
        Controles
    }
    void Awake()
    {
        DesactivarEntrada();

        gameState = GameState.Inicio;
        CambiarEstado(gameState);
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.Inicio:
                
                if (Input.GetKeyDown(KeyCode.X))
                {
                    Time.timeScale = 1f;
                    CambiarEstado(GameState.Jugando);
                }else if (Input.GetKeyDown(KeyCode.C))
                {
                    CambiarEstado(GameState.Controles);
                }
                break;
            case GameState.Controles:
                if (Input.GetKeyDown(KeyCode.C))
                {
                    CambiarEstado(GameState.Inicio);
                }
                break;
            case GameState.Jugando:
                if (Input.GetKeyDown(KeyCode.X))
                {
                    Instruccioness.SetActive(false);
                    ActivarEntrada();
                    siguiente = true;
                }
                break;
            case GameState.GameOver:
                DesactivarEntrada();
                if (Input.GetKeyDown(KeyCode.X))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    CambiarEstado(GameState.Inicio);
                }
                break;
            case GameState.Victoria:
                DesactivarEntrada();
                if (Input.GetKeyDown(KeyCode.X))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    CambiarEstado(GameState.Inicio);
                }
                break;
        }
    }
    public void CambiarEstado(GameState nuevoEstado)
    {
        gameState = nuevoEstado;
        panelInicio.SetActive(gameState == GameState.Inicio);
        panelGameOver.SetActive(gameState == GameState.GameOver);
        panelVictoria.SetActive(gameState == GameState.Victoria);
        panelControles.SetActive(gameState == GameState.Controles);
        
    }
    void DesactivarEntrada()
    {
        Time.timeScale = 0f;
    }
    void ActivarEntrada()
    {
        Time.timeScale = 1f;
    }
}
