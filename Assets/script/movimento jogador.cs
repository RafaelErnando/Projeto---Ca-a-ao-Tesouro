using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControladorJogador : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("Parâmetros do Movimento")]
    [Tooltip("Velocidade de movimento horizontal do jogador.")]
    public float velocidade = 5f;

    [Tooltip("Força aplicada ao pular.")]
    public float forcaPulo = 5f;

    [Header("Limites e Estado")]
    [Tooltip("Altura mínima antes de considerar que o jogador caiu.")]
    public float limiteInferior = -10f;

    [Tooltip("Número de vidas do jogador.")]
    public int vidas = 3;

    [Tooltip("Nome da próxima cena a ser carregada quando o jogador alcançar a meta.")]
    public string proximaCena = "Fase02";


    private Vector3 posicaoInicial;

    
    private Rigidbody2D rb;
    private float input;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        posicaoInicial = transform.position;
    }
    void Update()
    {
        input = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(input * velocidade, rb.linearVelocity.y);

        if(input != 0) {
            animator.SetBool("correr", true);
        }
        else{
            animator.SetBool("correr", false);
        }

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            
            rb.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
        }

        
        if (transform.position.y < limiteInferior)
        {
            vidas--;
            Debug.Log("Você caiu! Vidas restantes: " + vidas);

            if (vidas > 0)
            {
                
                transform.position = posicaoInicial;
                rb.linearVelocity = Vector2.zero;
            }
            else
            {
                
                Debug.Log("Game Over");
                
            }
        }
    }

    /// <summary>
    /// Detecta colisões com objetos marcados como "Meta" (fim da fase).
    /// </summary>
    /// <param name="outro">O collider com o qual houve colisão.</param>
    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Meta"))
        {
            Debug.Log("Parabéns! Você completou a fase!");
            StartCoroutine(CarregarProximaFase());
            // Aqui pode ir lógica para passar de fase, mostrar tela de vitória, etc.
        }
        if (outro.gameObject.CompareTag("Inimigo"))
        {
            vidas--;
            Debug.Log("Você perdeu 1 vida, boa sorte da proxima: " + vidas);

            if (vidas > 0)
            {

                transform.position = posicaoInicial;
                rb.linearVelocity = Vector2.zero;
            }
            else
            {

                Debug.Log("Game Over");
                SceneManager.LoadScene("Game over");
            }
        }

    }
    IEnumerator CarregarProximaFase()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("Final");
    }

    
}