using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameLogic : MonoBehaviour {
	public enum GAME_STATE {
		STAGE_BEGIN,
		AREA_BEGIN,
		PLAYER,
		PLAYER_TO_ENEMY,
		ENEMY,
		ENEMY_TO_PLAYER,
		AREA_CLEARED,
		AREA_MOVING,
		STAGE_CREARED,
		GAMEOVER,
		EMPTY
	}

	//public static bool can_control;
	public static GAME_STATE game_state;

	[SerializeField] Text cutin;

	// Use this for initialization
	void Start () {
		game_state = GAME_STATE.STAGE_BEGIN;
		cutin.gameObject.SetActive( false );
	}
	
	// Update is called once per frame
	void Update () {
		TurnControl( );
	}

	void TurnControl( ) {
		switch( game_state ) {
		case GAME_STATE.STAGE_BEGIN:
			TurnChange( 3, "START", GAME_STATE.AREA_BEGIN );
			break;
		case GAME_STATE.AREA_BEGIN:
			TurnChange( 2, "NEXT AREA", GAME_STATE.PLAYER );
			break;
		case GAME_STATE.PLAYER:
			break;
		case GAME_STATE.PLAYER_TO_ENEMY:
			TurnChange( 3, "ENEMY TURN", GAME_STATE.ENEMY );
			break;
		case GAME_STATE.ENEMY:
			game_state = GAME_STATE.ENEMY_TO_PLAYER;
			break;
		case GAME_STATE.ENEMY_TO_PLAYER:
			TurnChange (3, "NEXT TURN", GAME_STATE.PLAYER);
			break;
		case GAME_STATE.AREA_CLEARED:
		case GAME_STATE.AREA_MOVING:
		case GAME_STATE.STAGE_CREARED:
		case GAME_STATE.GAMEOVER:
		case GAME_STATE.EMPTY:
		default:
			break;
		}
	}

	void TurnChange( float time, string cutionText, GAME_STATE nextTurn ) {
		cutin.text = cutionText;
		game_state = GAME_STATE.EMPTY;
		StartCoroutine( CutionTextActive( time, game_state ) );
	}

	IEnumerator CutionTextActive( float time, GAME_STATE nextTurn ) {
		cutin.gameObject.SetActive( true );
		yield return new WaitForSeconds( time );
		cutin.gameObject.SetActive( false );
		game_state = nextTurn;
	}
}