using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Tetro Mov
public class tetroMov : MonoBehaviour {
    
    // Attributes
    public bool podeRodar;
    public bool roda360;
    public float queda;
    public float velocidade;
    public float timer;
    public AudioClip landSound;
    private AudioSource audioSource;
    gameManager gManager;
    spawnTetro gSpawner;

    // Initialization
    void Start()
    {
        timer = velocidade;
        audioSource = GetComponent<AudioSource>();
        gManager = GameObject.FindObjectOfType<gameManager>();
        gSpawner = GameObject.FindObjectOfType<spawnTetro>();
    }

    // Update
    void Update()
    {
        if (!gManager.pause)
        {
            // Structure decision (Aumenta a dificuldade ao passar de 1000 pontos)
            if (gManager.pontoDificuldade > 1000)
            {
                gManager.pontoDificuldade -= 1000;
                gManager.dificuldade += .5f;
            }

            // Iguala a velocidade ao time
            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKey(KeyCode.Space))
            {
                timer = velocidade;
            } 

            // Press Right Key
            if (Input.GetKey(KeyCode.RightArrow))
            {
                IrDireita();
            } 

            // Press Left Key
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                IrEsquerda();
            } 

            // Press Key Down
            if (Input.GetKey(KeyCode.DownArrow))
            {
                IrBaixo();
            } 

            // Press Space Key
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Abater();
            } 

            // Decisão que Garante a correção da queda
            if (Time.time - queda >= (1 / gManager.dificuldade) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKeyUp(KeyCode.Space))
            {
                quedaTime();
            } 
            
            // Press to Up Arrow
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                checarRoda();
            } 
        }
    }
    
    // Checa se a peça pode ou não rodar
    void checarRoda()
    {
        if (podeRodar == posicaoValida())
        {
            if (!roda360)
            {
                if (transform.rotation.z < 0)
                {
                    transform.Rotate(0, 0, 90);

                    if (posicaoValida())
                    {
                        gManager.atualizaGrade(this);
                    }
                    else
                    {
                        transform.Rotate(0, 0, -90);
                    }
                }
                else
                {
                    transform.Rotate(0, 0, -90);

                    if (posicaoValida())
                    {
                        gManager.atualizaGrade(this);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
            }
            else
            {
                transform.Rotate(0, 0, -90);

                if (posicaoValida())
                {
                    gManager.atualizaGrade(this);
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }
            }
        }
    }

    // Checa cada quadrado em cada peça
    bool posicaoValida()
    {
        foreach (Transform child in transform)
        {
            Vector2 posBloco = gManager.arredonda(child.position);

            if (gManager.dentroGrade(posBloco) == false)
            {
                return false;
            }
            if (gManager.posicaoTransformGrade(posBloco) != null && gManager.posicaoTransformGrade(posBloco).parent != transform)
            {
                return false;
            }
        }
        return true;
    } 

    // Faz a peça descer rapido até seu limite
    public void Abater()
    {
        timer += Time.deltaTime;
        while (posicaoValida())
        {
            transform.position += new Vector3(0, -1, 0);
        }
        if (!posicaoValida())
        {
            transform.position += new Vector3(0, 1, 0);
            
            gManager.atualizaGrade(this);
            
            gManager.apagaLinha();
            
            if (gManager.acimaGrade(this))
            {
                gManager.gameOver();
            }

            PlayLandAudio();
            
            gManager.score += 10;
            
            gManager.pontoDificuldade += 10;
            
            enabled = false;
            
            gSpawner.proximaPeca();
        }
    }

    // Faz a peça ir para a direita
    public void IrDireita()
    {
        timer += Time.deltaTime;

        if (timer > velocidade)
        {
            transform.position += new Vector3(1, 0, 0);
            timer = 0;
        }

        if (posicaoValida())
        {
            gManager.atualizaGrade(this);
        }
        else
        {
            transform.position += new Vector3(-1, 0, 0);
        }
    }

    // Faz a peça ir para a esquerda
    public void IrEsquerda()
    {
        timer += Time.deltaTime;
        
        if (timer > velocidade)
        {
            transform.position += new Vector3(-1, 0, 0);
            
            timer = 0;
        }

        if (posicaoValida())
        {
            gManager.atualizaGrade(this);
        }
        else
        {
            transform.position += new Vector3(1, 0, 0);
        }
    }

    // Faz a peça descer rapido moderadamente
    public void IrBaixo()
    {
        timer += Time.deltaTime;
        
        if (timer > velocidade)
        {
            transform.position += new Vector3(0, -1, 0);
            
            timer = 0;
        }

        if (posicaoValida())
        {
            gManager.atualizaGrade(this);
        }
        else
        {
            transform.position += new Vector3(0, 1, 0);
            
            gManager.apagaLinha();

            if (gManager.acimaGrade(this))
            {
                gManager.gameOver();
            }
            
            PlayLandAudio();
            
            gManager.score += 10;
            
            gManager.pontoDificuldade += 10;
            
            enabled = false;
            
            gSpawner.proximaPeca();
        }
    } 

    // Fix the fall with time
    void quedaTime()
    {
        transform.position += new Vector3(0, -1, 0);
        
        if (posicaoValida())
        {
            gManager.atualizaGrade(this);
        }
        else
        {
            transform.position += new Vector3(0, 1, 0);
            
            gManager.apagaLinha();

            if (gManager.acimaGrade(this) == posicaoValida())
            {
                gManager.gameOver();
            }

            PlayLandAudio();
            
            gManager.score += 10;
            
            gManager.pontoDificuldade += 10;
            
            enabled = false;
            
            gSpawner.proximaPeca();
        }

        queda = Time.time;
    } 

    // Sound Effects
    void PlayLandAudio()
    {
        // Execute the sound effect when the piece fall
        audioSource.PlayOneShot(landSound);
    } 
}
