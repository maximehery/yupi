﻿using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Browser.Composers
{
    class NavigatorMetaDataComposer
    {
     public static SimpleServerMessageBuffer Compose()
        {
            SimpleServerMessageBuffer navigatorMetaDataParser = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("NavigatorMetaDataComposer"));

            navigatorMetaDataParser.AppendInteger(4);
            navigatorMetaDataParser.AppendString("official_view");
            navigatorMetaDataParser.AppendInteger(0);
            navigatorMetaDataParser.AppendString("hotel_view");
            navigatorMetaDataParser.AppendInteger(0);
            navigatorMetaDataParser.AppendString("roomads_view");
            navigatorMetaDataParser.AppendInteger(0);
            navigatorMetaDataParser.AppendString("myworld_view");
            navigatorMetaDataParser.AppendInteger(0);

            return navigatorMetaDataParser;
        }
    }
}
