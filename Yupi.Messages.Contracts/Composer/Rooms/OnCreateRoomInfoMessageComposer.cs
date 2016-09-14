using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class OnCreateRoomInfoMessageComposer : AbstractComposer<RoomData>
    {
        public override void Compose(ISender session, RoomData data)
        {
            // Do nothing by default.
        }
    }
}