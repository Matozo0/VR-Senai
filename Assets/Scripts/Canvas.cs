using UnityEngine;

public class VRCanvasController : MonoBehaviour
{
    public Camera vrCamera;
    public float distanceFromCamera = 1.0f; // Diminuído para ajustar a distância
    public float tamanho;

    void Update()
    {
        Vector3 newPosition = vrCamera.transform.position + vrCamera.transform.forward * distanceFromCamera;
        transform.position = newPosition;
        transform.rotation = Quaternion.LookRotation(transform.position - vrCamera.transform.position);

        // Ajustar a escala do Canvas
        transform.localScale = new Vector3(tamanho, tamanho, 1f); // Ajuste a escala conforme necessário
    }
}
