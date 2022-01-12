using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DigitalSalmon.C360
{
    public class LangPopup:Popup
    {
        private PhraseLanguageData.Language _ourLnaguage;
        [SerializeField]
        private Text _title1;
        [SerializeField]
        private Text _title2;
 
        protected override void OnEnable()
        {

            base.OnEnable();
            OnTrigger += OnTriggerPop;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            OnTrigger -= OnTriggerPop;
        }

        private void OnTriggerPop(Hotspot h)
        {
            OnLanguagePop?.Invoke(_ourLnaguage);
        }

        public event Action<PhraseLanguageData.Language> OnLanguagePop;


        public void SetLang(PhraseLanguageData.Language lang)
        {
            _ourLnaguage = lang;
            var s = GetLangText(lang);
            _title1.text = s;
            _title2.text = s; 

        }

        private string GetLangText(PhraseLanguageData.Language lang)
        {
            switch (lang)
            {
                case PhraseLanguageData.Language.Ua:
                    return "UKR";
                case PhraseLanguageData.Language.En:
                    return "ENG";
            }
            return "POL";
        }
    }
}