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

        private void OnEnable()
        {
            _leftLangPopup.OnLanguagePop += SwitchLang;
            _rightLangPopup.OnLanguagePop += SwitchLang;
        }

        private void OnDisable()
        {
            _leftLangPopup.OnLanguagePop -= SwitchLang;
            _rightLangPopup.OnLanguagePop -= SwitchLang;
        }

        [SerializeField]
        private LangPopup _leftLangPopup;
        [SerializeField]
        private LangPopup _rightLangPopup;

        [SerializeField]
        private PhraseLanguageData _mainMessagePhrase;
        [SerializeField]
        private PhraseLanguageData _choseVrTour;

        [SerializeField]
        private PhraseLanguageData[] _cardsTitlePhrase= new PhraseLanguageData[3];
        [SerializeField]
        private PhraseLanguageData[] _cardsDiscPhrase= new PhraseLanguageData[3];
 
        public void SwitchLang(PhraseLanguageData.Language lang)
        {
           _mainMessagePhrase.SetText(lang);
           _choseVrTour.SetText(lang);
           for (int i = 0; i < _cardsTitlePhrase.Length; i++)
           {
                _cardsTitlePhrase[i].SetText(lang);
                _cardsDiscPhrase[i].SetText(lang); 
           }

           switch (lang)
           {
               case PhraseLanguageData.Language.Ua:
                    _leftLangPopup.SetLang(PhraseLanguageData.Language.En);
                    _rightLangPopup.SetLang(PhraseLanguageData.Language.Pl);
               break;
               case PhraseLanguageData.Language.En:
                    _leftLangPopup.SetLang(PhraseLanguageData.Language.Ua);
                    _rightLangPopup.SetLang(PhraseLanguageData.Language.Pl);
               break;
               case PhraseLanguageData.Language.Pl:
                    _leftLangPopup.SetLang(PhraseLanguageData.Language.Ua);
                    _rightLangPopup.SetLang(PhraseLanguageData.Language.En);
               break;
           }


        }

    }

    public interface ILangSwitch
    {
        void SwitchLang(PhraseLanguageData.Language lang);
    }
}