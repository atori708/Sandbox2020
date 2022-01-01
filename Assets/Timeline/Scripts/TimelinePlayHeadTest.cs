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

        // TimelineWindow�N���X�̃C���X�^���X�𐶐�
        Type timeilneWindowType = assem.GetType("UnityEditor.Timeline.TimelineWindow");
        timelineWindowObj = timeilneWindowType.GetProperty("instance", BindingFlags.Public | BindingFlags.Static).GetValue(null);
        
        // OnChangedPlayHead�̃C�x���g���擾
        onChangedPlayHead = timeilneWindowType.GetEvent("OnChangedPlayHead");
        Type tDelegate = onChangedPlayHead.EventHandlerType;

        // �n���h���[���擾
        MethodInfo miHandler = typeof(TimelinePlayHeadTest).GetMethod("ChangePlayHeadHandler", BindingFlags.NonPublic | BindingFlags.Instance);

        // �n���h���[�̃f���Q�[�g���쐬
        changedPlayHeadHandlerDelegate = Delegate.CreateDelegate(tDelegate, this, miHandler);

        // OnChangedPlayHead��Add���\�b�h���擾
        MethodInfo addHandler = onChangedPlayHead.GetAddMethod();

        // �n���h���[��������Add���\�b�h�����s
        System.Object[] addHandlerArgs = { changedPlayHeadHandlerDelegate };
        addHandler.Invoke(timelineWindowObj, addHandlerArgs);
    }

    private void OnDestroy()
    {
        // OnChangedPlayHead��Remove���\�b�h���擾
        MethodInfo addHandler = onChangedPlayHead.GetRemoveMethod();

        // �n���h���[��������Remove���\�b�h�����s
        System.Object[] addHandlerArgs = { changedPlayHeadHandlerDelegate };
        addHandler.Invoke(timelineWindowObj, addHandlerArgs);
    }

    void ChangePlayHeadHandler(double oldTime, double currentTime)
    {
        Debug.Log($"Changed! oldTime:{oldTime}, currentTime:{currentTime}");
    }
}
