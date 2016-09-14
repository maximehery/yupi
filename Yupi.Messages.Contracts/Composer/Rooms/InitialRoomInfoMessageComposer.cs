using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class InitialRoomInfoMessageComposer : AbstractComposer<RoomData>
    {
        public override void Compose(ISender session, RoomData room)
        {
            // Do nothing by default.
        }
    }
}