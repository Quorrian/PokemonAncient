using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;

namespace PokemonAncient.relics;

[Pool(typeof(SharedRelicPool))]
public class ReuniclusPack() : MovePack
{
    // TODO ADD CARDS HERE
    public override IEnumerable<CardModel> CardList =>
    [
    ];

    protected override async Task SummonPet()
    {
        // TODO: FIGURE OUT HOW TO MAKE POKEMON AS PETS WITH A SIMPLE SPRITE
        var creature = await PlayerCmd.AddPet<MegaCrit.Sts2.Core.Models.Monsters.Byrdpip>(Owner);
    }
}