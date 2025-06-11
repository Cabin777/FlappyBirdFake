using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipelineManager : MonoBehaviour {

	public GameObject template;
	public List<Pipeline> pipelines;
	public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//协程
	Coroutine runner = null;
	
	public void Init()
	{
		for (int i = 0; i < pipelines.Count; ++i)
		{
			Destroy(pipelines[i].gameObject);
		}
		pipelines.Clear();
	}

	public void StartRun()
	{
		runner = StartCoroutine(GeneratePipelines());
	}
	

	public void stop()
	{
		StopCoroutine(runner);
		for (int i = 0; i < pipelines.Count; ++i)
		{
			pipelines[i].enabled = false;
		}
	}
	//管道生成协程
	IEnumerator GeneratePipelines()
	{
	
		for(int i = 0; i < 3; ++i)
		{
			if (pipelines.Count < 3)
			{
				CreatPipeline();
			}
			else
			{
				pipelines[i].enabled = true;
				pipelines[i].Init();
			}
			//等待进入下一步
			yield return new WaitForSeconds(speed);
		}
	}

	//生成管道函数，实例化
	void CreatPipeline()
	{
		if(pipelines.Count < 3)
		{
			GameObject obj = Instantiate(template, this.transform);//有父物体
			Pipeline p = obj.GetComponent<Pipeline>();
			pipelines.Add(p);
		}
		
	}
}
