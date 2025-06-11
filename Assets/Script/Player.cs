using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {
	//为玩家附加刚体组件
	public Rigidbody2D rigidbodyBird;

	//一个控制小鸟移动的力
	public float force;

	//动画组件
	public Animator ani;

	//碰撞器组件
	public Collider2D col;

	//小鸟动画状态控制
	public void Fly()
	{
		this.ani.SetTrigger("Fly");
		this.rigidbodyBird.simulated = true;
	}
	public void Wait()
	{
		this.ani.SetTrigger("Idle");
		this.rigidbodyBird.simulated = false;
	}
	public void Dead()
	{
		this.ani.SetTrigger("Dead");
		this.rigidbodyBird.simulated = true;
	}

	//小鸟生存状态+死亡判定
	public bool death = false;

	//委托+事件：小鸟生存否
	public delegate void DeathNoity();
	public event DeathNoity OnDeath;
	//Score events
	public UnityAction<int> OnScore;

	void Die()
	{
		this.death = true;
		//如果有“人”订阅小鸟死亡事件，则执行所有订阅事件
		if (this.OnDeath != null)
		{
			this.OnDeath();
		}
		
	}


	public Vector3 initPos;
	public void Init()
	{
		this.transform.position = initPos;
		this.Wait();
		this.death = false;
	}

	public void OnTriggerEnter2D(Collider2D col)
	{
		//触发
		Debug.Log("OnTriggerEnter2D : " + col.gameObject.name + " : " + Time.time);
		if (col.gameObject.name != "ScoreArea" && !this.death)
		{
			this.Die();
		}

	}
	public void OnTriggerExit2D(Collider2D col)
	{
		//触发
		Debug.Log("OnTriggerExit2D : " + col.gameObject.name + " : " + Time.time);
		if (col.gameObject.name.Equals("ScoreArea") && !this.death)
		{
			if (OnScore != null)
			{
				this.OnScore(1);
			}
		}
	}
	public void OnCollisionEnter2D(Collision2D col)
	{
		//碰撞
		Debug.Log("OnCollisionEnter2D : " + col.gameObject.name + " : " + Time.time);
		if (!this.death)
		{
			this.Die();
		}
	}



	// Use this for initialization
	void Start () {
		//也可以通过代码绑定刚体rigidbodyBird = GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(death)
		{
			return;
		}
		//鼠标点击时，先清空力再施加一个力使小鸟移动
		if (Input.GetMouseButtonDown(0))
		{
			rigidbodyBird.velocity = Vector2.zero;
			rigidbodyBird.AddForce(new Vector2(0, force), ForceMode2D.Force);
		}
	}
	

}
