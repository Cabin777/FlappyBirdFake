using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LockAspectRatio : MonoBehaviour
{
	public float targetAspect = 800f / 1280f; // 目标宽高比
	private Camera cam;

	void Awake()
	{
		cam = GetComponent<Camera>();
		UpdateAspect();
	}

	void UpdateAspect()
	{
		float currentAspect = (float)Screen.width / Screen.height;
		// 计算视口矩形，强制裁剪/黑边
		if (currentAspect > targetAspect)
		{
			float scale = targetAspect / currentAspect;
			cam.rect = new Rect((1 - scale) / 2, 0, scale, 1);
		}
		else
		{
			float scale = currentAspect / targetAspect;
			cam.rect = new Rect(0, (1 - scale) / 2, 1, scale);
		}
	}

	void OnRectTransformDimensionsChange()
	{
		UpdateAspect(); // 屏幕变化时重新计算
	}
}

