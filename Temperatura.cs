using UnityEngine;
using UnityEngine.UI;

public class SumarPuntajes : MonoBehaviour{
    public Slider sliderTemperatura;
    public Text textoTemperatura;   
    private void Start(){
        int puntajeTotalJuego1 = PlayerPrefs.GetInt("PuntajeTotal", 0);
        int puntajeTotalVotacion = PlayerPrefs.GetInt("PuntajeTotalVotacion", 0);


        int puntajeTotalFinal = puntajeTotalJuego1 + puntajeTotalVotacion;
        Debug.Log("Puntaje total final: " + puntajeTotalFinal);
        AplicarLogicaDeTemperatura(puntajeTotalFinal);
    }

    private void AplicarLogicaDeTemperatura(int puntajeTotal){
        float temperaturaInicial = ObtenerTemperatura();
        float aumentoTemperatura = 0f;
        float disminucionTemperatura = 0f;

      
        if (puntajeTotal >= 3650){
            disminucionTemperatura = 1.5f;
        }
        else if (puntajeTotal >= 3000 && puntajeTotal < 3650){
            disminucionTemperatura = 0.5f;
        }
        else if (puntajeTotal >= 2000 && puntajeTotal < 3000){

            aumentoTemperatura = 1f;
        }
        else if (puntajeTotal >= 1000 && puntajeTotal < 2000){
            aumentoTemperatura = 1.5f;
        }
        else{
       
            aumentoTemperatura = 2.0f;
        }

        float nuevaTemperatura = temperaturaInicial + (aumentoTemperatura - disminucionTemperatura);

        nuevaTemperatura = Mathf.Clamp(nuevaTemperatura, 1.5f, 10f);

        Debug.Log("Nueva temperatura del planeta: " + nuevaTemperatura + "°C");

        GuardarTemperatura(nuevaTemperatura);
        if (sliderTemperatura != null)
        {
            sliderTemperatura.value = nuevaTemperatura;
        }
        if (textoTemperatura != null){
            textoTemperatura.text = nuevaTemperatura.ToString("F1") + "°C";
        }
    }

    private float ObtenerTemperatura(){
    
        return PlayerPrefs.GetFloat("TemperaturaPlaneta", 1.5f);
    }

    private void GuardarTemperatura(float temperatura){
        PlayerPrefs.SetFloat("TemperaturaPlaneta", temperatura);
        PlayerPrefs.Save();
    }
    public void EliminacionPuntaje(){
        PlayerPrefs.DeleteKey("PuntajeTotal");
        PlayerPrefs.DeleteKey("PuntajeTotalVotacion");

    }
}
