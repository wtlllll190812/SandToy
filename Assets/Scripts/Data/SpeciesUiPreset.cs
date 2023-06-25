using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu]
public class SpeciesUiPreset : SerializedScriptableObject
{
    [Serializable]
    public class SpeciesUiItem
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private Species species;
        [SerializeField] private string name;
        [SerializeField] private string description;
        [SerializeField]LocalizedString _name;
        public Sprite Icon => icon;
        public Species Species => species;
        public string Name => name;
        public string Description => description;
    }

    [SerializeField][TableList] private List<SpeciesUiItem> species;

    public List<SpeciesUiItem> SpeciesNames => species;
}