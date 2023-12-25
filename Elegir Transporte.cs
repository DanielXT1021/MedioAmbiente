using UnityEngine;
using UnityEngine.UI;

public class Tansporte : MonoBehaviour
{
    public GameObject Elegir;
    public GameObject Votar;
    public Text preguntatexto;
    public Button[] respuestacor;
    public Text textoPuntaje;

    private string[] Preguntas = { "Los coche y autobuses contaminan mucho. ¿Como harias el transporte mas verde?", "Los transportes generan un 23 % de las emisiones. ¿Como los harias mas ecologicos?", "¿Como mejorarias la calidad del aire en ciudades y comunidades rurales?" };
    private string[][] respuesta = {
        new string[] { "Usando bicis, autobuses y coches electricos", "Incentivando el compartir vehiculo", "Reduciendo el transporte publico" },
        new string[] { "Usando barcos, trenes y caminiones de energia limpia", "Con transporte mas eficiente", "No haciendo mantenimiento de los vehiculos" },
        new string[] { "Mejorando el diseño y la planificacion", "Incentivando la construccion de edificios verdes", "Talando todos los arboles" }
    };

    private int puntajeCorrecta = 1000;
    private int puntajeSemicorrecta = 700;
    private int puntajeMala = 10;

    private int nropregunta = 0;
    private int puntajeTotal = 0;
    private bool respuestaSeleccionada = false;
    private string respuestaSeleccionadaTexto;

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

            Debug.Log("Puntaje total: " + puntajeTotal);

            sgtepregunta();

            if (nropregunta >= Preguntas.Length)
            {
                PlayerPrefs.SetInt("PuntajeTotal", puntajeTotal);
                PlayerPrefs.SetString("RespuestaSeleccionada", respuestaSeleccionadaTexto);
                PlayerPrefs.Save();
            }
        }
    }

    private string ObtenerTextoRespuestaSeleccionada()
    {
        for (int i = 0; i < respuestacor.Length; i++)
        {
            if (respuestacor[i].interactable == false)
            {
                string nombreRespuesta = "Respuesta" + (char)('A' + i);
                Text textoRespuesta = respuestacor[i].transform.Find(nombreRespuesta).GetComponent<Text>();
                return textoRespuesta.text;
            }
        }
        return "";
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

