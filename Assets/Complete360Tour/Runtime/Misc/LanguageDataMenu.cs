using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DigitalSalmon.C360
{
    public class LanguageDataMenu : MonoBehaviour,ILangSwitch

    {
        public PhraseLanguageData.Language _defaultLanguage;

        private void Start()
        {
            SwitchLang(_defaultLanguage);
        }

        [SerializeField]
        private PhraseLanguageData _mainMessagePhrase;

        [SerializeField]
        private PhraseLanguageData[] _cardsTitlePhrase= new PhraseLanguageData[3];
        [SerializeField]
        private PhraseLanguageData[] _cardsDiscPhrase= new PhraseLanguageData[3];
 
        public void SwitchLang(PhraseLanguageData.Language lang)
        {
           _mainMessagePhrase.SetText(lang);
           for (int i = 0; i < _cardsTitlePhrase.Length; i++)
           {
                _cardsTitlePhrase[i].SetText(lang);
                _cardsDiscPhrase[i].SetText(lang);

               
           }

        }

    }

    public interface ILangSwitch
    {
        void SwitchLang(PhraseLanguageData.Language lang);
    }
}