using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using PokemonAncient.Code.Pets;
using PokemonAncient.Code.Powers;

namespace PokemonAncient.Code.Cards.Reuniclus;



public class PsychicTerrain() : CustomCardModel(1, CardType.Power, CardRarity.Ancient, TargetType.Self)
{
    
    public override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<PsychicTerrainPower>(50M)
    ];

    public override IEnumerable<IHoverTip> ExtraHoverTips => [EnergyHoverTip];

    public override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var pet = Owner.PlayerCombatState?.GetPet<ReuniclusPet>();
        await CreatureCmd.TriggerAnim(pet, "Power", ReuniclusPet.AnimDelay);
        await PowerCmd.Apply<PsychicTerrainPower>(Owner.Creature, DynamicVars["PsychicTerrainPower"].BaseValue, Owner.Creature, this);
    }

    public override void OnUpgrade() => DynamicVars["PsychicTerrainPower"].UpgradeValueBy(25M);
}