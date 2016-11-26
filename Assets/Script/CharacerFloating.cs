using UnityEngine;
using System.Collections;

public class CharacerFloating : MonoBehaviour {

	[SerializeField] SpriteRenderer _sprite;
	[SerializeField] float _range = 0.1f;
	[SerializeField] float _speed = 2f;
	float _elapsed;
	float _rand;

	// Use this for initialization
	void Start () {
		_rand = Random.value * Mathf.PI * 2;
	}
	
	// Update is called once per frame
	void Update () {
		_elapsed += Time.unscaledTime * _speed;
		_sprite.transform.localPosition = new Vector3( 0, Mathf.Sin(_elapsed + _rand) * _range, 0 );
	}
}
