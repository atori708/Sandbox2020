using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    Scene title;

    [SerializeField]
    Scene inGame;

    [SerializeField]
    Scene resule;

    [SerializeField]
    Transition transition;

	// Start is called before the first frame update
	async void Start()
	{
		while (true) {
			await title.WaitNextScene();
			await Transition(title, inGame);

			await inGame.WaitNextScene();
            await Transition(inGame, title);
        }
    }

    async UniTask Transition(Scene prevScene, Scene nextScene)
	{
        await transition.FadeOut();
        await prevScene.Exit();
        await nextScene.Enter();
        await UniTask.Delay(1000); // Ç∑ÇÆêÿÇËë÷ÇÌÇ¡ÇøÇ·Ç§ÇÃÇ≈è≠Çµë“Ç¬
        await transition.FadeIn();
    }
}
