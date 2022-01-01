using System;
using System.Reflection;
using UnityEngine;
using UnityEditor.Timeline;

public class TimelinePlayHeadTest : MonoBehaviour
{
    System.Object timelineWindowObj;
    EventInfo onChangedPlayHead;
    Delegate changedPlayHeadHandlerDelegate;

    void Start()
    {
        Assembly assem = typeof(TimelineEditor).Assembly;

        // TimelineWindowクラスのインスタンスを生成
        Type timeilneWindowType = assem.GetType("UnityEditor.Timeline.TimelineWindow");
        timelineWindowObj = timeilneWindowType.GetProperty("instance", BindingFlags.Public | BindingFlags.Static).GetValue(null);
        
        // OnChangedPlayHeadのイベントを取得
        onChangedPlayHead = timeilneWindowType.GetEvent("OnChangedPlayHead");
        Type tDelegate = onChangedPlayHead.EventHandlerType;

        // ハンドラーを取得
        MethodInfo miHandler = typeof(TimelinePlayHeadTest).GetMethod("ChangePlayHeadHandler", BindingFlags.NonPublic | BindingFlags.Instance);

        // ハンドラーのデリゲートを作成
        changedPlayHeadHandlerDelegate = Delegate.CreateDelegate(tDelegate, this, miHandler);

        // OnChangedPlayHeadのAddメソッドを取得
        MethodInfo addHandler = onChangedPlayHead.GetAddMethod();

        // ハンドラーを引数にAddメソッドを実行
        System.Object[] addHandlerArgs = { changedPlayHeadHandlerDelegate };
        addHandler.Invoke(timelineWindowObj, addHandlerArgs);
    }

    private void OnDestroy()
    {
        // OnChangedPlayHeadのRemoveメソッドを取得
        MethodInfo addHandler = onChangedPlayHead.GetRemoveMethod();

        // ハンドラーを引数にRemoveメソッドを実行
        System.Object[] addHandlerArgs = { changedPlayHeadHandlerDelegate };
        addHandler.Invoke(timelineWindowObj, addHandlerArgs);
    }

    void ChangePlayHeadHandler(double oldTime, double currentTime)
    {
        Debug.Log($"Changed! oldTime:{oldTime}, currentTime:{currentTime}");
    }
}
