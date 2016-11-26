using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyController : MonoBehaviour {

	[SerializeField] int _life;
	[SerializeField] int max_life = 100;
	[SerializeField] Slider hp_gauge;
	[SerializeField] EnemyManager enemyManager;

	// Use this for initialization
	void Start () {
		_life = max_life;
	}
	
	// Update is called once per frame
	void Update () {
		emptyLife( );
		hp_gauge.value = (float)_life / max_life;
	}

	public void Damage( int value ) {
		_life -= value;
	}

	public void setEnemyManager( EnemyManager em )  {
		enemyManager = em;
	}

	private void emptyLife( ) {
		if ( _life <= 0 ) {
			enemyManager.EnemyDead( );
			Destroy( gameObject );
		}
	}
}
