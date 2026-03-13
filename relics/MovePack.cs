using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace PokemonAncient.relics;

public abstract class MovePack() : CustomRelicModel
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    
    public override bool HasUponPickupEffect => true;
    public override bool AddsPet => true;
    public override bool SpawnsPets => true;

    public abstract IEnumerable<CardModel> CardList { get; }
    
    public override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new IntVar("CardCount", CardList.Count())
    ];
    
    public override IEnumerable<IHoverTip> ExtraHoverTips => CardList.Select(c => HoverTipFactory.FromCard(c));
    
    
    public override async Task AfterObtained()
    {
        foreach (var card in CardList)
        {
            await CardPileCmd.Add(card, PileType.Deck);
        }
    }
    
    public override async Task BeforeCombatStart() => await this.SummonPet();

    protected abstract Task SummonPet();
}