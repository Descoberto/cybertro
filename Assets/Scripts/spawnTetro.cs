using UnityEngine;
using System.Collections;
using System.Collections.Generic;

 Spawn Tetro
public class spawnTetro : MonoBehaviour {

    // Attributes
    public int proxPeca;
    public Transform[] criaPecas;
    public List<GameObject> mostraPecas;

    // Initialization
	void Start ()
    {
        proxPeca = Random.Range(0, 7);
		
        proximaPeca();
    }
    
    // Spawn and active the next piece
    public void proximaPeca()
    {
        Instantiate(criaPecas[proxPeca], transform.position, Quaternion.identity);
	
        proxPeca = Random.Range(0, 7);
	
        for (int i = 0; i < mostraPecas.Count; i++)
        {
            mostraPecas[i].SetActive(false);
        }

        mostraPecas[proxPeca].SetActive(true);
    }
}
