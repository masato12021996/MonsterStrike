using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	[SerializeField] GameObject enemy_prefab;
	[SerializeField] Vector3[] enemy_instance;
	[SerializeField] int enemy_count;
	[SerializeField] bool gameclear;

	void Awake( ) {
		enemy_prefab = ( GameObject )Resources.Load( "Prefab/Enemy" );
	}

	// Use this for initialization
	void Start () {
		enemy_prefab.GetComponent< EnemyController >( ).setEnemyManager( this );
		for ( int count = 0; count < enemy_instance.Length; count++ ) {
			enemy_prefab.transform.localScale = new Vector3( 1, 1, 1 );
			Instantiate( enemy_prefab, enemy_instance[ count ], Quaternion.identity );
		}
		GameObject[] enemyObjectCount;
		enemyObjectCount = GameObject.FindGameObjectsWithTag( "Enemy" );
		enemy_count = enemyObjectCount.Length;
	}

	// Update is called once per frame
	void Update () {
		if ( enemy_count == 0 ) {
			gameclear = true;
		}
	}

	public int GetEnemyCount( ) {
		return enemy_count;
	}

	public void EnemyDead( ) {
		enemy_count--;
	}

	public bool isGameClear( ) {
		return gameclear;
	}
}
