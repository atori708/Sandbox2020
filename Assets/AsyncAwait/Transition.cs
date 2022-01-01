using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
	[SerializeField]
	Image image = default;

	[SerializeField]
	float duration = 1;

    public async UniTask FadeIn()
	{
		await DOVirtual.Float(image.color.a, 0, duration, value => {
			var color = image.color;
			color.a = value;
			image.color = color;
		});
		image.gameObject.SetActive(false);
	}

	public async UniTask FadeOut()
	{
		image.gameObject.SetActive(true);
		await DOVirtual.Float(image.color.a, 1, duration, value => {
			var color = image.color;
			color.a = value;
			image.color = color;
		});
	}
}
