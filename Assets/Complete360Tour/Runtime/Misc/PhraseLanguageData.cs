using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DigitalSalmon.C360
{
    [Serializable]
    public class PhraseLanguageData
    {

        [SerializeField]
        private Text _field; 
        [SerializeField]
        private TextMeshProUGUI _fieldTMP; 

        [SerializeField]
        private string niceName; 
        [SerializeField]
        [Multiline(2,order = 0)]
        private string _uaText;
        [SerializeField]
        [Multiline(2,order = 0)]
        private string _enText;
        [SerializeField]
        [Multiline(2,order = 0)]
        private string _plText;


        public void SetText(Language lang)
        {
            if ( _fieldTMP!= null)
                _fieldTMP.text = GetText(lang);
            if (_field != null)
                _field.text = GetText(lang);

        }

        public string GetText(Language lang)
        {
            var result = TryGetText(lang);
            if(!string.IsNullOrEmpty(result))
                return result;
            return niceName;

        }

        private string TryGetText(Language lang)
        {
            switch (lang)
            {
                case Language.Ua:
                    return _uaText;
                case Language.En:
                    return _enText;
                case Language.Pl:
                    return _plText;

            }

            throw new Exception($"Not supported Language{lang}");

        }

        public enum Language
        {
            Ua =0,
            En = 1,
            Pl = 2,
        }
    }
}