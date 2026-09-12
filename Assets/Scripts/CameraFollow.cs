using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // 1 Variable publica para asignar a nuestro personaje  directamente desde el Inspector de Unity.
    public Transform target;

    // 2 Usamos LateUpdate en lugar de Update para garantizar que la camara se mueva DESPUES de que el jugador ya calculo su movimiento, evitando que la imagen tiemble.
    private void LateUpdate()
    {
        // 3 La camara copia la posicion X e Y del jugador en tiempo real, pero mantiene su propia posicion en Z para no pegarse al plano 2D.
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}