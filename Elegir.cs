using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour{
    public GameObject Elegir;
    public GameObject Votar;
    public Text preguntatexto;
    public Button[] respuestacor;
    public Text textoPuntaje;

    private string[] Preguntas = { "Pregunta 1", "Pregunta 2", "Pregunta 3" };
    private string[][] respuesta = {
        new string[] { "Respuesta 1A", "Respuesta 1B", "Respuesta 1C" },
        new string[] { "Respuesta 2A", "Respuesta 2B", "Respuesta 2C" },
        new string[] { "Respuesta 3A", "Respuesta 3B", "Respuesta 3C" }
    };

    private int puntajeCorrecta = 1000;
    private int puntajeSemicorrecta = 700;
    private int puntajeMala = 10;

    private int nropregunta = 0;
    private int puntajeTotal = 0;
    private bool respuestaSeleccionada = false;

    private void Start(){
        Elegir.SetActive(true);
        Votar.SetActive(false);
        verpregunta();
    }

    public void verpregunta(){
        preguntatexto.text = Preguntas[nropregunta];
        int[] indices = { 0, 1, 2 };
        ShuffleArray(indices);

        for (int i = 0; i < respuestacor.Length; i++){
            string nombreRespuesta = "Respuesta" + (char)('A' + i);
            Text textoRespuesta = respuestacor[i].transform.Find(nombreRespuesta).GetComponent<Text>();

            switch (indices[i]){
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

    private void SeleccionarRespuesta(int puntaje){
        if (!respuestaSeleccionada){
            respuestaSeleccionada = true;
            puntajeTotal += puntaje;
            Debug.Log("Puntaje total: " + puntajeTotal);

            if (textoPuntaje != null){
                textoPuntaje.text = "Puntaje: " + puntajeTotal.ToString();
            }

            sgtepregunta();
        }
    }

    public void sgtepregunta(){
        nropregunta++;
        if (nropregunta < Preguntas.Length){
            verpregunta();
        }
        else{
            Debug.Log("Puntaje final: " + puntajeTotal);
            Elegir.SetActive(false);
            Votar.SetActive(true);
        }
    }

    private void ShuffleArray<T>(T[] array){
        int n = array.Length;
        for (int i = n - 1; i > 0; i--){
            int j = Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
