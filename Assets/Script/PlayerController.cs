using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	const string TOP_WALL 		= "TopWall";
	const string BOTTOM_WALL 	= "BottomWall";
	const string RIGHT_WALL 	= "RightWall";
	const string LEFT_WALL		= "LeftWall";

	bool _isHolded = false;

	bool standby = true;
	public bool Standby { get{ return standby; } }

	Vector3 _force;
	[ SerializeField ] float _friction = 0.985f;
	[ SerializeField ] float _min_force = 0.2f;
	[ SerializeField ] float power = 4f;

	[ SerializeField ] SpriteRenderer _arrow;
	[ SerializeField ] float _arrow_scale = 100f;

	void Start( ) {
		//最初は非表示
		_arrow.gameObject.SetActive( false );
	}

	void Update( ) {
		transform.position += _force * Time.deltaTime;

		_force *= _friction;

		standby = false;

		if (_force.magnitude < _min_force) {
			_force = default( Vector3 );
			standby = true;
		}


		if (Input.GetMouseButtonDown ( 0 ) && standby) {
			RaycastHit2D hit = Physics2D.Raycast( Camera.main.ScreenToWorldPoint( Input.mousePosition ), Vector2.zero );
			if ( hit.collider != null ) {
				_arrow.gameObject.SetActive( true );
				_isHolded = true;
			}
		};

		if ( Input.GetMouseButton( 0 ) && standby ) {
			Vector3 pos = Camera.main.ScreenToWorldPoint ( Input.mousePosition );
			pos.z = 0;
			float scale = ( transform.position - pos ).magnitude / Screen.width * _arrow_scale;
			_arrow.transform.localScale = new Vector3 ( scale, scale, 1 );

			float angle = Vector3.Angle(transform.position - pos, Vector3.up);

			Vector3 cross = Vector3.Cross( transform.position - pos, Vector3.up );

			if ( cross.z > 0 ) {
				angle *= -1;
			}

			_arrow.transform.rotation = Quaternion.Euler( new Vector3 ( 0, 0, angle + 90 ) );
		}

		if (Input.GetMouseButtonUp (0) && standby && _isHolded) {
			Vector3 pos = Camera.main.ScreenToWorldPoint ( Input.mousePosition );

			pos.z = transform.position.z;
			_force = transform.position - pos;
			_force *= power;
			_isHolded = false;
			_arrow.gameObject.SetActive (false);
		};

	}

	void OnTriggerEnter2D( Collider2D coll ) {
		if (coll.CompareTag ( TOP_WALL ) || coll.CompareTag ( BOTTOM_WALL ) ) {
			_force.y *= -1;
		} else if (coll.CompareTag ( RIGHT_WALL ) || coll.CompareTag ( LEFT_WALL ) ) {
			_force.x *= -1;
		} else if ( coll.CompareTag( "Enemy" ) ) {
			Vector3 vec_to_enemy = coll.gameObject.transform.position - this.transform.position;

			_force.z = 0;
			vec_to_enemy.z = 0;
			float deg = Vector2.Angle (_force, vec_to_enemy);
			Vector3 cross = Vector3.Cross( _force, vec_to_enemy );
			if ( cross.z > 0 ) {
				deg *= -1;
			}
			_force = Quaternion.AngleAxis( deg * 2, Vector3.back ) * _force * -1;
			coll.GetComponent<EnemyController>( ).Damage( 10 );
		}
	}


	public void SetStandby( ) {
		standby = true;
	}
}
