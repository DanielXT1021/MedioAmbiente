using UnityEngine;
using UnityEngine.UI;

public class VotacionManager : MonoBehaviour
{
    public Toggle[] opciones;
    public Button botonEnviar;

    private int puntajePorOpcion = 250;
    private int puntajePorTodas = 1000;
    private int puntajeOpcion4 = 10;

    private void Start()
    {
        botonEnviar.interactable = false;

        for (int i = 0; i < opciones.Length; i++)
        {
            int index = i;
            opciones[i].onValueChanged.AddListener((value) => OpcionSeleccionada(index, value));
        }

        botonEnviar.onClick.AddListener(EnviarVoto);

    }

    private void OpcionSeleccionada(int index, bool value)
    {
        if (index == opciones.Length - 1)
        {
            botonEnviar.interactable = value;

            if (value)
            {
                for (int i = 0; i < opciones.Length - 1; i++)
                {
                    opciones[i].isOn = false;
                }
            }
        }
        else
        {
            opciones[opciones.Length - 1].isOn = false;
            botonEnviar.interactable = opciones[0].isOn || opciones[1].isOn || opciones[2].isOn || opciones[opciones.Length - 1].isOn;
        }
    }

    private void EnviarVoto()
    {
        int puntajeTotal = 0;
        int opcionesSeleccionadas = 0;
        for (int i = 0; i < opciones.Length - 1; i++)
        {
            if (opciones[i].isOn)
            {
                opcionesSeleccionadas++;
            }
        }

        if (opcionesSeleccionadas == 1)
        {
            puntajeTotal += puntajePorOpcion;
        }
        else if (opcionesSeleccionadas == 2)
        {
            puntajeTotal += puntajePorOpcion * 2;
        }
        else if (opcionesSeleccionadas == 3)
        {
            puntajeTotal += puntajePorOpcion * 3;
        }

        if (opciones[opciones.Length - 1].isOn)
        {
            puntajeTotal += puntajeOpcion4;
        }

        Debug.Log("Puntaje total: " + puntajeTotal);
        PlayerPrefs.SetInt("PuntajeTotalVotacion", puntajeTotal);
        PlayerPrefs.Save();
    }
}
