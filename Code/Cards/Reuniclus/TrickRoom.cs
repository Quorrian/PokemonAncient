using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using PokemonAncient.Code.Pets;
using PokemonAncient.Code.Powers;

namespace PokemonAncient.Code.Cards.Reuniclus;

public class TrickRoom() : CustomCardModel(1, CardType.Skill,
    CardRarity.Basic, TargetType.Self)
{
    public override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new CardsVar(1),
        new CalculatedVar("CalculatedCards").WithMultiplier((card, _) => (card?.Owner == null || !card.IsMutable ? 0 : card.Owner.Creature.GetPowerAmount<SpeedPower>()))
    ];
    
    public override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var pet = Owner.PlayerCombatState?.GetPet<ReuniclusPet>();
        await CreatureCmd.TriggerAnim(pet, "Skill", ReuniclusPet.AnimDelay);
        var speed = Owner.Creature.GetPowerAmount<SpeedPower>();
        if (speed == 0)
        {
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.IntValue, Owner);
            return;
        }
        //invert speed by adding two times the negative amount of it
        speed = -speed;
        await PowerCmd.Apply<SpeedPower>(Owner.Creature, 2*speed, Owner.Creature, this);
        // Positive speed: move cards from draw to discard
        // Negative speed: move cards from discard to draw
        (PileType from, PileType to) movement = speed > 0
            ? (PileType.Draw, PileType.Discard)
            : (PileType.Discard, PileType.Draw);
        var prefs = new CardSelectorPrefs(SelectionScreenPrompt, 1);
        var cards = await CardSelectCmd.FromSimpleGrid(choiceContext, movement.from.GetPile(Owner).Cards.ToList(), Owner, prefs);
        await CardPileCmd.Add(cards, movement.to, CardPilePosition.Random);
        await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.IntValue, Owner);
    }

    public override void OnUpgrade() => DynamicVars.Cards.UpgradeValueBy(1);
}