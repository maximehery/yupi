using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class UpdateIgnoreStatusMessageComposer : AbstractComposer<UpdateIgnoreStatusMessageComposer.State, string>
	{
		public enum State {
			IGNORE = 1,
			LISTEN = 3 // TODO Any other valid values?
		}

		public override void Compose(Yupi.Protocol.ISender session, State state, string username)
		{
		 // Do nothing by default.
		}
	}
}