using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipeline : MonoBehaviour {
	//变量控制管道移动速度
	public float speed = 2;
	public float down = -3;
	public float up = 3;

	float t = 0;//下方用来实现时钟

	public void Init()
	{
		//变量y控制管道随机位置
		float y = Random.Range(down, up);
		this.transform.localPosition = new Vector3(0, y, 0);
	}
	
	// Use this for initialization
	void Start () {
		Init();
	}
	
	// Update is called once per frame
	void Update () {
		//管道移位
		this.transform.position += new Vector3(-speed, 0) * Time.deltaTime;
		//简单实现时钟，六秒后执行Init方法
		t += Time.deltaTime;
		if(t > 6f)
		{
			t = 0;
			this.Init();
		}
	}
}
