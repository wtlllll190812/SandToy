using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

[Serializable]
public class SpeciesUiItem
{
    [PreviewField(Alignment = ObjectFieldAlignment.Center)] [SerializeField]
    private Sprite icon;

    [SerializeField] private bool show;
    [SerializeField] private Species kind;
    [SerializeField] private LocalizedString name;
    [SerializeField] private LocalizedString description;
    public Sprite Icon => icon;
    public Species Kind => kind;
    public LocalizedString Name => name;
    public LocalizedString Description => description;
    public bool Show => show;
    
    public SpeciesUiItem(Sprite icon, Species kind, LocalizedString name, LocalizedString description)
    {
        this.icon = icon;
        this.kind = kind;
        this.name = name;
        this.description = description;
    }
}

[CreateAssetMenu]
public class SpeciesUiPreset : SerializedScriptableObject
{
    [SerializeField] private LocalizedStringTable tableReference;
    [SerializeField] [TableList] private List<SpeciesUiItem> speciesUiItems;
    public List<SpeciesUiItem> SpeciesUiItems => speciesUiItems;


    [Button]
    public void InitData()
    {
        foreach (string specie in Enum.GetNames(typeof(Species)))
        {
            tableReference.GetTable().AddEntry(specie, specie);
            tableReference.GetTable().AddEntry(specie + "_Description", specie + "_Description");
            speciesUiItems.Add(new SpeciesUiItem
            (
                null,
                (Species)Enum.Parse(typeof(Species),specie) ,
                new LocalizedString(tableReference.TableReference, specie.ToString()),
                new LocalizedString(tableReference.TableReference, specie + "_Description")
            ));
        }
    }
}