using System;

namespace ScriptableObjects.Chat
{
    [Serializable]
    public enum ChatDialogOptionAction
    {
        Continue,
        Back,
        AssignQuest,
        CompleteQuest,
        Close
    }
}