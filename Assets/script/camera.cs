using UnityEngine;

public class SeguirJogador : MonoBehaviour
{
    [Header("Referência do Jogador")]
    [Tooltip("Transform do jogador que será seguido.")]
    public Transform jogador;

    [Header("Configuração de Deslocamento")]
    [Tooltip("Distância relativa entre a câmera e o jogador.")]
    public Vector3 deslocamento = new Vector3(0, 1, -10);

    void Update()
    {
        // Pega a posição do jogador e passa para a camera somando a variavel de deslocamento
        transform.position = jogador.position + deslocamento;
    }
}