using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Relics;

namespace PokemonAncient.relics;

[Pool(typeof(RelicPool))]
public class MovePack() : CustomRelicModel
{
    public override RelicRarity Rarity =>
        RelicRarity.Starter;

    
}