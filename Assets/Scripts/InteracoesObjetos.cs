using TMPro;
using UnityEngine;

public class InteracoesObjetos : MonoBehaviour, IInterfaceObjetos
{
    public GameObject info;
    public TMP_Text infoTexto;
    public string texto;
    public float duracao;
    public LeanTweenType easeTipos;
    public LeanTweenType easeTipos2;

    public void OnFocoEnter()
    {
        infoTexto.text = texto;
        info.transform.localScale = Vector3.zero;
        info.SetActive(true);
        LeanTween.scale(info, Vector3.one, duracao).setEase(easeTipos);
    }
    public void OnFocoStay()
    {
        // a
    }
    public void OnFocoExit()
    {
        LeanTween.scale(info, Vector3.zero, duracao).setEase(easeTipos2);
        //info.SetActive(false);
    }
}
