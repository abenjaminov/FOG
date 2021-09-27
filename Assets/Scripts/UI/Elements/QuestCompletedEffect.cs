using System.Collections;
using ScriptableObjects.Channels;
using ScriptableObjects.Quests;
using UnityEngine;

namespace UI.Elements
{
    public class QuestCompletedEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _questCompletedEffect;
        [SerializeField] private GameObject _questCompletedImage;
        [SerializeField] private QuestsChannel _questsChannel;

        private bool isPlayingAnimation;
        
        private void Awake()
        {
            _questsChannel.QuestStateChangedEvent += QuestStateChangedEvent;
        }

        private void QuestStateChangedEvent(Quest quest)
        {
            if (quest.State == QuestState.PendingComplete)
            {
                StartCoroutine(nameof(PlayCompletedQuestAnimation));
            }
        }

        IEnumerator PlayCompletedQuestAnimation()
        {
            isPlayingAnimation = true;
            
            _questCompletedEffect.Play();
            _questCompletedImage.SetActive(true);
           
            yield return new WaitForSeconds(1.4f);
           
            _questCompletedImage.SetActive(false);

            isPlayingAnimation = false;
        }
        
        private void OnDestroy()
        {
            _questsChannel.QuestStateChangedEvent -= QuestStateChangedEvent;
        }
    }
}