using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Relics;

namespace PokemonAncient.relics;

[Pool(typeof(RelicPool))]
public class ReuniclusPack() : CustomRelicModel
{
    public override RelicRarity Rarity =>
        RelicRarity.Starter;

    
}