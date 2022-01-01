using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class Scene : MonoBehaviour
{
    [SerializeField]
    Button button = default;

    public async UniTask WaitNextScene()
	{
        await button.OnClickAsync().AsTask();
    }

    public async UniTask Enter()
	{
        gameObject.SetActive(true);
        await UniTask.CompletedTask;
	}

    public async UniTask Exit()
	{
        gameObject.SetActive(false);
        await UniTask.CompletedTask;
    }
}
