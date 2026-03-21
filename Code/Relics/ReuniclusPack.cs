using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using PokemonAncient.Code.Pets;

namespace PokemonAncient.Code.Relics;

[Pool(typeof(SharedRelicPool))]
public class ReuniclusPack() : MovePack
{
    // TODO ADD CARDS HERE
    public override IEnumerable<CardModel> CardList =>
    [
    ];

    protected override async Task SummonPet()
    {
        var creature = await PlayerCmd.AddPet<ReuniclusPet>(Owner);
    }
}