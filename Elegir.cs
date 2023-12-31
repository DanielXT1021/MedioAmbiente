using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject Elegir;
    public GameObject Votar;
    public Text preguntatexto;
    public Button[] respuestacor;
    public Text textoPuntaje;


    private string[] Preguntas = { "Solo un 15% de la tierra esta protegida. �Como evitarias perder mas naturaleza?", "�Cual es tu maxima prioridad para los oceanos y canales?", "Los grupos locales e indigenas protegen el medio ambiente. �Como puedes ayudar?" };
    private string[][] respuesta = {
        new string[] { "Conservando bosques y tierras", "Creando mas parques urbanos", "Talando todos los arboles" },
        new string[] { "Mantenerlos limpios", "Usar menos plastico", "Crear pesqueros mas grandes" },
        new string[] { "Apoyando a comunidaes locales y pueblos indigenas", "Aprendiendo sobre su saber tradicionales", "Contaminando sus comunidades" }
    };


    private int puntajeCorrecta = 1000;
    private int puntajeSemicorrecta = 700;
    private int puntajeMala = 10;

    private int nropregunta = 0;
    private int puntajeTotal = 0;
    private bool respuestaSeleccionada = false;
    private string respuestaSeleccionadaTexto;
    private string respuestaSeleccionadaActual;

    private void Start()
    {
        Elegir.SetActive(true);
        Votar.SetActive(false);
        puntajeTotal = PlayerPrefs.GetInt("PuntajeTotal", 0);
        verpregunta();
    }

    public void verpregunta()
    {
        preguntatexto.text = Preguntas[nropregunta];
        int[] indices = { 0, 1, 2 };
        ShuffleArray(indices);

        for (int i = 0; i < respuestacor.Length; i++)
        {
            string nombreRespuesta = "Respuesta" + (char)('A' + i);
            Text textoRespuesta = respuestacor[i].transform.Find(nombreRespuesta).GetComponent<Text>();

            switch (indices[i])
            {
                case 0:
                    textoRespuesta.text = respuesta[nropregunta][0];
                    respuestacor[i].onClick.RemoveAllListeners();
                    respuestacor[i].onClick.AddListener(() => SeleccionarRespuesta(puntajeCorrecta));
                    break;

                case 1:
                    textoRespuesta.text = respuesta[nropregunta][1];
                    respuestacor[i].onClick.RemoveAllListeners();
                    respuestacor[i].onClick.AddListener(() => SeleccionarRespuesta(puntajeSemicorrecta));
                    break;

                case 2:
                    textoRespuesta.text = respuesta[nropregunta][2];
                    respuestacor[i].onClick.RemoveAllListeners();
                    respuestacor[i].onClick.AddListener(() => SeleccionarRespuesta(puntajeMala));
                    break;
            }
        }

        respuestaSeleccionada = false;
    }

    private void SeleccionarRespuesta(int puntaje)
    {
        if (!respuestaSeleccionada)
        {
            respuestaSeleccionada = true;
            puntajeTotal += puntaje;

            respuestaSeleccionadaTexto = ObtenerTextoRespuestaSeleccionada();
            respuestaSeleccionadaActual = respuestaSeleccionadaTexto;  // Nueva l�nea

            Debug.Log("Respuesta almacenada: " + respuestaSeleccionadaTexto);
            Debug.Log("Puntaje total: " + puntajeTotal);

            sgtepregunta();

            if (nropregunta >= Preguntas.Length)
            {
                PlayerPrefs.SetInt("PuntajeTotal", puntajeTotal);
                PlayerPrefs.Save();
            }
        }
    }

    private string ObtenerTextoRespuestaSeleccionada()
    {
        string respuesta = "";

        for (int i = 0; i < respuestacor.Length; i++)
        {
            string nombreRespuesta = "Respuesta" + (char)('A' + i);
            Text textoRespuesta = respuestacor[i].transform.Find(nombreRespuesta).GetComponent<Text>();
            respuesta = textoRespuesta.text;
        }

        return respuesta;
    }

    public void sgtepregunta()
    {
        nropregunta++;
        if (nropregunta < Preguntas.Length)
        {
            verpregunta();
        }
        else
        {
            Debug.Log("Puntaje final: " + puntajeTotal);
            Elegir.SetActive(false);
            Votar.SetActive(true);
        }
    }

    private void ShuffleArray<T>(T[] array)
    {
        int n = array.Length;
        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
