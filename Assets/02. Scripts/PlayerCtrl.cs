using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Anim{
	public AnimationClip idle;
	public AnimationClip runForward;
	public AnimationClip rumBackward;
	public AnimationClip runRight;
	public AnimationClip runLeft;
}

public class PlayerCtrl : MonoBehaviour {
    private float h = 0.0f;
    private float v = 0.0f;

	//접근해야 하는 컴포넌트는 반드시 변수에 할당한 후 사용
	private Transform tr;

	private float moveSpeed = 10.0f;


	//회전 속도 변수
	public float rotSpeed = 100.0f;


	//인스펙터뷰에 표시할 애니메이션 변수 클래스
	public Anim anim;

	public Animation _animation;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();	

		_animation = GetComponentInChildren<Animation> ();

		_animation.clip = anim.idle;
		_animation.Play();
	}



	// Update is called once per frame
	void Update () {
		h = Input.GetAxis ("Horizontal");
		v = Input.GetAxis ("Vertical");

		Debug.Log ("H=" + h.ToString ());
		Debug.Log ("V=" + v.ToString ());

		Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

		//Translate(이동 방향 * Time.delaTime * 변위값 * 속도, 기준좌표);
		tr.Translate (moveDir.normalized * Time.deltaTime * moveSpeed, Space.Self);

		//tr.Translate (Vector3.forward * moveSpeed * v * Time.deltaTime, Space.Self);
		//이동할 방향 * 속도 * 전진, 후진변수 * Time.deltaTime, 기준좌표계

		tr.Rotate (Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis ("Mouse X"));

		//키보드 입력값을 기준으로 동작할 애니메이션 수행
		if (v >= 0.1f) {
			//전진 애니메이션
			_animation.CrossFade (anim.runForward.name, 0.3f);
		} else if (v <= -0.1f) {
			_animation.CrossFade (anim.rumBackward.name, 0.3f);
		} else if (h >= 0.1f) {
			_animation.CrossFade (anim.runRight.name, 0.3f);
		} else if (h <= -0.1f) {
			_animation.CrossFade (anim.runLeft.name, 0.3f);
		} else {
			//정지시 idle애니메이션
			_animation.CrossFade (anim.idle.name, 0.3f);
		}
	}
}
