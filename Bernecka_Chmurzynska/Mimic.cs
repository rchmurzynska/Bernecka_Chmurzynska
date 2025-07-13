using System.Runtime.CompilerServices;

namespace Bernecka_Chmurzynska;

class MimicNPC : NonPlayerCharacter

{
    public MimicNPC(string name, string symbol) : base(name, symbol)
    {
        isActive = false;
    }

    public override string ChooseAction()
    {
        return "";
    }

    public bool CanGaveRing(bool firstQuestCompleted)
    {
        return firstQuestCompleted;
    }

    public string GaveRing()
    {
        return "ring";
    }
}