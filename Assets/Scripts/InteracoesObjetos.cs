using UnityEngine;

public class InteracoesObjetos : MonoBehaviour, IInterfaceObjetos
{
    public void OnFocoEnter()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }
    public void OnFocoStay()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
    public void OnFocoExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}
