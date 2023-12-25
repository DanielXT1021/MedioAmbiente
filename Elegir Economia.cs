using UnityEngine;
using UnityEngine.UI;

public class Economia : MonoBehaviour
{
    public GameObject Elegir;
    public GameObject Votar;
    public Text preguntatexto;
    public Button[] respuestacor;
    public Text textoPuntaje;

    private string[] Preguntas = { "¿Como ayudarias al crecimiento de nuevas industrias limpias?", "Se podrian reducir emisiones al cambiar el modo en que se producen. ¿Que haras?", "¿Como podemos asegurando de que los negocios contaminen menos?" };
    private string[][] respuesta = {
        new string[] { "Invirtiendo mas en trabajos y negocios verdes", "Potenciando las emprrsas verdes", "Apoyando a las compañias contaminantes" },
        new string[] { "informarse sobre como se fabrican los productos", "Invertir en paquetes verdes", "Envolverlos todo en plastico" },
        new string[] { "Obligando a las compañias a pagar por su contaminancion", "Premiando a compañias que no contaminen", "Haciendo que solo pueda haber anuncios de compañeros contaminantes" }
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
